using SharedKernel.Commands;

namespace Application.GameLogs.Commands.ReduceReviewLike;

public sealed record ReduceGameLogReviewLikeCommand(Guid GameLogId) : ICommand<GameLogDto>;
