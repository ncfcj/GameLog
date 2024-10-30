using System.Text.Json.Serialization;
using Domain.GameLogs.Entities;
using Domain.Users;
using SharedKernel;

namespace Domain.GameLogs;

public class GameLog : Entity
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string GameName { get; private set; }
    public DateTime? StartDate { get; private set; }
    public DateTime? EndDate { get; private set; }
    public string? Review { get; private set; }
    public long ReviewLikeCount { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public Platform Platform { get; private set; }
    public Rating? Rating { get; private set; }
    public LogStatus LogStatus { get; private set; }
    public List<Genre> Genres { get; private set; } = [];
    
    public string? SteamAppId { get; private set; }
    public Guid UserId { get; }
    
    [JsonIgnore]
    public User User { get; private set; }

    public GameLog(Guid userId)
    {
        UserId = userId;
        CreatedAt = DateTime.UtcNow;
    }
    
    public void ChangeBasicInformation(string gameName, Platform platform, List<Genre> genres)
    {
        GameName = gameName;
        Platform = platform;
        ChangeGenres(genres);
    }

    public void StartPlaying()
    {
        StartDate = DateTime.UtcNow;
        LogStatus = LogStatus.Playing;

        if (EndDate.HasValue)
        {
            EndDate = null;
        }
    }

    public void Complete(string? review, Rating rating)
    {
        EndDate = DateTime.UtcNow;
        LogStatus = LogStatus.Complete;
        Rating = rating;

        if (review is not null)
        {
            Review = review;
        }
    }

    public void Abandon()
    {
        EndDate = DateTime.UtcNow;
        LogStatus = LogStatus.Abandoned;
    }

    public void WaitingForReleases()
    {
        LogStatus = LogStatus.WaitingForNewReleases;
    }

    public void PutGameOnHold()
    {
        LogStatus = LogStatus.OnHold;

        if (EndDate.HasValue)
        {
            EndDate = null;
        }
    }
    
    public void LikeReview()
    {
        ReviewLikeCount++;
    }

    public void RemoveLikeFromReview()
    {
        if (ReviewLikeCount <= 0)
        {
            return;
        }
        
        ReviewLikeCount--;
    }

    public void ChangeStartDate(DateTime startDate)
    {
        StartDate = startDate;
    }

    public void ChangeEndDate(DateTime endDate)
    {
        EndDate = endDate;
    }
    
    public void ChangeStatus(LogStatus status)
    {
        switch (status)
        {
            case LogStatus.OnHold:
                PutGameOnHold();
                break;
            case LogStatus.Abandoned:
                Abandon();
                break;
            case LogStatus.Playing:
                StartPlaying();
                break;
            case LogStatus.WaitingForNewReleases:
                WaitingForReleases();
                break;
        }
    }
    
    public void AddSteamAppId(string steamAppId)
    {
        SteamAppId = steamAppId;
    }

    public void UpdateMainData(string? gameName, DateTime? startDate, DateTime? endDate,
        Rating? rating, List<Genre>? genres)
    {
        GameName = gameName ?? GameName;
        StartDate = startDate ?? StartDate;
        EndDate = endDate ?? EndDate;
        Rating = rating ?? Rating;
        
        if (genres is not null)
        {
            ChangeGenres(genres);
        }
    }
    
    private void ChangeGenres(List<Genre> genreList)
    {
        Genres.Clear();
        Genres.AddRange(genreList);
    }
}
