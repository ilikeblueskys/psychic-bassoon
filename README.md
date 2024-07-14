# FatturazioneXML
Convertire raccolta di fatture elettroniche XML in formato CSV esportando le seguenti informazioni:
- Data;
- Denominazione;
- TipoDocumento;
- Imponibile;
- Imposta;
- Importo;

Vedi [specifica](./FatturaRidotta.cs)

## Motivazione
Molte piccole realtà italiane (P.IVA) non hanno bisogno di un gestionale e possono controllare la loro contabilità tramite uno spreadsheet (il mio caso). In questo modo non si deve pagare abbonamento alcuno o cedere dati e informazioni di fatturaziona a terzi. 

## Usage 
```bash
> ./FatturazioneXML
dotnet run <xml_directory> <csv_output> <mode>
```

## Dependencies
- [.NET 8.0](https://dotnet.microsoft.com/it-it/download/dotnet/8.0)
- [FatturaElettronica](https://github.com/FatturaElettronica/FatturaElettronica.NET)
- [CSVHelper](https://github.com/JoshClose/CsvHelper)