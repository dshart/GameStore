using GameStore.Api.Data;
using GameStore.Api.Features.Games;
using GameStore.Api.Features.Genres;

var builder = WebApplication.CreateBuilder(args);

var connString = "Data Source=GameStore.db";

builder.Services.AddSqlite<GameStoreContext>(connString);
builder.Services.AddSingleton<GameStoreData>();
builder.Services.AddTransient<GameDataLogger>();

builder.Services.AddValidation();

var app = builder.Build();

app.MapGames();
app.MapGenres();

app.Run();

