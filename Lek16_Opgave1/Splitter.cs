using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MYFirstMSMQ
{
    internal class Splitter
    {
        protected MessageQueue inQueue;
        MessageQueueTransaction msgTx;
        public Splitter(MessageQueue inQueue)
        {
            this.inQueue = inQueue;

            inQueue.ReceiveCompleted += new ReceiveCompletedEventHandler(OnMessage);
            inQueue.BeginReceive();
            msgTx = new MessageQueueTransaction();
            string label = inQueue.Label;

        }
        private void OnMessage(Object source, ReceiveCompletedEventArgs asyncResult)
        {
            msgTx.Begin();
            MessageQueue mq = (MessageQueue)source;
            try
            {

                // Ved ikke hvad alt det her er men tror det opretter et xml objekt med det som er blevet sendt i messagequeue
                // Og den finder automatisk ud af der er blevet sendt en besked
                Message message = mq.EndReceive(asyncResult.AsyncResult);
                string label = message.Label;
                XmlDocument xml = new XmlDocument();
                string XMLDocument = null;
                Console.WriteLine(label);
                Stream body = message.BodyStream;
                StreamReader reader = new StreamReader(body);
                XMLDocument = reader.ReadToEnd().ToString();
                xml.LoadXml(XMLDocument);
                XmlNode itemNode = xml.SelectSingleNode("/FlightDetailsInfoResponse");

                //string corId = lav noget uuid og sæt det som correlation id på alle messages. gem dem så i en map i resequenceren med id som key og en liste af messages
                // Hvis den skal udvides med dette

                if (itemNode != null)
                {
                    // finder bare flightnumber og gemmer det i en variable flightNo
                    string flightNo = null;
                    XmlNode flightNoNode = itemNode.SelectSingleNode("Flight");
                    if (flightNoNode != null)
                    {
                        flightNo = flightNoNode.Attributes["number"].Value;
                    }

                    // Finder passenger noden og sender den med metoden passengerQueueSendMessage
                    XmlNode valuePassenger = itemNode.SelectSingleNode("Passenger");
                    if (valuePassenger != null)
                    {
                        Console.WriteLine("Sendt passenger videre");
                        passengerQueueSendMessage(valuePassenger, flightNo, message.Id);
                    }

                    // Finder alle luggage nodes og sender dem med LuggageQueueSendMessage hvor der også bliver sendt sekvensnummer og antallet af luggage
                    XmlNodeList valueLuggage = itemNode.SelectNodes("Luggage");
                    int seqNumber = 1;
                    foreach (XmlNode node in valueLuggage)
                    {
                        luggageQueueSendMessage(node, flightNo, seqNumber, valueLuggage.Count, message.Id);
                        //throw new FileNotFoundException("det fuckd");
                        seqNumber--;
                    }
                }
                Console.WriteLine("Besked sendt");
                msgTx.Commit();
                mq.BeginReceive();
            } catch (Exception ex) 
            { 
                Console.WriteLine(ex.Message); 
                msgTx.Abort();
            } finally
            {
                mq.Close();
            }

        }

        private void passengerQueueSendMessage(XmlNode xmlMessage, string flightNo, string id)
        {
            MessageQueue messageQueue = null;
            if (MessageQueue.Exists(@".\Private$\AirportCheckInPassengerTrans"))
            {
                messageQueue = new MessageQueue(@".\Private$\AirportCheckInPassengerTrans");
                messageQueue.Label = "CheckIn Queue";
            }
            else
            {
                // Create the Queue
                MessageQueue.Create(@".\Private$\AirportCheckInPassengerTrans", true);
                messageQueue = new MessageQueue(@".\Private$\AirportCheckInPassengerTrans");
                messageQueue.Label = "Newly Created Queue";
            }
            Message message = new Message();
            message.Label = "Passenger for flightNo: " + flightNo;
            message.Body = xmlMessage;
            message.CorrelationId = id;
            Console.WriteLine("Sender en Passenger med ID: " + id);
            messageQueue.Send(message, msgTx);
        }

        private void luggageQueueSendMessage(XmlNode xmlMessage, string flightNo, int seqNumber, int size, string id)
        {
            MessageQueue messageQueue = null;
            if (MessageQueue.Exists(@".\Private$\AirportCheckInLuggageTrans"))
            {
                messageQueue = new MessageQueue(@".\Private$\AirportCheckInLuggageTrans");
                messageQueue.Label = "CheckInLuggage Queue";
            }
            else
            {
                // Create the Queue
                MessageQueue.Create(@".\Private$\AirportCheckInLuggageTrans", true);
                messageQueue = new MessageQueue(@".\Private$\AirportCheckInLuggageTrans");
                messageQueue.Label = "Newly Created Queue";
            }
            Message message = new Message();
            message.Label = "sequenceNumber:" + seqNumber + ", size:" + size;
            message.Body = xmlMessage;
            message.CorrelationId = id;
            Console.WriteLine("Sender en luggage med ID: " + id);
            messageQueue.Send(message, msgTx);
        }
    }
}
