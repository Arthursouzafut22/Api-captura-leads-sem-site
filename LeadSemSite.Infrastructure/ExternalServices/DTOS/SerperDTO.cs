namespace LeadSemSite.Infrastructure.ExternalServices.DTOS
{
    internal record SerperMapsResponse(List<SerperPlace>? Places);
    internal record SerperPlace(
        string? Title, string? Address, double? Latitude, double? Longitude,
        double? Rating, int? RatingCount, string? Type,
        string? PhoneNumber, string? Website, string? Cid, string? PlaceId, string? thumbnailUrl);
}
