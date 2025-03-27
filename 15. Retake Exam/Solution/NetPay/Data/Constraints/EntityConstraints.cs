namespace NetPay.Data.Constraints;

public static class EntityConstraints
{
    public static class Household
    {
        public const byte ContactPersonMinLength = 5;
        public const byte ContactPersonMaxLength = 50;
        public const byte EmailMinLength = 6;
        public const byte EmailMaxLength = 80;
        public const byte PhoneNumberLength = 15;
        public const string PhoneNumberRegex = @"\+\d{3}/\d{3}-\d{6}";
    }

    public static class Expense
    {
        public const byte ExpenseNameMinLength = 5;
        public const byte ExpenseNameMaxLength = 50;
        public const string AmountMinValue = "0.01";
        public const string AmountMaxValue = "100000";
    }

    public static class Service
    {
        public const byte ServiceNameMinLength = 5;
        public const byte ServiceNameMaxLength = 30;
    }

    public static class Supplier
    {
        public const byte SupplierNameMinLength = 3;
        public const byte SupplierNameMaxLength = 60;
    }
}