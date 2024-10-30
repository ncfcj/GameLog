using System.Text.Json.Serialization;
using SharedKernel;

namespace Domain.Users;

public sealed class Friends : Entity
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid FirstUserId { get; private set; }
    public Guid SecondUserId { get; private set; }
    
    [JsonIgnore]
    public User FirstUser { get; private set; }
    
    [JsonIgnore]
    public User SecondUser { get; private set; }
}
