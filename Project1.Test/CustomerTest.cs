namespace Project1.Test;
using Xunit;
using Moq;
using Project1.Models;
using Project1.Repositories;
using Microsoft.EntityFrameworkCore;
public class CustomerTest
{
    private readonly Mock<ICustomerRepository> _mockRepository;
    private readonly ApplicationDbContext _mockContext;
    private readonly CustomerRepository _customerRepository;

    Customer validCustomer = new Customer { PhoneNumber = "5555555555", FName = "John", LName = "Doe" };


    public CustomerTest()
    {

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("TestDatabase")
            .Options;

        _mockContext = new ApplicationDbContext(options);


        _mockRepository = new Mock<ICustomerRepository>();


        _customerRepository = new CustomerRepository(_mockContext);
    }
    [Fact]
    public async Task TestCreationSuccess()
    {

        //Arrange
        _mockRepository.Setup(r => r.PostCustomer(It.IsAny<Customer>()))
        .ReturnsAsync(validCustomer);

        // Act
        var createdCustomer = await _customerRepository.PostCustomer(validCustomer);

        // Assert
        Assert.NotNull(createdCustomer);
        Assert.Equal("John", createdCustomer.FName);
        Assert.Equal("Doe", createdCustomer.LName);

    }
    [Fact]
    public async Task TestRetrivalSuccess()
    {
        //Arrange
        _mockRepository.Setup(r => r.GetCustomer(validCustomer.Id)).ReturnsAsync(validCustomer);

        // Act
        var retrievedCustomer = await _mockRepository.Object.GetCustomer(validCustomer.Id);

        // Assert
        Assert.NotNull(retrievedCustomer);
        Assert.Equal(validCustomer.Id, retrievedCustomer.Id);
        Assert.Equal(validCustomer.FName, retrievedCustomer.FName);
        Assert.Equal(validCustomer.LName, retrievedCustomer.LName);
        Assert.Equal(validCustomer.PhoneNumber, retrievedCustomer.PhoneNumber);
    }
    [Fact]
    public async Task TestRetrivalFailureIdNotFound()
    {
        //Arrange
        var invalidId = Guid.NewGuid();
        _mockRepository.Setup(r => r.GetCustomer(invalidId)).ReturnsAsync((Customer)null);

        // Act

        var retrievedCustomer = await _mockRepository.Object.GetCustomer(validCustomer.Id);

        // Assert
        Assert.Null(retrievedCustomer);

    }
    [Fact]
    public async Task TestDeleteSuccess()
    {
        //Arrange
        _mockRepository.Setup(c => c.GetCustomer(validCustomer.Id)).ReturnsAsync(validCustomer);
        _mockRepository.Setup(v => v.DeleteCustomer(validCustomer.Id)).ReturnsAsync(true);


        //Act
        var deletedSuccess = await _mockRepository.Object.DeleteCustomer(validCustomer.Id);

        //Assert
        Assert.True(deletedSuccess);
    }
    
}
