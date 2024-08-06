using System.ComponentModel.DataAnnotations;

namespace FilmesAPI;

public class ReadFilmeDto
{
    public int Id { get; set; }

    public string Titulo { get; set; } = string.Empty;

    public string Genero { get; set; } = string.Empty;

    public int Duracao { get; set; }

    public DateTime HoraDaConsulta { get; set; } = DateTime.Now;
}


