using System.ComponentModel;
using GameStore.Api.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation();

var app = builder.Build();

const string GetGameEndpointName = "GetGame";

List<Genre> genres =
[
    new Genre { Id = new Guid("4e179397-c3f1-45ec-a271-c26f07ff64f3"), Name = "Fighting"},
    new Genre { Id = new Guid("b2c3d4e5-f678-90a1-b2c3-d4e5f67890a1"), Name = "Kids and Family" },
    new Genre { Id = new Guid("c3d4e5f6-7890-a1b2-c3d4-e5f67890a1b2"), Name = "Racing" },
    new Genre { Id = new Guid("d4e5f678-90a1-b2c3-d4e5-f67890a1b2c3"), Name = "Roleplaying" },
    new Genre { Id = new Guid("e5f67890-a1b2-c3d4-e5f6-7890a1b2c3d4"), Name = "Sports" }
];

List<Game> games =
[
    new Game {
        Id = Guid.NewGuid(),
        Name = "Donkey Kong",
        Genre = genres[0],
        Price = 19.99m,
        ReleaseDate = new DateOnly(1982, 7, 15),
        Description = "Donkey Kong[a] is a video game series and media franchise created by the Japanese game designer Shigeru Miyamoto for Nintendo. It follows the adventures of Donkey Kong, a large, powerful gorilla, and other members of the Kong family of simians.."
    },
    new Game {
        Id = Guid.NewGuid(),
        Name = "Lego Villains",
        Genre = genres[1],
        Price = 59.99m,
        ReleaseDate = new DateOnly(2010, 9, 30),
        Description = "Smash legos and grab gold bricks as the Joker."
    },
    new Game
    {
        Id = Guid.NewGuid(),
        Name = "FIFA 23",
        Genre = genres[2],
        Price = 69.99m,
        ReleaseDate = new DateOnly(2022, 9, 27),
        Description = "FIFA 23 is a football video game published by EA Sports. It is the 30th and final installment in the FIFA series that is developed by EA Sports"
    }
];

// GET /games
app.MapGet("/games", () => games);

// GET /games/122233-434d-43434....
app.MapGet("/games/{id}", (Guid id) =>
{
    Game? game = games.Find(game => game.Id == id);

    return game is null ? Results.NotFound() : Results.Ok(game);
})
.WithName(GetGameEndpointName);

// POST /games
app.MapPost("/games", (Game game) =>
{
    game.Id = Guid.NewGuid();
    games.Add(game);

    return Results.CreatedAtRoute(
        GetGameEndpointName,
        new { id = game.Id },
        game);
});

// PUT /games/122233-434d-43434....
app.MapPut("/games/{id}", (Guid id, Game updatedGame) =>
{
    var existingGame = games.Find(game => game.Id == id);

    if (existingGame is null)
    {
        return Results.NotFound();
    }

    existingGame.Name = updatedGame.Name;
    existingGame.Genre = updatedGame.Genre;
    existingGame.Price = updatedGame.Price;
    existingGame.ReleaseDate = updatedGame.ReleaseDate;
    existingGame.Description = updatedGame.Description;


    return Results.NoContent();
});

// DELETE /games/122233-434d-43434....
app.MapDelete("/games/{id}", (Guid id) =>
{
    games.RemoveAll(game => game.Id == id);

    return Results.NoContent();
});

app.Run();
