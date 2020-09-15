# AutoMapper

Em projetos MVC as vezes precisamos de ViewModels, que são classes de domínio adaptadas para a visualização, atribuídas a Views Tipadas. 

Quando as diferenças da classe de domínio para a classe view são pequenas, e essas diferenças não necessitam serem salvas no banco ( como exemplo a validação com data annotations ), não se faz necessário criar uma Action ou Controller para tratar dessa ViewModel, basta mapeá-la da/para a classe de domínio.

Além dos arquivos do projeto neste repositório, segue abaixo, de forma resumida, como implementar o AutoMapper:

#### Pacote:
```
dotnet add package AutoMapper --version 10.0.0
```

#### Classe Model Usuario:
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }        
        public DateTime DataNascimento { get; set; }
        public bool Ativo { get; set; } 
        public char Sexo { get; set; }
        public string Altura { get; set; }
    }

#### Classe ViewModel UsuarioViewModel:
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
    
    
#### Classe Startup => Método ConfigureServices:
    var config = new AutoMapper.MapperConfiguration(cfg =>
    {
        cfg.CreateMap<ViewModels.UsuarioViewModel, Models.Usuario>();
        cfg.CreateMap<Models.Usuario, ViewModels.UsuarioViewModel>();
    });
    IMapper mapper = config.CreateMapper();
    services.AddSingleton(mapper);

#### Injeção de dependência no HomeController:
    private readonly IMapper _mapper;
    public HomeController(IMapper mapper)
    {
        _mapper = mapper;
    }
    
#### Mapear da classe de domínio (Usuario) para a viewModel(UsuarioViewModel):

    public IActionResult Index(){
        var listaModel = _context.Usuario.ToList(); 
        
        // Convert listModel in listViewModel:
        var listaviewModel = _mapper.Map<IEnumerable<UsuarioViewModel>>(listaModel); 
        
        // Adiciona valor em atributo viewModel não existente em model (mensagem):
        foreach (var item in listaviewModel){
            item.Mensagem = "Atributo de outra entidade";
        }
        return View(listaviewModel);
    }
    
#### Tipagem da View Index:
    @model IEnumerable<NomeProjeto.ViewModels.UsuarioViewModel>
    

#### Obs: É necessário converter de Model para ViewModel, e vice-versa:

#### Tipagem da View Create:
    @model NomeProjeto.ViewModels.UsuarioViewModel
    
#### Mapear de UsuarioViewModel para Usuario:
No método Index mapeamos a lista de Usuario para uma lista de UsuarioViewModel, para mostrar o modelo otimizado na View.
Agora, para o cadastro de um novo usuário precisamos converter de UsuarioViewModel para Usuario. Ou seja, o caminho inverso. 
Se você prestou atenção a classe 'Startup' possui essa configuração. 

    [HttpPost]
    public IActionResult Create(UsuarioViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError(String.Empty, "Erro no preenchimento das informações");
            return View(viewModel);
        }
        
        // Convert viewModel in Model:
        var model = _mapper.Map<Usuario>(viewModel);
        
        _context.Add(model);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
    


