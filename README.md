🛒 HỆ THỐNG QUẢN LÝ SIÊU THỊ MINI (MINISUPERMARKET SYSTEM)
> \\\\\\\\\\\\\\\*\\\\\\\\\\\\\\\*Môn học:\\\\\\\\\\\\\\\*\\\\\\\\\\\\\\\* Lập trình Ứng dụng .NET Core (Mã môn: 229162)
> \\\\\\\\\\\\\\\*\\\\\\\\\\\\\\\*Buổi 1:\\\\\\\\\\\\\\\*\\\\\\\\\\\\\\\* Xây dựng Web API quản lý nhóm hàng và kết nối WinForms Client (CRUD)
> \\\\\\\\\\\\\\\*\\\\\\\\\\\\\\\*Buổi 2:\\\\\\\\\\\\\\\*\\\\\\\\\\\\\\\* Bổ sung Xác thực (Authentication) bằng JWT và Phân quyền (Authorization) theo Role
> \\\\\\\\\\\\\\\*\\\\\\\\\\\\\\\*Buổi 3:\\\\\\\\\\\\\\\*\\\\\\\\\\\\\\\* Kết nối SQL Server bằng Entity Framework Core (Code-First), Migration, Data Seeding và bổ sung chức năng quản lý Khách hàng thân thiết
---
🏗️ 1. Mô hình Kiến trúc Hệ thống (Client - Server)
Dự án được xây dựng theo mô hình phân tầng, tách biệt hoàn toàn giữa Backend và Frontend:
`MiniSupermarket.API` (Backend): Dự án ASP.NET Core Web API chịu trách nhiệm xử lý nghiệp vụ, truy xuất dữ liệu SQL Server qua Entity Framework Core, xác thực người dùng bằng JWT và cung cấp các RESTful API.
`MiniSupermarket.WinForms` (Frontend Client): Ứng dụng Windows Forms đóng vai trò máy trạm tại quầy. Người dùng đăng nhập (`FormLogin`) để lấy JWT Token, sau đó ứng dụng dùng `HttpClient` kèm Token để gọi API và hiển thị dữ liệu lên `DataGridView`.
SQL Server LocalDB: Cơ sở dữ liệu `MiniSupermarketDb`, được tạo và cập nhật cấu trúc bằng EF Core Migration.
```text
 WinForms Client  ──(HTTP + JWT Bearer)──►  ASP.NET Core Web API  ──(EF Core)──►  SQL Server LocalDB
 (FormLogin, ...)                           (Controllers)                         (MiniSupermarketDb)
```
---
🛠️ 2. Công nghệ Sử dụng
Ngôn ngữ / Nền tảng: C# (.NET 8.0)
Backend: ASP.NET Core Web API, Controllers, LINQ, async/await
Cơ sở dữ liệu: SQL Server LocalDB, Entity Framework Core 8.0 (Code-First, Migration, Data Seeding, Data Annotations)
Bảo mật: JWT Bearer Authentication (`Microsoft.AspNetCore.Authentication.JwtBearer`), Role-based Authorization
Frontend: Windows Forms (.NET 8.0), `System.Net.Http.Json`, `System.Net.Http.Headers`
Công cụ kiểm thử: Swagger UI, SQL Server Object Explorer / SSMS
Gói NuGet chính của project API:
Gói	Phiên bản
`Microsoft.EntityFrameworkCore.SqlServer`	8.0.31
`Microsoft.EntityFrameworkCore.Design`	8.0.31
`Microsoft.EntityFrameworkCore.Tools`	dùng để chạy `Add-Migration` / `Update-Database`
`Microsoft.AspNetCore.Authentication.JwtBearer`	8.0.31
`System.IdentityModel.Tokens.Jwt`	8.23.0
`Swashbuckle.AspNetCore`	10.2.3
---
📂 3. Cấu trúc Solution
```text
MiniSupermarketSystem/
│
├── MiniSupermarket.API/                  # Dự án Web API (Backend)
│   ├── Controllers/
│   │   ├── AuthController.cs             # Đăng nhập \\\\\\\\\\\\\\\& phát hành JWT Token
│   │   ├── CategoriesController.cs       # CRUD \\\\\\\\\\\\\\\& Search nhóm hàng
│   │   └── CustomersController.cs        # CRUD \\\\\\\\\\\\\\\& Search khách hàng thân thiết
│   ├── Data/
│   │   └── SupermarketDbContext.cs       # DbContext, DbSet, cấu hình mặc định, Data Seeding
│   ├── Models/
│   │   ├── Category.cs                   # Thực thể Nhóm hàng (1 - N với Product)
│   │   ├── Product.cs                    # Thực thể Sản phẩm (khóa ngoại CategoryId)
│   │   └── Customer.cs                   # Thực thể Khách hàng thân thiết
│   ├── Migrations/                       # Lịch sử migration do EF Core sinh ra
│   ├── appsettings.json                  # Connection string \\\\\\\\\\\\\\\& cấu hình JWT
│   └── Program.cs                        # Đăng ký DbContext, JWT Middleware, Swagger
│
└── MiniSupermarket.WinForms/             # Dự án Windows Forms (Frontend Client)
    ├── FormLogin.cs                      # Màn hình đăng nhập, lấy JWT Token
    ├── FormCategoryManagement.cs         # Quản lý nhóm hàng (CRUD + Tìm kiếm)
    ├── FormCustomerManagement.cs         # Quản lý khách hàng thân thiết (CRUD + Tìm kiếm)
    └── SessionManager.cs                 # Lưu Token/Role của phiên làm việc dùng chung toàn ứng dụng
```
---
🗄️ 4. Cơ sở dữ liệu (SQL Server + Entity Framework Core)
4.1. Kết nối
Chuỗi kết nối nằm trong `appsettings.json` (khóa `ConnectionStrings:DefaultConnection`):
```text
Server=(localdb)\\\\\\\\\\\\\\\\MSSQLLocalDB;Database=MiniSupermarketDb;Trusted\\\\\\\\\\\\\\\_Connection=True;TrustServerCertificate=True;
```
`Program.cs` đăng ký `SupermarketDbContext` bằng `AddDbContext` với `UseSqlServer` thông qua Dependency Injection.
4.2. Các bảng dữ liệu
`Categories` – Nhóm hàng
Cột	Kiểu / Ràng buộc
`CategoryId`	PK, tự tăng
`CategoryName`	`nvarchar(100)`, bắt buộc
`Description`	`nvarchar(255)`, cho phép null
`Products` – Sản phẩm
Cột	Kiểu / Ràng buộc
`ProductId`	PK, tự tăng
`Barcode`	`nvarchar(50)`, bắt buộc
`ProductName`	`nvarchar(200)`, bắt buộc
`Price`	`decimal(18,2)`
`StockQuantity`	`int`
`CategoryId`	FK → `Categories.CategoryId`
> Quan hệ \\\\\\\\\\\\\\\*\\\\\\\\\\\\\\\*1 - N\\\\\\\\\\\\\\\*\\\\\\\\\\\\\\\*: một `Category` có nhiều `Product` (`Category.Products`). Hiện tại bảng `Products` đã được tạo trong CSDL nhưng chưa có Controller/Form thao tác.
`Customers` – Khách hàng thân thiết
Cột	Kiểu / Ràng buộc	Giá trị mặc định
`CustomerId`	PK, tự tăng	
`CustomerName`	`nvarchar(100)`, bắt buộc	
`PhoneNumber`	`varchar(15)`, bắt buộc	
`Address`	`nvarchar(200)`, cho phép null	
`RewardPoints`	`int`	`0`
`MembershipRank`	`nvarchar(50)`	`Chuẩn`
4.3. Dữ liệu mẫu (Data Seeding)
Cấu hình trong `SupermarketDbContext.OnModelCreating` bằng `HasData`, được nạp vào CSDL khi chạy migration:
25 nhóm hàng (Bánh kẹo, Nước giải khát, Sữa, Mì gói, Gia vị, Gạo, Thực phẩm tươi sống, Mẹ & Bé, Văn phòng phẩm, ...).
3 khách hàng mẫu: Nguyễn Văn An (hạng Vàng, 150 điểm), Trần Thị Bình (hạng Bạc, 40 điểm), Lê Hoàng Cường (hạng Chuẩn, 0 điểm).
4.4. Migration
Các lệnh chạy trong Package Manager Console (Default project chọn `MiniSupermarket.API`):
```powershell
Add-Migration <TênMigration>   # Sinh file migration từ thay đổi của Model
Update-Database                # Áp dụng migration vào SQL Server (tạo/cập nhật bảng)
Get-Migration                  # Xem migration nào đã áp dụng (Applied) hoặc chưa (Pending)
```
Lịch sử migration của dự án: `MiniSupermarket` (tạo `Categories`, `Products` và seed nhóm hàng) → `MiniSupermarkt` → `NguyenLeChiCong\\\\\\\\\\\\\\\_212410094` → `AddCustomersTable` (tạo bảng `Customers` và seed khách hàng).
---
🔐 5. Xác thực (Authentication) & Phân quyền (Authorization)
`CategoriesController` và `CustomersController` đều gắn `\\\\\\\\\\\\\\\[Authorize]`, nghĩa là client bắt buộc phải đăng nhập lấy JWT Token thì mới gọi được API.
5.1. Tài khoản mẫu
Tài khoản được khai báo cố định trong `AuthController` (chưa lưu trong CSDL):
Username	Password	Role
`admin`	`123456`	Admin
`cashier`	`123456`	Cashier
5.2. Endpoint đăng nhập
`POST /api/auth/login`: không yêu cầu Token.
Body: `{ "username": "admin", "password": "123456" }`
Thành công `200 OK`: `{ "success": true, "token": "<JWT>", "role": "Admin" }`
Sai tài khoản hoặc mật khẩu: `401 Unauthorized`.
Token ký bằng `HmacSha256`, khóa bí mật cấu hình tại `JwtSettings:Secret` trong `appsettings.json`, hiệu lực 2 giờ, chứa claim `Name` và `Role`.
5.3. Gửi kèm Token khi gọi API
Mọi endpoint có `\\\\\\\\\\\\\\\[Authorize]` yêu cầu header:
```
Authorization: Bearer <token>
```
Phía WinForms, `SessionManager` lưu `JwtToken` và `CurrentRole` sau khi đăng nhập thành công. Mỗi form quản lý (`FormCategoryManagement`, `FormCustomerManagement`) tự gắn header này vào `HttpClient` khi được load.
5.4. Bảng endpoint và phân quyền
Xác thực
Endpoint	Method	Quyền	Mô tả
`/api/auth/login`	POST	Public	Đăng nhập, phát hành JWT
Nhóm hàng (`/api/categories`)
Endpoint	Method	Quyền truy cập	Mô tả
`/api/categories`	GET	Admin, Cashier	Lấy danh sách nhóm hàng
`/api/categories/{id}`	GET	Admin, Cashier	Lấy chi tiết theo ID
`/api/categories/search?keyword=...`	GET	Admin, Cashier	Tìm kiếm theo từ khóa
`/api/categories`	POST	Chỉ Admin	Thêm nhóm hàng
`/api/categories/{id}`	PUT	Chỉ Admin	Cập nhật nhóm hàng
`/api/categories/{id}`	DELETE	Chỉ Admin	Xóa nhóm hàng
`/api/categories/admin-dashboard`	GET	Chỉ Admin	Trang quản trị
`/api/categories/staff-pos`	GET	Admin, Cashier	Màn hình POS thu ngân dùng chung
Khách hàng thân thiết (`/api/customers`)
Endpoint	Method	Quyền truy cập	Mô tả
`/api/customers`	GET	Admin, Cashier	Lấy danh sách khách hàng
`/api/customers/{id}`	GET	Admin, Cashier	Lấy chi tiết theo ID
`/api/customers/search?keyword=...`	GET	Admin, Cashier	Tìm theo họ tên hoặc số điện thoại
`/api/customers`	POST	Admin, Cashier	Thêm khách hàng
`/api/customers/{id}`	PUT	Admin, Cashier	Cập nhật khách hàng
`/api/customers/{id}`	DELETE	Chỉ Admin	Xóa khách hàng
> Gọi endpoint \\\\\\\\\\\\\\\*\\\\\\\\\\\\\\\*không kèm Token\\\\\\\\\\\\\\\*\\\\\\\\\\\\\\\* trả về `401 Unauthorized`. Đăng nhập đúng nhưng \\\\\\\\\\\\\\\*\\\\\\\\\\\\\\\*sai Role\\\\\\\\\\\\\\\*\\\\\\\\\\\\\\\* (ví dụ `cashier` gọi `DELETE /api/customers/{id}` hoặc `admin-dashboard`) trả về `403 Forbidden`.
---
🖥️ 6. Chức năng phía WinForms
6.1. `FormLogin`: Đăng nhập
Kiểm tra bỏ trống tài khoản hoặc mật khẩu trước khi gọi API.
Gọi `POST /api/auth/login`, lưu `JwtToken` và `CurrentRole` vào `SessionManager`.
Thông báo quyền đăng nhập, ẩn form đăng nhập và mở form quản lý; đóng ứng dụng khi form quản lý tắt.
Báo lỗi khi sai tài khoản/mật khẩu hoặc không kết nối được Server.
6.2. `FormCategoryManagement`: Quản lý nhóm hàng
Hiển thị danh sách nhóm hàng trên `DataGridView`; bấm vào một dòng để đưa dữ liệu lên các ô nhập.
Thêm mới, Cập nhật, Xóa, Tìm kiếm theo từ khóa, Tải lại danh sách.
Gắn JWT Token vào mọi request.
6.3. `FormCustomerManagement`: Quản lý khách hàng thân thiết
Hiển thị danh sách khách hàng (Mã KH, Họ tên, SĐT, Địa chỉ, Điểm tích lũy, Hạng thành viên) trên `DataGridView`.
Tìm kiếm theo họ tên hoặc số điện thoại, Tải lại danh sách.
Thêm / Sửa / Xóa khách hàng; bấm vào một dòng để đưa dữ liệu lên các ô nhập (ô Mã KH chỉ đọc).
Kiểm tra dữ liệu phía Client: bắt buộc nhập họ tên và số điện thoại; điểm tích lũy phải là số nguyên không âm; để trống hạng thành viên sẽ dùng mặc định `Chuẩn`.
Có hộp thoại xác nhận trước khi xóa; nếu tài khoản không phải Admin, API trả `403` và form hiển thị thông báo "Chỉ tài khoản Admin mới được xóa khách hàng".
6.4. Luồng hoạt động
Ứng dụng khởi động vào `FormLogin`.
Người dùng nhập tài khoản và mật khẩu, gọi `POST /api/auth/login`.
Đăng nhập thành công, Token và Role được lưu vào `SessionManager`, mở form quản lý.
Các form quản lý gắn Token vào header `Authorization` cho mọi request CRUD/Search tiếp theo.
Nếu Token hết hạn (sau 2 giờ) hoặc chưa đăng nhập, API trả `401`, cần đăng nhập lại.
---
🚀 7. Hướng dẫn Chạy và Kiểm thử Dự án
Yêu cầu: Visual Studio 2022, .NET 8 SDK, SQL Server LocalDB (đi kèm Visual Studio).
Bước 1: Tạo cơ sở dữ liệu
Mở Solution bằng Visual Studio 2022.
Mở Tools → NuGet Package Manager → Package Manager Console, chọn Default project là `MiniSupermarket.API`.
Chạy lệnh:
```powershell
   Update-Database
   ```
Mở View → SQL Server Object Explorer → (localdb)\MSSQLLocalDB → Databases → MiniSupermarketDb → Tables để kiểm tra các bảng `Categories`, `Products`, `Customers` và dữ liệu mẫu.
Bước 2: Chạy Backend (Web API)
Chuột phải project `MiniSupermarket.API` → Set as Startup Project.
Chạy bằng launch profile `http` (cổng `7049`) để khớp với địa chỉ WinForms đang gọi, rồi nhấn F5. Trình duyệt tự mở Swagger UI.
Trong Swagger, gọi `POST /api/auth/login` với tài khoản mẫu để lấy Token, bấm Authorize và dán `Bearer <token>` để thử các API nhóm hàng và khách hàng.
Bước 3: Chạy Frontend (WinForms Client)
Đảm bảo địa chỉ `http://localhost:7049/api/` trong `FormLogin`, `FormCategoryManagement`, `FormCustomerManagement` và `SessionManager` khớp với cổng Web API đang chạy.
Chuột phải project `MiniSupermarket.WinForms` → Debug → Start new instance.
Đăng nhập bằng `admin`/`123456` hoặc `cashier`/`123456`.
Thử nghiệm các chức năng: tải danh sách, thêm, sửa, xóa, tìm kiếm.
Đăng nhập bằng `cashier` rồi thử xóa khách hàng để kiểm chứng cơ chế phân quyền (nhận lỗi `403`).
Kịch bản kiểm thử nhanh phần Khách hàng
`GET /api/customers` trả về 3 khách hàng mẫu.
`GET /api/customers/search?keyword=0901` trả về khách hàng Nguyễn Văn An.
`POST /api/customers` thêm khách hàng mới không có `rewardPoints` và `membershipRank`, kết quả tự có `0` điểm và hạng `Chuẩn`.
`DELETE /api/customers/{id}` bằng token `cashier` trả `403`, bằng token `admin` trả `204`.
---
🧯 8. Xử lý sự cố thường gặp
Hiện tượng	Cách xử lý
`Add-Migration` báo `Build failed`	Mở View → Error List, sửa các lỗi `error` rồi Rebuild Solution trước khi chạy lại.
Không thấy bảng `Customers`	Chạy `Get-Migration` kiểm tra migration `AddCustomersTable` đã `Applied` chưa; nếu chưa thì chạy `Update-Database`. Sau đó Refresh trong SQL Server Object Explorer.
WinForms báo lỗi kết nối Server	Kiểm tra Web API đang chạy và cổng `7049` khớp với địa chỉ trong code WinForms (dùng profile `http`).
Lỗi `401 Unauthorized`	Chưa đăng nhập hoặc Token hết hạn (2 giờ), cần đăng nhập lại.
Lỗi `403 Forbidden`	Tài khoản không đủ quyền (ví dụ `cashier` thực hiện thao tác chỉ dành cho Admin).
---
👨‍💻 9. Tác giả
Họ tên sinh viên: Nguyễn Lê Chí Công
Mã sinh viên: 2124110094
Lớp học phần: CCQ2411C