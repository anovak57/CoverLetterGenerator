using src.Interfaces;
using src.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHttpClient();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.AddJsonFile("./appsettings.Development.json", optional: false, reloadOnChange: true);

builder.Services.AddScoped<ICoverLetterService, CoverLetterService>();
builder.Services.AddScoped<IFileReaderService, FileReaderService>();
builder.Services.AddSingleton<IChatClient>(sp => new ChatClientWrapper(
        sp.GetRequiredService<IConfiguration>()["OpenAI:GptModel"],
        sp.GetRequiredService<IConfiguration>()["OpenAI:ApiKey"]
    ));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
