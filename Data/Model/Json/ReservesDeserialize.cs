namespace Data.Model.Json;

public class ReservesDeserialize
{
    public class Reserf
    {
        public Guid Id { get; set; }
        public List<string>? TableIds { get; set; }
        public DateTime EstimatedStartTime { get; set; }
        public int DurationInMinutes { get; set; }
        public int GuestsCount { get; set; }
    }
    
    public class Root
    {
        public string correlationId { get; set; }
        public List<Reserf> reserves { get; set; }
    }
}