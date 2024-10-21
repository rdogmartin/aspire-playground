using Aspire.WebApi.ErrorHandling;
using Aspire.WebApi.Managers;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
// .ConfigureApiBehaviorOptions((options) =>
//     // This defines the structure of the JSON that is returned when the .NET model validation fails.
//     options.InvalidModelStateResponseFactory = (context) => InvalidModelStateResponse.Generate(context)
// );

// If we want to use an action filter:
// builder.Services.AddControllers(options =>
// {
//     options.Filters.Add<AspireExceptionFilter>();
// });

builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ContactManager>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();