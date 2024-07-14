using FatturaElettronica.Ordinaria;

public class FatturaRidottaWithDescription : FatturaRidotta
{
  public string Descrizione { get; set; }
  public FatturaRidottaWithDescription(DateOnly Data, string Denominazione, string Descrizione, string TipoDocumento, decimal Imponibile, decimal Imposta, decimal Importo) : base(Data, Denominazione, TipoDocumento, Imponibile, Imposta, Importo)
  {
    this.Descrizione = Descrizione;
  }

  public FatturaRidottaWithDescription(FatturaOrdinaria fattura) : base(fattura) {
    var anagrafica = fattura.FatturaElettronicaHeader.CessionarioCommittente.DatiAnagrafici.Anagrafica;
    this.Denominazione = anagrafica.Denominazione ?? $"{anagrafica.CognomeNome}";
    this.Descrizione = fattura.FatturaElettronicaBody.First().DatiBeniServizi.DettaglioLinee.First().Descrizione;

    Console.WriteLine(this.Denominazione);
    Console.WriteLine(this.Descrizione);

  }

  override public string ToString()
  {
    return $"Data: {Data.ToShortDateString()}\n"
           + $"Denominazione: {Denominazione}\n"
           + $"Descrizione: {Descrizione}\n"
           + $"TipoDocumento: {TipoDocumento}\n"
           + $"Imponibile: {Imponibile}\n"
           + $"Imposta: {Imposta}\n"
           + $"Importo: {Importo}\n";
  }

  public new Dictionary<string, object> GetPropertiesAsDictionary()
  {
    return new Dictionary<string, object>
      {
        { nameof(Data), Data.ToShortDateString() },
        { nameof(Denominazione), Denominazione },
        { nameof(Descrizione), Descrizione },
        { nameof(TipoDocumento), TipoDocumento },
        { nameof(Imponibile), Imponibile },
        { nameof(Imposta), Imposta },
        { nameof(Importo), Importo }
      };
  }
}