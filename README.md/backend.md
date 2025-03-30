# Hệ thống Quản lý Công việc - TaskManagement

## Giới thiệu
Dự án này là một hệ thống quản lý công việc sử dụng .NET 8 và SQL Server. Hệ thống cho phép người dùng liệt kê các công việc, lưu trữ vào cơ sở dữ liệu, và tạo issue trên Jira.

## Yêu cầu hệ thống
- .NET 8 SDK
- SQL Server
- Công cụ quản lý package như Visual Studio hoặc CLI

## Cấu trúc thư mục
```
TaskManagement/
│-- TaskManagement.sln
│-- TaskManagement.Api/        # Dự án Web API
│-- TaskManagement.Core/       # Chứa Models và Interfaces
│-- TaskManagement.Infrastructure/ # Chứa Data Access (Entity Framework, Repositories)
│-- TaskManagement.Application/ # Chứa Services và Business Logic
```

## Hướng dẫn thiết lập dự án

### 1. Tạo Solution và các Dự án con
```sh
dotnet new sln -n TaskManagement
mkdir TaskManagement
cd TaskManagement
dotnet new webapi -n TaskManagement.Api
dotnet new classlib -n TaskManagement.Core
dotnet new classlib -n TaskManagement.Infrastructure
dotnet new classlib -n TaskManagement.Application
```
```
    Giải thích:
    TaskManagement.Api → Xử lý HTTP request (Controllers).
    TaskManagement.Core → Chứa các Models, DTOs, Interface.
    TaskManagement.Application → Xử lý nghiệp vụ (Services, Use Cases).
    TaskManagement.Infrastructure → Kết nối Database, triển khai Repository.
```

### 2. Thêm các Dự án vào Solution
```sh
dotnet sln add TaskManagement.Api/TaskManagement.Api.csproj
dotnet sln add TaskManagement.Core/TaskManagement.Core.csproj
dotnet sln add TaskManagement.Application/TaskManagement.Application.csproj
dotnet sln add TaskManagement.Infrastructure/TaskManagement.Infrastructure.csproj
```

### 3. Cấu hình các Tham chiếu giữa Dự án
```sh
cd TaskManagement.Api
dotnet add reference ../TaskManagement.Application/TaskManagement.Application.csproj
cd ..
cd TaskManagement.Application
dotnet add reference ../TaskManagement.Core/TaskManagement.Core.csproj
cd ..
cd TaskManagement.Infrastructure
dotnet add reference ../TaskManagement.Core/TaskManagement.Core.csproj
cd ..
```

### 4. Cài đặt Entity Framework Core và SQL Server
```sh
cd TaskManagement.Infrastructure
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.Tools
```

### 5. Cấu hình Cơ sở dữ liệu
Thêm chuỗi kết nối vào `appsettings.json` trong `TaskManagement.Api`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=TaskManagementDb;User Id=sa;Password=yourpassword;TrustServerCertificate=True;"
}
```

### 6. Chạy Dự án
```sh
cd TaskManagement.Api
dotnet run
```
Dự án Web API sẽ chạy tại `http://localhost:5000` hoặc cổng được chỉ định.

## Đóng góp
Nếu bạn muốn đóng góp, hãy tạo Pull Request với mô tả chi tiết.

## Giấy phép
Dự án này được phát hành dưới giấy phép MIT.

