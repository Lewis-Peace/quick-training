using Lift.Buddy.Core.Models.Enums;
using System.Text.Json.Serialization;

namespace Lift.Buddy.Core.Models;

public class UserDTO
{
    public Guid? UserId { get; set; }

    [JsonPropertyName("username")]
    public string Userame { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("surname")]
    public string Surname { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("isTrainer")]
    public bool IsTrainer { get; set; }

    [JsonPropertyName("private")]
    public bool Private { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("subscriptionState")]
    public SubscriptionState SubscriptionState { get; set; } = SubscriptionState.Undefined;

    public Credentials Credentials { get; set; }

    public IEnumerable<SecurityQuestionDTO> SecurityQuestions { get; set; } = Enumerable.Empty<SecurityQuestionDTO>();
}
