using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Connect4.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Nome { get; set; }
        public DateTime Nascimento { get; set; }
        public string CPF { get; set; }
        public string CEP { get; set; }
        public string Endereco { get; set; }
        public string NumeroCasa { get; set; }
        public JogadorPessoa jogador { get; set; } = new JogadorPessoa();
    }
}
