using Microsoft.EntityFrameworkCore;
using ProvaRemotaCapta.Data;
using ProvaRemotaCapta.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ProvaCaptaContext>(options => options.UseLazyLoadingProxies().UseSqlServer(
builder.Configuration.GetConnectionString("ClienteConnection")
));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<ClienteService, ClienteService>();
builder.Services.AddScoped<TipoService, TipoService>();
builder.Services.AddScoped<SituacaoService, SituacaoService>();

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
