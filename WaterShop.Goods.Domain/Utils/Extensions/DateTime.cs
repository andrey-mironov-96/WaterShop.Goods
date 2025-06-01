using System.Globalization;

namespace WaterShop.Goods.Domain.Utils.Extensions;

public static class DateTimeExtensions
{
    public static DateTime Parse(string value)
    {
        string[] dateFormats = new[] { "dd.MM.yyyy", "MM.dd.yyyy" };
        CultureInfo provider = new CultureInfo("ru-RU");
        DateTime date = DateTime.ParseExact(value, dateFormats, provider);
        return date;
    }
}