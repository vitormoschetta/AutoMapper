using System;
using System.ComponentModel.DataAnnotations;

namespace EFSemMigrations.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }        
        public DateTime DataNascimento { get; set; }
        public bool Ativo { get; set; } 
        public char Sexo { get; set; }
        public string Altura { get; set; }
    }
}