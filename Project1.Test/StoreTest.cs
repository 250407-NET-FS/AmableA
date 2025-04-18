namespace Project1.Test;
using Xunit;
using Moq;
using Project1.Models;
using Project1.Repositories;
using Microsoft.EntityFrameworkCore;
public class StoreTest : ITest
{

    private readonly Mock<IStoreRepository> _mockRepository;
    private readonly ApplicationDbContext _mockContext;
    private readonly StoreRepository _storeRepository;

    Store validStore = new Store{StoreNumber = 1, Address = "valid address", PhoneNumber = "valid phone"} ;

    public StoreTest()
    {


        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("TestDatabase")
            .Options;

        _mockContext = new ApplicationDbContext(options);


        _mockRepository = new Mock<IStoreRepository>();


        _storeRepository = new StoreRepository(_mockContext);
    }

    [Fact]
    public async Task TestCreationSuccess()
    {

        //Arrange
        _mockRepository.Setup(r => r.PostStore(It.IsAny<Store>()))
        .ReturnsAsync(validStore);

        // Act
        var createdStore = await _storeRepository.PostStore(validStore);

        // Assert
        Assert.NotNull(createdStore);
        Assert.Equal(1, createdStore.StoreNumber);
        Assert.Equal("valid address", createdStore.Address);
        Assert.Equal("valid phone", createdStore.PhoneNumber);


    }

    [Fact]
    public async Task TestRetrivalSuccess()
    {
        //Arrange
        _mockRepository.Setup(r => r.GetStore(validStore.StoreNumber)).ReturnsAsync(validStore);

        // Act
        var retrievedStore = await _mockRepository.Object.GetStore(validStore.StoreNumber);

        // Assert
        Assert.NotNull(retrievedStore);
        Assert.Equal(retrievedStore.StoreNumber, retrievedStore.StoreNumber);
        Assert.Equal(retrievedStore.PhoneNumber, retrievedStore.PhoneNumber);
        Assert.Equal(retrievedStore.Address, retrievedStore.Address);

    }
        [Fact]
        public async Task TestRetrivalFailureIdNotFound()
    {
        //Arrange
        var invalidStoreNumber = -1;
        _mockRepository.Setup(r => r.GetStore(invalidStoreNumber)).ReturnsAsync((Store)null);

        // Act
        
        var retrievedStore = await _mockRepository.Object.GetStore(validStore.StoreNumber);

        // Assert
        Assert.Null(retrievedStore);

    }

}
//Store validStore = new Store{StoreNumber = 0, Address = "valid address", PhoneNumber = "valid phone"}