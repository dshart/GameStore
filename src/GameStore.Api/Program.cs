using GameStore.Api.Data;
using GameStore.Api.Features.Games;
using GameStore.Api.Features.Genres;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<GameStoreData>();

builder.Services.AddValidation();

var app = builder.Build();

GameStoreData data = new();

app.MapGames(data);
app.MapGenres(data);

app.Run();

