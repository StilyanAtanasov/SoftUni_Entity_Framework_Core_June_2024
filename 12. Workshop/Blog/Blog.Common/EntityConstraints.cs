namespace Blog.Common;

public static class EntityConstraints
{
    public static class Article
    {
        public const byte TitleMinLength = 10;
        public const byte TitleMaxLength = 50;
        public const byte ContentMinLength = 50;
        public const ushort ContentMaxLength = 1500;
    }

    public static class ApplicationUser
    {
        public const byte UserNameMinLength = 5;
        public const byte UserNameMaxLength = 20;
        public const byte EmailMinLength = 10;
        public const byte EmailMaxLength = 50;
        public const byte PasswordMinLength = 5;
        public const byte PasswordMaxLength = 20;
        public const byte HashedPasswordMaxLength = 255;
    }
}