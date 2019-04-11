using OtelRezervasyonu.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelRezervasyonu.ViewModel
{
    class BosOdaView
    {
        public string OdaNo { get; set; }
        public OdaTuruEnum OdaTuru { get; set; }
        public OdaKapasitesiEnum OdaKapasitesi { get; set; }
        public decimal Fiyat { get; set; }
    }
}
