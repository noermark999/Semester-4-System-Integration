using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Lek10_Opgave3
{
    internal class CDM
    {
        [JsonPropertyName("airline")]
        public string airline {  get; set; }
        [JsonPropertyName("flightNo")]
        public string flightNo { get; set; }
        [JsonPropertyName("destination")]
        public string destination { get; set; }
        [JsonPropertyName("origin")]
        public string origin { get; set; }
        [JsonPropertyName("arrivalDeparture")]
        public string arrivalDeparture {  get; set; }
        [JsonPropertyName("tidspunkt")]
        public DateTime timeArrival {  get; set; }
        [JsonPropertyName("timeDepature")]
        public DateTime timeDeparture { get; set; }


        public CDM(string airline, string flightNo, string destination, string origin, string arrivalDeparture, DateTime timeArrival, DateTime timeDeparture)
        {
            this.airline = airline;
            this.flightNo = flightNo;
            this.destination = destination;
            this.origin = origin;
            this.arrivalDeparture = arrivalDeparture;
            this.timeArrival = timeArrival;
            this.timeDeparture = timeDeparture;
        }

        public static CDM getModel(string jsonString)
        {
            CDM result;
            result = JsonSerializer.Deserialize<CDM>(jsonString);
            if (result.timeDeparture == null) { 
            
            }
            return result;
        }

        public override string ToString()
        {
            return airline + ", " + flightNo + ", " + destination + ", " + origin + ", " + arrivalDeparture + ", " + timeArrival.ToString() + ", " + timeDeparture.ToString();
        }
    }
}
