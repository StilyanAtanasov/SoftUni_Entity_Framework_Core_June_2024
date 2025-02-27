using Boardgames.Data.Models.Enums;

namespace Boardgames.Data;

public static class DataConstraints
{
    // --- Boardgame ---
    public const byte BoardGameNameMinLength = 10;
    public const byte BoardGameNameMaxLength = 20;
    public const double BoardGameMinRating = 1.00d;
    public const double BoardGameMaxRating = 10.00d;
    public const short BoardGameMinYearPublished = 2018;
    public const short BoardGameMaxYearPublished = 2023;
    public const byte BoardGameMinCategoryType = (byte)CategoryType.Abstract;
    public const byte BoardGameMaxCategoryType = (byte)CategoryType.Strategy;

    // --- Seller ---
    public const byte SellerNameMinLength = 5;
    public const byte SellerNameMaxLength = 20;
    public const byte SellerAddressMinLength = 2;
    public const byte SellerAddressMaxLength = 30;
    public const string SellerWebsitePattern = @"www\.[A-Za-z0-9-]+\.com";

    // --- Creator ---
    public const byte CreatorFirstNameMinLength = 2;
    public const byte CreatorFirstNameMaxLength = 7;
    public const byte CreatorLastNameMinLength = 2;
    public const byte CreatorLastNameMaxLength = 7;
}