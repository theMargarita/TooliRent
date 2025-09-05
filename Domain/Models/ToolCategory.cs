using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models
{
    public class ToolCategory
    {
        [Key]
        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        //navigation property
        public ICollection<Tools> Tools { get; set; } = new List<Tools>();
    }
}