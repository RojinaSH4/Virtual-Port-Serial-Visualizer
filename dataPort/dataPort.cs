using System.IO.Ports;
using dataCalc; // Importing the Domain logic layer 

namespace dataPort
{
    public class portInput
    {
        // Buffer to store all bytes received from the serial port
        private List<byte> reciveData = new List<byte>();

        // SerialPort objects for handling communication
        private SerialPort receiver;
        private SerialPort sender;

        // Separate threads for asynchronous reading and writing to prevent UI blocking
        private Thread receiveThread;
        private Thread sendThread;

        // Flag to control the execution loop of background threads
        private bool isRunning = false;

        /// <summary>
        /// Orchestrates the communication between the Serial Interface and the Domain Layer.
        /// </summary>
        public void PortManage(string receiverName, string senderName)
        {
            if (receiver != null && receiver.IsOpen) return;

            // Use the names provided by the UI, or fallback to defaults if null
            string portRec = receiverName ?? "COM6";
            string portSend = senderName ?? "COM5";

            int baud = 9600;
            receiver = new SerialPort(portRec, baud);
            sender = new SerialPort(portSend, baud);

            receiver.Open();
            sender.Open();
            isRunning = true;


            // Receiver Thread Logic
            receiveThread = new Thread(() =>
            {
                while (isRunning && receiver.IsOpen)
                {
                    try
                    {
                        // Check if there is data available in the receive buffer
                        if (receiver.BytesToRead > 0)
                        {
                            int bytesToRead = receiver.BytesToRead;
                            byte[] buffer = new byte[bytesToRead];

                            // Read raw bytes into the buffer
                            receiver.Read(buffer, 0, bytesToRead);

                            // Thread-safe access to the shared reciveData list
                            lock (GlobalLocks.LockObj)
                            {
                                reciveData.AddRange(buffer);
                            }
                        }

                        // Small delay to prevent high CPU usage (polling interval)
                        Thread.Sleep(10);
                    }
                    catch { break; }
                }

            });

            // Run as background thread so it terminates when the application closes
            receiveThread.IsBackground = true;
            receiveThread.Start();

            // Sender Thread Logic (Consuming Domain Data)
            sendThread = new Thread(() =>
            {
                /* DOMAIN INTERACTION: 
                  The 'dataProcess' class from 'dataCalc' namespace represents the Domain layer.
                  It encapsulates the business logic for data preparation/generation.
               */
                dataProcess dp = new dataProcess(new byte[0]);

                while (isRunning && sender.IsOpen)
                {
                    /* The Infrastructure layer (Serial Port) consumes the 
                       processed data provided by the Domain layer (dp.Data).
                    */
                    foreach (byte b in dp.Data)
                    {
                        if (!isRunning || !sender.IsOpen) break;
                        try
                        {
                            sender.Write(new byte[] { b }, 0, 1);
                            Thread.Sleep(15);
                        }
                        catch { break; }
                    }
                }

            });
            sendThread.IsBackground = true;
            sendThread.Start();
        }

        /// <summary>
        /// Stops the background loops and closes both serial port connections.
        /// </summary>
        public void Stop()
        {
            isRunning = false;
            Thread.Sleep(50);
            if (receiver != null && receiver.IsOpen)
                receiver.Close();

            if (sender != null && sender.IsOpen)
                sender.Close();
        }

        /// <summary>
        /// Provides a thread-safe copy of the accumulated received data.
        /// </summary>
        /// <returns>A new list containing all received bytes.</returns>
        public List<byte> GetReceivedData()
        {
            lock (GlobalLocks.LockObj)
            {
                // Return a copy of the list to prevent external modification issues during iteration
                List<byte> newList = new List<byte>(reciveData);

                return newList;
            }
        }

    }
}