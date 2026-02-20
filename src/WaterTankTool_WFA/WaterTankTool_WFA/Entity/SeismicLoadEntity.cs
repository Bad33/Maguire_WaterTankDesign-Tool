using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterTankTool_WFA.Entity
{
    public class SeismicLoadEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public double Ss { get; set; }
        public double S1 { get; set; }
        public string SiteClass { get; set; }
        public double Fa {  get; set; }
        public double Fv { get; set; }
        public double Sds { get; set; }
        public double Sd1 { get; set; }
        public double Ri { get; set; }
        public double Ie { get; set; }
        public double Tl { get; set; } 
        public double Ti { get; set; }
        public double Ts { get; set; }
        public double Sa { get; set; }
        public double Lambda { get; set; }
        public double Ai { get; set; }

        public double V { get; set; }
    }
}
