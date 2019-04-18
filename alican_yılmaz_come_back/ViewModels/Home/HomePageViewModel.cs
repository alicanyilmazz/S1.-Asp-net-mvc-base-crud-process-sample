using alican_yılmaz_come_back.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace alican_yılmaz_come_back.ViewModels.Home
{
    public class HomePageViewModel
    {
        public List<kisiler> kisiler { get; set; }
        public List<adresler> adresler { get; set; }
    }
}