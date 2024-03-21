using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Messaging;
using System.Text.Json;


namespace Lek9_Opgave3
{
    internal class Adapter
    {
        private static int x = 0;
        private static int y = 0;
        Excel.Application oXL;
        Excel._Workbook oWB;
        Excel._Worksheet oSheet;
        Excel.Range oRng;
        string[,] flightInfo = new string[5, 2];

        public void receiveMessage()
        {
            MessageQueue msExcel = new MessageQueue(@".\Private$\Lek9_Opg3_Excel");
            Message msg = msExcel.Receive();
            msg.Formatter = new XmlMessageFormatter(new String[] { "System.String,mscorlib" });
            string body = (string)msg.Body;
            string flightNumber;
            string estTimeOfArrival;
            string[] msgSplittet;
            msgSplittet = body.Split(new char[] { ';' });
            flightNumber = msgSplittet[0];
            estTimeOfArrival = msgSplittet[1];
            sendToExcel(flightNumber, estTimeOfArrival);
            x++;
            y++;
        }

        public void sendToExcel(string flightNumber, string estTimeOfArrival)
        {

            try
            {
                if (oXL == null)
                {
                    //Start Excel and get Application object.
                    oXL = new Excel.Application();
                    oXL.Visible = true;

                    //Get a new workbook.
                    oWB = (Excel._Workbook)(oXL.Workbooks.Add(Missing.Value));
                    oSheet = (Excel._Worksheet)oWB.ActiveSheet;

                    //Add table headers going cell by cell.
                    oSheet.Cells[1, 1] = "Flight Number";
                    oSheet.Cells[1, 2] = "Estimated time of arrival";

                    //Format A1:D1 as bold, vertical alignment = center.
                    oSheet.get_Range("A1", "B1").Font.Bold = true;
                    oSheet.get_Range("A1", "B1").VerticalAlignment =
                        Excel.XlVAlign.xlVAlignCenter;
                }
                flightInfo[x, 0] = flightNumber;
                flightInfo[y, 1] = estTimeOfArrival;


                //Fill A2:B6 with an array of values (First and Last Names).
                oSheet.get_Range("A2", "B6").Value2 = flightInfo;


                //Make sure Excel is visible and give the user control
                //of Microsoft Excel's lifetime.
                oXL.Visible = true;
                oXL.UserControl = true;
            }
            catch (Exception theException)
            {
                String errorMessage;
                errorMessage = "Error: ";
                errorMessage = String.Concat(errorMessage, theException.Message);
                errorMessage = String.Concat(errorMessage, " Line: ");
                errorMessage = String.Concat(errorMessage, theException.Source);

                Console.WriteLine(errorMessage);
            }
        }
    }
}
