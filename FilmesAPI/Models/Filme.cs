using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Models;

public class Filme
{
    [Required(ErrorMessage = "O título do filme é obrigatório.")]
    public string Titulo { get; set; } = string.Empty;

    [Required(ErrorMessage = "O gênero é obrigatório.")]
    [MaxLength(ErrorMessage = "O tamanho do gênero não pode exceder 50 casracteres.")]
    public string Genero { get; set; } = string.Empty;

    [Required(ErrorMessage = "A duração é obrigatória.")]
    [Range(70, 600, ErrorMessage = "A duração deve ser entre 70 e 600 minutos")]
    public int Duracao { get; set; }

}