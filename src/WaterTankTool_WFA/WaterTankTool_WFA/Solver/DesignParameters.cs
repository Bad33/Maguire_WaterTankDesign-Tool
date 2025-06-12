using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterTankTool_WFA.Solver
{
    public class DesignParameters
    {
        public Double Height { get; set; }
        public Double HeadRange { get; set; }
        public Double FreeBoard { get; set; }
        public Double SnowLoad { get; set; }
        public string SeismicUseGroup { get; set; }
        public string SiteClass { get; set; }
        public double SeismicImportanceFactor { get; set; }
        public double Ss {  get; set; }
        public double S1 { get; set; }
        public double Tl { get; set; }
        public double Fa { get; set; }
        public double Fv { get; set; }
        public double SDS { get; set; }
        public double SD1 { get; set; }

    }
}
