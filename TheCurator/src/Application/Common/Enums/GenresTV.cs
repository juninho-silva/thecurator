using System.ComponentModel;

namespace Application.Common.Enums
{
    public enum GenresTV
    {
        [Description("Ação e Aventura")]
        ActionAdventure = 10759,
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
        [Description("Crianças")]
        Kids = 10762,
        [Description("Mistério")]
        Mystery = 9648,
        [Description("Notícias")]
        News = 10763,
        [Description("Realidade")]
        Reality = 10764,
        [Description("Ficção Científica & Fantasia")]
        SciFiFantasy = 10765,
        [Description("Novela")]
        Soap = 10766,
        [Description("Talk Show")]
        Talk = 10767,
        [Description("Guerra & Política")]
        WarPolitics = 10768,
        [Description("Western")]
        Western = 37
    }
}