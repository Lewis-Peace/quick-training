using System.ComponentModel.DataAnnotations;

namespace Lift.Buddy.Core.Database.Entities
{
    public class Frontpage
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public virtual User User { get; set; }
    }
}
