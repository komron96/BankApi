using DataAccess;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("ToDoDb"));
builder.Services.AddControllers();
builder.Services.AddMvc();


WebApplication app = builder.Build();
app.MapControllers();
app.Run();
