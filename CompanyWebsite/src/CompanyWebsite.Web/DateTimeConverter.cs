using System.Text.Json;
using System.Text.Json.Serialization;

namespace CompanyWebsite.Web;

public class DateTimeConverter(string format) : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string value = reader.GetString()!;
        return DateTime.ParseExact(value, format, null);
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(format));
    }
}