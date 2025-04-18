namespace Project1.Test;
using Xunit;
using Moq;
using Project1.Models;
using Project1.Repositories;
using Microsoft.EntityFrameworkCore;
public class VisitTest : ITest
{

    private readonly Mock<IVisitRepository> _mockRepository;
    private readonly ApplicationDbContext _mockContext;
    private readonly VisitRepository _visitRepository;


    Visit validVisit = new Visit { CustomerId = Guid.NewGuid(), StoreId = 1, VisitDate = DateTime.Now, PointsAccumulated = 0 };

    public VisitTest()
    {

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("TestDatabase")
            .Options;

        _mockContext = new ApplicationDbContext(options);


        _mockRepository = new Mock<IVisitRepository>();


        _visitRepository = new VisitRepository(_mockContext);
    }



    [Fact]
    public async Task TestCreationSuccess()
    {
        //Arrange
        _mockRepository.Setup(v => v.PostVisit(It.IsAny<Visit>()))
        .ReturnsAsync(validVisit);

        // Act
        var createdVisit = await _visitRepository.PostVisit(validVisit);

        // Assert
        Assert.NotNull(createdVisit);
        Assert.Equal(validVisit.Id, createdVisit.Id);
        Assert.Equal(validVisit.CustomerId, createdVisit.CustomerId);
        Assert.Equal(1, createdVisit.StoreId);
        Assert.Equal(validVisit.VisitDate, createdVisit.VisitDate);
        Assert.Equal(0, createdVisit.PointsAccumulated);
    }
    public async Task TestRetrivalSuccess()
    {
        //Arrange
        _mockRepository.Setup(v => v.GetVisit(validVisit.Id)).ReturnsAsync(validVisit);

        // Act
        var retrievedVisit = await _mockRepository.Object.GetVisit(validVisit.Id);

        // Assert
        Assert.NotNull(retrievedVisit);
        Assert.Equal(validVisit.Id, retrievedVisit.Id);
        Assert.Equal(validVisit.CustomerId, retrievedVisit.CustomerId);
        Assert.Equal(validVisit.StoreId, retrievedVisit.StoreId);
        Assert.Equal(validVisit.VisitDate, retrievedVisit.VisitDate);
        Assert.Equal(validVisit.PointsAccumulated, retrievedVisit.PointsAccumulated);
    }

    public async Task TestRetrivalFailureIdNotFound()
    {
        //Arrange
        var invalidId = Guid.NewGuid();
        _mockRepository.Setup(v => v.GetVisit(invalidId)).ReturnsAsync((Visit)null);

        // Act

        var retrievedVisit = await _mockRepository.Object.GetVisit(validVisit.Id);

        // Assert
        Assert.Null(retrievedVisit);
    }
}