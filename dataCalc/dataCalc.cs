namespace dataCalc
{
    /// <summary>
    /// Base class for holding and managing raw byte data.
    /// </summary>
    public class dataCalculate
    {
        // Property to store the byte array used for processing or transmission
        public byte[] Data { get; set; }

        // Constructor to initialize the object with an existing byte array
        public dataCalculate(byte[] data)
        {
            Data = data;
        }
    }

    /// <summary>
    /// Derived class that handles the logic for generating or processing specific data patterns.
    /// </summary>
    public class dataProcess : dataCalculate
    {
        // Constructor that calls the base class constructor and triggers data generation
        public dataProcess(byte[] data) : base(data)
        {
            GenerateNewData();
        }

        /// <summary>
        /// Populates the Data array with a sequence of bytes from 0 to 100.
        /// This acts as a mock data generator for testing or simulation.
        /// </summary>
        public void GenerateNewData()
        {
            List<byte> tempData = new List<byte>();
            for (byte i = 0; i <= 100; i++)
            {
                tempData.Add(i);
            }

            // Convert the list back to an array for the Data property
            Data = tempData.ToArray();
        }
    }

    /// <summary>
    /// Static class containing synchronization primitives to prevent race conditions.
    /// </summary>
    public static class GlobalLocks
    {
        public static object LockObj = new object();
    }
}