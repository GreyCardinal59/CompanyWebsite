using System.Text.Json;
using System.Text.Json.Serialization;

namespace CompanyWebsite.Web;

public class DateTimeConverter : JsonConverter<DateTime>
{
    private readonly string[] _formats;
    
    public DateTimeConverter(params string[] formats)
    {
        _formats = formats.Length > 0 ? formats : new[] { "yyyy-MM-dd", "dd.MM.yyyy" };
    }
    
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string value = reader.GetString()!;
        
        foreach (var format in _formats)
        {
            if (DateTime.TryParseExact(value, format, null, System.Globalization.DateTimeStyles.None, out DateTime result))
            {
                return result;
            }
        }
        
        if (DateTime.TryParse(value, out DateTime dateTime))
        {
            return dateTime;
        }
        
        throw new FormatException($"Строка '{value}' не распознана как допустимый формат даты.");
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(_formats[0]));
    }
}