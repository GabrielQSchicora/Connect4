using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Connect4.Models
{
    public class JogadorPessoa : Jogador
    {
        [InverseProperty("Jogador")]
        public virtual ApplicationUser Usuario { get; set; }        
        public IList<Jogo> Jogos { get; set; } = new List<Jogo>();
        public override string Nome { 
            get
            {
                if(Usuario == null)
                {
                    return "Nome não recuperado";
                }
                return Usuario.Nome;
            }
        }
    }
}
