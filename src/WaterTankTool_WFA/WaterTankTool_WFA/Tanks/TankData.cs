using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WaterTankTool_WFA.Tanks
{
    public class TankData
    {
        [JsonPropertyName("tanks")]
        public List<TankDataDimensions> Tanks { get; set; }
    }
}
