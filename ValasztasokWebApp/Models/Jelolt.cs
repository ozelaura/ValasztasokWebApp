namespace ValasztasokWebApp.Models
{
    public class Jelolt
    {
        public int Id { get; set; }
        public string Nev { get; set; }
        public int Kerulet { get; set; }
        public int Szavazatokszama { get; set; }
        public string PartRovidNev { get; set; }
        public virtual Part Part { get; set; } //Idegen kulcs
    }
}
