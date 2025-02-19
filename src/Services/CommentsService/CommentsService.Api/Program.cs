using CommentsService.DAL;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection(nameof(MongoDBSettings)));
BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

builder.Services.AddScoped<MongoDBContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseDeveloperExceptionPage(); // Показывает детальную информацию об ошибках
  app.UseSwagger(c =>
  {
    c.RouteTemplate = "api/comments/swagger/{documentName}/swagger.json";
  });
  app.UseSwaggerUI(c =>
  {
    c.SwaggerEndpoint("/api/comments/swagger/v1/swagger.json", "Comments Service API");
    c.RoutePrefix = "api/comments/swagger";
  });
}

app.UseAuthorization();

app.MapControllers();

app.Run();
