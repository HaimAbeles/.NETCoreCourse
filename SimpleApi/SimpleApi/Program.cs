using SimpleBL.Interfaces;
using SimpleBL.Services;
using SimpleDB.Interfaces;
using SimpleDB.Services;
using SimpleEntites;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<AppSettings>(builder.Configuration);

// Add services to the container.
builder.Services.AddScoped<IIndexBL, IndexBL>();
builder.Services.AddScoped<IUserBL, UserBL>();
builder.Services.AddScoped<IUserDB, UserDB>();
builder.Services.AddScoped<IIndexDB, IndexDB>();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
