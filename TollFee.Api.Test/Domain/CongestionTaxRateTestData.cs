namespace TollFee.Api.Test.Domain;

using Api.Domain;

public class CongestionTaxRateTestData
{
    public static CongestionTaxRate[] CongestionTaxFeeRates =
    {
        new CongestionTaxRate
        {
            FromMinuteOfTheDay = 360,
            ToMinuteOfTheDay = 389,
            Price = 9
        },
        new CongestionTaxRate
        {
            FromMinuteOfTheDay = 390,
            ToMinuteOfTheDay = 419,
            Price = 16
        },
        new CongestionTaxRate
        {
            FromMinuteOfTheDay = 420,
            ToMinuteOfTheDay = 479,
            Price = 22
        },
        new CongestionTaxRate
        {
            FromMinuteOfTheDay = 480,
            ToMinuteOfTheDay = 509,
            Price = 16
        },
        new CongestionTaxRate
        {
            FromMinuteOfTheDay = 510,
            ToMinuteOfTheDay = 899,
            Price = 9
        },
        new CongestionTaxRate
        {
            FromMinuteOfTheDay = 900,
            ToMinuteOfTheDay = 929,
            Price = 16
        },
        new CongestionTaxRate
        {
            FromMinuteOfTheDay = 930,
            ToMinuteOfTheDay = 1019,
            Price = 22
        },
        new CongestionTaxRate
        {
            FromMinuteOfTheDay = 1020,
            ToMinuteOfTheDay = 1079,
            Price = 16
        },
        new CongestionTaxRate
        {
            FromMinuteOfTheDay = 1080,
            ToMinuteOfTheDay = 1109,
            Price = 9
        }
    };

    public static CongestionTaxZeroRate[] CongestionTaxZeroFeeRates =
    {
        new CongestionTaxZeroRate
        {
            ZeroTaxRateDate = new DateOnly(2024, 1, 1)
        },
        new CongestionTaxZeroRate
        {
            ZeroTaxRateDate = new DateOnly(2024, 1, 6)
        },
        new CongestionTaxZeroRate
        {
            ZeroTaxRateDate = new DateOnly(2024, 3, 29)
        },
        new CongestionTaxZeroRate
        {
            ZeroTaxRateDate = new DateOnly(2024, 3, 31)
        },
        new CongestionTaxZeroRate
        {
            ZeroTaxRateDate = new DateOnly(2024, 4, 1)
        },
        new CongestionTaxZeroRate
        {
            ZeroTaxRateDate = new DateOnly(2024, 5, 1)
        },
        new CongestionTaxZeroRate
        {
            ZeroTaxRateDate = new DateOnly(2024, 5, 9)
        },
        new CongestionTaxZeroRate
        {
            ZeroTaxRateDate = new DateOnly(2024, 6, 6)
        },
        new CongestionTaxZeroRate
        {
            ZeroTaxRateDate = new DateOnly(2024, 6, 22)
        },
        new CongestionTaxZeroRate
        {
            ZeroTaxRateDate = new DateOnly(2024, 10, 2)
        },
        new CongestionTaxZeroRate
        {
            ZeroTaxRateDate = new DateOnly(2024, 12, 25)
        },
        new CongestionTaxZeroRate
        {
            ZeroTaxRateDate = new DateOnly(2024, 12, 26)
        }
    };
}