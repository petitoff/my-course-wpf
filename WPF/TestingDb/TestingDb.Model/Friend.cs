
using System.ComponentModel.DataAnnotations;

namespace TestingDb.Model
{
    public class Friend
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
