namespace LeadSemSite.Application.DTOS
{
    public record LeadDto(
    string Id, string Nome, string? Endereco, string? Telefone, string? Categoria,
    double? Avaliacao, int? TotalAvaliacoes, string? UrlImagemEmpresa, double? Latitude, double? Longitude, bool PossuiSite);

    public record LeadsResponse(
    string Cidade, string Termo, int Pagina, bool TemMaisPaginas,
    int Total, IEnumerable<LeadDto> Leads);
}
