using Lift.Buddy.Core.Models.Enums;

namespace Lift.Buddy.Core.Models
{
    public class FrontpageDTO
    {
        public string Description { get; set; }
        public CRUD state { get; set; } = CRUD.Update;
    }
}
