using Invoices.Data.Models.Enums;

namespace Invoices.Data;

public static class DataConstraints
{
    // --- Product --- 
    public const byte ProductNameMinLength = 9;
    public const byte ProductNameMaxLength = 30;
    public const string ProductMinPrice = "5.00";
    public const string ProductMaxPrice = "1000.00";
    public const byte ProductMinCategoryType = (byte)CategoryType.ADR;
    public const byte ProductMaxCategoryType = (byte)CategoryType.Tyres;

    // --- Address ---
    public const byte AddressStreetNameMinLength = 10;
    public const byte AddressStreetNameMaxLength = 20;
    public const byte AddressCityMinLength = 5;
    public const byte AddressCityMaxLength = 15;
    public const byte AddressCountryMinLength = 5;
    public const byte AddressCountryMaxLength = 15;

    // --- Invoice ---
    public const int InvoiceMinNumber = 1_000_000_000;
    public const int InvoiceMaxNumber = 1_500_000_000;
    public const byte InvoiceMinCurrencyType = (byte)CurrencyType.BGN;
    public const byte InvoiceMaxCurrencyType = (byte)CurrencyType.USD;

    // --- Client ---
    public const byte ClientNameMinLength = 10;
    public const byte ClientNameMaxLength = 25;
    public const byte ClientNumberVatMinLength = 10;
    public const byte ClientNumberVatMaxLength = 15;
}