using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterTankTool_WFA
{
    public static class AppState
    {
        public static int NoOfColumns { get; set; } = 1;

        public static TankType CurrentTankType { get; set; } = TankType.None;

        public static string Fy { get; set; } = "36000";
        public static int Rtc { get; set; } = 334;


    }
}
