using System.Text.Json;
using System.Text.Json.Serialization;

namespace MovieApp.BL.Utilities.Helpers;
public class DateOnlyJsonConverter : JsonConverter<DateOnly>
{
    private const string DateFormat = "yyyy-MM-dd";

    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            var str = reader.GetString();
            if (DateOnly.TryParseExact(str, DateFormat, null, System.Globalization.DateTimeStyles.None, out var date))
                return date;
            throw new JsonException($"Geçersiz tarih formatı. Beklenen format: {DateFormat}");
        }
        else if (reader.TokenType == JsonTokenType.StartObject)
        {
            int year = 0, month = 0, day = 0;
            while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
            {
                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    string propertyName = reader.GetString();
                    reader.Read(); 
                    switch (propertyName)
                    {
                        case "year":
                            year = reader.GetInt32();
                            break;
                        case "month":
                            month = reader.GetInt32();
                            break;
                        case "day":
                            day = reader.GetInt32();
                            break;
                        default:
                            reader.Skip();
                            break;
                    }
                }
            }
            return new DateOnly(year, month, day);
        }

        throw new JsonException($"Tarih dönüşümü için beklenmeyen token türü: {reader.TokenType}");
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(DateFormat));
    }
}

