using AutoMapper;
using Desafio.Application.DTO1.Profiles;
using Desafio.Application1.ProjetoModule;
using Desafio.Application1.TarefaModule;
using Desafio.Application1.UsuarioModule;
using Desafio.Domain1.ProjetoModule;
using Desafio.Domain1.TarefaModule;
using Desafio.Domain1.UsuarioModule;
using Desafio.Infra.Repositories.ProjetoModule;
using Desafio.Infra.Repositories.TarefaModule;
using Desafio.Infra.Repositories.UsuarioModule;
using Desafio.Infra.UnityOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

string mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(op => op.UseSqlServer(mySqlConnection));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioAppService, UsuarioAppService>();

builder.Services.AddScoped<ITarefaRepository, TarefaRepository>();
builder.Services.AddScoped<ITarefaAppService, TarefaAppService>();

builder.Services.AddScoped<IProjetoRepository, ProjetoRepository>();
builder.Services.AddScoped<IProjetoAppService, ProjetoAppService>();

builder.Services.AddScoped<ILoginAppService, LoginAppService>();

var config = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<AppDbContext, AppDbContext>();
    cfg.AddProfile(new UsuarioProfile());
    cfg.AddProfile(new TarefaProfile());
    cfg.AddProfile(new ProjetoProfile());

});
var mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
