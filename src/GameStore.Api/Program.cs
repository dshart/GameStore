using GameStore.Api.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation();

var app = builder.Build();

List<Game> games =
[
    new Game {
        Id = Guid.NewGuid(),
        Name = "Donkey Kong",
        Genre = "Arcade",
        Price = 19.99m,
        ReleaseDate = new DateOnly(1982, 7, 15)
    },
    new Game {
        Id = Guid.NewGuid(),
        Name = "Lego Villains",
        Genre = "Family",
        Price = 59.99m,
        ReleaseDate = new DateOnly(2010, 9, 30) },
    new Game {
        Id = Guid.NewGuid(),
        Name = "FIFA 23",
        Genre = "Sports",
        Price = 69.99m,
        ReleaseDate = new DateOnly(2022, 9, 27) }
];

const string GetGameEndpointName = "GetGame";

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

    return Results.NoContent();
});

// DELETE /games/122233-434d-43434....
app.MapDelete("/games/{id}", (Guid id) =>
{
    games.RemoveAll(game => game.Id == id);

    return Results.NoContent();
});

app.Run();
