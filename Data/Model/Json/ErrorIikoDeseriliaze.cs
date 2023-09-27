namespace Data.Model.Json;

public class ErrorIikoDeseriliaze
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Root
    {
        public string correlationId { get; set; }
        public string errorDescription { get; set; }
        public string error { get; set; }
    }


}