using Domain.GameLogs;
using Domain.GameLogs.Entities;

namespace Application.GameLogs.Commands;

public class GameLogDto
{
    public Guid Id { get; set; }
    public string GameName { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Review { get; set; }
    public long ReviewLikeCount { get; set; }
    public DateTime? CreatedAt { get; set; }
    public Platform Platform { get; set; }
    public Rating? Rating { get; set; }
    public LogStatus LogStatus { get; set; }
    public List<Genre> Genres { get; set; }
    public string? SteamAppId { get; set; }
    public Guid UserId { get; }

    public GameLogDto(GameLog gameLog)
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
    
    public GameLogDto() { }
}
