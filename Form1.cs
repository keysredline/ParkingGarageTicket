//DEV: Justin Fredericks
//Script: L08 Unit Exam 
//Date: 10/12/2021
//Class: 151
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;

namespace ParkingGarage
{
    public partial class Form1 : Form
    {
        List<Ticket> Spot = new List<Ticket>();
        XmlDocument doc = new XmlDocument();
        int TicketNum = 101;
        public Form1()
        {
            InitializeComponent();
            SpacesSet();
        }
        private void cmbBx1_SelectedIndexChanged(object sender, EventArgs e) { }
        private void SpacesSet()
        {//create 5 Ticket objects representing spots in the garage, add them to the list"Spot"
            for (int i = 0; i < 5; i++)
                Spot.Add(new Ticket(true));
        }
        private void btn_1_Click(object sender, EventArgs e)
        {//Ticket button funct
            int index = checkGarage();
            if (index != -1) printTicket(index);
            else
                MessageBox.Show("Lot Full");
            CheckTakenSpots();
        }
        private int checkGarage()
        {//check list objs for open spot
            foreach (var item in Spot)
            {
                if (item.Open == true) return Spot.IndexOf(item);
            }
            return -1;
        }
        private void printTicket(int index)
        {//set ticket in's time and number
            Spot[index].TimeIn = getDT();
            Spot[index].TicketNumber = index;
            Spot[index].TicketCode = TicketNum;
            Spot[index].Open = false; 
            MessageBox.Show(Convert.ToString(index + "" + TicketNum++));           
            if (TicketNum > 999) TicketNum = 101;
        }
        private void CheckTakenSpots()
        {//fill combobox with current tickets (numbers and time in)
            cmbBx1.Items.Clear();
            Spot.ForEach(x => { if (x.Open == false) { cmbBx1.Items.Add(Spot.IndexOf(x)+" "+x.TimeIn.ToString()); } });
        }
        private void btn_2_Click(object sender, EventArgs e)
        {//Pay button set time out, show charge, reset obj to open
            Spot[cmbBx1.SelectedIndex].TimeOut = getDT();
            MessageBox.Show("$" + Spot[cmbBx1.SelectedIndex].CalculateCharge().ToString());
            Spot[cmbBx1.SelectedIndex].Open = true;
            cmbBx1.Items.Remove(cmbBx1.SelectedItem);
        }
        private void roundBTN1_Click(object sender, EventArgs e)
        {//get xml print out of all current tickets in garage
            doc.Load("receipt.xml");
            doc.DocumentElement.RemoveAll();
            doc.Save("receipt.xml");
            foreach (var item in Spot)
            {
                if (item.Open == false) item.WriteXML();
            }
        }
        private DateTime getDT()
        {
            return DateTime.Now;
        }
    }//class
}


