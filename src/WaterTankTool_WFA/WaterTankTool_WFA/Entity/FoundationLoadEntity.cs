using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterTankTool_WFA.Solver;

namespace WaterTankTool_WFA.Entity
{
    public class FoundationLoadEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //Wsup = superimposed load 
        public double Wsup {  get; set; }

        //Mu = factored flexural moment for footing strip 
        public double Mu { get; set; }

        //Vu = one-way shear demand 
        public double Vu { get; set; }

        //Vup = punching shear demand 
        public double Vup { get; set; }

        //Bu = concrete bearing demand 
        public double Bu { get; set; }

        //A1 = loaded area for bearing 
        public double AreaLoaded { get; set; }

        //A2 = supporting area for bearing 
        public double SupportingArea {  get; set; }

        //b0 = critical punching perimeter
        public double PunchingPerimeter { get; set; }


    }
}
