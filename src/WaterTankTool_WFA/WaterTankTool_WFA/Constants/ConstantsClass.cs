using System; 
using System.Collections.Generic;

namespace WaterTankTool_WFA.Constants
{
    public static class ConstantsClass
    {
        public const double rw = 62.4; //pcf
        public const double rs = 490; //pcf
        public const double rc = 144; // pcf

    }

    public static class Lambda
    {
        public const double Y = 0.6;
    }



    public static class WindLoadConstants
    {
        public const double Kzt = 1.0;
        public const double Ke = 1.0;
        public const double Kd = 0.85;
        public const double G = 1.0;
        public const double I = 1.15;
        public const int V = 90;
    }

    public static class WindLoadExposure_C
    {
        public const double Alpha = 9.5;
        public const int Zg = 900;
    }

    public static class WindLoadExposure_D
    {
        public const double Alpha = 11.5;
        public const int Zg = 700;
    }

    public static class SnowRiskCategoryII
    {
        public const double Is = 1.0;
    }

    public static class SnowRiskCategoryIII
    {
        public const double Is = 1.1;

    }



    public static class SnowRiskCategoryIV
    {
        public const double Is = 1.2;

    }


    public static class SnowExposureC
    {
        public const double Ce = 0.9; 
    }

    public static class SnowExposureD
    {
        public const double Ce = 0.8;

    }


    public class Range
    {
        public double Start { get; }
        public double End { get; }

        public Range(double start, double end)
        {
            Start = start;
            End = end;
        }

        public bool Contains(double value)
        {
            return value >= Start && value <= End;
        }
    }


    public static class SiteClassTable
    {

        public static readonly Dictionary<string, List<(Range range, double value)>> FvValues =
            new Dictionary<string, List<(Range, double)>>
            {
            { "A", new List<(Range, double)>
                {
                    (new Range(0, 0.1), 0.8),
                    (new Range(0.1, 0.2), 0.8),
                    (new Range(0.2, 0.3), 0.8),
                    (new Range(0.3, 0.4), 0.8),
                    (new Range(0.4, double.MaxValue), 0.8)
                }
            },
            { "B", new List<(Range, double)>
                {
                    (new Range(0, 0.1), 1.0),
                    (new Range(0.1, 0.2), 1.0),
                    (new Range(0.2, 0.3), 1.0),
                    (new Range(0.3, 0.4), 1.0),
                    (new Range(0.4, double.MaxValue), 1.0)
                }
            },
            { "C", new List<(Range, double)>
                {
                    (new Range(0, 0.1), 1.7),
                    (new Range(0.1, 0.2), 1.6),
                    (new Range(0.2, 0.3), 1.5),
                    (new Range(0.3, 0.4), 1.4),
                    (new Range(0.4, double.MaxValue), 1.3)
                }
            },
            { "D", new List<(Range, double)>
                {
                    (new Range(0, 0.1), 2.4),
                    (new Range(0.1, 0.2), 2.0),
                    (new Range(0.2, 0.3), 1.8),
                    (new Range(0.3, 0.4), 1.6),
                    (new Range(0.4, double.MaxValue), 1.5)
                }
            },
            { "E", new List<(Range, double)>
                {
                    (new Range(0, 0.1), 3.5),
                    (new Range(0.1, 0.2), 3.2),
                    (new Range(0.2, 0.3), 2.8),
                    (new Range(0.3, 0.4), 2.4),
                    (new Range(0.4, double.MaxValue), 2.4)
                }
            },
            { "F", new List<(Range, double)>
                {
                    (new Range(0, double.MaxValue), double.NaN) // Indicates special conditions
                }
            }
            };


        public static readonly Dictionary<string, List<(Range range, double value)>> FaValues =
            new Dictionary<string, List<(Range, double)>>
            {
            { "A", new List<(Range, double)>
                {
                    (new Range(0, 0.25), 0.8),
                    (new Range(0.25, 0.5), 0.8),
                    (new Range(0.5, 0.75), 0.8),
                    (new Range(0.75, 1.0), 0.8),
                    (new Range(1.0, double.MaxValue), 0.8)
                }
            },
            { "B", new List<(Range, double)>
                {
                    (new Range(0, 0.25), 1.0),
                    (new Range(0.25, 0.5), 1.0),
                    (new Range(0.5, 0.75), 1.0),
                    (new Range(0.75, 1.0), 1.0),
                    (new Range(1.0, double.MaxValue), 1.0)
                }
            },
            { "C", new List<(Range, double)>
                {
                    (new Range(0, 0.25), 1.2),
                    (new Range(0.25, 0.5), 1.2),
                    (new Range(0.5, 0.75), 1.1),
                    (new Range(0.75, 1.0), 1.0),
                    (new Range(1.0, double.MaxValue), 1.0)
                }
            },
            { "D", new List<(Range, double)>
                {
                    (new Range(0, 0.25), 1.6),
                    (new Range(0.25, 0.5), 1.4),
                    (new Range(0.5, 0.75), 1.2),
                    (new Range(0.75, 1.0), 1.1),
                    (new Range(1.0, double.MaxValue), 1.0)
                }
            },
            { "E", new List<(Range, double)>
                {
                    (new Range(0, 0.25), 2.5),
                    (new Range(0.25, 0.5), 1.7),
                    (new Range(0.5, 0.75), 1.2),
                    (new Range(0.75, 1.0), 0.9),
                    (new Range(1.0, double.MaxValue), 0.9)
                }
            },
            { "F", new List<(Range, double)>
                {
                    (new Range(0, double.MaxValue), double.NaN) // Indicates special conditions
                }
            }
            };

        public static double GetFaValue(string siteClass, double sValue)
        {
            if (FaValues.ContainsKey(siteClass))
            {
                foreach (var (range, value) in FaValues[siteClass])
                {
                    if (range.Contains(sValue))
                    {
                        return value;
                    }
                }
            }
            return double.NaN; 
        }

        public static double GetFvValue(string siteClass, double sValue)
        {
            if (FvValues.ContainsKey(siteClass))
            {
                foreach (var (range, value) in FvValues[siteClass])
                {
                    if (range.Contains(sValue))
                    {
                        return value;
                    }
                }
            }
            return double.NaN; // Return NaN if the value is out of range or site class is not found
        }
    }





    public static class RiskCategoryConstants
    {
        public static readonly Dictionary<string, Dictionary<string, double>> RiskCategoryTable =
            new Dictionary<string, Dictionary<string, double>>
            {
                { "I", new Dictionary<string, double>
                    {
                        { "Snow", 0.80 },
                        { "Ice_Thickness", 0.80 },
                        { "Ice_Wind", 1.00 },
                        { "Seismic", 1.00 }
                    }
                },
                { "II", new Dictionary<string, double>
                    {
                        { "Snow", 1.00 },
                        { "Ice_Thickness", 1.00 },
                        { "Ice_Wind", 1.00 },
                        { "Seismic", 1.00 }
                    }
                },
                { "III", new Dictionary<string, double>
                    {
                        { "Snow", 1.10 },
                        { "Ice_Thickness", 1.15 },
                        { "Ice_Wind", 1.00 },
                        { "Seismic", 1.25 }
                    }
                },
                { "IV", new Dictionary<string, double>
                    {
                        { "Snow", 1.20 },
                        { "Ice_Thickness", 1.25 },
                        { "Ice_Wind", 1.00 },
                        { "Seismic", 1.50 }
                    }
                }
            };


        public static double GetFactor(string riskCategory, string factorType)
        {
            if (RiskCategoryTable.ContainsKey(riskCategory) &&
                RiskCategoryTable[riskCategory].ContainsKey(factorType))
            {
                return RiskCategoryTable[riskCategory][factorType];
            }
            else
            {
                return double.NaN;
            }
        }
    }
}
