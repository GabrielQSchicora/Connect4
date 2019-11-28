using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Connect4.Views.Torneios
{
    public class RankingModel : PageModel
    {
        public Dictionary<String, int> ranking;
        public String TorneioNome;

        public void OnGet()
        {
        }
    }
}
