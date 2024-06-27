using System.Xml;
using FatturaElettronica.Ordinaria;

public static class Handler
{
  public static class Configuration
  {
    public static XmlReaderSettings DefaultXmlReaderSettings()
    {
      return new XmlReaderSettings
      {
        IgnoreWhitespace = true,
        IgnoreComments = true,
        IgnoreProcessingInstructions = true
      };
    }
  }

  public static string[] LoadXMLFilePathsFromDirectory(string directoryPath)
  {
    if (!Directory.Exists(directoryPath)) throw new Exception($"Could not find the specified XML folder: {directoryPath}");
    return Directory.GetFiles(directoryPath);
  }

  public static FatturaOrdinaria LoadFatturaOrdinaria(string path, XmlReaderSettings? settings = null)
  {
    settings ??= Configuration.DefaultXmlReaderSettings();
    var fattura = new FatturaOrdinaria();
    using var reader = XmlReader.Create(path, settings);
    fattura.ReadXml(reader);

    return fattura;
  }

  public static List<FatturaOrdinaria> LoadFattureOrdinarie(string[] paths, XmlReaderSettings? settings = null) =>
  paths.ToList().ConvertAll(path => LoadFatturaOrdinaria(path));
  public static List<FatturaRidotta> LoadFattureRidotte(string[] paths, XmlReaderSettings? settings = null) =>
  LoadFattureOrdinarie(paths).ConvertAll(fattura => new FatturaRidotta(fattura));
  public static List<FatturaRidotta> LoadFattureRidotte(string xmlDiretory, XmlReaderSettings? settings = null) =>
  LoadFattureOrdinarie(LoadXMLFilePathsFromDirectory(xmlDiretory)).ConvertAll(fattura => new FatturaRidotta(fattura));
}