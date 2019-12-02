using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Connect4.Models;

namespace Connect4.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Connect4.Models.Torneio> Torneio { get; set; }
        public DbSet<Connect4.Models.ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Connect4.Models.Jogo> Jogo { get; set; }
        public DbSet<Connect4.Models.Tabuleiro> Tabuleiros { get; set; }
        public DbSet<Connect4.Models.JogadorPessoa> JogadorPessoa { get; set; }
        public DbSet<Connect4.Models.JogadorMaquina> JogadorMaquina { get; set; }
    }
}
