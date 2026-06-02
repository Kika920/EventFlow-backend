namespace WebTemplate.DTO
{
    public class ListaDashboardDTO
    {
         public bool Zavrsen { get; set; } = false;

        public DateTime DatumKreiranja { get; set; } = DateTime.Now;
        public string Tekst { get; set; } = string.Empty;
    }
}