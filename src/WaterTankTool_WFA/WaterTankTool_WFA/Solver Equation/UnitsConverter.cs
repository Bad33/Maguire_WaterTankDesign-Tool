using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterTankTool_WFA.Solver_Equation
{
    public class UnitsConverter
    {

        public UnitsConverter()
        {

        }

        public double inch_TO_Ft(double value)
        {
            return value * 0.08333333;
        }
    }
}
