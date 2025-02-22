using System;

namespace ConsoleApplication
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filepath = "C:\\Users\\Gebruiker\\RiderProjects\\Eindopdracht-CSharpAdvanced\\ConsoleApp1\\ConsoleApp1\\sensor_data.txt";
            try
            {
                string data = SensordataConfiguration.Instance.ReadSensorData(filepath);
                Console.WriteLine("Sensor Data:\n" + data);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex.Message);
            }
        }
    }
}