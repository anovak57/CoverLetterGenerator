using Microsoft.EntityFrameworkCore;
using src.Data;
using src.Interfaces;
using src.Middleware;
using src.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHttpClient();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.AddJsonFile("./appsettings.Development.json", optional: false, reloadOnChange: true);

builder.Services.AddDbContext<AppDbContext>(option => 
{
    option.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<ICoverLetterService, CoverLetterService>();
builder.Services.AddScoped<IFileReaderService, FileReaderService>();
builder.Services.AddSingleton<IChatClient>(sp => new ChatClientWrapper(
        sp.GetRequiredService<IConfiguration>()["OpenAI:GptModel"],
        sp.GetRequiredService<IConfiguration>()["OpenAI:ApiKey"]
    ));

builder.Services.AddScoped<ICoverLetterRepository, CoverLetterRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
