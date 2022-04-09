using System.Runtime.Serialization;

namespace INF27507_Boutique_En_Ligne.Models
{
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public enum UserType
    {
        [EnumMember(Value = "Client")]
        Client,
        [EnumMember(Value = "Seller")]
        Seller
    }
}
