using InfrastructureManagement.Api.Authentication;
using InfrastructureManagement.Api.Middlewares;
using InfrastructureManagement.Core;
using InfrastructureManagement.Core.Interfaces.IRepositories;
using InfrastructureManagement.Infrastructure;
using InfrastructureManagement.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


builder.Services.AddScoped(typeof(IJwtAuthenticationManager), typeof(JwtAuthenticationManager));

builder.Services.AddTransient<CustomBearer>();

//builder.Services.AddDbContext<DatabaseContext>(options =>
//{
//    options.UseMySQL(builder.Configuration.GetConnectionString("MySqlDev"));
//});

//builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
//builder.Services.AddScoped(typeof(IAccountRepository), typeof(AccountRepository));
//builder.Services.AddScoped(typeof(ITokenAccountRepository), typeof(TokenAccountRepository));

//builder.Services.AddAuthentication(x =>
//{
//    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(x =>
//{
//    x.RequireHttpsMetadata = false;
//    x.SaveToken = true;
//    x.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuerSigningKey = true,
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetConnectionString("Key"))),
//        ValidateIssuer = false,
//        ValidateAudience = false
//    };
//    x.EventsType = typeof(CustomBearer);
//});



builder.Services.AddCors(options => options.AddPolicy("CorsPolicy",
        builder =>
        {
            builder.AllowAnyHeader()
                   .AllowAnyMethod()
                   .SetIsOriginAllowed((host) => true)
                   .AllowCredentials();
        }));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ExceptionHandlingMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("CorsPolicy");

//app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
