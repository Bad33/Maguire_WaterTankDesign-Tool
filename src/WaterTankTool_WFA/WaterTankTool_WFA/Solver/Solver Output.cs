#region Using directives
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WaterTankTool_WFA.Entity;
using WaterTankTool_WFA.Migrations;
using WaterTankTool_WFA.Solver_Equation;
using WaterTankTool_WFA.Tanks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Rectangle = System.Drawing.Rectangle;
#endregion

namespace WaterTankTool_WFA.Solver
{
    public partial class Solver_Output : Form
    {
        #region Fields / Properties
        private readonly WaterTankDbContext _context;
        private readonly WaterTank _waterTankForm;

        private readonly UnitsConverter inchToFtConverter = new UnitsConverter();   //  kept; not used here but left intact

        private Label rtcLabel;                 // set in LoadAllowableCompressiveStress()

        // Exposed to other members
        public string waterWeight;
        public string snowWeight;
        public string selfWeight;
        public string Fy;

        // Data caches
        public List<string> selfWeightData = new List<string>();
        public List<segmentGravityLoad> cummulativeLoadData = new List<segmentGravityLoad>();
        public List<WindTable> windLoadData = new List<WindTable>();
        public List<designTableData> segmentPropertiesTableData = new List<designTableData>();
        public List<tabelData2> tabelData2s = new List<tabelData2>();
        public List<SeismicLoad> seismicLoads = new List<SeismicLoad>();

        public string _loadCombo;
        #endregion

        #region Constructor
        public Solver_Output(string loadCombo, string titleLoad)
        {
            InitializeComponent();

            this.Text = $"{this.Text} ({titleLoad})";

            // Context (singleton)
            _context = WaterTankDbContext.GetInstance();
            //_waterTankForm = waterTankForm;
            _loadCombo = loadCombo;
            Qwind = _context.WindLoadEntity.FirstOrDefault();

            if (_context.SnowLoadEntity.FirstOrDefault() == null)
            {
                ShowError("Please add Snow Load first!");   // shows once
                return;                                     // skip the rest
            }
            var solverForm = new DesignTable();
            tabelData2s = (List<tabelData2>)solverForm.TableData2Results;

            try
            {
                LoadData();
                LoadSegmentWeightData();
                LoadCummulativeWeightData();
                WindLoadPerSegment();
                LoadCheckTableData();
                SeismicLoadTable();
            }
            catch (Exception ex)
            {
                ShowError($"Unexpected error while initialising Solver Output: {ex.Message}");
            }
        }
        #endregion

        #region *** Utilities ***

        /// <summary> Small helper to show consistent error pop-ups. </summary>
        private void ShowError(string msg, string title = "Error")
            => MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error);

        /// <summary> Reads a JSON file safely. </summary>
        private bool TryReadJson(string path, out string json)
        {
            json = null!;
            try
            {
                json = File.ReadAllText(path);
                return true;
            }
            catch (Exception ex)
            {
                ShowError($"Failed to read '{path}': {ex.Message}");
                return false;
            }
        }

        /// <summary> Converts a string that may contain units / text to a double. </summary>
        public static double ExtractDoubleValue(string input)
        {
            string[] parts = input?.Split() ?? Array.Empty<string>();
            if (parts.Length == 0 || !double.TryParse(parts[0], out double result))
                throw new FormatException($"Unable to parse '{input}' as a double.");
            return result;
        }

        /// <summary> Helper to add a red status label once. </summary>
        private void AddStatusLabel(string message)
        {
            if (statusStrip2.Items.OfType<ToolStripStatusLabel>().Any(l => l.Text == message)) return;

            statusStrip2.Items.Add(new ToolStripStatusLabel
            {
                ForeColor = Color.Red,
                Text = message
            });
        }
        #endregion

        // Order: Tanks (0), Cylinder (1), Riser (2), Base (3, if present)
        // then by HeightFinal descending.
        private static int SegmentTypeOrder(string t)
            => t == "Tanks" ? 0
             : t == "Cylinder" ? 1
             : t == "Riser" ? 2
             : 3; // Base/others last



        private void SeismicLoadTable()
        {
            List<SegmentProperties> segmentData = _context?.SegmentProperties?.ToList() ?? new();
            segmentData = segmentData
                .OrderBy(s => SegmentTypeOrder(s.SegmentType))
                .ThenByDescending(s => s.HeightFinal)
                .ToList();

            var areaC1 = segmentData
                    .Where(x => x.SegmentType == "Cylinder")
                    .OrderBy(x => x.HeightInitial)  // lowest HeightInitial first
                    .Select((segment, index) =>
                    {
                        return Math.Round((Math.PI / 4) *
                         (Math.Pow(12 * segment.Diameter, 2) -
                          Math.Pow((12 * segment.Diameter - 2 * segment.Thickness), 2)), 4);
                    })
                    .FirstOrDefault();  // only return the first (lowest)

            var areaR1 = segmentData
                    .Where(x => x.SegmentType == "Riser")
                    .OrderBy(x => x.HeightInitial)  // lowest HeightInitial first
                    .Select(segment =>
                    {
                        return Math.Round((Math.PI / 4) *
                         (Math.Pow(12 * segment.Diameter, 2) -
                          Math.Pow((12 * segment.Diameter - 2 * segment.Thickness), 2)), 4);
                    })
                    .FirstOrDefault();


            var _seismicLoads = _context.SeismicLoadEntity.FirstOrDefault();
            var tankProperties = _context?.TankProperties?.FirstOrDefault();
            if (tankProperties == null)
            {
                ShowError("Please add segments first! (Tank Properties were not found)");
                return;
            }

            var cylinderEq = new Segment_Cylinder_Equations();
            var baseEq = new Segment_Conical_Equations();
            var multicolumn = new Multileg_Cylinders();

            double seismic_V = _seismicLoads.V;



            double weight = 0;
            double centroid = 0;
            double mSeismic = 0;
            double tankCentroid = 0;

            foreach (SegmentProperties segment in segmentData)
            {
                if (segment.SegmentType == "Tanks")
                {
                    weight = Double.Parse(tankProperties.TotalWeight);
                    centroid = Double.Parse(tankProperties.Centroid) + segment.HeightInitial;
                    tankCentroid = centroid;
                    mSeismic = 0;
                }
                else if (segment.SegmentType == "Cylinder")
                {
                    weight = AppState.CurrentTankType == TankType.SingleColumn ? cylinderEq.weightOfPedestal(segment.HeightInitial, segment.HeightFinal, segment.Diameter, segment.Thickness)
                                : multicolumn.weightOfPedestal(segment.HeightInitial, segment.HeightFinal, segment.Diameter, segment.Thickness, segment.SegmentType);

                    centroid = AppState.CurrentTankType == TankType.SingleColumn ? cylinderEq.Centroid(segment.HeightInitial, segment.HeightFinal)
                                : multicolumn.Centroid(segment.HeightInitial, segment.HeightFinal);

                    var eq = (Double.Parse(tankProperties.TotalWeight) * (areaC1 / ((AppState.NoOfColumns * areaC1) + areaR1)) * (tankCentroid - segment.HeightInitial));

                    double extra = 0;

                    if (seismicLoads.Count() > 1)
                    {
                        foreach (var ele in seismicLoads.Skip(1))

                            extra += ele.Weight * (ele.Centroid - segment.HeightInitial);
                    }
                    extra += weight / AppState.NoOfColumns * (centroid - segment.HeightInitial);
                    mSeismic = eq + extra;

                }
                else if (segment.SegmentType == "Riser")
                {
                    weight = AppState.CurrentTankType == TankType.SingleColumn ? cylinderEq.weightOfPedestal(segment.HeightInitial, segment.HeightFinal, segment.Diameter, segment.Thickness)
                             : multicolumn.weightOfPedestal(segment.HeightInitial, segment.HeightFinal, segment.Diameter, segment.Thickness, segment.SegmentType);

                    centroid = AppState.CurrentTankType == TankType.SingleColumn ? cylinderEq.Centroid(segment.HeightInitial, segment.HeightFinal)
                                : multicolumn.Centroid(segment.HeightInitial, segment.HeightFinal);

                    var eq = (Double.Parse(tankProperties.TotalWeight) * (areaR1 / ((AppState.NoOfColumns * areaC1) + areaR1)) * (tankCentroid - segment.HeightInitial));

                    double extra = 0;

                    var riserSegment = segmentData.FindAll(ele => ele.SegmentType == "Cylinder" || ele.SegmentType == "Tanks");

                    if (seismicLoads.Count() > riserSegment.Count())
                    {
                        foreach (var ele in seismicLoads.Skip(riserSegment.Count()))

                            extra += ele.Weight * (ele.Centroid - segment.HeightInitial);
                    }
                    extra += weight * (centroid - segment.HeightInitial);

                    mSeismic = eq + extra;

                }
                else
                {
                    weight = baseEq.weight(segment.HeightInitial, segment.HeightFinal, (double)segment.DiameterInitial, (double)segment.DiameterFinal, segment.Thickness);
                    centroid = baseEq.Centroid(segment.HeightInitial, segment.HeightFinal, (double)segment.DiameterInitial, (double)segment.DiameterFinal);

                    mSeismic = 0;

                }




                if (_loadCombo == "A" || _loadCombo == "C")
                {

                    seismic_V = 0.6 * seismic_V;
                    mSeismic = 0.6 * _seismicLoads.Ai * mSeismic;
                }
                else if (_loadCombo == "B")
                {

                    seismic_V = 0.75 * (0.6 * seismic_V);
                    mSeismic = 0.6 * _seismicLoads.Ai * mSeismic;


                }

                seismicLoads.Add(new SeismicLoad
                {
                    Fseismic = Math.Round(seismic_V, 4),
                    Weight = Math.Round(segment.SegmentType == "Cylinder" ? weight / AppState.NoOfColumns : weight, 4),
                    Centroid = Math.Round(centroid, 4),
                    Ai_W_Arm = Math.Round(mSeismic, 4)
                });
            }



            dataGridView2.DataSource = seismicLoads;

        }


        #region Wind Load
        private void WindLoadPerSegment()
        {
            // Defensive fetch
            List<SegmentProperties> segmentData = _context?.SegmentProperties?.ToList() ?? new();
            segmentData = segmentData
                .OrderBy(s => SegmentTypeOrder(s.SegmentType))
                .ThenByDescending(s => s.HeightFinal)
                .ToList();


            double prevContribMulti = 0;


            var tankProperties = _context?.TankProperties?.FirstOrDefault();
            if (tankProperties == null)
            {
                ShowError("Please add segments first! (Tank Properties were not found)");
                return;
            }

            double projectedArea;
            try
            {
                projectedArea = ExtractDoubleValue(tankProperties.ProjectedArea);
            }
            catch (Exception ex)
            {
                ShowError($"Invalid projected area in Tank Properties: {ex.Message}");
                return;
            }

            var cylinderEq = new Segment_Cylinder_Equations();
            var baseEq = new Segment_Conical_Equations();
            var multicolumn = new Multileg_Cylinders();
            var areaC1 = segmentData
                .Where(x => x.SegmentType == "Cylinder")
                .OrderBy(x => x.HeightInitial)  // lowest HeightInitial first
                .Select((segment, index) =>
                {
                    return Math.Round((Math.PI / 4) *
                     (Math.Pow(12 * segment.Diameter, 2) -
                      Math.Pow((12 * segment.Diameter - 2 * segment.Thickness), 2)), 4);
                })
                .FirstOrDefault();  // only return the first (lowest)

            var areaR1 = segmentData
                    .Where(x => x.SegmentType == "Riser")
                    .OrderBy(x => x.HeightInitial)  // lowest HeightInitial first
                    .Select(segment =>
                    {
                        return Math.Round((Math.PI / 4) *
                         (Math.Pow(12 * segment.Diameter, 2) -
                          Math.Pow((12 * segment.Diameter - 2 * segment.Thickness), 2)), 4);
                    })
                    .FirstOrDefault();


            double cumulativeFwind = 0;
            double commonvar = 0;
            foreach (var segment in segmentData)
            {
                double fwind, loadLocation;

                if (segment.SegmentType == "Tanks")
                {
                    fwind = calculateF(
                                        cylinderEq.qzi(segment.HeightInitial),
                                        cylinderEq.qzf(segment.HeightFinal),
                                        projectedArea);
                    commonvar = fwind;

                    if (_loadCombo == "A" || _loadCombo == "C")
                    {

                        commonvar = 0.6 * commonvar;
                    }
                    else if (_loadCombo == "B")
                    {

                        commonvar = 0.75 * (0.6 * commonvar);

                    }

                    loadLocation = segment.HeightInitial + ExtractDoubleValue(tankProperties.Centroid);
                }
                else if (segment.SegmentType == "Cylinder")
                {
                    fwind = AppState.CurrentTankType == TankType.SingleColumn ? cylinderEq.F(segment.HeightInitial, segment.HeightFinal, segment.Diameter) : multicolumn.F(segment.HeightInitial, segment.HeightFinal, segment.Diameter, "Cylinder") / AppState.NoOfColumns;
                    loadLocation = AppState.CurrentTankType == TankType.SingleColumn ? cylinderEq.L(segment.HeightInitial, segment.HeightFinal) : multicolumn.L(segment.HeightInitial, segment.HeightFinal);
                }
                else
                {
                    fwind = AppState.CurrentTankType == TankType.SingleColumn ? baseEq.F(segment.HeightInitial, segment.HeightFinal, segment.Diameter) : multicolumn.F(segment.HeightInitial, segment.HeightFinal, segment.Diameter, "Riser");

                    loadLocation = AppState.CurrentTankType == TankType.SingleColumn ? baseEq.L(segment.HeightInitial, segment.HeightFinal) : multicolumn.L(segment.HeightInitial, segment.HeightFinal);

                }

                double baseElevation = segment.HeightInitial;
                double armLength = loadLocation - baseElevation;

                double prevContrib = windLoadData
                                     .Sum(row => Double.Parse(row.Fwind) *
                                                 (Double.Parse(row.LoadLocation) - baseElevation));





                if (segment.SegmentType == "Tanks")
                {
                    prevContribMulti = windLoadData
                     .Sum(row => Double.Parse(row.Fwind) * (areaR1 / ((AppState.NoOfColumns * areaC1) + areaR1)) *
                                 ((Double.Parse(row.LoadLocation) - segment.HeightInitial)));
                }
                if (segment.SegmentType == "Cylinder")
                {
                    var prevSegment = segmentData.Find(x =>
                        x.HeightInitial == segment.HeightFinal && x.SegmentType == segment.SegmentType);

                    var refRow = windLoadData
                        .OrderBy(r => Double.Parse(r.BaseElevation))
                        .FirstOrDefault(r => Double.Parse(r.BaseElevation) > segment.HeightInitial);

                    if (refRow != null)
                    {
                        var scale = areaC1 / ((AppState.NoOfColumns * areaC1) + areaR1);
                        prevContribMulti = Double.Parse(windLoadData[0].Fwind) * scale *
                                            (Double.Parse(windLoadData[0].LoadLocation) - segment.HeightInitial);
                    }
                    double _extra = 0;

                    if (prevSegment != null && windLoadData.Count > 0)
                    {
                        foreach (var ele in windLoadData.Skip(1))
                        {
                            _extra += Double.Parse(ele.Fwind) *
                                      (Double.Parse(ele.LoadLocation) - segment.HeightInitial);
                        }



                        prevContribMulti += _extra;
                    }
                }
                else if (segment.SegmentType == "Riser")
                {
                    var refRow = windLoadData
                        .OrderBy(r => Double.Parse(r.BaseElevation))
                        .FirstOrDefault(r => Double.Parse(r.BaseElevation) > segment.HeightInitial);

                    if (refRow != null)
                    {
                        var scaleR = areaR1 / ((AppState.NoOfColumns * areaC1) + areaR1);
                        prevContribMulti = Double.Parse(windLoadData[0].Fwind) * scaleR *
                                            (Double.Parse(windLoadData[0].LoadLocation) - segment.HeightInitial);
                    }

                    var riserSegment = segmentData.FindAll(ele => ele.SegmentType == "Cylinder" || ele.SegmentType == "Tanks");

                    if (windLoadData.Count > riserSegment.Count())
                    {
                        double extra = 0;
                        foreach (var ele in windLoadData.Skip(riserSegment.Count()))
                            extra += Double.Parse(ele.Fwind) *
                                     (Double.Parse(ele.LoadLocation) - segment.HeightInitial);

                        prevContribMulti += extra;
                    }
                }



                if (_loadCombo == "A" || _loadCombo == "C")
                {
                    fwind = 0.6 * fwind;

                }
                else if (_loadCombo == "B")
                {
                    fwind = 0.75 * (0.6 * fwind);


                }
                if (AppState.CurrentTankType == TankType.SingleColumn)
                {

                    cumulativeFwind += fwind;
                }
                else
                {

                    if (segment.SegmentType == "Tanks")
                    {
                        cumulativeFwind = fwind;
                    }
                    else if (segment.SegmentType == "Cylinder")
                    {

                        var gg = commonvar * (areaC1 / ((AppState.NoOfColumns * areaC1) + areaR1)) + fwind;

                        cumulativeFwind = (cumulativeFwind == 0) ? cumulativeFwind + gg : cumulativeFwind + fwind;

                    }
                    else
                    {
                        var gg = commonvar * (areaR1 / ((AppState.NoOfColumns * areaC1) + areaR1)) + fwind;

                        cumulativeFwind = (cumulativeFwind == 0) ? cumulativeFwind + gg : cumulativeFwind + fwind;

                    }
                }
                double farm = fwind * armLength;

                double mwind = AppState.CurrentTankType == TankType.SingleColumn ? farm + prevContrib : farm + prevContribMulti;


                windLoadData.Add(new WindTable
                {
                    Fwind = Math.Round(fwind, 4).ToString(),
                    Vwind = Math.Round(cumulativeFwind, 4).ToString(),
                    BaseElevation = Math.Round(baseElevation, 4).ToString(),
                    LoadLocation = Math.Round(loadLocation, 4).ToString(),
                    ArmLength = Math.Round(armLength, 4).ToString(),
                    FArm = Math.Round(farm, 4).ToString(),
                    Mwind = Math.Round(mwind, 4).ToString()
                });
                if (segment.SegmentType == "Tanks")
                {
                    cumulativeFwind = 0;
                    prevContribMulti = 0;
                }

                if (segment.SegmentType == "Cylinder")
                {
                    bool isLastCylinder = !segmentData
                        .SkipWhile(s => s != segment)   // look at remaining segments
                        .Skip(1)                        // skip current one
                        .Any(s => s.SegmentType == "Cylinder");

                    if (isLastCylinder)
                    {
                        cumulativeFwind = 0;
                        prevContribMulti = 0;

                    }
                }

            }

            // ensure segment weights are loaded once (no duplicates)
            if (selfWeightData.Count == 0)
                LoadSegmentWeightData();

            dataGridView7.DataSource = windLoadData;
        }
        private WindLoadEntity Qwind;
        private double calculateF(double qzi, double qzf, double projectedArea)
        {


            var result1 = 30 * Qwind.Cf * (projectedArea / 1000);

            var result2 = (((qzi + qzf) / 2) * Qwind.Cf * Qwind.G * (projectedArea / 1000));

            return Math.Max(result1, result2);
        }
        #endregion

        #region Segment Gravity Loads
        private void LoadSegmentWeightData()
        {
            List<SegmentProperties> segmentData = _context?.SegmentProperties?.ToList() ?? new();
            segmentData = segmentData
                .OrderBy(s => SegmentTypeOrder(s.SegmentType))
                .ThenByDescending(s => s.HeightFinal)
                .ToList();


            var snowEntity = _context?.SnowLoadEntity?.FirstOrDefault();
            var liveLoad = _context?.LiveLoadEntity?.FirstOrDefault();
            var deadLoad = _context?.DeadLoadEntity?.FirstOrDefault();
            if (snowEntity == null || liveLoad == null || deadLoad == null)
            {
                ShowError("Please add all the Loads first!");
                return;
            }

            if (segmentData.Count == 0)
            {
                AddStatusLabel("No Segments Added. Please add the segments to see the output.");
                return;
            }

            // Choose the correct file depending on tank type
            string fileName = AppState.CurrentTankType == TankType.MultiColumn
                              ? "MultiLeg-Tanks.json"
                              : "tanks.json";

            string jsonPath = Path.Combine(Application.StartupPath, fileName);

            if (!File.Exists(jsonPath))
            {
                ShowError($"{fileName} file not found.");
                return;
            }

            if (!TryReadJson(jsonPath, out string json)) return;

            TanksData data;
            try
            {
                data = JsonSerializer.Deserialize<TanksData>(json);
            }
            catch (Exception ex)
            {
                ShowError($"Failed to parse {fileName}: {ex.Message}");
                return;
            }

            // Find matching tank record
            string tankCapacity = segmentData[0].SegmentName;
            Tank foundTank = data?.tanks?.Find(t => t.type == tankCapacity);

            if (foundTank == null)
            {
                ShowError($"Tank type '{tankCapacity}' not found in {fileName}!");
                return;
            }

            // … proceed with foundTank …


            string numericPart = new(foundTank.Weight_of_Water
                                      .Where(c => char.IsDigit(c) || c == '.' || c == '-').ToArray());

            if (!double.TryParse(numericPart, NumberStyles.Any, CultureInfo.InvariantCulture,
                                 out double waterWeightDbl))
            {
                ShowError("Failed to parse Weight_of_Water.");
                return;
            }
            waterWeight = numericPart;

            string snowWeightStr = snowEntity.TotalSnowLoad.ToString();
            snowWeight = snowWeightStr;
            selfWeight = foundTank.Weight_of_Steel;
            double miscLoad = 0;

            if (AppState.CurrentTankType == TankType.SingleColumn)
            {
                miscLoad = deadLoad.Miscellaneous_Load;
            }

            double final_load = 0;

            if (_loadCombo == "B")
            {
                var extraP_Load = (0.75 * liveLoad.Live_Load) + 0.75 * (Math.Max(liveLoad.Roof_Live_Load, snowEntity.TotalSnowLoad));
                final_load = (double.Parse(selfWeight) + extraP_Load + miscLoad);
                snowWeightStr = "0";

            }
            else if (_loadCombo == "C")
            {
                final_load = 0.6 * (double.Parse(selfWeight) + miscLoad);
                waterWeight = (0.6 * (double.Parse(waterWeight))).ToString();

            }
            else if (_loadCombo == "A")
            {
                final_load = (double.Parse(selfWeight) + miscLoad);
                snowWeightStr = "0";

            }


            var cylinderEq = new Segment_Cylinder_Equations();
            var conicalEq = new Segment_Conical_Equations();

            var cylinderMultileg = new Multileg_Cylinders();

            List<segmentGravityLoad> viewModelData1 = new List<segmentGravityLoad>();
            List<segmentGravityLoad> viewModelData2 = new List<segmentGravityLoad>();
            // Tanks
            // Tanks (usually single row, order doesn’t matter)


            // Tanks first (HeightFinal desc)
            var viewModelData = segmentData
                .Where(x => x.SegmentType == "Tanks")
                .OrderByDescending(x => x.HeightFinal)
                .Select(segment => new segmentGravityLoad
                {
                    waterWeight = waterWeight,
                    snowWeight = snowWeightStr,
                    selfWeight = final_load.ToString("f5")
                })
                .ToList();


            if (AppState.CurrentTankType == TankType.SingleColumn)
            {
                // Cylinders next (single column)
                viewModelData1 = segmentData
                    .Where(x => x.SegmentType == "Cylinder")
                    .OrderByDescending(x => x.HeightFinal)
                    .Select(segment => new segmentGravityLoad
                    {
                        waterWeight = "0",
                        snowWeight = "0",
                        selfWeight = Math.Round(
                            cylinderEq.weightOfPedestal(
                                segment.HeightInitial,
                                segment.HeightFinal,
                                segment.Diameter,
                                segment.Thickness
                            ), 4).ToString()
                    })
                    .ToList();

                // Base last (single column)
                viewModelData2 = segmentData
                    .Where(x => x.SegmentType == "Base")
                    .OrderByDescending(x => x.HeightFinal)
                    .Select(segment => new segmentGravityLoad
                    {
                        waterWeight = "0",
                        snowWeight = "0",
                        selfWeight = Math.Round(
                            conicalEq.weight(
                                segment.HeightInitial,
                                segment.HeightFinal,
                                (double)segment.DiameterInitial,
                                (double)segment.DiameterFinal,
                                segment.Thickness
                            ), 4).ToString()
                    })
                    .ToList();
            }
            else // MultiColumn
            {
                // Cylinders next (multi-leg) — per-column weight; keep your division by NoOfColumns
                viewModelData1 = segmentData
                    .Where(x => x.SegmentType == "Cylinder")
                    .OrderByDescending(x => x.HeightFinal)
                    .Select(segment => new segmentGravityLoad
                    {
                        waterWeight = "0",
                        snowWeight = "0",
                        selfWeight = Math.Round(
                            cylinderMultileg.weightOfPedestal(
                                segment.HeightInitial,
                                segment.HeightFinal,
                                segment.Diameter,
                                segment.Thickness,
                                "Cylinder"
                            ) / AppState.NoOfColumns, 4).ToString()
                    })
                    .ToList();

                // Riser last (multi-leg)
                viewModelData2 = segmentData
                .Where(x => x.SegmentType == "Riser")
                    .OrderByDescending(x => x.HeightFinal)
                    .Select(segment => new segmentGravityLoad
                    {
                        waterWeight = "0",
                        snowWeight = "0",
                        selfWeight = Math.Round(
                            cylinderMultileg.weightOfPedestal(
                                segment.HeightInitial,
                                segment.HeightFinal,
                                segment.Diameter,
                                segment.Thickness,
                                "Riser"
                            ), 4).ToString()
                    })
                    .ToList();
            }

            // Combine in the desired order for the grid
            var combined = viewModelData
                .Concat(viewModelData1)
                .Concat(viewModelData2)
                .ToList();



            selfWeightData.AddRange(combined.Select(x => x.selfWeight));

            dataGridView4.DataSource = combined;

            AddStatusLabel("Double-click a cell in the Segment Check Table to change thickness. (It’s recommended to change T by ±25 % each time) | For more info open Help.");
        }
        #endregion
        TankData tankData = new TankData();
        TankDataDimensions dimensions = new TankDataDimensions();
        #region Cumulative Gravity Loads
        private void LoadCummulativeWeightData()
        {
            List<SegmentProperties> segmentData = _context?.SegmentProperties?.ToList() ?? new();
            segmentData = segmentData
                .OrderBy(s => SegmentTypeOrder(s.SegmentType))
                .ThenByDescending(s => s.HeightFinal)
                .ToList();

            var deadLoad = _context?.DeadLoadEntity?.FirstOrDefault();

            if (segmentData.Count == 0)
            {
                ShowError("No segment data found. Please add segments before proceeding.");
                return;
            }
            // Choose the correct file depending on tank type
            string fileName = AppState.CurrentTankType == TankType.MultiColumn
                              ? "MultiLeg-Tanks.json"
                              : "tanks.json";

            string jsonPath = Path.Combine(Application.StartupPath, fileName);

            if (!File.Exists(jsonPath))
            {
                ShowError($"{fileName} file not found.");
                return;
            }

            if (!TryReadJson(jsonPath, out string json)) return;

            TanksData data;
            try
            {
                data = JsonSerializer.Deserialize<TanksData>(json);
            }
            catch (Exception ex)
            {
                ShowError($"Failed to parse {fileName}: {ex.Message}");
                return;
            }

            // Find matching tank record
            string tankCapacity = segmentData[0].SegmentName;
            Tank foundTank = data?.tanks?.Find(t => t.type == tankCapacity);

            if (foundTank == null)
            {
                ShowError($"Tank type '{tankCapacity}' not found in {fileName}!");
                return;
            }

            // … proceed with foundTank …


            string numericPart = new(foundTank.Weight_of_Water
                                      .Where(c => char.IsDigit(c) || c == '.' || c == '-').ToArray());
            string waterWeightStr = numericPart;



            var snowEntity = _context?.SnowLoadEntity?.FirstOrDefault();
            if (snowEntity == null)
            {
                ShowError("Please add Snow Load first!");
                return;
            }
            string snowWeightStr = snowEntity.TotalSnowLoad.ToString();

            double miscLoad = deadLoad != null ? deadLoad.Miscellaneous_Load : 15;

            // Build cumulative self-weight list (res[i] holds Σ selfWeight[0..i])
            List<string> cumulative = new();
            for (int i = 0; i < selfWeightData.Count; i++)
            {
                if (!double.TryParse(selfWeightData[i], out double thisWeight))
                {
                    ShowError($"Invalid selfWeight '{selfWeightData[i]}' at index {i}.");
                    return;
                }
                double previous = i == 0 ? 0
                                         : double.Parse(cumulative[i - 1]);
                cumulative.Add((thisWeight + previous).ToString());
            }










            var cylinderEq = new Segment_Cylinder_Equations();
            var conicalEq = new Segment_Conical_Equations();

            var cylinderMultileg = new Multileg_Cylinders();


            List<segmentGravityLoad> viewModelData = new List<segmentGravityLoad>();

            List<segmentGravityLoad> viewModelData1 = new List<segmentGravityLoad>();
            List<segmentGravityLoad> viewModelData2 = new List<segmentGravityLoad>();
            var _selfTankWeight = "";

            if (AppState.CurrentTankType == TankType.SingleColumn)
            {
                cummulativeLoadData = segmentData.Select((segment, index) =>
                {
                    _selfTankWeight = index < cumulative.Count
                                   ? Math.Round(double.Parse(cumulative[index]), 4).ToString()
                                   : "0";

                    double snowDbl = double.TryParse(snowWeightStr, out double sn) ? sn : 0;
                    string snowSum = (snowDbl).ToString();

                    if (_loadCombo == "A" || _loadCombo == "B")
                    {
                        snowSum = "0";
                    }

                    return new segmentGravityLoad
                    {
                        waterWeight = waterWeightStr,
                        snowWeight = snowSum,
                        selfWeight = _selfTankWeight
                    };
                }).ToList();
            }
            else // MultiColumn
            {
                //string otherTankWeight = foundTank;

                viewModelData = segmentData
                    .Where(x => x.SegmentType == "Tanks")
                    .OrderByDescending(x => x.HeightFinal)
                    .Select((segment, index) =>
                    {
                        _selfTankWeight = index < cumulative.Count
                   ? Math.Round(double.Parse(cumulative[index]), 4).ToString()
                   : "0";


                        return new segmentGravityLoad
                        {
                            waterWeight = waterWeightStr,
                            snowWeight = snowWeightStr,
                            selfWeight = _selfTankWeight
                        };
                    })
                    .ToList();

                // Cylinders next (multi-leg) — per-column weight; keep your division by NoOfColumns
                var areaC1 = segmentData
                    .Where(x => x.SegmentType == "Cylinder")
                    .OrderBy(x => x.HeightInitial)  // lowest HeightInitial first
                    .Select((segment, index) =>
                    {
                        return Math.Round((Math.PI / 4) *
                         (Math.Pow(12 * segment.Diameter, 2) -
                          Math.Pow((12 * segment.Diameter - 2 * segment.Thickness), 2)), 4);
                    })
                    .FirstOrDefault();  // only return the first (lowest)

                var areaR1 = segmentData
                        .Where(x => x.SegmentType == "Riser")
                        .OrderBy(x => x.HeightInitial)  // lowest HeightInitial first
                        .Select(segment =>
                        {
                            return Math.Round((Math.PI / 4) *
                             (Math.Pow(12 * segment.Diameter, 2) -
                              Math.Pow((12 * segment.Diameter - 2 * segment.Thickness), 2)), 4);
                        })
                        .FirstOrDefault();  // only return the first (lowest)
                                            // Cylinders (multi-leg) — keep your order and water split logic,
                                            // but make selfWeight cumulative after the first row.
                var cylindersOrdered = segmentData
                    .Where(x => x.SegmentType == "Cylinder")
                    .OrderByDescending(x => x.HeightFinal)
                    .ToList();


                double prevSelfWeight = 0.0; // cumulative tracker (per row)

                for (int i = 0; i < cylindersOrdered.Count; i++)
                {
                    var segment = cylindersOrdered[i];

                    // area for water split (unchanged logic)
                    var area = Math.Round(
                        (Math.PI / 4) * (Math.Pow(12 * segment.Diameter, 2) -
                                         Math.Pow((12 * segment.Diameter - 2 * segment.Thickness), 2)), 4);

                    var _waterWeight = (area / ((AppState.NoOfColumns * areaC1 + areaR1)) * Double.Parse(waterWeightStr));

                    // per-column self weight for this cylinder
                    double perColCylinderSelf = cylinderMultileg.weightOfPedestal(
                        segment.HeightInitial,
                        segment.HeightFinal,
                        segment.Diameter,
                        segment.Thickness,
                        "Cylinder") / AppState.NoOfColumns;

                    // first row: your original formula (perColCylinderSelf + tank share)
                    // subsequent rows: cumulative = previous selfWeight + perColCylinderSelf
                    double selfW;
                    if (i == 0)
                    {
                        selfW = Math.Round(
                            perColCylinderSelf + (Double.Parse(_selfTankWeight) / AppState.NoOfColumns), 4);
                    }
                    else
                    {
                        selfW = Math.Round(prevSelfWeight + perColCylinderSelf, 4);
                    }

                    viewModelData1.Add(new segmentGravityLoad
                    {
                        waterWeight = _waterWeight.ToString("F5"),
                        snowWeight = "0",
                        selfWeight = selfW.ToString()
                    });

                    prevSelfWeight = selfW; // carry forward for cumulative sum
                }



                GetTanksJsonData();

                var tankProperties = tankData.Tanks.FirstOrDefault(data => data.Type == foundTank.type);

                var otherTakWeight = "0";

                if (tankProperties != null)
                {
                    otherTakWeight = tankProperties.Weight_of_Bowl_and_cone;
                }


                // Riser last (multi-leg)
                // Riser (multi-leg): first = highest HeightInitial uses original formula,
                // subsequent = cumulative (prev selfWeight + current riser weight)
                var risersOrdered = segmentData
                    .Where(x => x.SegmentType == "Riser")
                    .OrderByDescending(x => x.HeightInitial)   // ensure "first" = highest HeightInitial
                    .ToList();

                viewModelData2 = new List<segmentGravityLoad>();
                double prevRiserSelf = 0.0;

                for (int i = 0; i < risersOrdered.Count; i++)
                {
                    var segment = risersOrdered[i];

                    // area for water split (unchanged)
                    var area = Math.Round(
                        (Math.PI / 4) * (Math.Pow(12 * segment.Diameter, 2) -
                                         Math.Pow((12 * segment.Diameter - 2 * segment.Thickness), 2)), 4);
                    var _waterWeight = (area / ((AppState.NoOfColumns * areaC1 + areaR1)) * Double.Parse(waterWeightStr));

                    // this riser's self-weight (no column division for riser per your original code)
                    double thisRiserWeight = cylinderMultileg.weightOfPedestal(
                        segment.HeightInitial,
                        segment.HeightFinal,
                        segment.Diameter,
                        segment.Thickness,
                        "Riser");

                    double selfW;
                    if (i == 0)
                    {
                        // first riser: original formula
                        selfW = Math.Round(thisRiserWeight + Double.Parse(otherTakWeight), 4);
                    }
                    else
                    {
                        // cumulative from previous
                        selfW = Math.Round(prevRiserSelf + thisRiserWeight, 4);
                    }

                    viewModelData2.Add(new segmentGravityLoad
                    {
                        waterWeight = _waterWeight.ToString("F5"),
                        snowWeight = "0",
                        selfWeight = selfW.ToString()
                    });

                    prevRiserSelf = selfW; // carry forward
                }


                cummulativeLoadData = viewModelData
                     .Concat(viewModelData1)
                     .Concat(viewModelData2)
                     .ToList();
            }

            // Combine in the desired order for the grid




            //selfWeightData.AddRange(combined.Select(x => x.selfWeight));

            dataGridView6.DataSource = cummulativeLoadData;








            //dataGridView6.DataSource = cummulativeLoadData;
        }
        #endregion
        private TankType _tankType;


        private void GetTanksJsonData()
        {
            try
            {
                string fileName = AppState.CurrentTankType == TankType.MultiColumn
                                  ? "MultiLeg-Tanks.json"
                                  : "tanks.json";

                string jsonPath = Path.Combine(Application.StartupPath, fileName);

                if (!File.Exists(jsonPath))
                {
                    MessageBox.Show($"{fileName} not found in application folder.");
                    return;
                }

                string jsonString = File.ReadAllText(jsonPath);
                tankData = JsonSerializer.Deserialize<TankData>(jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while reading tank JSON: {ex.Message}");
            }
        }
        #region Check Table (fa / fb / check)
        private void LoadCheckTableData()
        {


            List<SegmentProperties> segmentData = _context?.SegmentProperties?.ToList() ?? new();
            segmentData = segmentData
                .OrderBy(s => SegmentTypeOrder(s.SegmentType))
                .ThenByDescending(s => s.HeightFinal)
                .ToList();

            if (segmentData.Count == 0) return;

            var viewModel = segmentData.Select((segment, i) =>
            {
                double fa = 0, fb = 0;
                string chk = "NA";

                if (segment.SegmentType != "Tanks")
                {
                    fa = Math.Round(
                            (Double.Parse(cummulativeLoadData[i].waterWeight) +
                             Double.Parse(cummulativeLoadData[i].snowWeight) +
                             Double.Parse(cummulativeLoadData[i].selfWeight)) /
                            segmentPropertiesTableData[i].A, 4);

                    fb = Math.Round(
                            (Double.Parse(windLoadData[i].Mwind) * 12) /
                             segmentPropertiesTableData[i].S, 4);

                    if ((fa + fb) != 0)
                        chk = Math.Round(
                                (fa / tabelData2s[i].Fa) +
                                (fb / tabelData2s[i].Fb), 4).ToString();
                }

                return new CheckTableData
                {
                    SegmentID = segment.SegmentNumber,
                    Segment = segment.SegmentName,
                    fa = fa,
                    fb = fb,
                    check = chk
                };
            }).ToList();

            dataGridView1.DataSource = viewModel;
            dataGridView1.Columns["SegmentID"].Visible = false;
        }
        #endregion





        #region Segment Properties Table (dataGridView5)
        private void LoadData()
        {
            List<SegmentProperties> segmentData = _context?.SegmentProperties?.ToList() ?? new();
            segmentData = segmentData
                .OrderBy(s => SegmentTypeOrder(s.SegmentType))
                .ThenByDescending(s => s.HeightFinal)
                .ToList();


            if (segmentData.Count == 0) return;

            segmentPropertiesTableData = segmentData.Select(segment =>
            {
                double thickness = 0;
                double dFinal = segment.DiameterFinal ?? segment.Diameter;
                double A = Math.Round((Math.PI / 4) *
                         (Math.Pow(12 * dFinal, 2) -
                          Math.Pow((12 * dFinal - 2 * segment.Thickness), 2)), 4);

                double I = Math.Round((Math.PI / 64) *
                         (Math.Pow(12 * dFinal, 4) -
                          Math.Pow((12 * dFinal - 2 * segment.Thickness), 4)), 4);

                double S = Math.Round((I * 2) / (12 * dFinal), 4);
                thickness = segment.Thickness;
                if (segment.SegmentType == "Tanks")
                {
                    // all zero for "Tanks"
                    dFinal = 0;
                    thickness = 0;
                    A = 0;
                    I = 0;
                    S = 0;

                }

                return new designTableData
                {
                    Segment = segment.SegmentName,
                    Diameter = Math.Round(12 * dFinal, 4),
                    Thickness = thickness,
                    A = A,
                    I = I,
                    S = S
                };
            }).ToList();

            dataGridView5.DataSource = segmentPropertiesTableData;
        }
        #endregion

        #region --- Printing ---
        #region --- Printing (multi-table per page, paginated) ---

        private List<(string Title, DataGridView Grid)> _printItems;
        private int _printItemIdx = 0;
        private int _printRowIdx = 0;
        private bool _printingHeader = true;
        private float _currentY = 0f;

        private void PreparePrintItems()
        {
            _printItems = new List<(string Title, DataGridView Grid)>
    {
        ("Check Table Data", dataGridView1),
        ("Segment Properties Data", dataGridView5),
        ("Wind Load Data", dataGridView7),
        ("Cumulative Gravity Loads", dataGridView6),
        ("Segment Weights", dataGridView4),
        ("Seismic Load Data", dataGridView2)
    }
            .Where(x => x.Grid != null && x.Grid.Columns.Count > 0 && x.Grid.Rows.Count > 0)
            .ToList();

            _printItemIdx = 0;
            _printRowIdx = 0;
            _printingHeader = true;
            _currentY = 0f; // will be set to margin.Top on first PrintPage
        }

        private float DrawTableTitle(Graphics g, RectangleF area, string title, System.Drawing.Font fTitle)
        {
            g.DrawString(title, fTitle, Brushes.Black, area.Left, area.Top);
            return area.Top + fTitle.GetHeight(g) + 14;
        }

        // returns (nextRowIndex, newY)
        private (int nextRow, float newY) DrawGridPage(Graphics g, RectangleF area, DataGridView dgv, System.Drawing.Font f, int startRow, bool drawHeader)
        {
            float rowH = f.GetHeight(g) + 8;
            float x = area.Left;
            float y = area.Top;
            float availableW = area.Width;

            float[] colWidths;
            {
                float sum = dgv.Columns.Cast<DataGridViewColumn>().Sum(c => (float)Math.Max(1, c.Width));
                colWidths = dgv.Columns.Cast<DataGridViewColumn>()
                            .Select(c => availableW * ((float)Math.Max(1, c.Width) / sum))
                            .ToArray();
            }

            if (drawHeader)
            {
                // if not enough room for header alone, bail to next page
                if (y + rowH > area.Bottom) return (startRow, y);
                for (int c = 0; c < dgv.Columns.Count; c++)
                {
                    var rect = new RectangleF(x, y, colWidths[c], rowH);
                    g.DrawRectangle(Pens.Black, rect.X, rect.Y, rect.Width, rect.Height);
                    g.DrawString(dgv.Columns[c].HeaderText, f, Brushes.Black, rect);
                    x += colWidths[c];
                }
                y += rowH;
            }

            int r = startRow;
            for (; r < dgv.Rows.Count; r++)
            {
                if (dgv.Rows[r].IsNewRow) continue;
                if (y + rowH > area.Bottom) break;

                x = area.Left;
                for (int c = 0; c < dgv.Columns.Count; c++)
                {
                    var val = dgv.Rows[r].Cells[c].Value;
                    var rect = new RectangleF(x, y, colWidths[c], rowH);
                    g.DrawRectangle(Pens.Black, rect.X, rect.Y, rect.Width, rect.Height);
                    g.DrawString(Convert.ToString(val), f, Brushes.Black, rect);
                    x += colWidths[c];
                }
                y += rowH;
            }

            return (r, y);
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            var margin = e.MarginBounds;
            var fTitle = new System.Drawing.Font("Arial", 12, FontStyle.Bold);
            var f = new System.Drawing.Font("Arial", 10);

            if (_currentY <= 0f) _currentY = margin.Top;

            while (_printItemIdx < _printItems.Count)
            {
                var (title, dgv) = _printItems[_printItemIdx];

                float neededHeader = fTitle.GetHeight(e.Graphics) + 14;
                float minRowH = f.GetHeight(e.Graphics) + 8;

                // ensure at least room for header + one row; if not, new page
                if (_printingHeader && (_currentY + neededHeader + minRowH > margin.Bottom))
                {
                    e.HasMorePages = true;
                    _currentY = margin.Top;
                    return;
                }

                if (_printingHeader)
                    _currentY = DrawTableTitle(e.Graphics, new RectangleF(margin.Left, _currentY, margin.Width, margin.Bottom - _currentY), title, fTitle);

                var tableArea = new RectangleF(margin.Left, _currentY, margin.Width, margin.Bottom - _currentY);
                var (nextRow, newY) = DrawGridPage(e.Graphics, tableArea, dgv, f, _printRowIdx, _printingHeader);

                if (nextRow == _printRowIdx) // nothing fit—force new page
                {
                    e.HasMorePages = true;
                    _currentY = margin.Top;
                    _printingHeader = false; // we already drew header; continue rows next page
                    return;
                }

                _currentY = newY;

                if (nextRow < dgv.Rows.Count)
                {
                    _printRowIdx = nextRow;
                    _printingHeader = false; // continue same table on next page
                    e.HasMorePages = true;
                    _currentY = margin.Top;
                    return;
                }
                else
                {
                    // finished this table; add spacing and try to print next one on same page
                    _printItemIdx++;
                    _printRowIdx = 0;
                    _printingHeader = true;

                    _currentY += 24; // spacer before next table

                    // if the spacer pushes us over page, move to next page
                    if (_currentY + neededHeader + minRowH > margin.Bottom)
                    {
                        e.HasMorePages = true;
                        _currentY = margin.Top;
                        return;
                    }
                }
            }

            e.HasMorePages = false;
            _currentY = margin.Top;
        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                PreparePrintItems();
                var doc = new PrintDocument();
                doc.DocumentName = "Output Data";
                doc.PrintPage += PrintDocument_PrintPage;

                using var dlg = new PrintDialog { Document = doc };
                if (dlg.ShowDialog() == DialogResult.OK) doc.Print();
            }
            catch (Exception ex)
            {
                ShowError($"Printing failed: {ex.Message}");
            }
        }
        #endregion


        #endregion

        #region --- Event Handlers (grid formatting / double-click) ---

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name != "Check" || e.Value == null) return;

            if (double.TryParse(e.Value.ToString(), out double val))
                e.CellStyle.ForeColor = (val >= 0.95 || val <= 0.75) ? Color.Red : Color.Black;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dataGridView1.Rows.Count)
            {
                ShowError("Please select a valid row to modify.");
                return;
            }

            if (dataGridView1.Rows[e.RowIndex].DataBoundItem is not CheckTableData check) return;

            SegmentDialogBox dlg = new SegmentDialogBox(check.SegmentID, "Modify", _waterTankForm);
            if (dlg.ShowDialog() != DialogResult.OK) return;

            // refresh all dependent tables
            try
            {
                selfWeightData.Clear();
                windLoadData.Clear();
                LoadData();
                LoadSegmentWeightData();
                LoadCummulativeWeightData();
                WindLoadPerSegment();
                LoadCheckTableData();
            }
            catch (Exception ex)
            {
                ShowError($"Error while refreshing data after modification: {ex.Message}");
            }
        }

        private void helpToolStripButton_Click(object sender, EventArgs e)
        {
            using Help h = new Help();
            h.ShowDialog();
        }
        #endregion

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }

    #region --- DTO / ViewModel classes (unchanged) ---

    public class designTableData
    {
        public string Segment { get; set; }
        public double Diameter { get; set; }
        public double Thickness { get; set; }
        public double A { get; set; }
        public double I { get; set; }
        public double S { get; set; }
    }

    public class segmentGravityLoad
    {
        public string waterWeight { get; set; }
        public string snowWeight { get; set; }
        public string selfWeight { get; set; }
    }

    public class CummulativeGravityLoad
    {
        public double water { get; set; }
        public double snow { get; set; }
        public double selfWeight { get; set; }
    }

    public class SeismicLoad
    {
        public double Fseismic { get; set; }
        public double Weight { get; set; }
        public double Centroid { get; set; }
        public double Ai_W_Arm { get; set; }
    }


    public class CheckTableData
    {
        public int SegmentID { get; set; }
        public string Segment { get; set; }
        public double fa { get; set; }
        public double fb { get; set; }
        public string check { get; set; }
    }

    public class TanksData
    {
        public List<Tank> tanks { get; set; }
    }

    public class Tank
    {
        public string type { get; set; }
        public string Weight_of_Water { get; set; }
        public string Weight_of_Steel { get; set; }
        public string Total_Weight { get; set; }
        public string Projected_Area { get; set; }
        public string height { get; set; }
        public string diameter { get; set; }
        public string thickness { get; set; }
    }

    public class WindTable
    {
        public string Fwind { get; set; }
        public string Vwind { get; set; }
        public string BaseElevation { get; set; }
        public string LoadLocation { get; set; }
        public string ArmLength { get; set; }
        public string FArm { get; set; }
        public string Mwind { get; set; }

        // ==================================================================
        //  Designer-generated event hooks – keep these methods even if empty
        // ==================================================================
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // No action needed – left for Designer compatibility
        }

        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // No action needed – left for Designer compatibility
        }

        private void Solver_Output_Load(object sender, EventArgs e)
        {
            // Form-load logic is already handled in the constructor.
            // Keep this stub so the Designer remains happy.
        }

    }



    #endregion
}
