LAB 5. XỬ LÝ SỰ KIỆN, KIỂM TRA DỮ LIỆU VÀ ỨNG DỤNG ĐA FORM
Học phần: Lập trình .NET (IT3212)

Sinh viên: Nguyễn Tiến Hùng
Mã sinh viên: 24000091
Lớp: Lập trình .NET-1-1-26(N03)

1. CÁCH CHẠY
- Công cụ: Visual Studio, project Windows Forms App (C#, .NET 10).
- Mở file Lab05_MultiForm.sln bằng Visual Studio.
- Chọn Build > Rebuild Solution, sau đó nhấn F5 để chạy.
- Giao diện được tạo bằng code trong từng Form nên không có file Designer.

2. DỮ LIỆU THỬ
- Tài khoản đăng nhập: admin / 123456
- Sinh viên có sẵn: SV001 - Nguyễn Văn An, SV002 - Trần Thị Bình, SV003 - Lê Minh Cường
- Khoa có sẵn: Công nghệ thông tin, Kinh tế, Điện - Điện tử
- Có thể thêm khoa mới ở menu Danh mục > Khoa, thêm sinh viên ở Danh mục > Sinh viên.

3. CẤU TRÚC MÃ NGUỒN
- Program.cs: điểm vào, hiển thị Form đăng nhập rồi mở Form chính
- Models.cs: lớp SinhVien, Khoa và lớp UI (hàm dùng chung tạo control)
- frmLogin.cs: Form đăng nhập
- frmMain.cs: Form chính (MDI, MenuStrip, StatusStrip, đồng hồ)
- frmSinhVien.cs: nhập sinh viên, kiểm tra dữ liệu, danh sách sinh viên
- frmChiTietSinhVien.cs: xem chi tiết sinh viên
- frmTimKiem.cs: hộp thoại tìm kiếm
- frmChonKhoa.cs: Form chọn khoa
- frmKhoa.cs: danh mục khoa
- frmGioiThieu.cs: Form giới thiệu

4. CÁC BÀI ĐÃ HOÀN THÀNH
Bài 1. Kiểm tra bắt buộc bằng ErrorProvider
  Phương thức ValidateInput() trong frmSinhVien. Không cho lưu khi mã hoặc họ tên
  trống; báo lỗi cạnh control. Kiểm tra thêm: chưa chọn khoa, điểm ngoài [0,10],
  trùng mã sinh viên.

Bài 2. Giới hạn ký tự nhập điểm
  Sự kiện KeyPress của txtDiemTB chỉ cho nhập chữ số và một dấu thập phân.
  Miền [0,10] vẫn được kiểm tra khi bấm Lưu.

Bài 3. Cập nhật trạng thái theo thời gian thực
  Một handler Input_TextChanged dùng chung cho các ô mã, họ tên, điểm và combobox
  khoa; cập nhật Label xem trước và Label trạng thái hợp lệ.

Bài 4. Truyền dữ liệu sang Form chi tiết
  frmChiTietSinhVien nhận đối tượng SinhVien qua constructor và hiển thị dữ liệu.

Bài 5. MenuStrip mở các Form chức năng
  frmMain có menu Danh mục, Nghiệp vụ, Hệ thống. Hàm OpenChildForm() mở Form con
  dạng MDI và không mở trùng (Form đã mở sẽ được kích hoạt lại).

Bài 6. Form đăng nhập có giới hạn lần thử
  frmLogin: sai 3 lần thì khóa nút Đăng nhập trong 30 giây (Timer, có đếm ngược),
  hết thời gian thì mở khóa.

Bài 7. Hộp thoại tìm kiếm
  frmTimKiem nhận danh sách và từ khóa, lọc theo mã hoặc họ tên; trả kết quả về
  Form gọi qua property SinhVienChon và DialogResult.

Bài 8. Form chọn khoa
  frmChonKhoa nhận danh sách khoa qua constructor, trả về khoa được chọn qua
  property KhoaChon, không dùng biến toàn cục.

Bài 9. Xác nhận đóng ứng dụng
  Bắt sự kiện FormClosing của frmMain, hỏi xác nhận; chọn No thì hủy đóng
  (e.Cancel = true).

Bài 10. Dashboard đa Form
  frmMain có menu, thanh trạng thái (tên người dùng, đồng hồ cập nhật mỗi giây)
  và các Form chức năng: Sinh viên, Khoa, Giới thiệu.

5. GHI CHÚ
- Điểm được đọc bằng InvariantCulture (dấu thập phân là dấu chấm) để không bị
  sai trên máy dùng định dạng số Việt Nam.
- Danh sách sinh viên và khoa được lưu trong bộ nhớ nên sẽ mất khi đóng chương
  trình. Lab 6 sẽ hiển thị bằng DataGridView và tổ chức theo mô hình 3 lớp.