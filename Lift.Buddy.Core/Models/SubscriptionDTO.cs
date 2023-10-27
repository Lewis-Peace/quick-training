using Lift.Buddy.Core.Database.Entities;
using System.Text.Json.Serialization;

namespace Lift.Buddy.Core.Models
{
    public class SubscriptionDTO
    {
        [JsonPropertyName("trainerId")]
        public Guid TrainerId { get; set; }
        [JsonPropertyName("athleteId")]
        public Guid AthleteId { get; set; }
        [JsonPropertyName("subscriptionType")]
        public SubscriptionType SubscriptionType { get; set; } = SubscriptionType.Free;
        [JsonPropertyName("expiration")]
        public DateTime? Expiration { get; set; }
    }
}
