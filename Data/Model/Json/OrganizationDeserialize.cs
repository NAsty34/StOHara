namespace Data.Model.Json;

public class OrganizationDeserialize
{
    public class Organization
    {
        public string responseType { get; set; }
        public string country { get; set; }
        public string restaurantAddress { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public bool useUaeAddressingSystem { get; set; }
        public string version { get; set; }
        public string currencyIsoName { get; set; }
        public double currencyMinimumDenomination { get; set; }
        public string countryPhoneCode { get; set; }
        public bool marketingSourceRequiredInDelivery { get; set; }
        public string defaultDeliveryCityId { get; set; }
        public object deliveryCityIds { get; set; }
        public string deliveryServiceType { get; set; }
        public string defaultCallCenterPaymentTypeId { get; set; }
        public bool orderItemCommentEnabled { get; set; }
        public string inn { get; set; }
        public string addressFormatType { get; set; }
        public bool isConfirmationEnabled { get; set; }
        public int confirmAllowedIntervalInMinutes { get; set; }
        public bool isCloud { get; set; }
        public bool isAnonymousGuestsAllowed { get; set; }
        public List<object> addressLookup { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
    }

    public class Root
    {
        public string correlationId { get; set; }
        public List<Organization> organizations { get; set; }
    }


}