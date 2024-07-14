using CsvHelper.Configuration;

public static class Mapper {
  public sealed class FatturaRidottaWithDescriptionMapper : ClassMap<FatturaRidottaWithDescription> {
    public FatturaRidottaWithDescriptionMapper() {
      Map(fatturaRidottaWithDescription => fatturaRidottaWithDescription.Data).Index(0);
      Map(fatturaRidottaWithDescription => fatturaRidottaWithDescription.Denominazione).Index(1);
      Map(fatturaRidottaWithDescription => fatturaRidottaWithDescription.Descrizione).Index(2);
      Map(fatturaRidottaWithDescription => fatturaRidottaWithDescription.TipoDocumento).Index(3);
      Map(fatturaRidottaWithDescription => fatturaRidottaWithDescription.Imponibile).Index(4);
      Map(fatturaRidottaWithDescription => fatturaRidottaWithDescription.Imposta).Index(5);
      Map(fatturaRidottaWithDescription => fatturaRidottaWithDescription.Importo).Index(6);
    }
  }
}