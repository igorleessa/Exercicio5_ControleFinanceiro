using ControleFinanceiro.Application.Conta;
using ControleFinanceiro.Application.EnvioEmail;
using ControleFinanceiro.Application.Transacao;
using ControleFinanceiro.Repository.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Repositories
builder.Services.AddScoped<UsuarioRepository>();
builder.Services.AddScoped<TransacaoRepository>();
builder.Services.AddScoped<ContaRepository>();

//Services
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<TransacaoService>();
builder.Services.AddScoped<ContaService>();
builder.Services.AddScoped<EnvioEmailService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
