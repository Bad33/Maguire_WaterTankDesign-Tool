using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterTankTool_WFA.Entity
{
    public class FoundationGeoTechEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        //qallow = allowable soil bearing pressure 
        public double qallow { get; set; }
        //μ = friction coefficient, if needed
        public double frictionCoeff { get; set; }
    }
}
