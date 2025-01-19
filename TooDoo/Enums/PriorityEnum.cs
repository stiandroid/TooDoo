using System.ComponentModel;

namespace TooDoo.Enums;

public enum PriorityEnum
{
    [Description("0 - Ikke angitt")]    Unset       = 0,
    [Description("1 - Kritisk")]        Critical    = 1,
    [Description("2 - Svært høyt")]     VeryHigh    = 2,
    [Description("3 - Høyt")]           High        = 3,
    [Description("4 - Normalt")]        Normal      = 4,
    [Description("5 - Lavt")]           Low         = 5,
    [Description("6 - Svært lavt")]     VeryLow     = 6
}