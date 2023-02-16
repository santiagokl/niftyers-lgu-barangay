using Niftyers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<NiftyersDB>();
builder.Services.AddSingleton<IConfig>(builder.Configuration.GetSection("Config").Get<Config>());
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddTransient<IAccountServices, AccountServices>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
    {
        builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
    }));

var app = builder.Build();

app.UseHttpsRedirection();

app.UseCors("corsapp");

app.UseAuthorization();

app.MapControllers();

app.Run();
