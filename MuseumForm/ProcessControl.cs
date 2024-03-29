﻿using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Museum;
using Process = Museum.Process;

namespace MuseumForm
{
    public partial class ProcessControl : UserControl
    {
        public Process Process;
        private Point LeftPosition { get;}
        private Point RightPosition { get;}
        private Point CenterPosition { get;}

        public ProcessControl()
        {
            InitializeComponent();
            LeftPosition = ConfirmEvent.Location;
            RightPosition = CancelEventButton.Location;
            CenterPosition = EditPriceButton.Location;
        }

        public void UpdateViewPerUser()
        {
            if (ParentForm != null)
            {
                var appForms = (MadeiraMuseum) ParentForm;
                var dashboardControl = appForms.DashboardControl;

                var roomsLabel = RoomsLabel;
                var endTimeLabel = EndTimeLabel;
                var entityLabel = Entity;
                var entetyNameLabel = EntityName;
                var firstDayLabel = FirstDayLabel;
                var lastDayLabel = LastDayLabel;
                var priceLabel = PriceLabel;
                var resultLabel = ResultLabel;
                var startTimeLabel = StartTimeLabel;
                var stateLabel = StateLabel;

                if (dashboardControl.Role.Equals(nameof(Employee)))
                {
                    entityLabel.Text = nameof(Exhibitor);
                    entetyNameLabel.Text = Process.Exhibitor.Name;
                }
                else
                {
                    entityLabel.Text = nameof(Employee);
                    entetyNameLabel.Text = Process.Employee.Name;
                }

                endTimeLabel.Text = Process.Schedule.EndTime;
                firstDayLabel.Text = Process.Schedule.FirstDay + @"/" + Process.Schedule.FirstMonth + @"/" +
                                     Process.Schedule.FirstYear;
                lastDayLabel.Text = Process.Schedule.LastDay + @"/" + Process.Schedule.LastMonth + @"/" +
                                    Process.Schedule.LastYear;
                priceLabel.Text = Process.Price.ToString();
                if (Process.Result == null)
                    resultLabel.Text = nameof(Pendent);
                else if (Process.Result == 1)
                    resultLabel.Text = Process.Active == 1 ? nameof(Approved) : nameof(Confirmed);
                else
                    resultLabel.Text = nameof(Denied);
                startTimeLabel.Text = Process.Schedule.StartTime;
                stateLabel.Text = nameof(Process.Actual);
                roomsLabel.Text = nameof(Room) + @"s ";
                foreach (var room in Process.Room) roomsLabel.Text += room.ToString();


                var accept = AcceptButton;
                var refuse = RefuseButton;
                var addArtPiece = AddArtPieceButton;
                var confirmEvent = ConfirmEvent;
                var cancelEvent = CancelEventButton;
                var editPrice = EditPriceButton;
                var editProcess = EditProcessButton;

                if (dashboardControl.Role.Equals(nameof(Employee)))
                {
                    if (Process.Active == 0)
                    {
                        if (Process.Result == 1)
                        {
                            CancelEventButton.Location = CenterPosition;
                            accept.Visible = false;
                            refuse.Visible = false;
                            addArtPiece.Visible = false;
                            confirmEvent.Visible = false;
                            cancelEvent.Visible = true;
                            editPrice.Visible = false;
                            editProcess.Visible = false;
                        }
                        else if (Process.Result == 0)
                        {
                            accept.Visible = false;
                            refuse.Visible = false;
                            addArtPiece.Visible = false;
                            confirmEvent.Visible = false;
                            cancelEvent.Visible = false;
                            editPrice.Visible = false;
                            editProcess.Visible = false;
                        }
                    }
                    else if (Process.Active == 1)
                    {
                        if (Process.Result == null)
                        {
                            accept.Visible = true;
                            refuse.Visible = true;
                            addArtPiece.Visible = false;
                            confirmEvent.Visible = false;
                            cancelEvent.Visible = false;
                            editPrice.Visible = true;
                            editProcess.Visible = false;
                        }
                        else
                        {
                            accept.Visible = false;
                            refuse.Visible = false;
                            addArtPiece.Visible = false;
                            confirmEvent.Visible = false;
                            cancelEvent.Visible = false;
                            editPrice.Visible = false;
                            editProcess.Visible = false;
                        }
                    }
                }
                else if (dashboardControl.Role.Equals(nameof(Exhibitor)))
                {
                    if (Process.Active == 0)
                        if (Process.Result == 1)
                        {
                            cancelEvent.Location = CenterPosition;
                            accept.Visible = false;
                            refuse.Visible = false;
                            addArtPiece.Visible = false;
                            confirmEvent.Visible = false;
                            cancelEvent.Visible = true;
                            editPrice.Visible = false;
                            editProcess.Visible = false;
                        }
                        else
                        {
                            accept.Visible = false;
                            refuse.Visible = false;
                            addArtPiece.Visible = false;
                            confirmEvent.Visible = false;
                            cancelEvent.Visible = false;
                            editPrice.Visible = false;
                            editProcess.Visible = false;
                        }
                    else if (Process.Active == 1)
                        if (Process.Result == null)
                        {
                            accept.Visible = false;
                            refuse.Visible = false;
                            addArtPiece.Visible = true;
                            confirmEvent.Visible = false;
                            cancelEvent.Visible = false;
                            editPrice.Visible = false;
                            editProcess.Visible = true;
                        }
                        else if (Process.Result == 1)
                        {
                            confirmEvent.Location = LeftPosition;
                            CancelEventButton.Location = RightPosition;
                            accept.Visible = false;
                            refuse.Visible = false;
                            addArtPiece.Visible = false;
                            confirmEvent.Visible = true;
                            cancelEvent.Visible = true;
                            editPrice.Visible = false;
                            editProcess.Visible = false;
                        }
                        else if (Process.Result == 0)
                        {
                            accept.Visible = false;
                            refuse.Visible = false;
                            addArtPiece.Visible = false;
                            confirmEvent.Visible = false;
                            cancelEvent.Visible = false;
                            editPrice.Visible = false;
                            editProcess.Visible = false;
                        }
                }
            }
        }

        private void AcceptButton_Click(object sender, EventArgs e)
        {
            Process.Actual.Accept();
            UpdateViewPerUser();

            Information.Text = @"You just accept the process";
            Information.Visible = true;
            var myTimer = new Timer {Interval = 3000};
            myTimer.Tick += HideInformation;
            myTimer.Start();
        }

        private void HideInformation(object sender, EventArgs e)
        {
            Information.Visible = false;
            var timer = (Timer) sender;
            timer.Enabled = false;
        }

        private void EditPriceButton_Click(object sender, EventArgs e)
        {
            if (ParentForm != null)
            {
                var appForms = (MadeiraMuseum) ParentForm;
                var editPriceControl = appForms.EditPriceControl;
                editPriceControl.BringToFront();
                editPriceControl.Process = Process;
            }

            UpdateViewPerUser();
        }

        private void EditProcessButton_Click(object sender, EventArgs e)
        {
            if (ParentForm != null)
            {
                var appForms = (MadeiraMuseum) ParentForm;
                var editProcessControl = appForms.EditProcessControl;
                editProcessControl.BringToFront();
                editProcessControl.Process = Process;
            }

            UpdateViewPerUser();
        }

        private void AddArtPieceButton_Click(object sender, EventArgs e)
        {
            if (ParentForm == null) return;
            var appForms = (MadeiraMuseum) ParentForm;
            var addArtPieceControl = appForms.AddArtPieceControl;
            addArtPieceControl.BringToFront();
            addArtPieceControl.Process = Process;
        }

        private void RefuseButton_Click(object sender, EventArgs e)
        {
            Process.Actual.Refuse();
            UpdateViewPerUser();

            Information.Text = @"You just refuse the process";
            Information.Visible = true;
            var myTimer = new Timer {Interval = 3000};
            myTimer.Tick += HideInformation;
            myTimer.Start();
        }

        private void ConfirmEvent_Click(object sender, EventArgs e)
        {
            Process.Actual.Confirm();
            UpdateViewPerUser();

            Information.Text = @"You just confirmed the process";
            Information.Visible = true;
            var myTimer = new Timer {Interval = 3000};
            myTimer.Tick += HideInformation;
            myTimer.Start();
        }

        private void RefuseEventButton_Click(object sender, EventArgs e)
        {
            Process.Actual.Cancel();
            UpdateViewPerUser();

            Information.Text = @"You just canceled the process";
            Information.Visible = true;
            var myTimer = new Timer {Interval = 3000};
            myTimer.Tick += HideInformation;
            myTimer.Start();
        }

        private void SeeArtPieces_Click(object sender, EventArgs e)
        {
            var appForms = (MadeiraMuseum) ParentForm;
            var listArtPiecesControl = appForms?.ListOfArtPieces;
            if (listArtPiecesControl == null) return;
            listArtPiecesControl.Process = Process;
            listArtPiecesControl.ListArtPieces();
            listArtPiecesControl.BringToFront();
        }
    }
}