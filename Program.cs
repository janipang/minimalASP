using MinAPISeparateFile;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
HelloHandler.Map(app);
// app.UseCors("AllowClient");
app.Run("http://localhost:8080");
