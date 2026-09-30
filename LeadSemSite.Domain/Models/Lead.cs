namespace LeadSemSite.Domain.Models
{
    public class Lead
    {
        public string Id { get; init; } = default!;
        public string Nome { get; init; } = default!;
        public string? Endereco { get; init; }
        public string? Telefone { get; init; }
        public string? Categoria { get; init; }
        public double? Avaliacao { get; init; }
        public int? TotalAvaliacoes { get; init; }
        public double? Latitude { get; init; }
        public double? Longitude { get; init; }
        public string? Site { get; init; }
        public string? UrlImagemEmpresa { get; set; }
        public bool PossuiSite => !string.IsNullOrWhiteSpace(Site);

    }
}
