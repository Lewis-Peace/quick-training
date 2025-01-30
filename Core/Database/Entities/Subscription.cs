using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lift.Buddy.Core.Database.Entities
{
    public enum SubscriptionType
    {
        Free,
        Timespan,
        Lifetime
    }
    public class Subscription
    {
        public Guid AthleteId { get; set; }
        public Guid TrainerId { get; set; }
        public SubscriptionType SubscriptionType { get; set; }
        public DateTime? Expiration { get; set; }

        public User Athlete { get; set; }
        public User Trainer { get; set; }

    }
}
