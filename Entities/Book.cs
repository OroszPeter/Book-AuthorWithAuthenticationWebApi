using System.Text.Json.Serialization;

namespace Book_AuthorWIthAuthenticationWebApi.Entities
{
    public class Book
    {
        public int Id { get; set; } 
        public string Title { get; set; } = string.Empty; 
        public DateTime PublishedDate { get; set; } 
        public int AuthorId { get; set; }

        [JsonIgnore]
        public Author Author { get; set; } 
    }
}
