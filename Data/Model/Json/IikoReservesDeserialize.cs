namespace Data.Model.Json;

public class IikoReservesDeserialize
{
    public class ReserveInfo
    {
        public string id { get; set; }
        public string externalNumber { get; set; }
        public string organizationId { get; set; }
        public long timestamp { get; set; }
        public string creationStatus { get; set; }
        public object errorInfo { get; set; }
        public bool isDeleted { get; set; }
        public object reserve { get; set; }
    }

    public class Root
    {
        public string correlationId { get; set; }
        public ReserveInfo reserveInfo { get; set; }
    }


}