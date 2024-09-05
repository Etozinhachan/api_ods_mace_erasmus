using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using api_ods_mace_erasmus.data;
using api_ods_mace_erasmus.Identity;
using api_ods_mace_erasmus.Interfaces;
using api_ods_mace_erasmus.Repositories;
using api_ods_mace_erasmus.helper;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.

//var root = Directory.GetParent(Directory.GetCurrentDirectory());

//var dotenv = Path.Combine(root?.ToString()!, "/etc/secrets/.env");

//var dotenv = Path.Combine(root, "etc/secrets/.env");

//Console.WriteLine(dotenv);

//Console.WriteLine(dotenv);

//Environment.SetEnvironmentVariable("JWT_Key", "rawr");

DotEnv.Load("/etc/secrets/.env");

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        //ValidIssuer = config["JwtSettings:Issuer"],
        //ValidAudience = config["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_Key")!)),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(IdentityData.AdminUserPolicyName, p =>
        p.RequireClaim(IdentityData.AdminUserClaimName, "True"));
});

builder.Services.AddControllers();
builder.Services.AddScoped<IActivityRepository, ActivityRepository>();
builder.Services.AddScoped<ITranslationRepository, TranslationRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//var dbConnectionString = "server=" + Environment.GetEnvironmentVariable("Database_Server") + ";database=" + Environment.GetEnvironmentVariable("Database_Name") + ";user=" + Environment.GetEnvironmentVariable("Database_User") + ";password=" + Environment.GetEnvironmentVariable("Database_Password") + ";";

var dbConnectionString = "server=erasmus-esl.pt; database=erasmus3_api_ods_mace_erasmus; user=erasmus3_api_ods_mace_erasmus; password=hwyE1V2by75z; default command timeout=60;";

//var dbConnectionString = config.GetConnectionString("Default");

Console.WriteLine("BBBBBBBBBBBBB " + dbConnectionString);

builder.Services.AddDbContext<DbDataContext>(opt => /*opt.UseInMemoryDatabase(
    
    "db_potente")*/opt.UseMySql(
        dbConnectionString, ServerVersion.AutoDetect(dbConnectionString)
    )

    );
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


// SWAGGER THINGS


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please provide a valid token.",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
            },
            Array.Empty<string>()
        }
    });

    /*
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme{
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>{
        {"Bearer", Enumerable.Empty<string>()},
    });*/
});



var app = builder.Build();

// MORE SWAGGER
// Configure the HTTP request pipeline.

//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.RoutePrefix = "swagger";
});
//}


app.UseHttpsRedirection();


app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
