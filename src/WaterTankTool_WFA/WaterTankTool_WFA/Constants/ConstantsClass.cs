using System;
using System.Collections.Generic;

namespace WaterTankTool_WFA.Constants
{
    public static class ConstantsClass
    {
        // This class can be used to store additional global constants in the future.
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

    public static class SiteClassTable
    {
        // Readonly Dictionary to store site class constants
        public static readonly Dictionary<string, Dictionary<double, double>> Values =
            new Dictionary<string, Dictionary<double, double>>
            {
                { "A", new Dictionary<double, double> { { 0.25, 0.8 }, { 0.5, 0.8 }, { 0.75, 0.8 }, { 1.0, 0.8 }, { 1.25, 0.8 } } },
                { "B", new Dictionary<double, double> { { 0.25, 1.0 }, { 0.5, 1.0 }, { 0.75, 1.0 }, { 1.0, 1.0 }, { 1.25, 1.0 } } },
                { "C", new Dictionary<double, double> { { 0.25, 1.2 }, { 0.5, 1.2 }, { 0.75, 1.1 }, { 1.0, 1.0 }, { 1.25, 1.0 } } },
                { "D", new Dictionary<double, double> { { 0.25, 1.6 }, { 0.5, 1.4 }, { 0.75, 1.2 }, { 1.0, 1.1 }, { 1.25, 1.0 } } },
                { "E", new Dictionary<double, double> { { 0.25, 2.5 }, { 0.5, 1.7 }, { 0.75, 1.2 }, { 1.0, 0.9 }, { 1.25, 0.9 } } },
                { "F", new Dictionary<double, double> { { 0.25, double.NaN }, { 0.5, double.NaN }, { 0.75, double.NaN }, { 1.0, double.NaN }, { 1.25, double.NaN } } }
            };

        // Helper method to safely retrieve values
        public static double GetValue(string siteClass, double sValue)
        {
            if (Values.ContainsKey(siteClass) && Values[siteClass].ContainsKey(sValue))
            {
                return Values[siteClass][sValue];
            }
            else
            {
                return double.NaN;
            }
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
