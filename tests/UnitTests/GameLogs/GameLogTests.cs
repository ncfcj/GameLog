using Application.GameLogs.Commands;
using Application.GameLogs.Commands.ChangeStatus;
using Domain.GameLogs;
using Domain.GameLogs.Entities;
using FluentAssertions;
using Moq;

namespace TestProject1.GameLogs;

public class GameLogTests
{
    private readonly Mock<IGameLogRepository> _gameLogRepositoryMock;
    private readonly ChangeGameLogStatusCommandHandler _changeGameLogStatusCommandHandler;

    private static readonly CancellationToken CancellationToken = It.IsAny<CancellationToken>();

    public GameLogTests()
    {
        _gameLogRepositoryMock = new Mock<IGameLogRepository>();
        _changeGameLogStatusCommandHandler = new ChangeGameLogStatusCommandHandler(_gameLogRepositoryMock.Object);
    }
    
    #region ChangeStatus
    [Fact]
    public async Task ChangeStatus_ShouldChange_StatusToPlaying()
    {
        //Arrange
        var userId = Guid.NewGuid();
        var gameLogId = Guid.NewGuid();
        var gameLog = new GameLog(
            "TEST",
            DateTime.Now, 
            DateTime.Now.AddDays(5),
            "TestReview",
            10,
            Platform.Pc,
            Rating.Good,
            LogStatus.OnHold,
            [Genre.Adventure],
            "10000000",
            userId);

        _gameLogRepositoryMock.Setup(x => x.GetGameLogByIdAsync(gameLogId, CancellationToken))
            .ReturnsAsync(gameLog);

        var command = new ChangeGameLogStatusCommand(gameLogId, LogStatus.Playing);

        //Act
        var result = await _changeGameLogStatusCommandHandler.Handle(command, CancellationToken);

        //Assert
        result.IsSuccess.Should().BeTrue();

        result.Value.Should().BeOfType<GameLogDto>();
        result.Value.Should().NotBeNull();

        var gameLogDto = result.Value;
        gameLogDto.LogStatus.Should().Be(LogStatus.Playing);
    }
    
    #endregion
}
