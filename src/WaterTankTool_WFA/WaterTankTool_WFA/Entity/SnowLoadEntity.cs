using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterTankTool_WFA.Entity
{
    public class SnowLoadEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public double HeightToConsider { get; set; }

        public double GroundSnowLoad { get; set; }

        public String RiskCategory { get; set; }

        public double ImportanceFactor { get; set; }

        public String Exposure {  get; set; }
        public double ExposureFactor { get; set; }
        public double AreaSubjectedToSnow {  get; set; }
        public double TotalSnowLoad { get; set; }



    }
}
