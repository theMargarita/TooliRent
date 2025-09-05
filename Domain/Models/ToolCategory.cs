namespace Infrastructure.Models
{
    public class ToolCategory
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        //navigation property
        public List<Tools> Tools { get; set; } = new List<Tools>();
    }
}