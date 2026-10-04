using GameStore.Api.Data;
using GameStore.Api.Features.Games;
using GameStore.Api.Features.Genres.GetGenres;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation();

var app = builder.Build();

GameStoreData data = new();

app.MapGames(data);
app.MapGetGenres(data);

app.Run();

