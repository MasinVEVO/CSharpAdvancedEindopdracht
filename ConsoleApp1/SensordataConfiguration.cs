namespace ConsoleApplication;

public sealed class SensordataConfiguration
{
    private static SensordataConfiguration _instance;
    private SensordataConfiguration() { }
    
    // De reden voor de singleton aanpak is om een configuratiepunt te maken voor de sensor_data.txt file. Dit onderbehoudt het makkelijker en hoeven er niet 
    // meerdere instanties gebruikt te worden voor deze configuratiepunt.

    public static SensordataConfiguration Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new SensordataConfiguration();
            }
            return _instance;
        }
    }

    public string ReadSensorData(string filepath)
    {
        if (!File.Exists(filepath))
        {
            throw new FileNotFoundException("Bestand is niet gevonden", filepath);
        }

        return File.ReadAllText(filepath);
    }
}