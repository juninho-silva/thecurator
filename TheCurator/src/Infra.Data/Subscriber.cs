using System.ComponentModel.DataAnnotations.Schema;

namespace Infra.Data
{
    public class Subscriber
    {
        [Column("id")]
        public Guid Id { get; set; }
        [Column("name")]
        public string Name { get; set; } = string.Empty;
        [Column("email")]
        public string Email { get; set; } = string.Empty;
        [Column("unsubscribe_token")]
        public string UnsubscribeToken { get; set; } = string.Empty;
        [Column("genre_movies")]
        public List<string> GenreMovies { get; set; } = new();
        [Column("genre_tv")]
        public List<string> GenreTVs { get; set; } = new();
        [Column("active")]
        public bool Active { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}
