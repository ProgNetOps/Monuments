

var builder = WebApplication.CreateBuilder(args);


var app = builder
    .ConfigureServices()
    .ConfigurePipeline();

//Run the app
app.Run();
