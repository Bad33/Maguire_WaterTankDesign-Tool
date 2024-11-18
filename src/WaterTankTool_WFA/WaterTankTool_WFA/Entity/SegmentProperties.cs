using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterTankTool_WFA.Entity
{
    public class SegmentProperties
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SegmentNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string SegmentName { get; set; }

        [Required]
        public string SegmentType { get; set; }

        [Range(0, double.MaxValue)]
        public double Diameter { get; set; }

        [Range(0, double.MaxValue)]
        public double Thickness { get; set; }

        [Range(0, double.MaxValue)]
        public double HeightInitial { get; set; }

        [Range(0, double.MaxValue)]
        public double HeightFinal { get; set; }

        [Range(0, double.MaxValue)]
        public double? DiameterInitial { get; set; }

        [Range(0, double.MaxValue)]
        public double? DiameterFinal { get; set; }
    }
}
