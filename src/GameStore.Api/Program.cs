using System.ComponentModel.DataAnnotations;
using GameStore.Api.Models;
using GameStore.Api.Data;
using GameStore.Api.Features.Games.GetGames;
using GameStore.Api.Features.Games.GetGameById;
using GameStore.Api.Features.Games.CreateGame;
using GameStore.Api.Features.Games.UpdateGame;
using GameStore.Api.Features.Games.DeleteGame;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation();

var app = builder.Build();

GameStoreData data = new();

app.MapGetGames(data);
app.MapGetGameById(data);
app.MapCreateGame(data);
app.MapUpdateGame(data);
app.MapDeleteGame(data);


// GET /genres
app.MapGet("/genres", () =>
    data.GetGenres()
            .Select(genre => new GenreDto(genre.Id, genre.Name)));

app.Run();


public record UpdateGameDto(
    [Required][StringLength(50)] string Name,
    Guid GenreId,
    [Range(1, 100)] decimal Price,
    DateOnly ReleaseDate,
    [Required][StringLength(500)] string Description
);

public record GenreDto(Guid Id, string Name);
