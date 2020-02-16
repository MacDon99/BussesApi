using ApiBus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiBus.Repository
{
    public class BussesRepository
    {
        public List<Bus> Busses { get; set; }

        public BussesRepository()
        {
            Busses = new List<Bus>()
            {
                new Bus { Number = 1,
                    StationsFirstDirection = { "https://mzkwejherowo.pl/rozklad-jazdy/1-263-01.html", "https://mzkwejherowo.pl/rozklad-jazdy/1-261-01.html", "https://mzkwejherowo.pl/rozklad-jazdy/1-101-01.html", "https://mzkwejherowo.pl/rozklad-jazdy/1-102-01.html", "https://mzkwejherowo.pl/rozklad-jazdy/1-103-01.html", "https://mzkwejherowo.pl/rozklad-jazdy/1-104-01.html", "https://mzkwejherowo.pl/rozklad-jazdy/1-105-01.html", "https://mzkwejherowo.pl/rozklad-jazdy/1-106-01.html", "https://mzkwejherowo.pl/rozklad-jazdy/1-107-01.html", "https://mzkwejherowo.pl/rozklad-jazdy/1-144-01.html", "https://mzkwejherowo.pl/rozklad-jazdy/1-108-01.html", "https://mzkwejherowo.pl/rozklad-jazdy/1-109-01.html", "https://mzkwejherowo.pl/rozklad-jazdy/1-110-01.html", "https://mzkwejherowo.pl/rozklad-jazdy/1-163-01.html", "https://mzkwejherowo.pl/rozklad-jazdy/1-164-01.html", "https://mzkwejherowo.pl/rozklad-jazdy/1-165-01.html", "https://mzkwejherowo.pl/rozklad-jazdy/1-166-01.html", "https://mzkwejherowo.pl/rozklad-jazdy/2-411-01.html", "https://mzkwejherowo.pl/rozklad-jazdy/2-412-01.html", "https://mzkwejherowo.pl/rozklad-jazdy/2-413-01.html", "https://mzkwejherowo.pl/rozklad-jazdy/2-414-01.html", "https://mzkwejherowo.pl/rozklad-jazdy/2-415-01.html", "https://mzkwejherowo.pl/rozklad-jazdy/2-441-01.html", "https://mzkwejherowo.pl/rozklad-jazdy/2-442-01.html", "https://mzkwejherowo.pl/rozklad-jazdy/2-443-01.html", "https://mzkwejherowo.pl/rozklad-jazdy/2-458-01.html", "https://mzkwejherowo.pl/rozklad-jazdy/2-451-01.html", "https://mzkwejherowo.pl/rozklad-jazdy/2-452-01.html", "https://mzkwejherowo.pl/rozklad-jazdy/2-453-01.html", "https://mzkwejherowo.pl/rozklad-jazdy/2-454-01.html", "https://mzkwejherowo.pl/rozklad-jazdy/2-455-01.html", "https://mzkwejherowo.pl/rozklad-jazdy/2-456-01.html", "https://mzkwejherowo.pl/rozklad-jazdy/2-457-00.html" },
                    StationsSecondDirection = { "https://mzkwejherowo.pl/rozklad-jazdy/2-448-02.html", "https://mzkwejherowo.pl/rozklad-jazdy/2-447-02.html", "https://mzkwejherowo.pl/rozklad-jazdy/2-446-02.html", "https://mzkwejherowo.pl/rozklad-jazdy/2-445-02.html", "https://mzkwejherowo.pl/rozklad-jazdy/2-444-02.html", "https://mzkwejherowo.pl/rozklad-jazdy/2-443-02.html", "https://mzkwejherowo.pl/rozklad-jazdy/2-441-02.html", "https://mzkwejherowo.pl/rozklad-jazdy/2-415-02.html", "https://mzkwejherowo.pl/rozklad-jazdy/2-414-02.html", "https://mzkwejherowo.pl/rozklad-jazdy/2-413-02.html", "https://mzkwejherowo.pl/rozklad-jazdy/2-412-02.html", "https://mzkwejherowo.pl/rozklad-jazdy/2-411-02.html", "https://mzkwejherowo.pl/rozklad-jazdy/1-166-02.html", "https://mzkwejherowo.pl/rozklad-jazdy/1-165-02.html", "https://mzkwejherowo.pl/rozklad-jazdy/1-164-02.html", "https://mzkwejherowo.pl/rozklad-jazdy/1-163-02.html", "https://mzkwejherowo.pl/rozklad-jazdy/1-111-02.html", "https://mzkwejherowo.pl/rozklad-jazdy/1-110-02.html", "https://mzkwejherowo.pl/rozklad-jazdy/1-109-02.html", "https://mzkwejherowo.pl/rozklad-jazdy/1-108-02.html", "https://mzkwejherowo.pl/rozklad-jazdy/1-107-02.html", "https://mzkwejherowo.pl/rozklad-jazdy/1-106-02.html", "https://mzkwejherowo.pl/rozklad-jazdy/1-105-02.html", "https://mzkwejherowo.pl/rozklad-jazdy/1-104-02.html", "https://mzkwejherowo.pl/rozklad-jazdy/1-103-02.html", "https://mzkwejherowo.pl/rozklad-jazdy/1-102-02.html", "https://mzkwejherowo.pl/rozklad-jazdy/1-101-04.html", "https://mzkwejherowo.pl/rozklad-jazdy/1-263-00.html" } }
            };
        }
    }
}
