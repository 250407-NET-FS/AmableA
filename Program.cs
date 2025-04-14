using Project1.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Text;
using Project1.Repositories;



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




app.MapPost("/customer", async (ICustomerRepository customerRepository , Customer customer) => {

    var persistedCustomer = await customerRepository.PostCustomer(customer);

    return Results.Created($"/customer/{persistedCustomer.Id}", persistedCustomer);
});



app.MapGet("/customer/{id}", async (ICustomerRepository customerRepository, string id) =>
{   

    //https://learn.microsoft.com/en-us/dotnet/api/system.guid.tryparse?view=net-9.0
    if (!Guid.TryParse(id, out var parseId)){
        return Results.BadRequest("Not a valid GUID.");
    }
    
    var obtainedCustomer = await customerRepository.GetCustomer(parseId);
    
    if (obtainedCustomer is null){
        return Results.NotFound($"No customer was found with Id: {id}");
    }
    
    return Results.Ok(obtainedCustomer);


});













app.Run();
