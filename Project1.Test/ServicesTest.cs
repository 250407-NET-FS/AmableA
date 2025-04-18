using Project1.Models;
namespace Project1.Test;
using Xunit;
using Moq;
using Project1.Services;
using Project1.Repositories;
using Microsoft.EntityFrameworkCore;

public class ServicesTest
{
    private readonly IReceiptService _receiptService;
    private readonly Mock<IReceiptRepository> _mockReceiptRepository;
    private readonly Mock<IReceiptItemRepository> _mockReceiptItemRepository;
    private readonly Mock<ILoyaltyBenefitsService> _mockLoyaltyBenefitsService;
    private readonly ApplicationDbContext _mockContext;

    public ServicesTest()
    {
        
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("TestDatabase")
            .Options;
        _mockContext = new ApplicationDbContext(options);

        
        _mockReceiptRepository = new Mock<IReceiptRepository>();
        _mockReceiptItemRepository = new Mock<IReceiptItemRepository>();
        _mockLoyaltyBenefitsService = new Mock<ILoyaltyBenefitsService>();

        
        _receiptService = new ReceiptService(
            _mockReceiptRepository.Object,
            _mockReceiptItemRepository.Object,
            _mockLoyaltyBenefitsService.Object
        );
    }


    Receipt validReceipt = new Receipt { VisitId = Guid.NewGuid(), TotalAmount = 10.0m };
    List<ReceiptItem> validReceiptItems = new List<ReceiptItem>
    {
        new ReceiptItem
        {
            ItemName = "Item A",
            Price = 10.99m,
            Quantity = 1
        },
        new ReceiptItem
        {
            ItemName = "Item B",
            Price = 5.49m,
            Quantity = 2
        },
        new ReceiptItem
        {
            ItemName = "Item C",
            Price = 3.25m,
            Quantity = 3
        }
    };

    [Fact]
    public void TestCalculateTotalReceiptsPrice()
    {
        // Act
        Receipt testedReceipt = _receiptService.CalculateTotalReceiptsPrice(validReceipt, validReceiptItems);

        // Assert
        Assert.NotNull(testedReceipt);
        Assert.Equal(31.72m, testedReceipt.TotalAmount);
    }
}
