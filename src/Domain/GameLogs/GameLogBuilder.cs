using Domain.GameLogs.Entities;

namespace Domain.GameLogs;

public class GameLogBuilder
{
    private Guid UserId { get; }
    private GameLog GameLog { get; }
    
    public GameLogBuilder(Guid userId)
    {
        UserId = userId;
        GameLog = new GameLog(UserId);
    }

    public GameLogBuilder WithBasicInformation(string gameName, Platform platform, List<Genre> genres)
    {
        GameLog.ChangeBasicInformation(gameName, platform, genres);
        
        return this;
    }

    public GameLogBuilder WithStatus(LogStatus? status)
    {
        if (status == null)
        {
            return this;
        }

        switch (status)
        {
            case LogStatus.Abandoned:
                GameLog.Abandon();
                break;

            case LogStatus.Playing:
                GameLog.StartPlaying();
                break;

            case LogStatus.OnHold:
                GameLog.PutGameOnHold();
                break;

            case LogStatus.WaitingForNewReleases:
                GameLog.WaitingForReleases();
                break;
        }

        return this;
    }

    public GameLogBuilder WithDates(DateTime? startDate, DateTime? endDate)
    {
        if (startDate != null)
        {
            GameLog.ChangeStartDate(startDate.Value);
        }

        if (endDate != null)
        {
            GameLog.ChangeEndDate(endDate.Value);
        }
        
        return this;
    }

    public GameLogBuilder WithReviewAndRating(string? review, Rating? rating)
    {
        if (review != null && rating != null)
        {
            GameLog.Complete(review, rating.Value);
        }

        return this;
    }

    public GameLog Build()
    {
        return GameLog;
    }
}
