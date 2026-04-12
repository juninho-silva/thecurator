using System.ComponentModel;

namespace Application.Common.Enums
{
    public enum GenresMovie
    {
        [Description("Ação")]
        Action = 28,
        [Description("Aventura")]
        Adventure = 12,
        [Description("Animação")]
        Animation = 16,
        [Description("Comédia")]
        Comedy = 35,
        [Description("Crime")]
        Crime = 80,
        [Description("Documentário")]
        Documentary = 99,
        [Description("Drama")]
        Drama = 18,
        [Description("Família")]
        Family = 10751,
        [Description("Fantasia")]
        Fantasy = 14,
        [Description("História")]
        History = 36,
        [Description("Horror")]
        Horror = 27,
        [Description("Música")]
        Music = 10402,
        [Description("Mistério")]
        Mystery = 9648,
        [Description("Romance")]
        Romance = 10749,
        [Description("Ficção Científica")]
        ScienceFiction = 878,
        [Description("Filme TV")]
        TVMovie = 10770,
        [Description("Thriller")]
        Thriller = 53,
        [Description("Filme de Guerra")]
        War = 10752,
        [Description("Western")]
        Western = 37
    }
}