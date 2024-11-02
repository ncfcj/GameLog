using SharedKernel.Commands;

namespace Application.GameLogs.Commands.Delete;

public sealed record DeleteGameLogCommand(Guid GameLogId) : ICommand<bool>;
