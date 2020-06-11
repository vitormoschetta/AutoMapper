using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFSemMigrations.ViewModels
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")] 
        public string Nome { get; set; }        

        [Required(ErrorMessage = "Campo Obrigatório")] 
        [DataType(DataType.Date, ErrorMessage = "Digite uma data válida")]
        public DateTime DataNascimento { get; set; }
        public bool Ativo { get; set; } 

        [Required(ErrorMessage = "Campo Obrigatório")] 
        public char Sexo { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")] 
        [DisplayFormat(DataFormatString = "0.00")]
        public string Altura { get; set; }

        public string Mensagem { get; set; }
    }
}