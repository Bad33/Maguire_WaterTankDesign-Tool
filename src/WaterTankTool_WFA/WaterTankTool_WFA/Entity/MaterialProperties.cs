using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterTankTool_WFA.Entity
{
    public class MaterialProperties
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaterialNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string MaterialName { get; set; }

        [Required]
        public string MaterialType { get; set; }

        [Required]
        public int Density { get; set; }

        [Required]
        public int ModulusOfElasticity { get; set; }

        [Required]
        public int TensileYieldStress { get; set; }

        [Required]
        public int TensileUltimateStress { get; set; }

    }
}
