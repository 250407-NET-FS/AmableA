namespace Project1.Test;
using Xunit;
using Moq;
using Project1.Models;
using Project1.Repositories;
using Microsoft.EntityFrameworkCore;
public class ReceiptTest : ITest
{

    private readonly Mock<IReceiptRepository> _mockRepository;

    private readonly ApplicationDbContext _mockContext;

    private readonly ReceiptRepository _receiptRepository;

    Receipt validReceipt = new Receipt{ VisitId = Guid.NewGuid(), TotalAmount = 10.0m };

    public ReceiptTest()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
           .UseInMemoryDatabase("TestDatabase")
           .Options;

        _mockContext = new ApplicationDbContext(options);


        _mockRepository = new Mock<IReceiptRepository>();


        _receiptRepository = new ReceiptRepository(_mockContext);
    }


    [Fact]
    public async Task TestCreationSuccess()
    {
        //Arrange
        _mockRepository.Setup(r => r.PostReceipt(It.IsAny<Receipt>()))
        .ReturnsAsync(validReceipt);

        // Act
        var createdReceipt = await _receiptRepository.PostReceipt(validReceipt);

        // Assert
        Assert.NotNull(createdReceipt);
        Assert.Equal(validReceipt.Id, createdReceipt.Id);
        Assert.Equal(validReceipt.VisitId, createdReceipt.VisitId);
        Assert.Equal(10.0m, createdReceipt.TotalAmount);
        Assert.Equal(validReceipt.ReceiptItem, createdReceipt.ReceiptItem);
    } 

    [Fact]
    public async Task TestRetrivalSuccess()
    {
        //Arrange
        _mockRepository.Setup(r => r.GetReceipt(validReceipt.Id)).ReturnsAsync(validReceipt);

        // Act
        var retrievedReceipt = await _mockRepository.Object.GetReceipt(validReceipt.Id);

        // Assert
        Assert.NotNull(retrievedReceipt);
        Assert.Equal(validReceipt.Id, retrievedReceipt.Id);
        Assert.Equal(validReceipt.VisitId, retrievedReceipt.VisitId);
        Assert.Equal(validReceipt.TotalAmount, retrievedReceipt.TotalAmount);
        Assert.Equal([], retrievedReceipt.ReceiptItem);
    }
    [Fact]
    public async Task TestRetrivalFailureIdNotFound()
    {
        //Arrange
        var invalidId = Guid.NewGuid();
        _mockRepository.Setup(r => r.GetReceipt(invalidId)).ReturnsAsync((Receipt)null);

        // Act

        var retrievedReceipt = await _mockRepository.Object.GetReceipt(validReceipt.Id);

        // Assert
        Assert.Null(retrievedReceipt);
    }

    
}