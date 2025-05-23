using TaskSystemHub.Infrastructure.Jira;
using TaskSystemHub.Application.Interfaces;
using TaskSystemHub.Persistence;
using TaskSystemHub.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using TaskSystemHub.Application.Services;
using Serilog;
using Hangfire;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Cấu hình Serilog để ghi log mỗi ngày vào file riêng
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console() // Hiển thị log trên console
    .WriteTo.File(
        "Logs/tasklog-.yyyy-MM-dd.txt",  // Đặt tên file log theo ngày
        rollingInterval: RollingInterval.Day,  // Tạo file mới mỗi ngày
        retainedFileCountLimit: 30  // Giới hạn giữ lại 30 file log gần nhất
    )
    .CreateLogger();

// Add services to the container
// builder.Services.AddScoped<JiraApiClient>();
// builder.Services.AddScoped<JiraService>();
// Đăng ký các service
builder.Services.AddScoped<IJiraApiClient, JiraApiClient>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<IJiraService, JiraService>();
builder.Services.AddScoped<JiraJobService>();
builder.Services.AddScoped<IScheduledIssueRepository, ScheduledIssueRepository>();
// builder.Services.AddScoped<ITaskService, TaskService>(); 

builder.Services.AddDbContext<TaskDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers(); // Add controllers để sử dụng trong API
builder.Services.AddHttpClient(); // Add HttpClient để sử dụng trong JiraApiClient

// Thêm Serilog vào hệ thống logging của .NET
builder.Host.UseSerilog();

// Thêm Hangfire
builder.Services.AddHangfire(x =>
    x.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddHangfireServer();

builder.Services.AddLogging(); // Đảm bảo rằng dịch vụ logging đã được đăng ký

var app = builder.Build();

// Middleware để kiểm tra logging
app.Use(async (context, next) =>
{
    Log.Information("HTTP Request: {Method} {Path}", context.Request.Method, context.Request.Path);
    await next();
    Log.Information("HTTP Response: {StatusCode}", context.Response.StatusCode);
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
// Thêm middleware Hangfire
app.UseHangfireDashboard();

app.MapControllers();

// Đảm bảo job được lên lịch sau khi app đã chạy
app.Run();

// Sau khi app.Run(), thêm job Hangfire
RecurringJob.AddOrUpdate<JiraJobService>(
    "jira-job-daily",
    job => job.RunCreateAndTransitionJobAsync(),
    "0 18 * * *", // 6h chiều mỗi ngày
    TimeZoneInfo.Local
);
