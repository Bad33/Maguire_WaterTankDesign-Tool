using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterTankTool_WFA.Constants;
using WaterTankTool_WFA.Entity;

namespace WaterTankTool_WFA.Solver_Equation
{

    public class Equations
    {
        //Constants
        private const double rw = 62.4; // Weight density of water (pcf)
        private const double rs = 490;  // Weight density of steel (pcf)
        private const double rc = 144;  // Weight density of concrete (pcf)


    }

    public class Segment_Cylinder_Equations
    {
        UnitsConverter unitsConverter = new UnitsConverter();

        Material_Property_Data Material_Property_Data;

        private WaterTankDbContext _context;

        private WindLoadEntity Qwind;

        public Segment_Cylinder_Equations() {
            _context = WaterTankDbContext.GetInstance();
            Qwind = _context.WindLoadEntity.FirstOrDefault();



        }

        public double ProjectedArea(double heightInitial,double heightfinal, double diameter)
        {
            var height = heightfinal - heightInitial;
            var result = (height * diameter);
            return result;
        }

        public double Centroid(double heightInitial,double heightFinal)
        {
            var height = heightFinal - heightInitial;
            var result = heightInitial + (height / 2);
            return result;
        }

        public double weightOfPedestal(double heightInitial,double heightFinal,double Diameter,double t)
        {
            var thickness = unitsConverter.inch_TO_Ft(t);

            var height = heightFinal - heightInitial;

            var outerVolume = (Math.PI / 4) * (Math.Pow(Diameter, 2) * height);

            var innerVolume = (Math.PI / 4) * Math.Pow((Diameter - (2*thickness)),2) * height;

            var segmentVolume = outerVolume - innerVolume;

            var weight = (segmentVolume * ConstantsClass.rs ) / 1000;

            return weight;
        }

        public double kzi(double heightinitial)
        {
            //this need to be changed according to the Exposure class selected
            var zg = WindLoadExposure_C.Zg;
            var a = WindLoadExposure_C.Alpha;
            var result = 2.01 * Math.Pow((heightinitial / zg), 2 / a);
            return result;
        }

        public double kzf(double heightfinal)
        {
            //this need to be changed according to the Exposure class selected
            var zg = WindLoadExposure_C.Zg;
            var a = WindLoadExposure_C.Alpha;
            var result = 2.01 * Math.Pow((heightfinal / zg), 2 / a);
            return result;
        }

        public double qzi(double heightInitial)
        {
            double result = 0;
            if (Qwind != null)
            {

                var value1 = Qwind.Q * kzi(heightInitial) * Qwind.G;
                var value2 = 30 * Qwind.Cf;
                result = Math.Max(value1,value2);
            }

            return result;
        }

        public double qzf(double heightFinal)
        {
            double result = 0;
            if (Qwind != null)
            {

                var value1 = Qwind.Q * kzf(heightFinal) * Qwind.G;
                var value2 = 30 * Qwind.Cf;
                result = Math.Max(value1, value2);
            }

            return result;
        }

        public double F(double heightInitial,double heightFinal,double diameter)
        {
            var height = heightFinal - heightInitial;
            var result = (((qzi(heightInitial) + qzf(heightFinal)) / 2) * ProjectedArea(heightInitial,heightFinal, diameter)) / 1000;

            return result;
        }
        public double L(double heightInitial,double heightFinal)
        {
            var h = heightFinal - heightInitial;

            double numerator = (qzi(heightInitial) * Math.Pow(h, 2) / 2) + (0.5 * (qzf(heightFinal) - qzi(heightInitial)) * Math.Pow(2 * h, 2) / 3);

            double denominator = (qzi(heightInitial) * h) + (0.5 * (qzf(heightFinal) - qzi(heightInitial)) * h);

            double F = heightInitial + (numerator / denominator);

            return F;

        }

        public double Mbase(double heightInitial, double heightFinal, double diameter)
        {
            var result = L(heightInitial, heightFinal) * F(heightInitial, heightFinal, diameter);
            return result;
        }
    }


    //Equations for Conical Segments
    public class Segment_Conical_Equations
    {
        UnitsConverter unitsConverter = new UnitsConverter();

        private WaterTankDbContext _context;

        private WindLoadEntity Qwind;

        public Segment_Conical_Equations()
        {
            _context = WaterTankDbContext.GetInstance();
            Qwind = _context.WindLoadEntity.FirstOrDefault();

        }

        public double ProjectedArea(double heightInitial, double heightfinal, double diameter)
        {
            var height = heightfinal - heightInitial;
            var result = (height * diameter);
            return result;
        }

        public double Centroid(double heightInitial, double heightFinal)
        {
            var height = heightFinal - heightInitial;
            var result = heightInitial + (height / 2);
            return result;
        }

        public double weight(double hi, double hf, double di,double df, double t)
        {
            var thickness = unitsConverter.inch_TO_Ft(t);

            var d = df - di;
            var h = hf - hi;

            var S1 = (Math.PI / 4) * Math.Pow(di,2);
            var S2 = (Math.PI / 4) * Math.Pow(df,2);
            var S3 = (Math.PI / 4) * Math.Pow((di - (thickness/12)),2);
            var S4 = (Math.PI / 4) * Math.Pow((df - (thickness / 12)), 2);

            var weight = ((h / 3) * (S1 + S2 - S3 - S4 + Math.Sqrt(S1 * S2) - Math.Sqrt(S3*S4))) * (ConstantsClass.rs/1000);


            return weight;
        }

        public double kzi(double heightinitial)
        {
            //this need to be changed according to the Exposure class selected
            var zg = WindLoadExposure_C.Zg;
            var a = WindLoadExposure_C.Alpha;
            var result = 2.01 * Math.Pow((heightinitial / zg), 2 / a);
            return result;
        }

        public double kzf(double heightfinal)
        {
            //this need to be changed according to the Exposure class selected
            var zg = WindLoadExposure_C.Zg;
            var a = WindLoadExposure_C.Alpha;
            var result = 2.01 * Math.Pow((heightfinal / zg), 2 / a);
            return result;
        }

        public double qzi(double heightInitial)
        {
            double result = 0;
            if (Qwind != null)
            {

                var value1 = Qwind.Q * kzi(heightInitial) * Qwind.G;
                var value2 = 30 * Qwind.Cf;
                result = Math.Max(value1, value2);
            }

            return result;
        }

        public double qzf(double heightFinal)
        {
            double result = 0;
            if (Qwind != null)
            {

                var value1 = Qwind.Q * kzf(heightFinal) * Qwind.G;
                var value2 = 30 * Qwind.Cf;
                result = Math.Max(value1, value2);
            }

            return result;
        }

        public double F(double heightInitial, double heightFinal, double diameter)
        {
            var height = heightFinal - heightInitial;
            var result = ((qzi(heightInitial) + qzf(heightFinal)) / 2) * ProjectedArea(heightInitial, heightFinal, diameter);

            return result;
        }
        public double L(double heightInitial, double heightFinal)
        {
            var h = heightFinal - heightInitial;

            double numerator = (qzi(heightInitial) * Math.Pow(h, 2) / 2) + (0.5 * (qzf(heightFinal) - qzi(heightInitial)) * Math.Pow(2 * h, 2) / 3);

            double denominator = (qzi(heightInitial) * h) + (0.5 * (qzf(heightFinal) - qzi(heightInitial)) * h);

            double F = heightInitial + (numerator / denominator);

            return F;

        }

        public double Mbase(double heightInitial, double heightFinal, double diameter)
        {
            var result = L(heightInitial, heightFinal) * F(heightInitial, heightFinal, diameter);
            return result;
        }

    }
}
