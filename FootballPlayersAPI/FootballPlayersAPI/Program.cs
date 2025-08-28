using FootballPlayersAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<DBData>(); //SINGLETON PARA DBData (Clase que accede a la bd)


//AGREGAR CORS PARA UTILIZAR ANGULAR
//Los cors son para restringir las solicitudes HTTP que se realizan desde un dominio diferente al dominio
//desde el cual se cargó la página web. Swagger usa el 5192 y angular el 4200, los cors lo bloquean.


builder.Services.AddCors(options =>
{
    options.AddPolicy("Politic", app =>
    {
        //permitir a la app conectarse a cualquier origen, header y cualquier metodo get, put, delete
        app.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

////servicio HttpClient para hacer solicitudes HTTP al servidor de las imagenes
builder.Services.AddHttpClient();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Politic");
app.UseAuthorization();

app.MapControllers();

app.Run();
