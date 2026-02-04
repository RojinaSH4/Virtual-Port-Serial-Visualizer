using System.IO.Ports;
using System.Windows.Forms.DataVisualization.Charting;
using dataPort;

namespace VPSG
{
    public partial class dataVision : Form
    {
        // Controls the lifecycle of the UI update task
        CancellationTokenSource cts;

        // COMMUNICATION BRIDGE:
        // Instance of portInput to act as the data provider for this view.
        private portInput pI = new portInput();

        // Execution state of the chart visualization
        private bool isRunning = false;

        // DATA TRACKING:
        // Maintains synchronization between the communication buffer and the chart points
        private int lastIndex = 0;
        private int pointCount = 0;

        // Reference to the chart's data series for adding points dynamically
        private Series liveSeries;

        public dataVision()
        {
            InitializeComponent();

            // Clear default chart configurations to prepar the chart canvas
            chartVision.Series.Clear();
            chartVision.ChartAreas.Clear();

            // Setup the main chart area configuration
            var area = new ChartArea("Default");

            // Allow the Y-axis to scale dynamically based on received data values
            area.AxisY.IsStartedFromZero = false;
            chartVision.ChartAreas.Add(area);

            // Initialize the data series as a line chart with specific visual styles
            liveSeries = new Series("LiveData")
            {
                ChartType = SeriesChartType.Line,
                BorderWidth = 2
            };
            chartVision.Series.Add(liveSeries);

            // Fetch all available port names on the currunt device
            string[] ports = SerialPort.GetPortNames();
            cmbReceiver.Items.AddRange(ports);
            cmbSender.Items.AddRange(ports);

            // Select the first two ports as default if available
            if (ports.Length >= 2)
            {
                cmbReceiver.SelectedIndex = 0;
                cmbSender.SelectedIndex = 1;
            }
        }

        /// <summary>
        /// CONSUMER LOGIC: 
        /// Periodically pulls data from the dataPort layer and renders it on the UI.
        /// </summary>
        private async Task UpdateChart(CancellationToken token)
        {

            // Continue looping until cancellation is requested via UI or Form closing
            while (!token.IsCancellationRequested)
            {
                if (pI != null)
                {
                    // DATA FETCHING:
                    // Retrieve the latest buffer from the Infrastructure layer (portInput)
                    var data = pI.GetReceivedData();

                    // Check if new data has arrived since the last update
                    if (data.Count > lastIndex)
                    {
                        // UI THREAD INVOCATION:
                        // Updating the chart with data retrieved from the serial port handler
                        this.Invoke((Action)(() =>
                        {
                            for (int i = lastIndex; i < data.Count; i++)
                            {
                                // Transforming raw bytes into visual data points
                                liveSeries.Points.AddXY(pointCount, data[i]);
                                pointCount++;
                            }

                            // Dynamic Window: Keep the view focused on the most recent stream
                            if (chartVision?.ChartAreas.Count > 0)
                            {
                                chartVision.ChartAreas[0].AxisX.Minimum = Math.Max(0, pointCount - 200);
                                chartVision.ChartAreas[0].AxisX.Maximum = pointCount;
                            }

                        }));

                        // Synchronize the index tracker with the data source length
                        lastIndex = data.Count;
                    }
                }
                await Task.Delay(2, token);
            }
        }

        /// <summary>
        /// Starts the data flow by triggering the Port manager and the local Update loop.
        /// </summary>
        private Task ThreadStart()
        {
            isRunning = true;
            cts = new CancellationTokenSource();

            // Get selected port names from UI controls
            string recPort = cmbReceiver.SelectedItem?.ToString();
            string sendPort = cmbSender.SelectedItem?.ToString();

            Task.Run(delegate
            {
                // Pass the selected ports to the communication layer
                pI.PortManage(recPort, sendPort);
            });


            // Sync the start point with the port's current buffer state
            lastIndex = pI.GetReceivedData().Count;

            // Start and return the UI update task
            return Task.Run(delegate
            {
                return UpdateChart(cts.Token);
            });
        }

        /// <summary>
        /// Stops background tasks and serial communication gracefully
        /// </summary>
        private void ThreadPause()
        {
            cts?.Cancel();
            isRunning = false;
            pI.Stop();
        }

        /// <summary>
        /// Handles the Start/Pause button logic
        /// </summary>
        private void btnRun_Click(object sender, EventArgs e)
        {
            if (isRunning)
            {
                ThreadPause();
                btnRun.Text = "START";

                // Prevent changing ports
                cmbReceiver.Enabled = true;
                cmbSender.Enabled = true;
            }
            else
            {
                ThreadStart();
                btnRun.Text = "PAUSE";

                // Prevent changing ports
                cmbReceiver.Enabled = false;
                cmbSender.Enabled = false;
            }
        }

        /// <summary>
        /// Performs real-time validation on user input. 
        /// Ensures that the same COM port is not assigned to both Receiver and Sender roles,
        /// preventing hardware access conflicts (Access Denied exceptions).
        /// </summary>
        private void ValidatePortSelection()
        {
            // Check if both selections are made and if they point to the same physical/virtual address
            if (cmbReceiver.SelectedItem != null && cmbSender.SelectedItem != null &&
                cmbReceiver.SelectedItem.ToString() == cmbSender.SelectedItem.ToString())
            {
                // UI FEEDBACK: Update the status label to notify the user of the conflict
                lblWarning.Text = "Warning: Receiver and Sender ports must be unique!";
                lblWarning.ForeColor = Color.Red;

                // PREVENT EXECUTION: Lock the 'START' button to ensure system stability
                btnRun.Enabled = false;
            }
            else
            {
                // CLEARANCE: Re-enable the system if the configuration is valid
                lblWarning.Text = "Ports configuration is valid.";
                lblWarning.ForeColor = Color.Lime;
                btnRun.Enabled = true;
            }
        }

        /// <summary>
        /// Ensures all background processes are terminated when the application is closed
        /// </summary>
        private void dataVision_FormClosed(object sender, FormClosedEventArgs e)
        {
            isRunning = false;
            pI?.Stop();
            cts?.Cancel();
        }

        /// <summary>
        /// Event handler triggered when the Receiver ComboBox selection changes.
        /// Initiates the input validation process.
        /// </summary>
        private void cmbSender_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Re-evaluate the system state based on the new selection
            ValidatePortSelection();
        }

        /// <summary>
        /// Event handler triggered when the Sender ComboBox selection changes.
        /// Initiates the input validation process.
        /// </summary>
        private void cmbReceiver_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Re-evaluate the system state based on the new selection
            ValidatePortSelection();
        }
    }
}