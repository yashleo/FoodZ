using Foodz.API.Data;
using Foodz.API.Interfaces;
using Foodz.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using AutoMapper;
using Foodz.API.Profiles;

var builder = WebApplication.CreateBuilder(args);

// ✅ 1. Add DB Context
builder.Services.AddDbContext<FoodZDbContext>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnectionString"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnectionString")));
}
);

// ✅ 2. Add AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

// ✅ 3. Register Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMenuItemService, MenuItemService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IDeliveryPersonnelService, DeliveryPersonnelService>();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

// ✅ 4. Add Controllers
builder.Services.AddControllers();

// ✅ 5. Configure JWT Authentication
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings.GetValue<string>("SecretKey");

// Add configuration sources if needed (default loads appsettings.json)

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
            )
        };
    });


// ✅ 6. Swagger with JWT Support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "FoodZ API", Version = "v1" });

    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        Description = "Enter JWT token in the format: Bearer {your token}",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });
});

var app = builder.Build();

// ✅ Apply Migrations Automatically (Optional in Dev)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<FoodZDbContext>();
    db.Database.Migrate();
    _ = DbSeeder.SeedAsync(db); // If you have a seeder method
}

// ✅ Use Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ✅ Enable Middleware Pipeline
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
