using System;

namespace atm
{
    class Program
    {
        class Musteri
        {
            public string musteriAdi { get; set; }
        }

        class Hesap : Musteri
        {
            public int hesapNo { get; set; }
            public decimal bakiye { get; set; }
            public string hesapTuru { get; set; }
            public decimal Kur { get; set; }
            public string ParaCek(Hesap hesap, Musteri musteri, decimal miktar)
            {
                this.musteriAdi = musteri.musteriAdi;
                this.hesapTuru = hesap.hesapTuru;
                this.bakiye = hesap.bakiye;
                this.Kur = hesap.Kur;
                decimal sonuc = this.bakiye - miktar;
                if (this.hesapTuru != "TL")
                {
                    sonuc = this.Kur * miktar;
                }
                this.bakiye -= sonuc;
                return ("işlem başarılı" + Environment.NewLine +
                    "Çekilen Hesap : " + this.hesapTuru + Environment.NewLine +
                    "Çekilen tutar : " + miktar + Environment.NewLine +
                    "TL Karşılığı : " + sonuc + Environment.NewLine +
                    "Kalan Tutar : " + this.bakiye.ToString());
            }
        }
        class KrediKarti : Hesap
        {
            public string sifre { get; set; }
            public string krediKartiNo { get; set; }
        }
        class Banka : KrediKarti
        {
            public string BankaAdi { get; set; }
            public string musteriBilgiGoster(Banka banka, Musteri musteri, Hesap hesap, KrediKarti krediKarti)
            {
                this.BankaAdi = banka.BankaAdi;
                this.musteriAdi = musteri.musteriAdi;
                this.hesapNo = hesap.hesapNo;
                this.krediKartiNo = krediKarti.krediKartiNo;
                this.bakiye = hesap.bakiye;
                this.hesapTuru = hesap.hesapTuru;
                return ("Banka Adı :" + this.BankaAdi + Environment.NewLine +
                    "Müşteri Adı :" + this.musteriAdi + Environment.NewLine +
                    "Hesap No :" + this.hesapNo + Environment.NewLine +
                    "Kredi Kartı No :" + this.krediKartiNo + Environment.NewLine +
                    "Hesap Türü :" + this.hesapTuru + Environment.NewLine +
                    "Bakiye :" + this.bakiye);
            }
            public string musteriGiris(Musteri musteri, Hesap hesap, KrediKarti krediKarti, int hesapNumarasi, string kullaniciSifre)
            {
                this.hesapNo = hesap.hesapNo;
                this.sifre = krediKarti.sifre;
                if (this.hesapNo == hesapNumarasi && this.sifre == kullaniciSifre)
                {
                    return ("Giriş Yapıldı");
                }
                else
                    return ("Hatalı Giriş");
            }
        }
        class ATM : Banka
        {
            public void ATMMakinesi()
            {
                int kontrol = 0;
                while (true)
                {
                    Banka banka = new Banka();
                    banka.BankaAdi = "Ziraat Bankası";
                    Musteri musteri = new Musteri();
                    musteri.musteriAdi = "Gamze TURAP";
                    Hesap hesap = new Hesap();
                    hesap.hesapNo = 4446772;
                    hesap.bakiye = 90000;
                    hesap.hesapTuru = "Dolar";
                    hesap.Kur = Convert.ToDecimal(5.43);
                    KrediKarti krediKarti = new KrediKarti();
                    krediKarti.krediKartiNo = "1231 1231 4325 1233";
                    krediKarti.sifre = "1718";
                    Console.WriteLine("Lütfen hesap numaranızı giriniz.");
                    int hesapNo = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Lütfen şifrenizi giriniz.");
                    string sifre = Convert.ToString(Console.ReadLine());
                    while (true)
                    {
                        if (banka.musteriGiris(musteri, hesap, krediKarti, hesapNo, sifre) == "Hatalı Giriş")
                        {
                            Console.WriteLine("Hatalı Giriş");
                            kontrol++;
                            if (kontrol == 3)
                            {
                                Console.WriteLine("Makine Tarafından kartınız yutulmuştur");
                                Environment.Exit(1);

                            }
                            break;
                        }
                        if (banka.musteriGiris(musteri, hesap, krediKarti, hesapNo, sifre) == "Giriş Yapıldı")
                        {
                            while (true)
                            {
                                Console.WriteLine("Lütfen işlem yapmak seçeneği tuşlayınız.");
                                Console.WriteLine("Para çekmek için       : 1");
                                Console.WriteLine("Bakiye sorgulamak için : 2");
                                int islem = Convert.ToInt16(Console.ReadLine());
                                if (islem == 1)
                                {
                                    Console.WriteLine("Lütfen çekmek istediğiniz tutarı 10TL'nin katları ve 500TL'yi aşmayacak şekilde tuşlayınız.");
                                    int miktar = Convert.ToInt16(Console.ReadLine());
                                    if (hesap.bakiye >= miktar)
                                    {
                                        if (miktar % 10 == 0 && miktar <= 500)
                                        {
                                            Console.WriteLine(hesap.ParaCek(hesap, musteri, miktar) + Environment.NewLine + "Kartınız Geri Verilmiştir");
                                            Environment.Exit(1);
                                        }
                                        else
                                        {
                                            Console.WriteLine("Girdiğiniz Tutar 500 TL'den küçük ve 10,20,50 katları olmalıdır.");
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Girdiğiniz Tutar kadar bakiyeniz bulunmamaktadır.");
                                        break;
                                    }
                                }
                                else if (islem == 2)
                                {
                                    Console.WriteLine(banka.musteriBilgiGoster(banka, musteri, hesap, krediKarti));
                                }
                            }

                        }

                    }
                }
            }
            static void Main(string[] args)
            {
                ATM A = new ATM();
                A.ATMMakinesi();
                Console.ReadKey();
            }
        }
    }
}
