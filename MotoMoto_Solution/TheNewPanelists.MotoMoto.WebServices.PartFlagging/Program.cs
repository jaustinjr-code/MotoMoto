var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder => {
    builder.AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(o => true)
    .AllowCredentials();
});

app.UseHttpsRedirection();
<<<<<<<< HEAD:MotoMoto_Solution/TheNewPanelists.MotoMoto.WebServices.PartPriceAnalysis/Program.cs
========

>>>>>>>> main:MotoMoto_Solution/TheNewPanelists.MotoMoto.WebServices.PartFlagging/Program.cs
app.UseAuthorization();

app.MapControllers();

app.Run();
