using GameStore.Api.Data;
using GameStore.Api.Features.Games;
using GameStore.Api.Features.Genres.GetGenres;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<GameStoreData>();

builder.Services.AddValidation();

var app = builder.Build();

app.MapGames();
app.MapGetGenres();

app.Run();

