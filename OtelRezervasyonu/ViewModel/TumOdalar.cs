using OtelRezervasyonu.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelRezervasyonu
{
    class TumOdalar
    {
        public string OdaNo { get; set; }
        public int OdaKapasitesi { get; set; }
        public OdaTuruEnum OdaTuru { get; set; }
        public decimal Fiyat { get; set; }
    }
}
