﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Museum;

namespace MuseumForm
{
    public partial class SingleMessageControl : UserControl
    {
        private Museum.Message message;
       

        public Museum.Message Message
        {
            get => message;
            set => message = value;
        }
        public SingleMessageControl()
        {
           
            InitializeComponent();
            
        }

        public void UpdateText()
        {
            var db = DBConnection.Instance;
            string query = "SELECT * FROM employees WHERE employees.persons_id = " + message.Sender.Id;
            var result = db.Query(query);
            if (result.Count > 0)
            {
                headTitle.Text = "Message from: " + message.Sender.Name + " - Employee";
            }
            else
            {
                headTitle.Text = "Message from: " + message.Sender.Name + " - Exhibitor";
            }
            title.Text = "Title: " + message.Title;
                content.Text = message.Content;
                receivedTimeLabel.Text = "at " + message.LastUpdate;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var index = this.ParentForm.Controls.IndexOfKey(AppForms.Messages_Control);
            MessagesControl messagesControl = (MessagesControl)this.ParentForm.Controls[index];
            messagesControl.ResetView();
        }
    }
}
