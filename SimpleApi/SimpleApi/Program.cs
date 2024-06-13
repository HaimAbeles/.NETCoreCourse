using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SimpleApi;
using SimpleApi.Hubs;
using SimpleApi.Middleware;
using SimpleBL.Interfaces;
using SimpleBL.Services;
using SimpleDB;
using SimpleDB.EF.Contexts;
using SimpleDB.Interfaces;
using SimpleDB.Services;
using SimpleEntites;
using System.Text;

var builder = WebApplication
    .CreateBuilder(args);


builder.UseSerilog();

builder.Services.Configure<AppSettings>(builder.Configuration);

AppSettings appSettings = builder.Configuration.Get<AppSettings>();

// Add services to the container.
builder.Services.AddScoped<IIndexBL, IndexBL>();
builder.Services.AddScoped<IUserBL, UserBL>();
builder.Services.AddScoped<IUsersBL, UsersBL>();
builder.Services.AddScoped<IHomeBL, HomeBL>();
builder.Services.AddScoped<IUserDB, UserDB>();
builder.Services.AddScoped<IRestApiGW, RestApiGW>();
builder.Services.AddScoped<IUsersDB, UsersDB>();
builder.Services.AddScoped<IIndexDB, IndexDB>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.AddSession();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddMemoryCache();
builder.Services.AddAutoMapper(typeof(MapperManager));
builder.Services.AddSignalR();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = appSettings.Jwt.Issuer,
            ValidAudience = appSettings.Jwt.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.Jwt.SecretKey)),
            ClockSkew = TimeSpan.Zero
        };
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                context.Token = context.Request.Cookies[CookiesKeys.AccessToken];
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.WithOrigins("http://localhost:3000")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        );
});

builder.Services.AddDbContext<SimpleContext>(options =>
{
    options.UseSqlServer(appSettings.ConnectionStrings.Simple);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors("CorsPolicy");

app.UseMyCustomMiddleware();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();
app.MapControllers();
app.MapHub<ChatHub>("/chatHub");

app.Run();
