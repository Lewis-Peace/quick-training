using Lift.Buddy.Core.Models.Enums;
using System.Text.Json.Serialization;

namespace Lift.Buddy.Core.Models
{
    public class SettingsDTO
    {
        [JsonPropertyName("unitOfMeasure")]
        public UnitOfMeasure unitOfMeasure { get; set; } = UnitOfMeasure.KG;
    }
}
