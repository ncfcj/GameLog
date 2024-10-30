using Application.Abstractions.Messaging;
using Domain.GameLogs.Entities;

namespace Application.GameLogs.Commands.Complete;

public sealed record CompleteGameLogCommand(
    Guid GameLogId,
    Rating Rating,
    string? Review) 
    : ICommand<bool>;
