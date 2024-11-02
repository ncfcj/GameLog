using Domain.GameLogs;
using Domain.GameLogs.Entities;

namespace Application.GameLogs.Queries;

public sealed record GameLogResponse
{
    public Guid Id { get; init; }
    public string GameName { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public string? Review { get; init; }
    public long ReviewLikeCount { get; init; }
    public DateTime? CreatedAt { get; init; }
    public Platform Platform { get; init; }
    public Rating? Rating { get; init; }
    public LogStatus LogStatus { get; init; }
    public List<Genre> Genres { get; init; }
    public string? SteamAppId { get; init; }
    public Guid UserId { get; init; }

    public GameLogResponse(GameLog gameLog)
    {
        Id = gameLog.Id;
        GameName = gameLog.GameName;
        StartDate = gameLog.StartDate;
        EndDate = gameLog.EndDate;
        Review = gameLog.Review;
        ReviewLikeCount = gameLog.ReviewLikeCount;
        CreatedAt = gameLog.CreatedAt;
        Platform = gameLog.Platform;
        Rating = gameLog.Rating;
        LogStatus = gameLog.LogStatus;
        Genres = gameLog.Genres;
        SteamAppId = gameLog.SteamAppId;
        UserId = gameLog.UserId;
    }
}
