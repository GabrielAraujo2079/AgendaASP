using System.ComponentModel.DataAnnotations;

public class Agenda
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Nome é obrigatório")]
    [MaxLength(100, ErrorMessage = "Nome pode ter no máximo 100 caracteres")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "Telefone é obrigatório")]
    [MaxLength(20, ErrorMessage = "Telefone pode ter no máximo 20 caracteres")]
    public string Telefone { get; set; }
}
