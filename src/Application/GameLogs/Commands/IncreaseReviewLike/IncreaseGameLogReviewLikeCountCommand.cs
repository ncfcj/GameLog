namespace Application.GameLogs.Commands.IncreaseReviewLike;

public sealed record IncreaseGameLogReviewLikeCountCommand(Guid GameLogId) 
    : ICommand<bool>;
