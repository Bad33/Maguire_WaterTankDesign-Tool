using System;

namespace WaterTankTool_WFA.Solver_Equation
{
    public class FoundationEquations
    {
        public class AnchorBoltEquations
        {
            // 1) Bolt cross-sectional area
            public double CrossSectionalArea(double nominalBoltDiameter)
            {
                return Math.Round((Math.PI * Math.Pow(nominalBoltDiameter, 2)) / 4.0, 5);
            }

            // 2) Hole area
            public double HoleArea(double boltHoleDiameter)
            {
                return Math.Round((Math.PI * Math.Pow(boltHoleDiameter, 2)) / 4.0, 5);
            }

            // 3) Bolt angular spacing around full circle
            public double BoltAngularSpacing(int totalNoOfAnchorBolts)
            {
                if (totalNoOfAnchorBolts <= 0) return 0;
                return Math.Round(360.0 / totalNoOfAnchorBolts, 5);
            }

            // 4) Angle of each bolt
            public double BoltAngle(double startAngleFirstBoltDeg, int boltNumber, int totalNoOfAnchorBolts)
            {
                return Math.Round(
                    startAngleFirstBoltDeg + ((boltNumber - 1) * BoltAngularSpacing(totalNoOfAnchorBolts)),
                    5);
            }

            // 5) Coordinates of each anchor bolt
            public double BoltXCoordinate(double boltCircleRadius, double boltAngleDeg)
            {
                double angleRad = DegreeToRadian(boltAngleDeg);
                return Math.Round(boltCircleRadius * Math.Cos(angleRad), 5);
            }

            public double BoltYCoordinate(double boltCircleRadius, double boltAngleDeg)
            {
                double angleRad = DegreeToRadian(boltAngleDeg);
                return Math.Round(boltCircleRadius * Math.Sin(angleRad), 5);
            }

            // 6) Arc spacing between adjacent bolts
            public double ArcSpacing(double boltCircleRadius, int totalNoOfAnchorBolts)
            {
                if (totalNoOfAnchorBolts <= 0) return 0;

                double thetaRad = (2.0 * Math.PI) / totalNoOfAnchorBolts;
                return Math.Round(boltCircleRadius * thetaRad, 5);
            }

            // 7) Chord spacing between adjacent bolts
            public double ChordSpacing(double boltCircleRadius, int totalNoOfAnchorBolts)
            {
                if (totalNoOfAnchorBolts <= 0) return 0;

                return Math.Round(
                    2.0 * boltCircleRadius * Math.Sin(Math.PI / totalNoOfAnchorBolts),
                    5);
            }

            // 8) Number of bolts per segment
            public double BoltsPerSegment(int totalNoOfAnchorBolts, int noOfSegments)
            {
                if (noOfSegments <= 0) return 0;
                return Math.Round((double)totalNoOfAnchorBolts / noOfSegments, 5);
            }

            public bool BoltsPerSegmentIsInteger(int totalNoOfAnchorBolts, int noOfSegments)
            {
                if (noOfSegments <= 0) return false;
                return totalNoOfAnchorBolts % noOfSegments == 0;
            }

            // 9) Bolt hole clear edge distance check
            // e_clear = e - dh/2
            public double ClearEdgeDistance(double edgeDistance, double boltHoleDiameter)
            {
                return Math.Round(edgeDistance - (boltHoleDiameter / 2.0), 5);
            }

            // 10) Tension demand per bolt
            // Tb = Tu / Nb
            public double TensionDemandPerBolt_Equal(double totalTensionDemand, int totalNoOfAnchorBolts)
            {
                if (totalNoOfAnchorBolts <= 0) return 0;
                return Math.Round(totalTensionDemand / totalNoOfAnchorBolts, 5);
            }

            // Tb = Tu / Neffi
            public double TensionDemandPerBolt_Effective(double totalTensionDemand, int effectiveNoOfBoltsInTension)
            {
                if (effectiveNoOfBoltsInTension <= 0) return 0;
                return Math.Round(totalTensionDemand / effectiveNoOfBoltsInTension, 5);
            }

            // 11) Shear demand per bolt
            // Vb = Vu / Nb
            public double ShearDemandPerBolt(double totalShearDemand, int totalNoOfAnchorBolts)
            {
                if (totalNoOfAnchorBolts <= 0) return 0;
                return Math.Round(totalShearDemand / totalNoOfAnchorBolts, 5);
            }

            // 12) Tensile capacity of one bolt
            // Pnt = Ab * Fnt
            public double TensileCapacityBasic(double nominalBoltDiameter, double Fnt)
            {
                double Ab = CrossSectionalArea(nominalBoltDiameter);
                return Math.Round(Ab * Fnt, 5);
            }

            // φPnt = φ * Ab * Fnt
            public double TensileDesignStrength(double nominalBoltDiameter, double Fnt, double phi)
            {
                double Ab = CrossSectionalArea(nominalBoltDiameter);
                return Math.Round(phi * Ab * Fnt, 5);
            }

            // φPnt = φ * Ab * Fu
            public double TensileDesignStrengthUltimate(double nominalBoltDiameter, double Fu, double phi)
            {
                double Ab = CrossSectionalArea(nominalBoltDiameter);
                return Math.Round(phi * Ab * Fu, 5);
            }

            // 13) Shear capacity of one bolt
            // Vn = Ab * Fnv
            public double ShearCapacityBasic(double nominalBoltDiameter, double Fnv)
            {
                double Ab = CrossSectionalArea(nominalBoltDiameter);
                return Math.Round(Ab * Fnv, 5);
            }

            // Optional design strength
            public double ShearDesignStrength(double nominalBoltDiameter, double Fnv, double phi)
            {
                double Ab = CrossSectionalArea(nominalBoltDiameter);
                return Math.Round(phi * Ab * Fnv, 5);
            }

            // 14) Interaction check
            public double InteractionCheck(double tensionDemandPerBolt, double tensileStrength,
                                           double shearDemandPerBolt, double shearStrength)
            {
                if (tensileStrength <= 0 || shearStrength <= 0) return 0;

                return Math.Round(
                    (tensionDemandPerBolt / tensileStrength) +
                    (shearDemandPerBolt / shearStrength),
                    5);
            }

            public bool InteractionPass(double interactionValue)
            {
                return interactionValue <= 1.0;
            }

            private double DegreeToRadian(double degree)
            {
                return degree * Math.PI / 180.0;
            }
        }

        public class BasePlateEquations
        {
            // Gross area, Ag
            // Ag = (theta / 360) * pi * (Ro^2 - Ri^2)
            public double GrossArea(double outsideRadius, double insideRadius, double segmentAngleDeg)
            {
                double area = (segmentAngleDeg / 360.0) * Math.PI *
                              (Math.Pow(outsideRadius, 2) - Math.Pow(insideRadius, 2));

                return Math.Round(area, 5);
            }

            // Net area, An
            // An = Ag - Nh * ((pi * dh^2)/4) * (1/144)
            public double NetArea(double outsideRadius, double insideRadius, double segmentAngleDeg,
                                  int noOfBoltHoles, double boltHoleDiameterIn)
            {
                double Ag = GrossArea(outsideRadius, insideRadius, segmentAngleDeg);
                double oneHoleAreaFt2 = ((Math.PI * Math.Pow(boltHoleDiameterIn, 2)) / 4.0) * (1.0 / 144.0);

                double An = Ag - (noOfBoltHoles * oneHoleAreaFt2);

                return Math.Round(An, 5);
            }

            // Volume, V = Ag * (t/12)
            public double Volume(double outsideRadius, double insideRadius, double segmentAngleDeg, double thicknessIn)
            {
                double Ag = GrossArea(outsideRadius, insideRadius, segmentAngleDeg);
                double volume = Ag * (thicknessIn / 12.0);

                return Math.Round(volume, 5);
            }

            // Weight per segment, Wbp = Ag * (t/12) * (rs/1000)
            public double WeightPerSegment(double outsideRadius, double insideRadius, double segmentAngleDeg,
                                           double thicknessIn, double steelUnitWeight)
            {
                double Ag = GrossArea(outsideRadius, insideRadius, segmentAngleDeg);
                double weight = Ag * (thicknessIn / 12.0) * (steelUnitWeight / 1000.0);

                return Math.Round(weight, 5);
            }

            // Total weight, Wbp,total = n * Wbp
            public double TotalWeight(double weightPerSegment, int noOfSegments)
            {
                double totalWeight = noOfSegments * weightPerSegment;
                return Math.Round(totalWeight, 5);
            }

            // Outer arc length, Lo = Ro * θr
            public double OuterArcLength(double outsideRadius, double segmentAngleDeg)
            {
                double thetaRad = ThetaRadians(segmentAngleDeg);
                double length = outsideRadius * thetaRad;

                return Math.Round(length, 5);
            }

            // Inner arc length, Li = Ri * θr
            public double InnerArcLength(double insideRadius, double segmentAngleDeg)
            {
                double thetaRad = ThetaRadians(segmentAngleDeg);
                double length = insideRadius * thetaRad;

                return Math.Round(length, 5);
            }

            // θr = θ(π/180)
            public double ThetaRadians(double segmentAngleDeg)
            {
                return Math.Round(segmentAngleDeg * (Math.PI / 180.0), 5);
            }

            // Radial width, b = Ro - Ri
            public double RadialWidth(double outsideRadius, double insideRadius)
            {
                double width = outsideRadius - insideRadius;
                return Math.Round(width, 5);
            }

            // Centroid radius
            public double CentroidRadius(double outsideRadius, double insideRadius, double segmentAngleDeg)
            {
                double theta_s = segmentAngleDeg * (Math.PI / 180.0);

                if (theta_s == 0 || outsideRadius == insideRadius)
                    return 0;

                double term1 = (4.0 * Math.Sin(theta_s / 2.0)) / (3.0 * theta_s);
                double term2 = (Math.Pow(outsideRadius, 3) - Math.Pow(insideRadius, 3)) /
                               (Math.Pow(outsideRadius, 2) - Math.Pow(insideRadius, 2));

                double rBar = term1 * term2;

                return Math.Round(rBar, 5);
            }

            // β = α + θ/2
            public double CentroidAngle(double startAngleDeg, double segmentAngleDeg)
            {
                double beta = startAngleDeg + (segmentAngleDeg / 2.0);
                return Math.Round(beta, 5);
            }

            // xc = r̄ cosβ
            public double CentroidX(double outsideRadius, double insideRadius,
                                    double segmentAngleDeg, double startAngleDeg)
            {
                double rBar = CentroidRadius(outsideRadius, insideRadius, segmentAngleDeg);
                double betaDeg = CentroidAngle(startAngleDeg, segmentAngleDeg);
                double betaRad = betaDeg * (Math.PI / 180.0);

                double x = rBar * Math.Cos(betaRad);
                return Math.Round(x, 5);
            }

            // yc = r̄ sinβ
            public double CentroidY(double outsideRadius, double insideRadius,
                                    double segmentAngleDeg, double startAngleDeg)
            {
                double rBar = CentroidRadius(outsideRadius, insideRadius, segmentAngleDeg);
                double betaDeg = CentroidAngle(startAngleDeg, segmentAngleDeg);
                double betaRad = betaDeg * (Math.PI / 180.0);

                double y = rBar * Math.Sin(betaRad);
                return Math.Round(y, 5);
            }
        }
    }
}