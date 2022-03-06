using KanjiDesu.DataModels;
using KanjiDesu.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IKanaService, KanaService>();

builder.Services.AddControllers().AddJsonOptions(opt => opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddDbContext<KanjiDesuContext>(opt => opt.UseSqlServer("Server=tcp:kanjidesuserver.database.windows.net,1433;Initial Catalog=kanjidesudb;Persist Security Info=False;User ID=kanjidesuadmin;Password=kanjidesustrongpassw0Rd;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"));
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("1.0.0", new() { Title = "KanjiDesu API", Version = "1.0.0", Description = "ASP.NET Core Web API for KanjiDesu" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();