using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Org.BouncyCastle.Tls;

namespace FatturazioneXML;

class Program
{
    public static CsvConfiguration CSVConfig = new(CultureInfo.GetCultureInfo("it-IT")) { Delimiter = ";" };

    public static void Usage()
    {
        Console.WriteLine("usage: <xml_dir> <csv_output> <mode>");
        Console.WriteLine("availables <mode>: {emesse, ricevute}");
        Console.WriteLine("example: ../emesse ./emesse.csv emesse");
    }


    delegate void RunCommand(string XML_DIRECTORY, string CSV_OUTPUT_PATH);

    public static void RunRicevute(string XML_DIRECTORY, string CSV_OUTPUT_PATH)
    {
        List<FatturaRidotta> fatture = Handler.LoadFattureRidotte(XML_DIRECTORY);
        string CSVContent = FattureRidottaToCSV(fatture);
        WriteToFile(CSV_OUTPUT_PATH, CSVContent);
    }


    public static void RunEmesse(string XML_DIRECTORY, string CSV_OUTPUT_PATH)
    {
        List<FatturaRidottaWithDescription> fatture = Handler.LoadFattureRidotteWithDescription(XML_DIRECTORY);
        string CSVContent = FattureRidottaWithDescriptionToCSV(fatture);
        WriteToFile(CSV_OUTPUT_PATH, CSVContent);
    }

    static void Main(string[] args)
    {
        if (args.Length < 3 || args[0].Equals("--help"))
        {
            Usage();
            return;
        }

        string XML_DIRECTORY = args[0];
        string CSV_OUTPUT_PATH = args[1];
        string MODE = args[2];

        // TODO: Error Handling CLI Input

        RunCommand run = MODE.Equals("emesse") ? RunEmesse : RunRicevute;
        run(XML_DIRECTORY, CSV_OUTPUT_PATH);
    }


    public static string FatturaRidottaToCSV(FatturaRidotta fattura)
    {

        using var writer = new StringWriter();
        using var csv = new CsvWriter(writer, CSVConfig);
        csv.WriteRecords(new List<FatturaRidotta> { fattura });
        return writer.ToString();
    }

    public static string FattureRidottaToCSV(IEnumerable<FatturaRidotta> fatture)
    {
        using var writer = new StringWriter();
        using var csv = new CsvWriter(writer, CSVConfig);
        csv.WriteRecords(fatture);
        return writer.ToString();
    }


    public static string FattureRidottaWithDescriptionToCSV(IEnumerable<FatturaRidottaWithDescription> fatture)
    {
        using var writer = new StringWriter();
        using var csv = new CsvWriter(writer, CSVConfig);
        csv.Context.RegisterClassMap<Mapper.FatturaRidottaWithDescriptionMapper>();
        csv.WriteRecords(fatture);
        return writer.ToString();
    }

    public static void WriteToFile(string filePath, string data) => File.WriteAllText(filePath, data);
}
