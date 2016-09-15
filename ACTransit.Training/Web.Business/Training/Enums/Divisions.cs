using System.ComponentModel;

namespace ACTransit.Training.Web.Business.Training.Enums
{
    public enum Divisions
    {
        CMF = 8,
        D2 = 2,
        D4 = 1,
        D6 = 4,
        TED = 7,
        [Description("TED/DIV")]
        TEDDIV = 10,
        ALL = 256,
        [Description("?")]
        Unknown = 0
    }
}
