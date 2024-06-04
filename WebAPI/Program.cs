using DataAccess;
using DataAccess.Auth;
using DataAccess.Basket;
using DataAccess.BasketProduct;
using DataAccess.Brand;
using DataAccess.Category;
using DataAccess.Customer;
using DataAccess.CustomerOrder;
using DataAccess.CustomerOrderDetail;
using DataAccess.CustomerProductFavorite;
using DataAccess.DeliveryAddress;
using DataAccess.Email;
using DataAccess.OrderManagement;
using DataAccess.Product;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure dependency injection
builder.Services.AddSingleton<OracleDbContext>(sp =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    return new OracleDbContext(connectionString);
});

// Configure dependency injection for AuthManagementRepository
builder.Services.AddScoped<AuthManagementRepository>(sp =>
{
    var dbContext = sp.GetRequiredService<OracleDbContext>();
    return new AuthManagementRepository(dbContext);
});

// Configure dependency injection for ProductRepository
builder.Services.AddScoped<ProductRepository>(sp =>
{
    var dbContext = sp.GetRequiredService<OracleDbContext>();
    return new ProductRepository(dbContext);
});

// Configure dependency injection for CategoryRepository
builder.Services.AddScoped<CategoryRepository>(sp =>
{
    var dbContext = sp.GetRequiredService<OracleDbContext>();
    return new CategoryRepository(dbContext);
});

// Configure dependency injection for BrandRepository
builder.Services.AddScoped<BrandRepository>(sp =>
{
    var dbContext = sp.GetRequiredService<OracleDbContext>();
    return new BrandRepository(dbContext);
});

// Configure dependency injection for EmailRepository
builder.Services.AddScoped<EmailRepository>(sp =>
{
    var dbContext = sp.GetRequiredService<OracleDbContext>();
    return new EmailRepository(dbContext);
});

// Configure dependency injection for CustomerRepository
builder.Services.AddScoped<CustomerRepository>(sp =>
{
    var dbContext = sp.GetRequiredService<OracleDbContext>();
    return new CustomerRepository(dbContext);
});

// Configure dependency injection for CustomerRepository
builder.Services.AddScoped<BasketProductRepository>(sp =>
{
    var dbContext = sp.GetRequiredService<OracleDbContext>();
    return new BasketProductRepository(dbContext);
});

// Configure dependency injection for BasketRepository
builder.Services.AddScoped<BasketRepository>(sp =>
{
    var dbContext = sp.GetRequiredService<OracleDbContext>();
    return new BasketRepository(dbContext);
});

// Configure dependency injection for BasketRepository
builder.Services.AddScoped<DeliveryAddressRepository>(sp =>
{
    var dbContext = sp.GetRequiredService<OracleDbContext>();
    return new DeliveryAddressRepository(dbContext);
});

// Configure dependency injection for OrderManagementRepository
builder.Services.AddScoped<OrderManagementRepository>(sp =>
{
    var dbContext = sp.GetRequiredService<OracleDbContext>();
    return new OrderManagementRepository(dbContext);
});

// Configure dependency injection for CustomerOrderRepository
builder.Services.AddScoped<CustomerOrderRepository>(sp =>
{
    var dbContext = sp.GetRequiredService<OracleDbContext>();
    return new CustomerOrderRepository(dbContext);
});

// Configure dependency injection for CustomerOrderDetailRepository
builder.Services.AddScoped<CustomerOrderDetailRepository>(sp =>
{
    var dbContext = sp.GetRequiredService<OracleDbContext>();
    return new CustomerOrderDetailRepository(dbContext);
});

// Configure dependency injection for CustomerOrderDetailRepository
builder.Services.AddScoped<CustomerProductFavoriteRepository>(sp =>
{
    var dbContext = sp.GetRequiredService<OracleDbContext>();
    return new CustomerProductFavoriteRepository(dbContext);
});


// CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:3000") 
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// CORS middleware'ini kullan
app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
