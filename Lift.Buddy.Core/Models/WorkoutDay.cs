using MigraDoc.DocumentObjectModel;
using System.Text.Json.Serialization;

namespace Lift.Buddy.Core.Models
{
    public class WorkoutDay
    {
        [JsonPropertyName("day")]
        public int Day { get; set; }
        [JsonPropertyName("exercises")]
        public List<Exercise> Exercises { get; set; } = new List<Exercise>();


        public Document GetPDF()
        {
            var document = new Document();

            Section section = document.AddSection();

            var paragraph = section.AddParagraph();
            foreach (var exercise in Exercises)
            {
                paragraph.AddText(exercise.ToString());
            }

            return document;
        }
    }
}
