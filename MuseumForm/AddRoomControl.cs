﻿using System;
using System.Windows.Forms;
using Museum;

namespace MuseumForm
{
    public partial class AddRoomControl : UserControl
    {
        public AddRoomControl()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            var index = ParentForm.Controls.IndexOfKey(AppForms.Dashboard_Control);
            var dashboardControl = (DashboardControl) ParentForm.Controls[index];
            dashboardControl.BringToFront();
            CleanFields();
        }

        private void CleanFields()
        {
            SizeBox.Text = "";
            DescriptionBox.Text = "";
        }

        private void UpdatePrice_Click(object sender, EventArgs e)
        {
            var size = SizeBox.Text;
            var description = DescriptionBox.Text;
            if (description.Trim().Equals("") || size.Trim().Equals(""))
            {
                if (description.Trim().Equals(""))
                    MissingFields.Text = "You must fill description correcly";
                else if (size.Trim().Equals(""))
                    MissingFields.Text = "You must fill Size correcly";
                else
                    MissingFields.Text = "You must fill all the fields";
                MissingFields.Visible = true;

                var MyTimer = new Timer();
                MyTimer.Interval = 1000;
                MyTimer.Tick += HideMessage;
                MyTimer.Start();
            }
            else
            {
                var room = new Room(size, description);
                room.Save();
                BackButton_Click(null, null);
                CleanFields();
            }
        }

        private void HideMessage(object sender, EventArgs e)
        {
            MissingFields.Visible = false;
            var timer = (Timer) sender;
            timer.Enabled = false;
        }
    }
}