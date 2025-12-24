using Microsoft.EntityFrameworkCore;
using SmartSupport.TicketService.Data;
using SmartSupport.TicketService.Services;
using System.IO;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowUI", policy =>
    {
        policy
            .WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


// Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<TicketTriageService>();
builder.Services.AddScoped<ShadowMLClassifier>();

// Use InMemory database for development
builder.Services.AddDbContext<TicketDbContext>(options =>
    options.UseInMemoryDatabase("TicketDb"));

var app = builder.Build();

// Ensure database and tables are created
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<TicketDbContext>();
    dbContext.Database.EnsureCreated();
}

app.UseDeveloperExceptionPage();
// Middleware
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors("AllowUI");
app.UseAuthorization();
app.MapControllers();

app.Run();
