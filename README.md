# AutoMapper

-Models/Usuario:

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
    
    
-Pacote: PackageReference Include="AutoMapper" Version="9.0.0"
    
-Startup/ConfigureServices:

    var config = new AutoMapper.MapperConfiguration(cfg =>
    {
        cfg.CreateMap<ViewModels.UsuarioViewModel, Models.Usuario>();
        cfg.CreateMap<Models.Usuario, ViewModels.UsuarioViewModel>();
    });
    IMapper mapper = config.CreateMapper();
    services.AddSingleton(mapper);

-Controller/Injection:

    private readonly IMapper _mapper;
    public HomeController(IMapper mapper)
    {
        _mapper = mapper;
    }
    
-Controller/Index:

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
    
-View/Index/Type:

    @model IEnumerable<NomeProjeto.ViewModels.UsuarioViewModel>
    

-Obs: É necessário converter do Modelo/Context para ViewModel, e vice-versa:

-View/Create/Type:

    @model NomeProjeto.ViewModels.UsuarioViewModel
    
-Controller/Create/Post:
    
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
    


