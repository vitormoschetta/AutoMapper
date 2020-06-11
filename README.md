# AutoMapper

- Models/Usuario:

    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }        
        public DateTime DataNascimento { get; set; }
        public bool Ativo { get; set; } 
        public char Sexo { get; set; }
        public string Altura { get; set; }
    }

-ViewModels/UsuarioViewModel:

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
        public string Altura { get; set; }

        public string Mensagem { get; set; }
    }
