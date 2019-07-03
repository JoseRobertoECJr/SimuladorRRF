using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using static SimuladorRRF.Utils;

namespace SimuladorRRF.Classes
{
    public class Page
    {
        public string Id { get; set; }
        public BlockArray Pages;
        public Page(Process process)
        {
            Random randNum = new Random();
            num = randNum.Next();
            Id = process.Id + "num";
            Pages = new PageArray();
        }

        public Page(Page page)
        {
            Id = page.Id;
            Pages = new PageArray(page.Pages);
        }

    }
}
