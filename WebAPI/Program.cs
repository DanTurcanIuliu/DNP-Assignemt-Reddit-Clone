using Application.Dao_Interfaces;
using Application.Logic;
using Application.Logic_Interfaces;
using Application.Provider_Interfaces;
using FileData;
using FileData.DAOs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<FileContext>();

builder.Services.AddScoped<IUserDao, UserFileDao>();
builder.Services.AddScoped<IUserLogic, UserLogic>();
builder.Services.AddScoped<IUserProvider, UserFileDao>();

builder.Services.AddScoped<IPostDao, PostFileDao>();
builder.Services.AddScoped<IPostLogic, PostLogic>();

builder.Services.AddScoped<ICommentDao, CommentFileDao>();
builder.Services.AddScoped<ICommentLogic, CommentLogic>();

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