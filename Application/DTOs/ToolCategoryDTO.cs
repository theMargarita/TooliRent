namespace Services.DTOs
{
    public record ToolCategoryDTO
    {
        public int CategoryId { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
    }
}
