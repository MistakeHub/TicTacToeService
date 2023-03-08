using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using TTTService.Domain.Entities;
using TTTService.Domain.Interfaces;
using TTTService.infrastructure.Data;
using TTTService.infrastructure.InterfaceImpl;
using TTTService.HttpApi.Helpers.JWThelper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services=builder.Services;

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddControllers().AddNewtonsoftJson(options =>
 options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddDbContext<TttserviceContext>();
services.AddTransient<IBaseRepository<Game>, BaseRepository<Game>>();
services.AddTransient<IBaseRepository<User>, BaseRepository<User>>();
services.AddTransient<IBaseRepository<Move>, BaseRepository<Move>>();
services.AddTransient<IUserRepository, UserRepository>();
services.AddTransient<IGameMoveRepository, GameMoveRepository>();
services.AddSingleton<GameLobbyRepository>();
services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,

        ValidIssuer = AuthOption.ISSUER,


        ValidateAudience = true,

        ValidAudience = AuthOption.AUDIENCE,

        ValidateLifetime = true,


        IssuerSigningKey = AuthOption.GetSymmetricSecurityKey(),

        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,
    };
});
services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
