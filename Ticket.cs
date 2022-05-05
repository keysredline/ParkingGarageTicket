using System;
using System.Xml;

namespace ParkingGarage
{
    class Ticket
    {
        XmlDocument doc = new XmlDocument();
        private DateTime timeIn;
        private DateTime timeOut;
        private int ticketNumber;
        private int ticketCode;
        private bool open;
        public DateTime TimeIn { get => timeIn; set => timeIn = value; }
        public DateTime TimeOut { get => timeOut; set => timeOut = value; }
        public int TicketNumber { get => ticketNumber; set => ticketNumber = value; }
        public bool Open { get => open; set => open = value; }
        public int TicketCode { get => ticketCode; set => ticketCode = value; }
        public Ticket(){}
        public Ticket(bool open) {
            this.open = open;
        }
        public void WriteXML()
        {//write obj to the xml
            string date_Time = timeIn.ToString("dd/MM/yyyy HH:mm:ss");

            doc.Load("receipt.xml");
            XmlNode sale = doc.CreateElement("sale");//wrapper node

            XmlNode ticket = doc.CreateElement("ticket");
            ticket.InnerText = TicketNumber.ToString()+TicketCode.ToString();
            sale.AppendChild(ticket);//obj identifier + Code = unique ticket #

            XmlNode time = doc.CreateElement("time");
            time.InnerText = date_Time;
            sale.AppendChild(time);

            doc.DocumentElement.AppendChild(sale);
            doc.Save("receipt.xml");
        }
        public double CalculateCharge()
        {//return amount owed 
            TimeSpan ts = timeOut - timeIn;//get diff
            double totalMins = ts.TotalMinutes;//convert to mins
            if (totalMins <= 180.0)
                return 3.0;
            else if (totalMins <= 960.0) {
                totalMins = ((Math.Floor((totalMins - 180.0) / 60.0)) * 0.5)+3.0;
                return totalMins; }          
            else
                return 10.0;         
        }
    }//class
}
