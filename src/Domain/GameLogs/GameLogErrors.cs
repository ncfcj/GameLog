using SharedKernel;

namespace Domain.GameLogs;

public static class GameLogErrors
{
    public static Error NotFound(Guid gameLogId) => Error.NotFound(
        "GameLog.NotFound",
        $"The GameLog with the Id = '{gameLogId}' was not found");

    public static Error GameIsAlreadyComplete(Guid gameLogId) => new(
        "GameLog.GameIsAlreadyComplete",
        $"The GameLog with the Id = '{gameLogId}' is already complete",
        ErrorType.Conflict);
    
    public static Error GameWasNotBeingPlayed(Guid gameLogId) => new(
        "GameLog.GameWasNotBeingPlayed",
        $"The GameLog with the Id = '{gameLogId}' was not being played",
        ErrorType.Conflict);
    
    public static Error CannotCompleteTheGameInThisRoute(Guid gameLogId) => new(
        "GameLog.WrongRoute",
        $"The GameLog with the Id = '{gameLogId}' cannot be completed through this route",
        ErrorType.Conflict);
    
    public static Error GameLogIdIsNecessaryForUpdate => new("GameLog.WrongRoute",
    $"The GameLog Id must be informed to update the GameLog",
    ErrorType.Conflict);
}
