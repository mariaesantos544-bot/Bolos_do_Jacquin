using Bolos_do_Jacquin.BdContextEvent;
using Bolos_do_Jacquin.Interfaces;
using Bolos_do_Jacquin.Repositories;
using Bolos_do_Jacquin.Services;
using Bolos_do_Jacquin.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// adicionando a Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "Insira um token válido para ter acesso aos endoints da api"
    });

    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("Bearer", document)] = []
    });

});

//Configuração do EFCore - Banco de dados
builder.Services.AddDbContext<BolosDoJacquinContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // corta o ciclo Usuario -> TipoUsuario -> Usuario ->.............
        // colocando um null no ponto onde a referencia se repete
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

//Registra o serviço de controllers (mapeia automaticamente os controllers da pasta / Controllers)
builder.Services.AddControllers();


//AddScoped - Injeção de dependência

builder.Services.AddScoped<IAvaliacao, AvaliacaoRepository>();
builder.Services.AddScoped<IUsuario, UsuarioRepository>();
builder.Services.AddScoped<IProduto, ProdutoRepository>();
builder.Services.AddScoped<ICategoria, CategoriaRepository>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            //valida quem emitiu o token
            ValidateIssuer = true,
            ValidIssuer = "Bolos_do_Jacquin",

            //valida para quem o token foi emitido
            ValidateAudience = true,
            ValidAudience = "Bolos_do_Jacquin",

            //valida se o token ainda está dentro do prazo de validade
            ValidateLifetime = true,

            //define a tolerancia de clock entre servidores
            ClockSkew = TimeSpan.FromMinutes(5),

            //chave secreta utilizada para validar a assinatura do token
            IssuerSigningKey = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes("Jwt:Key")
            )
        };
    });

builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("Cloudinary"));

// --- Sightengine (plano Free, sem cartão) ---
builder.Services.Configure<SightengineSettings>(builder.Configuration.GetSection("Sightengine"));

builder.Services.AddHttpClient<IModerationService, SightengineModerationService>(client =>
{
    client.BaseAddress = new Uri("https://api.sightengine.com/1.0/");
});

//Registra o serviço de autorização (necessário para [Authorize] funcionar)
builder.Services.AddAuthorization();

//Registra o serviço de controllers(mapeia automaticamente os controllers da pasta /Controllers)
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();