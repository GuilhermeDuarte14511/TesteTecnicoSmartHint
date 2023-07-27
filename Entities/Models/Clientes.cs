namespace TesteSmartHint.Models;

public class Clientes
{

    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? Email { get; set; }
    public string? Telefone { get; set; }
    public DateTime DataCadastro { get; set; }
    public bool Bloqueado { get; set; }
    public string? TipoPessoa { get; set; }
    public string? Documento { get; set; }
    public string? InscricaoEstadual { get; set; }
    public bool? Isento { get; set; }
    public string? Genero { get; set; }
    public DateTime? DataNascimento { get; set; }
    public string? Senha { get; set; }
}
