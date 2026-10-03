using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Data
{
    // DbContext đại diện cho phiên làm việc với cơ sở dữ liệu SQL Server
    public class SupermarketDbContext : DbContext
    {
        public SupermarketDbContext(DbContextOptions<SupermarketDbContext> options) : base(options) { }

        // Khai báo các bảng dữ liệu ánh xạ từ Model
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }

        // Cấu hình dữ liệu mồi ban đầu (Data Seeding)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Nạp sẵn danh mục ban đầu vào SQL Server ngay khi tạo bảng
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Bánh kẹo & Đồ ăn vặt", Description = "Snack, bánh quy, kẹo dẻo, hạt khô" },
                new Category { CategoryId = 2, CategoryName = "Nước giải khát & Trà", Description = "Nước ngọt, nước khoáng, trà đóng chai" },
                new Category { CategoryId = 3, CategoryName = "Sữa & Sản phẩm từ sữa", Description = "Sữa tươi, sữa chua, phô mai, bơ" },
                new Category { CategoryId = 4, CategoryName = "Mì gói & Thực phẩm ăn liền", Description = "Mì ăn liền, phở khô, cháo gói, hủ tiếu" },
                new Category { CategoryId = 5, CategoryName = "Gia vị & Dầu ăn", Description = "Nước mắm, hạt nêm, dầu thực vật, nước tương" },
                new Category { CategoryId = 6, CategoryName = "Đồ đóng hộp & Chế biến sẵn", Description = "Cá hộp, thịt hộp, xúc xích, pate" },
                new Category { CategoryId = 7, CategoryName = "Gạo & Các loại ngũ cốc", Description = "Gạo tẻ, gạo nếp, đậu xanh, yến mạch" },
                new Category { CategoryId = 8, CategoryName = "Thực phẩm tươi sống", Description = "Thịt heo, thịt bò, gia cầm, hải sản, trứng" },
                new Category { CategoryId = 9, CategoryName = "Rau củ & Trái cây tươi", Description = "Rau xanh, củ quả, trái cây theo mùa" },
                new Category { CategoryId = 10, CategoryName = "Thực phẩm đông lạnh", Description = "Cá viên, bò viên, chả giò, bánh bao đông lạnh" },
                new Category { CategoryId = 11, CategoryName = "Kem & Đồ lạnh", Description = "Kem cây, kem hộp, đá viên đóng túi" },
                new Category { CategoryId = 12, CategoryName = "Cà phê & Nước tăng lực", Description = "Cà phê hòa tan, cà phê lon, nước tăng lực" },
                new Category { CategoryId = 13, CategoryName = "Bia & Đồ uống có cồn", Description = "Bia lon, rượu vang, nước trái cây lên men" },
                new Category { CategoryId = 14, CategoryName = "Chăm sóc cá nhân", Description = "Sữa tắm, dầu gội, kem đánh răng, xà phòng" },
                new Category { CategoryId = 15, CategoryName = "Mỹ phẩm & Dưỡng da", Description = "Sữa rửa mặt, kem chống nắng, tẩy trang, mặt nạ" },
                new Category { CategoryId = 16, CategoryName = "Chăm sóc nhà cửa", Description = "Nước rửa chén, nước lau nhà, nước giặt, xả vải" },
                new Category { CategoryId = 17, CategoryName = "Giấy & Màng bọc", Description = "Giấy vệ sinh, khăn giấy ăn, màng bọc thực phẩm, túi zip" },
                new Category { CategoryId = 18, CategoryName = "Dụng cụ vệ sinh & Rác", Description = "Bàn chải, khăn lau, túi đựng rác, miếng rửa bát" },
                new Category { CategoryId = 19, CategoryName = "Đồ dùng gia đình & Bếp", Description = "Nồi chảo, hộp đựng thực phẩm, ly nhựa, đũa muỗng" },
                new Category { CategoryId = 20, CategoryName = "Văn phòng phẩm", Description = "Bút viết, tập học sinh, băng keo, kéo, kẹp giấy" },
                new Category { CategoryId = 21, CategoryName = "Mẹ & Bé", Description = "Tã bỉm, sữa bột, ăn dầm, khăn ướt em bé" },
                new Category { CategoryId = 22, CategoryName = "Chăm sóc thú cưng", Description = "Thức ăn cho chó mèo, cát vệ sinh, phụ kiện" },
                new Category { CategoryId = 23, CategoryName = "Đồ dùng cá nhân & Phụ kiện", Description = "Khẩu trang, áo mưa, ô dù, pin gia dụng" },
                new Category { CategoryId = 24, CategoryName = "Bánh mì & Bánh tươi", Description = "Bánh mì sandwich, bánh ngọt tươi, bánh bao nóng" },
                new Category { CategoryId = 25, CategoryName = "Sản phẩm y tế cơ bản", Description = "Băng cá nhân, nước muối sinh lý, dán hạ sốt, cồn y tế" }
            );

            // Giá trị mặc định cho bảng Customers
            modelBuilder.Entity<Customer>().Property(c => c.RewardPoints).HasDefaultValue(0);
            modelBuilder.Entity<Customer>().Property(c => c.MembershipRank).HasDefaultValue("Chuẩn");

            // Nạp sẵn 3 khách hàng mẫu
            modelBuilder.Entity<Customer>().HasData(
                new Customer { CustomerId = 1, CustomerName = "Nguyễn Văn An", PhoneNumber = "0901234567", Address = "12 Lê Lợi, Q.1, TP.HCM", RewardPoints = 150, MembershipRank = "Vàng" },
                new Customer { CustomerId = 2, CustomerName = "Trần Thị Bình", PhoneNumber = "0912345678", Address = "45 Nguyễn Trãi, Q.5, TP.HCM", RewardPoints = 40, MembershipRank = "Bạc" },
                new Customer { CustomerId = 3, CustomerName = "Lê Hoàng Cường", PhoneNumber = "0987654321", Address = "78 CMT8, Q.3, TP.HCM", RewardPoints = 0, MembershipRank = "Chuẩn" }
            );
        }
    }
}
