var builder = WebApplication.CreateBuilder(args);

// Thêm Controllers và d?ch v? Swagger Gen
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // B?t bu?c ph?i có dòng này

var app = builder.Build();

// C?u hình s? d?ng Swagger ? môi tr??ng phát tri?n
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); // B?t bu?c ph?i có dòng này ?? b?t giao di?n web
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
