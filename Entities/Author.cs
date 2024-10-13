using System.Text.Json.Serialization;

namespace Book_AuthorWIthAuthenticationWebApi.Entities
{
    public class Author
    {
        public int Id { get; set; } 
        public string Name { get; set; } = string.Empty; 
        public DateTime BirthDate { get; set; }

        [JsonIgnore]
        public ICollection<Book> Books { get; set; } 
    }
}
