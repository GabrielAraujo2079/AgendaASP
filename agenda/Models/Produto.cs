using System.ComponentModel.DataAnnotations;

public class Produto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Nome é obrigatório")]
    [MaxLength(100, ErrorMessage = "Nome pode ter no máximo 100 caracteres")]
    public string Nome { get; set; }

    [Range(0.01, 99999.99, ErrorMessage = "Preço deve ser entre 0,01 e 99.999,99")]
    public decimal Preco { get; set; }

    [Range(0, 9999, ErrorMessage = "Quantidade deve ser entre 0 e 9999")]
    public int Quantidade { get; set; }
}