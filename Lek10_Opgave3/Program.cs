using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace Lek10_Opgave3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AirlineCompanySAS airline = new AirlineCompanySAS("SAS", "SK 239", "JFK", "CPH", "D", new DateTime(2017, 3, 6), new DateTime(2017, 3, 6, 16, 45, 00));
            string json = JsonSerializer.Serialize(airline);
            Console.WriteLine(json);
            CDM cdm = CDM.getModel(json);
            Console.WriteLine(cdm);
            Console.ReadLine();
        }
    }
    public class AirlineCompanySAS
    {
        public String airline { get; set; }             //SAS
        public String flightNo { get; set; }            //SK 239
        public String destination { get; set; }      //JFK
        public String origin { get; set; }              //CPH
        public String arrivalDeparture { get; set; }     //D
        public DateTime dato { get; set; }              //6. marts 2017
        public DateTime tidspunkt { get; set; }         //16:45

        public AirlineCompanySAS(string airline, string flightNo, string destination, string origin, string arrivalDeparture, DateTime dato, DateTime tidspunkt)
        {
            this.airline = airline;
            this.flightNo = flightNo;
            this.destination = destination;
            this.origin = origin;
            this.arrivalDeparture = arrivalDeparture;
            this.dato = dato;
            this.tidspunkt = tidspunkt;
        }
    }
}
