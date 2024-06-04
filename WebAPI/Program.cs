using DataAccess;
using DataAccess.Auth;
using DataAccess.Brand;
using DataAccess.Category;
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
