<h1 align="center">mini-blog</h1>

<br/>

<h1 align="center"><font color="red">Mô tả</font></h1>

**Mini Project cho môn học "Các công nghệ lập trình hiện đại"。Đề tài: Tìm hiểu về Asp.Net Core**

<br/>

## Mô tả Project

Proejct này được triển khai bằng **Asp.Net Core MVC** và sử dụng cơ sở dữ liệu là **SQL Server**

<br/>

## Xem trước project

> Ảnh chụp màn hình giao diện client

<br/>

![image](https://github.com/user-attachments/assets/601106c9-1ad3-4834-ba8c-5e88ed3491a2)
<br/>

![image](https://github.com/user-attachments/assets/fcf1f446-86d0-4261-bcd0-3b7aa914fb0b)<br/>

![image](https://github.com/user-attachments/assets/65a7c5c6-7dc7-40ed-b8dc-beff93954af9)<br/>

![image](https://github.com/user-attachments/assets/dc7b2988-a892-48af-87d5-bb860285b785)<br/>

![image](https://github.com/user-attachments/assets/a8de5dfc-6d0e-4a94-ae29-1fa22a90f603)<br/>

<br/>

> Ảnh chụp màn hình giao diện admin

![image](https://github.com/user-attachments/assets/5d63557e-d0e7-4589-91a1-0ede56eee627)<br/>


## Các chức năng của project

- Đăng ký, đăng nhập.
- Quản lý blog
- Duyệt, từ chối blog

<br/>

## Cài đặt

### 1、Clone project từ github

```bash
git clone https://github.com/imkhoanguyen/mini-blog.git
```

### 2、Sửa đổi tệp cấu hình

Trong file appsettings.Development.json thay đổi chuỗi ConnectionStrings của bạn

### 3、Restore the packages by running

```bash
# From the solution folder (mini-blog)
dotnet restore
```

### 4、Run Migration

1、Visual Studio
```bash
# In Package Manager Console
add-migration initDb
update-database
```

2、Visual Studio Code
```bash
dotnet ef migrations add initDb
dotnet ef database update
```

### 5、Run Project
```bash
# In Visual Studio Code
dotnet run
```

## License

Copyright (c) 2024 imkhoanguyen 
