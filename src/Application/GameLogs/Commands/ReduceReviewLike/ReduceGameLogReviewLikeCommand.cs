using Application.Abstractions.Messaging;

namespace Application.GameLogs.Commands.ReduceReviewLike;

public sealed record ReduceGameLogReviewLikeCommand(Guid GameLogId) : ICommand<bool>;
