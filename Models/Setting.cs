using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd_Camping.Models
{
    [Table("Settings")]
    public class Setting : BaseModel
    {
        [Key]
        public int SET_ID { get; set; }
        public required string Name { get; set; }
        public string? Value { get; set; }
        public string? Description { get; set; }
        public string? DataType { get; set; }
        public bool IsEditable { get; set; }
    }
}
