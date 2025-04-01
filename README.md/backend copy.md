# Hệ thống Quản lý Công việc - TaskManagement

## Giới thiệu
Dự án này là một hệ thống quản lý công việc sử dụng .NET 8 và SQL Server. Hệ thống cho phép người dùng liệt kê các công việc, lưu trữ vào cơ sở dữ liệu, và tạo issue trên Jira.

## Yêu cầu hệ thống
- .NET 8 SDK
- SQL Server
- Công cụ quản lý package như Visual Studio hoặc CLI

## Cấu trúc thư mục
```
/TaskSystemHub
│── /TaskSystemHub.API        → Dự án Web API (Application Entry Point)
│── /TaskSystemHub.Application → Chứa logic nghiệp vụ (Use Cases, Services)
│── /TaskSystemHub.Domain      → Chứa các Entities (Lớp Model chính)
│── /TaskSystemHub.Infrastructure → Chứa kết nối DB, Repository, Jira API Client
│── /TaskSystemHub.Persistence → Chứa DB Context và Migration
│── /TaskSystemHub.Shared      → Chứa các helper, logging, DTOs dùng chung
│── /TaskSystemHub.Tests       → Chứa Unit Tests
│── /TaskSystemHub.sln         → File Solution

```

## Hướng dẫn thiết lập dự án

### 1. Tạo Solution và Các Project

#### 1.1. Mở terminal hoặc command prompt và chạy lệnh sau:
```sh
mkdir TaskSystemHub
cd TaskSystemHub
dotnet new sln -n TaskSystemHub
```
```
Lệnh này tạo một solution (.sln) có tên TaskSystemHub.
```

#### 1.2. Tạo các dự án con
```sh
dotnet new webapi -n TaskSystemHub.API
dotnet new classlib -n TaskSystemHub.Application
dotnet new classlib -n TaskSystemHub.Domain
dotnet new classlib -n TaskSystemHub.Infrastructure
dotnet new classlib -n TaskSystemHub.Persistence
dotnet new classlib -n TaskSystemHub.Shared
dotnet new xunit -n TaskSystemHub.Tests
```
```
Các lệnh trên tạo các dự án tương ứng với kiến trúc đã định.
```
#### 1.3. Thêm các dự án vào Solution
```sh
dotnet sln add TaskSystemHub.API
dotnet sln add TaskSystemHub.Application
dotnet sln add TaskSystemHub.Domain
dotnet sln add TaskSystemHub.Infrastructure
dotnet sln add TaskSystemHub.Persistence
dotnet sln add TaskSystemHub.Shared
dotnet sln add TaskSystemHub.Tests
```

#### 1.4. Thêm reference giữa các project
```sh
dotnet add TaskSystemHub.API reference TaskSystemHub.Application
dotnet add TaskSystemHub.API reference TaskSystemHub.Infrastructure
dotnet add TaskSystemHub.API reference TaskSystemHub.Persistence

dotnet add TaskSystemHub.Application reference TaskSystemHub.Domain
dotnet add TaskSystemHub.Application reference TaskSystemHub.Shared
dotnet add TaskSystemHub.Application reference TaskSystemHub.Infrastructure

dotnet add TaskSystemHub.Infrastructure reference TaskSystemHub.Domain
dotnet add TaskSystemHub.Infrastructure reference TaskSystemHub.Shared
dotnet add TaskSystemHub.Infrastructure reference TaskSystemHub.Application
dotnet add TaskSystemHub.Infrastructure reference TaskSystemHub.Persistence
dotnet remove TaskSystemHub.Infrastructure reference TaskSystemHub.Application



dotnet add TaskSystemHub.Persistence reference TaskSystemHub.Domain
dotnet add TaskSystemHub.Tests reference TaskSystemHub.Application
dotnet add TaskSystemHub.Tests reference TaskSystemHub.Infrastructure

dotnet add TaskSystemHub.Shared reference TaskSystemHub.Domain
```
### 2. Cấu hình Database và Entity Framework Core
#### 2.1 Cài đặt Entity Framework Core
```sh
dotnet add TaskSystemHub.Persistence package Microsoft.EntityFrameworkCore.SqlServer
dotnet add TaskSystemHub.Persistence package Microsoft.EntityFrameworkCore.Design
dotnet add TaskSystemHub.Persistence package Microsoft.EntityFrameworkCore.Tools

dotnet add TaskSystemHub.API package Microsoft.EntityFrameworkCore.Design
dotnet add TaskSystemHub.Shared package Newtonsoft.Json

cd TaskSystemHub.Infrastructure
dotnet add package Microsoft.Extensions.Configuration
dotnet add package Microsoft.Extensions.Configuration.Json
```
#### 2.2 Tạo DbContext
Trong `TaskSystemHub.Persistence`, tạo file `TaskDbContext.cs`:

```csharp
using TaskSystemHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace TaskSystemHub.Persistence
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options) { }

        public DbSet<TaskItem> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
```
#### 2.3. Tạo Entity (Model)
Trong `TaskSystemHub.Domain`, tạo file `TaskItem.cs`:
```csharp
namespace TaskSystemHub.Domain
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
```
#### 2.4. Cấu hình Connection String
Trong `TaskSystemHub.API`, thêm chuỗi kết nối vào `appsettings.json`:
```json
"ConnectionStrings": {
    "DefaultConnection": "Server=VIETENGLISH;Database=TaskManagement;User Id=sa;Password=123456Aa@;TrustServerCertificate=True;"
}
```

#### 2.5. Đăng ký DbContext trong Startup
Mở Program.cs trong TaskSystemHub.API, thêm:
```csharp
using TaskSystemHub.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddDbContext<TaskDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

var app = builder.Build();
app.UseAuthorization();
app.MapControllers();
app.Run();
```
#### 2.6. Tạo Migrations và Cập nhật Database
```sh
dotnet ef migrations add InitialCreate --project TaskSystemHub.Persistence --startup-project TaskSystemHub.API
dotnet ef database update --project TaskSystemHub.Persistence --startup-project TaskSystemHub.API

```
### 3. Xây dựng Repository và Services
#### 3.1. Tạo Repository Interface
Trong `TaskSystemHub.Application`, tạo file `ITaskRepository.cs`:
```csharp
using TaskSystemHub.Domain.Entities;

namespace TaskSystemHub.Application
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskItem>> GetAllTasksAsync();
        Task<TaskItem> GetTaskByIdAsync(int id);
        Task AddTaskAsync(TaskItem task);
        Task UpdateTaskAsync(TaskItem task);
        Task DeleteTaskAsync(int id);
    }
}
```
#### 3.2. Implement Repository
Trong `TaskSystemHub.Infrastructure`, tạo file `TaskRepository.cs`:
```csharp
using TaskSystemHub.Application;
using TaskSystemHub.Domain.Entities;
using TaskSystemHub.Persistence;
using Microsoft.EntityFrameworkCore;

namespace TaskSystemHub.Infrastructure
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskDbContext _context;

        public TaskRepository(TaskDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskItem>> GetAllTasksAsync()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<TaskItem> GetTaskByIdAsync(int id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        public async Task AddTaskAsync(TaskItem task)
        {
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTaskAsync(TaskItem task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTaskAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            }
        }
    }
}

```
### 4. Xây dựng API Controller
Trong `TaskSystemHub.API`, tạo file `TasksController.cs`:
```csharp
using TaskSystemHub.Application;
using TaskSystemHub.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

[Route("api/tasks")]
[ApiController]
public class TaskController : ControllerBase
{
    private readonly ITaskRepository _taskRepository;

    public TaskController(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTasks()
    {
        var tasks = await _taskRepository.GetAllTasksAsync();
        return Ok(tasks);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] TaskItem task)
    {
        await _taskRepository.AddTaskAsync(task);
        return CreatedAtAction(nameof(GetAllTasks), new { id = task.Id }, task);
    }
}

```
#### Chạy Dự án
```sh
dotnet run --project TaskSystemHub.API
Sau đó truy cập:

Lấy danh sách task: GET http://localhost:5000/api/tasks

Tạo task mới: POST http://localhost:5000/api/tasks
```
Dự án Web API sẽ chạy tại `http://localhost:5000` hoặc cổng được chỉ định.

## Đóng góp
Nếu bạn muốn đóng góp, hãy tạo Pull Request với mô tả chi tiết.

## Giấy phép
Dự án này được phát hành dưới giấy phép MIT.

Cấu trúc TaskSystemHub
``` json
/TaskSystemHub
│── /TaskSystemHub.API                  → Dự án Web API (Application Entry Point)
│   ├── Controllers/
│   │   ├── TaskController.cs           → Xử lý request từ frontend (tạo/lấy Task)
│   │   ├── JiraController.cs           → Xử lý request lấy dữ liệu từ Jira
│   ├── Program.cs                       → Cấu hình Dependency Injection
│   ├── appsettings.json                 → Cấu hình kết nối DB, Jira API
│
│── /TaskSystemHub.Application          → Chứa logic nghiệp vụ (Use Cases, Services)
│   ├── Interfaces/
│   │   ├── ITaskRepository.cs           → Interface cho Task Repository
│   │   ├── ITaskService.cs              → Interface cho Task Service
│   │   ├── IJiraRepository.cs           → Interface cho Jira Repository
│   │   ├── IJiraService.cs              → Interface cho Jira Service
│   ├── Services/
│   │   ├── TaskService.cs               → Xử lý logic Task (Thêm, Sửa, Xóa, Lấy Task)
│   │   ├── JiraService.cs               → Xử lý logic Jira (Lấy issue từ Jira)
│
│── /TaskSystemHub.Domain               → Chứa các Entities (Lớp Model chính)
│   ├── Entities/
│   │   ├── TaskItem.cs                  → Entity Task
│   │   ├── JiraIssue.cs                 → Entity Jira Issue
│
│── /TaskSystemHub.Infrastructure       → Chứa kết nối API bên ngoài, Jira API Client
│   ├── Jira/
│   │   ├── JiraApiClient.cs             → Gọi API Jira để lấy dữ liệu
│
│── /TaskSystemHub.Persistence          → Chứa DB Context và Migration
│   ├── Repositories/
│   │   ├── TaskRepository.cs            → Repository thực hiện CRUD Task
│   │   ├── JiraRepository.cs            → Repository lưu dữ liệu Jira vào DB
│   ├── TaskDbContext.cs                  → Entity Framework DbContext
│   ├── Migrations/                        → Lưu trữ file migration của DB
│
│── /TaskSystemHub.Shared               → Chứa các helper, logging, DTOs dùng chung
│   ├── DTOs/
│   │   ├── TaskDTO.cs                    → DTO cho Task
│   │   ├── JiraIssueDTO.cs               → DTO cho Jira Issue
│   ├── Helpers/
│   │   ├── JiraApiHelper.cs              → Helper xử lý dữ liệu từ Jira API
│
│── /TaskSystemHub.Tests                → Chứa Unit Tests
│   ├── TaskServiceTests.cs              → Test Service của Task
│   ├── JiraServiceTests.cs              → Test Service của Jira
│
│── /TaskSystemHub.sln                   → File Solution
```

### Ghi log 
Chạy lệnh sau trong thư mục TaskSystemHub.API:
```sh
dotnet add package Serilog.AspNetCore
dotnet add package Serilog.Sinks.File
dotnet add package Serilog.Sinks.Console
```

