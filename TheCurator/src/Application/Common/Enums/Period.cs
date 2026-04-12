using System.ComponentModel;

namespace Application.Common.Enums
{
    public enum Period
    {
        [Description("Diariamente")]
        Daily,
        [Description("Final de semana")]
        Weekend,
        [Description("Segunda-feira")]
        Monday,
        [Description("Terça-feira")]
        Tuesday,
        [Description("Quarta-feira")]
        Wednesday,
        [Description("Quinta-feira")]
        Thursday,
        [Description("Sexta-feira")]
        Friday
    }
}