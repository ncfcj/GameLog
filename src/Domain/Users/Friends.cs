using System.Text.Json.Serialization;
using SharedKernel;

namespace Domain.Users;

public sealed class Friends : Entity
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid FirstUserId { get; set; }
    public Guid SecondUserId { get; set; }
    
    [JsonIgnore]
    public User FirstUser { get; set; }
    
    [JsonIgnore]
    public User SecondUser { get; set; }
}
