using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterTankTool_WFA.Entity
{
    public class BasePlateEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // Dbp = base plate diameter (ft or in)
        public double Dbp { get; set; }

        // Ro = base plate outside radius
        public double Ro { get; set; }

        // Ri = base plate inside radius
        public double Ri { get; set; }

        // θ = base plate segment angle (deg)
        public double Theta { get; set; }

        // t = base plate thickness (in)
        public double T { get; set; }

        // n = number of base plate segments
        public int N { get; set; }

        // rs = steel unit weight = 490 pcf
        public double Rs { get; set; }

        // Nh = number of anchor bolt holes in one segment
        public int Nh { get; set; }

        // dh = diameter of anchor bolt hole
        public double Dh { get; set; }

        // α = start angle of segment (deg), if you want location in plan
        public double? A { get; set; }

        // rb = bolt circle radius, if bolt layout is needed
        public double? Rb { get; set; }
    }
}
