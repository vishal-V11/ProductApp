using BookingAPI.Repository;
using BookingAPI.Services;
using BookingAPI.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
string serverConnectivity = builder.Configuration.GetConnectionString("LocalServerConnection");
builder.Services.AddDbContext<BookingDbContext>(options => options.UseSqlServer(serverConnectivity));
builder.Services.AddCors();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IBookingService, BookingService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/
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
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());



app.UseHttpsRedirection();





app.UseAuthorization();

app.MapControllers();

app.Run();
