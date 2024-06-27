using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace FatturazioneXML;

class Program
{
    public static CsvConfiguration CSVConfig = new(CultureInfo.GetCultureInfo("it-IT")) { Delimiter = ";" };

    public static void Usage() {
        Console.WriteLine("usage: <xml_dir> <csv_output>");
    }

    static void Main(string[] args)
    {

        if (args.Length < 2) {
            Usage();
            return;
        }

        string XML_DIRECTORY = args[0];
        string CSV_OUTPUT_PATH = args[1];
        List<FatturaRidotta> fatture = Handler.LoadFattureRidotte(XML_DIRECTORY);
        string CSVContent = FattureRidottaToCSV(fatture);
        WriteToFile(CSV_OUTPUT_PATH, CSVContent);
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

    public static void WriteToFile(string filePath, string data) => File.WriteAllText(filePath, data);
}
