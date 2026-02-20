using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterTankTool_WFA.Entity
{
    public class TankProperties
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TankNumber {  get; set; }

        public string Capacity { get; set; }

        public string WeightOfWater { get; set; }

        public string WeightOfSteel {  get; set; }
        public string TotalWeight { get; set; }
        public string ProjectedArea {  get; set; }

        public string Centroid { get; set; }

    }
}
