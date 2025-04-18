using Project1.Models;
using Microsoft.EntityFrameworkCore;
using Project1.Repositories;
using Project1.DTOs;
using Project1.Services;




var builder = WebApplication.CreateBuilder(args);
var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("Connection string"
        + "'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));


//Adding swagger to my dependencies
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "Project1";
    config.Title = "Project1";
    config.Version = "v1";
});


//Dependecy Injection Area
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IStoreRepository, StoreRepository>();
builder.Services.AddScoped<IVisitRepository, VisitRepository>();
builder.Services.AddScoped<IReceiptRepository, ReceiptRepository>();
builder.Services.AddScoped<IReceiptItemRepository, ReceiptItemRepository>();
builder.Services.AddScoped<IReceiptService, ReceiptService>();
builder.Services.AddScoped<ILoyaltyBenefitsService, LoyaltyBenefitsSerivce>();
builder.Services.AddScoped<ILoyaltyPointService, LoyaltyPointService>();



//
var app = builder.Build();

//Swagger
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi(config =>
    {
        config.DocumentTitle = "Project1";
        config.Path = "/swagger";
        config.DocumentPath = "/swagger/{documentName}/swagger.json";
        config.DocExpansion = "list";
    });
}
//https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/applying?tabs=dotnet-core-cli
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
}

app.MapGet("/", () => "Hello World!");



//--Start Customer
app.MapPost("/customer", async (ICustomerRepository customerRepository, Customer customer) =>
{

    var persistedCustomer = await customerRepository.PostCustomer(customer);



    if (persistedCustomer != null)
    {
        CustomerDTO customerDto = new CustomerDTO
        {
            Id = persistedCustomer.Id,
            FName = persistedCustomer.FName,
            MName = persistedCustomer.MName,
            LName = persistedCustomer.LName,
            PhoneNumber = persistedCustomer.PhoneNumber,
            LoyaltyStatus = persistedCustomer.Status
        };
        return Results.Created($"/customer/{customerDto.Id}", customerDto);
    }
    else
    {
        return Results.BadRequest($"Creation Failed");
    }

});

app.MapPost("/customers", async (ICustomerRepository customerRepository, List<Customer> customers) =>
{

    var persistedCustomers = await customerRepository.PostListOfCustomer(customers);


    var customerDtos = persistedCustomers.Select(c => new CustomerDTO
    {
        Id = c.Id,
        FName = c.FName,
        MName = c.MName,
        LName = c.LName,
        PhoneNumber = c.PhoneNumber
    }).ToList();


    //using this so that i can use the item on the result
    var firstCustomer = customerDtos.FirstOrDefault();


    return firstCustomer != null
    ? Results.Created($"/customer/{firstCustomer.Id}", firstCustomer)
    : Results.BadRequest($"Creation Failed");

});



app.MapGet("/customer/{id}", async (ICustomerRepository customerRepository, string id) =>
{

    //https://learn.microsoft.com/en-us/dotnet/api/system.guid.tryparse?view=net-9.0
    if (!Guid.TryParse(id, out var parseId))
    {
        return Results.BadRequest("Not a valid GUID.");
    }

    var obtainedCustomer = await customerRepository.GetCustomer(parseId);

    if (obtainedCustomer != null)
    {
        CustomerDTO customerDto = new CustomerDTO
        {
            Id = obtainedCustomer.Id,
            FName = obtainedCustomer.FName,
            MName = obtainedCustomer.MName,
            LName = obtainedCustomer.LName,
            PhoneNumber = obtainedCustomer.PhoneNumber,
            LoyaltyPoints = obtainedCustomer.LoyaltyPoints,
            LoyaltyStatus = obtainedCustomer.Status
        };
        return Results.Ok(customerDto);
    }
    else
    {
        return Results.NotFound($"No customer was found with Id: {id}");
    }



});


app.MapGet("/customer", async (ICustomerRepository customerRepository) =>
{

    var listOfCustomers = await customerRepository.GetAllCustomers();

    var customerDtos = listOfCustomers.Select(c => new CustomerDTO
    {
        Id = c.Id,
        FName = c.FName,
        MName = c.MName,
        LName = c.LName,
        PhoneNumber = c.PhoneNumber,
        LoyaltyPoints = c.LoyaltyPoints,
        LoyaltyStatus = c.Status
    }).ToList();

    return customerDtos.Count != 0 ? Results.Ok(customerDtos) : Results.NoContent();

});

app.MapDelete("/customer/{id}", async (ICustomerRepository customerRepository, string id) =>
{
    if (!Guid.TryParse(id, out var parseId))
    {
        return Results.BadRequest("Not a valid GUID.");
    }

    bool success = await customerRepository.DeleteCustomer(parseId);

    return success ? Results.Ok("Deleted successfully") : Results.BadRequest("Failed at deleting");

});
//End--Customer



//Start---Store
app.MapPost("/store", async (IStoreRepository storeRepository, Store store) =>
{

    var persistedStore = await storeRepository.PostStore(store);

    if (persistedStore != null)
    {
        StoreDTO storeDto = new StoreDTO
        {
            StoreNumber = persistedStore.StoreNumber,
            Address = persistedStore.Address,
            PhoneNumber = persistedStore.PhoneNumber,

        };
        return Results.Created($"/customer/{storeDto.StoreNumber}", storeDto);
    }
    else
    {
        return Results.BadRequest($"Creation Failed");
    }

});

app.MapPost("/stores", async (IStoreRepository storeRepository, List<Store> stores) =>
{

    var persistedStore = await storeRepository.PostListOfStore(stores);

    var storeDtos = persistedStore.Select(s => new StoreDTO
    {
        StoreNumber = s.StoreNumber,
        Address = s.Address,
        PhoneNumber = s.PhoneNumber,
    }).ToList();


    var firstStore = persistedStore.FirstOrDefault();

    return firstStore != null
    ? Results.Created($"/store/{firstStore.StoreNumber}", firstStore)
    : Results.BadRequest($"Creation Failed");
});


app.MapGet("/store/{id}", async (IStoreRepository storeRepository, int id) =>
{

    var obtainedStore = await storeRepository.GetStore(id);

    if (obtainedStore != null)
    {
        StoreDTO storeDto = new StoreDTO
        {
            StoreNumber = obtainedStore.StoreNumber,
            Address = obtainedStore.Address,
            PhoneNumber = obtainedStore.PhoneNumber,

        };
        return Results.Ok(storeDto);
    }
    else
    {
        return Results.NotFound($"No store was found with Id: {id}");
    }

});


app.MapGet("/store", async (IStoreRepository storeRepository) =>
{

    var listOfStores = await storeRepository.GetAllStores();

    var storeDtos = listOfStores.Select(s => new StoreDTO
    {
        StoreNumber = s.StoreNumber,
        Address = s.Address,
        PhoneNumber = s.PhoneNumber,
    }).ToList();

    return storeDtos.Count != 0 ? Results.Ok(listOfStores) : Results.NoContent();

});

app.MapDelete("/store/{id}", async (IStoreRepository storeRepository, int id) =>
{


    bool success = await storeRepository.DeleteStore(id);

    return success ? Results.Ok("Deleted successfully") : Results.BadRequest("Failed at deleting");

});
//----End Store

//Start -- Visits
app.MapPost("/visit", async (IVisitRepository visitRepository, Visit visit) =>
{
    var persistedVisit = await visitRepository.PostVisit(visit);

    if (persistedVisit != null)
    {
        VisitDTO visitDto = new VisitDTO
        {
            Id = persistedVisit.Id,
            CustomerId = persistedVisit.CustomerId,
            StoreId = persistedVisit.StoreId,
            VisitDate = persistedVisit.VisitDate,
            PointsAccumulated = persistedVisit.PointsAccumulated,

        };
        return Results.Created($"/customer/{visitDto.Id}", visitDto);
    }
    else
    {
        return Results.BadRequest($"Creation Failed");
    }

});

app.MapPost("/visits", async (IVisitRepository visitRepository, List<Visit> visits) =>
{

    var persistedVisit = await visitRepository.PostListOfVisits(visits);

    var storeDtos = persistedVisit.Select(v => new VisitDTO
    {
        Id = v.Id,
        CustomerId = v.CustomerId,
        StoreId = v.StoreId,
        VisitDate = v.VisitDate,
        PointsAccumulated = v.PointsAccumulated,
    }).ToList();

    var firstVisit = persistedVisit.FirstOrDefault();

    return firstVisit != null
    ? Results.Created($"/store/{firstVisit.Id}", firstVisit)
    : Results.BadRequest($"Creation Failed");
});

app.MapGet("/visit/{id}", async (IVisitRepository visitRepository, string id) =>
{
    if (!Guid.TryParse(id, out var parseId))
    {
        return Results.BadRequest("Not a valid GUID.");
    }

    var obtainedVisit = await visitRepository.GetVisit(parseId);

    if (obtainedVisit != null)
    {
        VisitDTO visitDto = new VisitDTO
        {
            Id = obtainedVisit.Id,
            CustomerId = obtainedVisit.CustomerId,
            StoreId = obtainedVisit.StoreId,
            VisitDate = obtainedVisit.VisitDate,
            PointsAccumulated = obtainedVisit.PointsAccumulated,

        };
        return Results.Ok(visitDto);
    }
    else
    {
        return Results.NoContent();
    }

});

app.MapGet("/visit", async (IVisitRepository visitRepository) =>
{

    var listOfVisits = await visitRepository.GetAllVisits();

    var storeDtos = listOfVisits.Select(v => new VisitDTO
    {
        Id = v.Id,
        CustomerId = v.CustomerId,
        StoreId = v.StoreId,
        VisitDate = v.VisitDate,
        PointsAccumulated = v.PointsAccumulated,
    }).ToList();

    return storeDtos.Count != 0 ? Results.Ok(storeDtos) : Results.NoContent();

});

app.MapGet("/visit_by_customer/{id}", async (IVisitRepository visitRepository, string id) =>
{
    if (!Guid.TryParse(id, out var parseId))
    {
        return Results.BadRequest("Not a valid GUID.");
    }

    var obtainedVisit = await visitRepository.GetAllVisitsByCustomerId(parseId);


    var storeDtos = obtainedVisit.Select(v => new VisitDTO
    {
        Id = v.Id,
        CustomerId = v.CustomerId,
        StoreId = v.StoreId,
        VisitDate = v.VisitDate,
        PointsAccumulated = v.PointsAccumulated,
    }).ToList();

    return storeDtos != null
    ? Results.Ok(storeDtos)
    : Results.NotFound($"No visit was found with Id: {id}");
});

app.MapGet("/visit_by_store/{id}", async (IVisitRepository visitRepository, int id) =>
{


    var obtainedVisit = await visitRepository.GetAllVisitsByStoreId(id);

    var storeDtos = obtainedVisit.Select(v => new VisitDTO
    {
        Id = v.Id,
        CustomerId = v.CustomerId,
        StoreId = v.StoreId,
        VisitDate = v.VisitDate,
        PointsAccumulated = v.PointsAccumulated,
    }).ToList();

    return storeDtos != null
    ? Results.Ok(storeDtos)
    : Results.NotFound($"No visit was found with Id: {id}");

});

app.MapDelete("/visit/{id}", async (IVisitRepository visitRepository, string id) =>
{
    if (!Guid.TryParse(id, out var parseId))
    {
        return Results.BadRequest("Not a valid GUID.");
    }

    bool success = await visitRepository.DeleteVisit(parseId);

    return success ? Results.Ok("Deleted successfully") : Results.BadRequest("Failed at deleting");

});

//End Visits

//Start Receipts

//Single receipt with possibility of many items, so... a normal receipt 
//Is it ok to be calling two different methods in the if case?
app.MapPost("/receipt", async (IReceiptService receiptService, Receipt receipt) =>
{

    //logic got out of hand put it in service layer
    ReceiptDTO receiptDTO = await receiptService.SetReceipt(receipt);

    if (receiptDTO != null)
    {
        return Results.Created($"/receipt/{receiptDTO.Id}", receiptDTO);
    }
    else
    {
        return Results.BadRequest($"Creation Failed");
    }
}
);


app.MapGet("/receipt/{id}", async (IReceiptRepository receiptRepository, string id) =>
{
    if (!Guid.TryParse(id, out var parseId))
    {
        return Results.BadRequest("Not a valid GUID.");
    }

    var obtainedReceipt = await receiptRepository.GetReceipt(parseId);
    
    if (obtainedReceipt != null)
    {

        ReceiptDTO receiptDTO = new ReceiptDTO
        {
            Id = obtainedReceipt.Id,
            VisitId = obtainedReceipt.VisitId,
            TotalAmount = obtainedReceipt.TotalAmount
        };
        return Results.Ok(receiptDTO);
    }

    return Results.NotFound($"No visit was found with Id: {id}");

});







app.MapDelete("/receipt/{id}", async (IReceiptRepository receiptRepository, string id) =>
{
    if (!Guid.TryParse(id, out var parseId))
    {
        return Results.BadRequest("Not a valid GUID.");
    }

    bool success = await receiptRepository.DeleteReceipt(parseId);

    return success ? Results.Ok("Deleted successfully") : Results.BadRequest("Failed at deleting");

});
//End Receipts










app.Run();
