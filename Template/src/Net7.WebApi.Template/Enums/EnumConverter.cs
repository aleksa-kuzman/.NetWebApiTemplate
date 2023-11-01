using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Net7.WebApi.Template.Enums
{
    public class EnumConverter : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            if (typeToConvert == typeof(EnumerationBase))
            {
                return true;
            }
            return false;
        }

        public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            Type keyType = typeToConvert.GetGenericArguments()[0];

            JsonConverter converter = (JsonConverter)Activator.CreateInstance(typeof(StringEnumConverter<>).MakeGenericType(
                new Type[] { keyType }),
                BindingFlags.Instance | BindingFlags.Public,
                binder: null,
                args: new object[] { options },
                culture: null)!;

            return converter;
        }

        public class StringEnumConverter<Tkey> : JsonConverter<Tkey> where Tkey : EnumerationBase
        {
            public override Tkey Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                string? name = reader.GetString();

                var enumeration = EnumerationBase.GetByName<Tkey>(name);

                return enumeration;
            }

            public override void Write(Utf8JsonWriter writer, Tkey value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(value.Name);
            }
        }
    }
}