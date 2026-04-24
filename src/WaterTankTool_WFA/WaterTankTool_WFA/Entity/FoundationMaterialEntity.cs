using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterTankTool_WFA.Entity
{
    public class FoundationMaterialEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //f'c = concrete compressive strength 
        public double fc {  get; set; }

        //fy = reinforcement yield strength
        public double fy { get; set; }
        //γc = concrete unit weight 

        public double yc { get; set; }

        //λ = lightweight concrete factor, if applicable

        public double lambda { get; set; }

    }
}
