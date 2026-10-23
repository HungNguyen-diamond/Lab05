using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace Lab05_MultiForm
{
    public class frmSinhVien : Form
    {
        private readonly BindingList<SinhVien> _ds;
        private readonly BindingList<Khoa> _dsKhoa;

        private TextBox txtMaSV, txtHoTen, txtDiemTB;
        private ComboBox cboKhoa;
        private Label lblXemTruoc, lblTrangThai;
        private Button btnLuu, btnChiTiet, btnTimKiem, btnLamMoi, btnChonKhoa;
        private ListBox lstSinhVien;
        private ErrorProvider errorProvider1;

        public frmSinhVien() : this(new BindingList<SinhVien>(), new BindingList<Khoa>()) { }

        public frmSinhVien(BindingList<SinhVien> ds, BindingList<Khoa> dsKhoa)
        {
            _ds = ds;
            _dsKhoa = dsKhoa;
            InitializeUI();
        }

        private void InitializeUI()
        {
            Text = "Nhập thông tin sinh viên";
            ClientSize = new Size(600, 440);
            errorProvider1 = new ErrorProvider();

            txtMaSV = UI.Txt(120, 20);
            txtHoTen = UI.Txt(120, 55, 300);
            cboKhoa = new ComboBox { Left = 120, Top = 90, Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };
            btnChonKhoa = UI.Btn("Chọn khoa...", 330, 88, 110);
            txtDiemTB = UI.Txt(120, 125, 100);
            lblXemTruoc = new Label { Left = 20, Top = 165, Width = 560, Font = new Font(Font, FontStyle.Bold) };
            lblTrangThai = new Label { Left = 20, Top = 190, Width = 560 };
            btnLuu = UI.Btn("Lưu", 20, 225);
            btnChiTiet = UI.Btn("Xem chi tiết", 130, 225);
            btnTimKiem = UI.Btn("Tìm kiếm...", 240, 225);
            btnLamMoi = UI.Btn("Làm mới", 350, 225);
            lstSinhVien = new ListBox { Left = 20, Top = 265, Width = 560, Height = 160 };

            Controls.AddRange(new Control[] {
            UI.Lbl("Mã sinh viên", 20, 20), txtMaSV,
            UI.Lbl("Họ tên", 20, 55), txtHoTen,
            UI.Lbl("Khoa", 20, 90), cboKhoa, btnChonKhoa,
            UI.Lbl("Điểm TB (0-10)", 20, 125), txtDiemTB,
            lblXemTruoc, lblTrangThai, btnLuu, btnChiTiet, btnTimKiem, btnLamMoi, lstSinhVien });

            // Bài 3: dùng chung một handler cho nhiều control
            txtMaSV.TextChanged += Input_TextChanged;
            txtHoTen.TextChanged += Input_TextChanged;
            txtDiemTB.TextChanged += Input_TextChanged;
            cboKhoa.SelectedIndexChanged += Input_TextChanged;
            // Bài 2
            txtDiemTB.KeyPress += txtDiemTB_KeyPress;

            btnLuu.Click += btnLuu_Click;
            btnChiTiet.Click += btnChiTiet_Click;
            btnTimKiem.Click += btnTimKiem_Click;
            btnChonKhoa.Click += btnChonKhoa_Click;
            btnLamMoi.Click += (s, e) => ClearInput();

            cboKhoa.DataSource = _dsKhoa;   // danh sách khoa tự cập nhật khi frmKhoa thêm mới
            cboKhoa.SelectedIndex = -1;
            lstSinhVien.DataSource = _ds;
            Input_TextChanged(null, EventArgs.Empty);
        }

        private static bool TryParseDiem(string s, out double diem) =>
            double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out diem);

        // ===== Bài 1: ErrorProvider =====
        private bool ValidateInput()
        {
            errorProvider1.Clear();
            bool valid = true;

            if (string.IsNullOrWhiteSpace(txtMaSV.Text))
            {
                errorProvider1.SetError(txtMaSV, "Nhập mã sinh viên");
                valid = false;
            }
            else if (_ds.Any(s => s.MaSV.Equals(txtMaSV.Text.Trim(), StringComparison.OrdinalIgnoreCase)))
            {
                errorProvider1.SetError(txtMaSV, "Mã sinh viên đã tồn tại");
                valid = false;
            }

            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                errorProvider1.SetError(txtHoTen, "Nhập họ tên");
                valid = false;
            }
            if (cboKhoa.SelectedItem == null)
            {
                errorProvider1.SetError(cboKhoa, "Chọn khoa");
                valid = false;
            }
            // Bài 2: vẫn phải kiểm tra miền [0,10] khi lưu
            if (!TryParseDiem(txtDiemTB.Text, out double d) || d < 0 || d > 10)
            {
                errorProvider1.SetError(txtDiemTB, "Điểm phải là số trong khoảng [0, 10]");
                valid = false;
            }
            return valid;
        }

        // ===== Bài 2: giới hạn ký tự =====
        private void txtDiemTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;
            if (char.IsDigit(e.KeyChar)) return;
            if (e.KeyChar == '.' && !txtDiemTB.Text.Contains('.')) return;
            e.Handled = true;
        }

        // ===== Bài 3: cập nhật thời gian thực =====
        private void Input_TextChanged(object sender, EventArgs e)
        {
            if (sender is Control c) errorProvider1.SetError(c, "");

            bool hopLe = TryParseDiem(txtDiemTB.Text, out double diem) && diem >= 0 && diem <= 10;
            lblXemTruoc.Text = txtMaSV.Text + " - " + txtHoTen.Text
                               + (cboKhoa.SelectedItem != null ? " - " + cboKhoa.SelectedItem : "");
            lblTrangThai.Text = hopLe ? "Dữ liệu điểm hợp lệ" : "Kiểm tra điểm";
            lblTrangThai.ForeColor = hopLe ? Color.Green : Color.Firebrick;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;
            TryParseDiem(txtDiemTB.Text, out double diem);
            _ds.Add(new SinhVien
            {
                MaSV = txtMaSV.Text.Trim(),
                HoTen = txtHoTen.Text.Trim(),
                TenKhoa = ((Khoa)cboKhoa.SelectedItem).TenKhoa,
                DiemTB = diem
            });
            MessageBox.Show("Đã lưu sinh viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearInput();
        }

        private void ClearInput()
        {
            errorProvider1.Clear();
            txtMaSV.Clear(); txtHoTen.Clear(); txtDiemTB.Clear();
            cboKhoa.SelectedIndex = -1;
            txtMaSV.Focus();
        }

        // ===== Bài 4: truyền đối tượng qua constructor =====
        private void btnChiTiet_Click(object sender, EventArgs e)
        {
            if (lstSinhVien.SelectedItem is not SinhVien sv)
            {
                MessageBox.Show("Hãy chọn một sinh viên trong danh sách.");
                return;
            }
            using var f = new frmChiTietSinhVien(sv);
            f.ShowDialog(this);
        }

        // ===== Bài 7: nhận kết quả qua property + DialogResult =====
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            using var f = new frmTimKiem(_ds);
            if (f.ShowDialog(this) == DialogResult.OK && f.SinhVienChon != null)
            {
                lstSinhVien.SelectedItem = f.SinhVienChon;
                txtMaSV.Text = f.SinhVienChon.MaSV;
                txtHoTen.Text = f.SinhVienChon.HoTen;
                txtDiemTB.Text = f.SinhVienChon.DiemTB.ToString(CultureInfo.InvariantCulture);
            }
        }

        // ===== Bài 8: truyền danh sách khoa, nhận khoa được chọn =====
        private void btnChonKhoa_Click(object sender, EventArgs e)
        {
            using var f = new frmChonKhoa(_dsKhoa);
            if (f.ShowDialog(this) == DialogResult.OK && f.KhoaChon != null)
                cboKhoa.SelectedItem = f.KhoaChon;
        }
    }
}
