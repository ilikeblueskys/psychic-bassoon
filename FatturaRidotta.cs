using FatturaElettronica.Ordinaria;

public class FatturaRidotta
{
  public DateOnly Data { get; set; }
  public string Denominazione { get; set; }
  public string TipoDocumento { get; set; }
  public decimal Imponibile { get; set; }
  public decimal Imposta { get; set; }
  public decimal Importo { get; set; }

  public static bool IsNotaDiCredito(FatturaOrdinaria fattura)
  {
    var tipoDocumento = fattura.FatturaElettronicaBody.First().DatiGenerali.DatiGeneraliDocumento.TipoDocumento;
    return tipoDocumento.Contains("TD04");
  }

  public FatturaRidotta(DateOnly Data, string Denominazione, string TipoDocumento, decimal Imponibile, decimal Imposta, decimal Importo)
  {
    this.Data = Data;
    this.Denominazione = Denominazione;
    this.TipoDocumento = TipoDocumento;
    this.Imponibile = Imponibile;
    this.Imposta = Imposta;
    this.Importo = Importo;
  }

  public FatturaRidotta(FatturaOrdinaria fattura) : this(
    DateOnly.FromDateTime(fattura.FatturaElettronicaBody.First().DatiGenerali.DatiGeneraliDocumento.Data),
    fattura.FatturaElettronicaHeader.CedentePrestatore.DatiAnagrafici.Anagrafica.Denominazione,
    fattura.FatturaElettronicaBody.First().DatiGenerali.DatiGeneraliDocumento.TipoDocumento,
    ImponibileTotale(fattura),
    ImpostaTotale(fattura),
    ImportoTotale(fattura)
  )
  { }

  override public string ToString()
  {
    return $"Data: {Data.ToShortDateString()}\n"
           + $"Denominazione: {Denominazione}\n"
           + $"TipoDocumento: {TipoDocumento}\n"
           + $"Imponibile: {Imponibile}\n"
           + $"Imposta: {Imposta}\n"
           + $"Importo: {Importo}\n";
  }

  public Dictionary<string, object> GetPropertiesAsDictionary()
  {
    return new Dictionary<string, object>
      {
        { nameof(Data), Data.ToShortDateString() },
        { nameof(Denominazione), Denominazione },
        { nameof(TipoDocumento), TipoDocumento },
        { nameof(Imponibile), Imponibile },
        { nameof(Imposta), Imposta },
        { nameof(Importo), Importo }
      };
  }

  public static decimal ImponibileTotale(FatturaOrdinaria fatturaOrdinaria)
  {
    decimal imponibileTotale = fatturaOrdinaria.FatturaElettronicaBody.Sum(
      body => body.DatiBeniServizi.DatiRiepilogo.Sum(
        dato => dato.ImponibileImporto
      )
    );

    // Alcune fattura di reso inseriscono il segno (-) altre no: con le due righe sottostanti si gestisce il problema.
    if (IsNotaDiCredito(fatturaOrdinaria) && imponibileTotale > 0) imponibileTotale *= -1;
    return imponibileTotale;
  }

  public static decimal ImpostaTotale(FatturaOrdinaria fatturaOrdinaria)
  {
    var tipoDocumento = fatturaOrdinaria.FatturaElettronicaBody.First().DatiGenerali.DatiGeneraliDocumento.TipoDocumento;

    decimal impostaTotale = fatturaOrdinaria.FatturaElettronicaBody.Sum(
      body => body.DatiBeniServizi.DatiRiepilogo.Sum(
        dato => dato.Imposta
      )
    );

    // Alcune fattura di reso inseriscono il segno (-) altre no: con le due righe sottostanti si gestisce il problema.
    if (IsNotaDiCredito(fatturaOrdinaria) && impostaTotale > 0) impostaTotale *= -1;
    return impostaTotale;
  }

  public static decimal ImportoTotale(FatturaOrdinaria fatturaOrdinaria)
  {
    var tipoDocumento = fatturaOrdinaria.FatturaElettronicaBody.First().DatiGenerali.DatiGeneraliDocumento.TipoDocumento;

    decimal importoTotale = fatturaOrdinaria.FatturaElettronicaBody.Sum(
      body => body.DatiGenerali.DatiGeneraliDocumento.ImportoTotaleDocumento ?? 0
    );

    // Alcune fattura di reso inseriscono il segno (-) altre no: con le due righe sottostanti si gestisce il problema.
    if (IsNotaDiCredito(fatturaOrdinaria) && importoTotale > 0) importoTotale *= -1;
    return importoTotale;

  }
}