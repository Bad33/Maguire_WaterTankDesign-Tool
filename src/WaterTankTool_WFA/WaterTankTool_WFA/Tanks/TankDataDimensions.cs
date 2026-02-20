using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WaterTankTool_WFA.Tanks
{
    public class TankDataDimensions
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("Weight_of_Water")]
        public string Weight_of_Water { get; set; }

        [JsonPropertyName("Weight_of_Steel")]
        public string Weight_of_Steel { get; set; }

        [JsonPropertyName("Total_Weight")]
        public string Total_Weight { get; set; }

        [JsonPropertyName("Projected_Area")]
        public string Projected_Area { get; set; }

        [JsonPropertyName("height")]
        public string Height { get; set; }

        [JsonPropertyName("diameter")]
        public string Diameter { get; set; }

        [JsonPropertyName("thickness")]
        public string Thickness { get; set; }

        [JsonPropertyName("Centroid")]
        public string Centroid { get; set; }

        [JsonPropertyName("Weight_of_Bowl_and_cone")]
        public string Weight_of_Bowl_and_cone { get; set; }

    }
}
