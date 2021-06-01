﻿using SCSSdkClient;
using SCSSdkClient.Object;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ETS2_Button_Box_Host.ButtonController;

namespace ETS2_Button_Box_Host
{
    public partial class ETS2ButtonBoxHost : Form
    {
        private ButtonBoxController buttonBoxController;

        public ETS2ButtonBoxHost()
        {
            // WinForms initialisation
            InitializeComponent();

            // Initialise button box controller
            this.buttonBoxController = new ButtonBoxController();
        }

        /// <summary>
        /// Post initialisation routine gets UI elements ready for user interaction
        /// </summary>
        private void ETS2ButtonBoxHost_Load(object sender, EventArgs e)
        {
            this.reloadCOMPorts();
        }

        /// <summary>
        /// Fill available COM ports into COM port combobox
        /// </summary>
        private void reloadCOMPorts()
        {
            this.cbCOMPort.Items.Clear();
            this.cbCOMPort.Items.AddRange(SerialPort.GetPortNames());
        }

        /// <summary>
        /// Connect to COM port button click handler
        /// </summary>
        private void btnConnect_Click(object sender, EventArgs e)
        {
            // Get and validate the selected COM port
            string comPort = this.cbCOMPort.SelectedItem as string;

            if (string.IsNullOrEmpty(comPort))
            {
                MessageBox.Show("Please select a COM port from the list.", "No COM port selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!SerialPort.GetPortNames().Contains(comPort))
            {
                MessageBox.Show("The selected COM port is not available anymore. Please check the connection and try again.", "COM port not available", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.reloadCOMPorts();
                return;
            }

            // Try and connect to button box
            if (!this.buttonBoxController.ConnectToBox(comPort))
            {
                MessageBox.Show("A connection to the button box could not be established. Please verify the connection.", "Connection failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        /// <summary>
        /// Reload COM ports button click handler
        /// </summary>
        private void btnReload_Click(object sender, EventArgs e)
        {
            this.reloadCOMPorts();
        }
    }
}
