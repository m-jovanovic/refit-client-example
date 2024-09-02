using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Refit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRefitClient<IBlogApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://jsonplaceholder.typicode.com"));

// Using Newtonsoft.Json for serialization:
//builder.Services.AddRefitClient<IBlogApi>(new RefitSettings
//{
//    ContentSerializer = new NewtonsoftJsonContentSerializer(new JsonSerializerSettings
//    {
//        ContractResolver = new CamelCasePropertyNamesContractResolver(),
//        NullValueHandling = NullValueHandling.Ignore
//    })
//});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/posts/{id}", async (int id, IBlogApi api) =>
    await api.GetPostAsync(id));

app.MapGet("/posts", async (int? userId, IBlogApi api) =>
    await api.GetPostsAsync(userId));

app.MapPost("/posts", async ([FromBody] Post post, IBlogApi api) =>
await api.CreatePostAsync(post));

app.MapPut("/posts/{id}", async (int id, [FromBody] Post post, IBlogApi api) =>
await api.UpdatePostAsync(id, post));

app.MapDelete("/posts/{id}", async (int id, IBlogApi api) =>
    await api.DeletePostAsync(id));


app.UseHttpsRedirection();

app.Run();