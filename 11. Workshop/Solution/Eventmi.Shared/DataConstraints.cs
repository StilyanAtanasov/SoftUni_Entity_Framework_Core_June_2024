namespace Eventmi.Shared;

public static class DataConstraints
{
    public static class Event
    {
        public const byte NameMaxLength = 50;
    }

    public static class Town
    {
        public const byte NameMinLength = 2;
        public const byte NameMaxLength = 100;
    }
}