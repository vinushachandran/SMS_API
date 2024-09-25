
using Microsoft.EntityFrameworkCore;
using SMS.BL.Allocation;
using SMS.BL.Allocation.Interface;
using SMS.BL.Student;
using SMS.BL.Student.Interface;
using SMS.BL.Subject;
using SMS.BL.Subject.Interface;
using SMS.BL.Teacher;
using SMS.BL.Teacher.Interface;
using SMS.Data;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddDbContext<SMS_Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("StudentManagementSystemContext")));

builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();
builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
builder.Services.AddScoped<IAllocationReposiory,AllocationRepository>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthorization();

app.MapControllers();

app.Run();
