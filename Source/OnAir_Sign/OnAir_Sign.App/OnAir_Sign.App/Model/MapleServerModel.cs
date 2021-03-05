using Meadow.Foundation.Maple.Client;

namespace OnAir_Sign.App.Model
{
    public class MapleServerModel : ServerModel
    {
        public string ServerName { get; set; }

        public MapleServerModel(string name, string ipAddress) 
        {
            Name = name;
            IpAddress = ipAddress;
            ServerName = $"{Name} ({IpAddress})";
        }        
    }
}
