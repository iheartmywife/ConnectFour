using Newtonsoft.Json;

namespace ConectFour
{
    public class TupleConverter : JsonConverter<Tuple<int, int>>
    {
        public override Tuple<int, int>? ReadJson(JsonReader reader, Type objectType, Tuple<int, int>? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            reader.Read();
            int item1 = Convert.ToInt32(reader.Value);
            reader.Read();
            int item2 = Convert.ToInt32(reader.Value);
            reader.Read();
            return new Tuple<int, int>(item1, item2);
        }

        public override void WriteJson(JsonWriter writer, Tuple<int, int>? value, JsonSerializer serializer)
        {
            writer.WriteStartArray();
            writer.WriteValue(value.Item1);
            writer.WriteValue(value.Item2);
            writer.WriteEndArray();
        }

        public override bool CanWrite => true;
    }
}
