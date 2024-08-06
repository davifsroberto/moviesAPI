
using Microsoft.AspNetCore.Mvc;
using FilmesAPI.Models;
using FilmesAPI;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;

namespace Name.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmeController : ControllerBase
{
    private FilmeContext _filmeContext;
    private IMapper _mapper;

    public FilmeController(FilmeContext filmeContext, IMapper mapper)
    {
        _filmeContext = filmeContext;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult AdicionarFilme([FromBody] CreateFilmeDto createFilmeDto)
    {
        Filme filme = _mapper.Map<Filme>(createFilmeDto);

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

    [HttpPut("{id}")]
    public IActionResult AtualizarFilme(int id, [FromBody] UpdateFilmeDto updateFilmeDto)
    {
        var filme = _filmeContext.Filmes.FirstOrDefault(filme => filme.Id == id);

        if (filme == null) return NotFound();

        _mapper.Map(updateFilmeDto, filme);
        _filmeContext.SaveChanges();

        return NoContent();
    }

    [HttpPatch("{id}")]
    public IActionResult AtualizarFilmeParcial(int id, JsonPatchDocument<UpdateFilmeDto> patch)
    {
        var filme = _filmeContext.Filmes.FirstOrDefault(filme => filme.Id == id);

        if (filme == null) return NotFound();

        var filmeParaAtualizar = _mapper.Map<UpdateFilmeDto>(filme);

        patch.ApplyTo(filmeParaAtualizar, ModelState);

        if (!TryValidateModel(filmeParaAtualizar))
        {
            return ValidationProblem(ModelState);
        }

        _mapper.Map(filmeParaAtualizar, filme);
        _filmeContext.SaveChanges();

        return NoContent();
    }

}