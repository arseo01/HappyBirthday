namespace backend.Models
{
    public class WishModel
    {
        public string? Name { get; set; }
        public required string Wish { get; set; }
        public byte? Mosha { get; set; }
        public DateTime Viti { get; set; }
    }
}