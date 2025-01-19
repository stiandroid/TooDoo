using System.ComponentModel;

namespace TooDoo.Enums;

public enum PriorityEnum
{
    [Description("Ikke angitt")]    Unset       = 0,
    [Description("Kritisk")]        Critical    = 1,
    [Description("Svært høyt")]     VeryHigh    = 2,
    [Description("Høyt")]           High        = 3,
    [Description("Normalt")]        Normal      = 4,
    [Description("Lavt")]           Low         = 5,
    [Description("Svært lavt")]     VeryLow     = 6
}