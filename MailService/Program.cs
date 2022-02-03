using MailService;
using MailService.DBModels;
using MailService.Services;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connection = builder.Configuration.GetConnectionString("DefaultConnection");

var serverConfig = new ServerConfiguration(builder.Configuration);

builder.Services.AddSingleton(serverConfig);

builder.Services.AddTransient<ILogMailService, LogMailService>();
builder.Services.AddTransient<ISendEMailService, SendEmailService>();
builder.Services.AddMemoryCache();

builder.Services.AddDbContext<EMailMessageContext>(options => options.UseSqlite(connection));


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
