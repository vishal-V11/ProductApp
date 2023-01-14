using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProductAdminApi.Context;
using ProductAdminApi.Repository;
using ProductAdminApi.Service;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
// Connection
string serverConnectivity = builder.Configuration.GetConnectionString("LocalServerConnection");
builder.Services.AddDbContext<ProductDbContext>(options => options.UseSqlServer(serverConnectivity));
builder.Services.AddCors();
//Token
ValidateTokenWithParameters(builder.Services, builder.Configuration);
void ValidateTokenWithParameters(IServiceCollection services, ConfigurationManager configuration)
{
    var userSecretKey = configuration["JwtvalidationDetails:UserApplicationSecretKey"];
    var userIssuer = configuration["JwtvalidationDetails:UserIssuer"];
    var userAudience = configuration["JwtvalidationDetails:UserAudience"];
    var usersecurityKeyinBytes = Encoding.UTF8.GetBytes(userSecretKey);
    var userSymmetricSecurity = new SymmetricSecurityKey(usersecurityKeyinBytes);
    var tokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidIssuer = userIssuer,

        ValidateAudience = true,
        ValidAudience = userAudience,

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = userSymmetricSecurity,

        ValidateLifetime = true
    };
    builder.Services.AddAuthentication(u =>
    {
        u.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        u.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(u => u.TokenValidationParameters = tokenValidationParameters);
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
app.UseAuthorization();

app.MapControllers();

app.Run();
