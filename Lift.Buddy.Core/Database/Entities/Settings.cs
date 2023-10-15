using Lift.Buddy.Core.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Lift.Buddy.Core.Database.Entities
{
    public class Settings
    {
        [Key]
        public Guid userId { get; set; }
        public UnitOfMeasure unitOfMeasure { get; set; } = UnitOfMeasure.KG;

        public virtual User User { get; set; }
    }
}
