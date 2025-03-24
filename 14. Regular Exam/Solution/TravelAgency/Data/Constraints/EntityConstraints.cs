namespace TravelAgency.Data.Constraints;

public static class EntityConstraints
{
    public static class Customer
    {
        public const byte FullNameMinLength = 4;
        public const byte FullNameMaxLength = 60;
        public const byte EmailMinLength = 6;
        public const byte EmailMaxLength = 50;
        public const byte PhoneNumberLength = 13;
        public const string PhoneNumberRegex = @"\+\d{12}";
    }

    public static class Guide
    {
        public const byte FullNameMinLength = 4;
        public const byte FullNameMaxLength = 60;
    }

    public static class TourPackage
    {
        public const byte PackageNameMinLength = 2;
        public const byte PackageNameMaxLength = 40;
        public const byte DescriptionMaxLength = 200;
    }
}