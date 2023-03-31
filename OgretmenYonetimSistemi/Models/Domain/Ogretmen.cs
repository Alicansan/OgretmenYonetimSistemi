namespace OgretmenYonetimSistemi.Models.Domain
{
    public class Ogretmen
    {
        public Guid Id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string TcNo { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public string Ders { get; set; }
        public double Maas { get; set; }    
    }
}
