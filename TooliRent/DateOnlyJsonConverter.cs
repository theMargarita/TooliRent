using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TooliRent.WebAPI
{

    public class DateOnlyJsonConverter : JsonConverter<DateOnly>
    {
        private readonly string _format;

        public DateOnlyJsonConverter(string? format = null)
        {
            _format = format ?? "yyyy-MM-dd"; //standard format
        }

        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();

            if (DateOnly.TryParse(value, out var date))
            {
                return date;
            }

            throw new JsonException($"Unable to convert \"{value}\" to DateOnly. \nExepted format: {_format}");
        }

        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(_format));
        }
    }
}
