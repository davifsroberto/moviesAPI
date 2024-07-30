
using Microsoft.AspNetCore.Mvc;
using FilmesAPI.Models;
using FilmesAPI;

namespace Name.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmeController : ControllerBase
{
    private FilmeContext _filmeContext;

    public FilmeController(FilmeContext filmeContext)
    {
        _filmeContext = filmeContext;
    }

    [HttpPost]
    public IActionResult AdicionarFilme([FromBody] Filme filme)
    {
        _filmeContext.Filmes.Add(filme);
        _filmeContext.SaveChanges();

        return CreatedAtAction(nameof(RecuperarFilmePorId), new { id = filme.Id }, filme);
    }

    [HttpGet]
    public IEnumerable<Filme> RecuperarFilmes([FromQuery] int skip = 0, [FromQuery] int take = 50)
    {
        return _filmeContext.Filmes.Skip(skip).Take(take);
    }

    [HttpGet("{id}")]
    public IActionResult RecuperarFilmePorId(int id)
    {
        var filme = _filmeContext.Filmes.FirstOrDefault(filme => filme.Id == id);

        if (filme == null) return NotFound();

        return Ok(filme);
    }
}