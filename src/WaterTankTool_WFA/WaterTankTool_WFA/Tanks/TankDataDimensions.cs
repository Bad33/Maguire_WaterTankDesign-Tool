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

        [JsonPropertyName("capacity")]
        public string Capacity { get; set; }

        [JsonPropertyName("height")]
        public string Height { get; set; }

        [JsonPropertyName("diameter")]
        public string Diameter { get; set; }

        [JsonPropertyName("thickness")]
        public string Thickness { get; set; }
    }
}
