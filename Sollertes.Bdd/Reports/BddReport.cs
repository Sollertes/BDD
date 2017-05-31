namespace Sollertes.Bdd.Reports
{
    public class BddReport
    {
        
    }

    //default = zbiorczy raport tworzony na Unload
    //  każde wywołanie BddScenario.Test musi odkładać rezultat statycznie
    //  każdy rezultat musi być powiązany z klasą i metodą, która go wygenerowała

    //przypisanie BddScenario do BddReport
    //  .ReportTo(BddReport)
    //  rejestracja BddReport statycznie, generowanie na Unload
    //  wszystkie niezarejestrowane do zbiorczego raportu

    //możliwość wymuszenia zapisania raportu + wyrejestrowanie z koleji do zapisu na Unload na życzenie

    //dołączanie ReportWriter globalnie i na poziomie BddReport
    //  zczytywanie z konfiguracji
    //  ustawianie ręczne podczas inicjalizacji
    //wymuszenie inicjalizacji najpóźniej przy pierwszym użyciu
}