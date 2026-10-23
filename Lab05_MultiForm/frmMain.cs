using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Lab05_MultiForm
{
    public class frmMain : Form
    {
        private readonly BindingList<SinhVien> _dsSinhVien = new()
    {
        new SinhVien { MaSV = "SV001", HoTen = "Nguyễn Văn An", TenKhoa = "Công nghệ thông tin", DiemTB = 8.5 },
        new SinhVien { MaSV = "SV002", HoTen = "Trần Thị Bình", TenKhoa = "Kinh tế", DiemTB = 7.25 },
        new SinhVien { MaSV = "SV003", HoTen = "Lê Minh Cường", TenKhoa = "Điện - Điện tử", DiemTB = 9.0 },
    };
        private readonly BindingList<Khoa> _dsKhoa = new()
    {
        new Khoa { MaKhoa = "CNTT", TenKhoa = "Công nghệ thông tin" },
        new Khoa { MaKhoa = "KT", TenKhoa = "Kinh tế" },
        new Khoa { MaKhoa = "DDT", TenKhoa = "Điện - Điện tử" },
    };

        private MenuStrip menuStrip1;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblUser, lblClock;
        private System.Windows.Forms.Timer tmrClock;

        public frmMain(string tenDangNhap)
        {
            InitializeUI(tenDangNhap);
        }

        private void InitializeUI(string user)
        {
            Text = "Quản lý sinh viên - Lab 5";
            IsMdiContainer = true;
            WindowState = FormWindowState.Maximized;

            menuStrip1 = new MenuStrip();
            var mnuDanhMuc = new ToolStripMenuItem("Danh mục");
            var mnuNghiepVu = new ToolStripMenuItem("Nghiệp vụ");
            var mnuHeThong = new ToolStripMenuItem("Hệ thống");

            var mnuSinhVien = new ToolStripMenuItem("Sinh viên");
            var mnuKhoa = new ToolStripMenuItem("Khoa");
            var mnuTimKiem = new ToolStripMenuItem("Tìm kiếm sinh viên...");
            var mnuGioiThieu = new ToolStripMenuItem("Giới thiệu");
            var mnuThoat = new ToolStripMenuItem("Thoát");

            mnuDanhMuc.DropDownItems.AddRange(new ToolStripItem[] { mnuSinhVien, mnuKhoa });
            mnuNghiepVu.DropDownItems.Add(mnuTimKiem);
            mnuHeThong.DropDownItems.AddRange(new ToolStripItem[] { mnuGioiThieu, new ToolStripSeparator(), mnuThoat });
            menuStrip1.Items.AddRange(new ToolStripItem[] { mnuDanhMuc, mnuNghiepVu, mnuHeThong });
            MainMenuStrip = menuStrip1;

            statusStrip1 = new StatusStrip();
            lblUser = new ToolStripStatusLabel("Người dùng: " + user) { Spring = true, TextAlign = ContentAlignment.MiddleLeft };
            lblClock = new ToolStripStatusLabel();
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblUser, lblClock });

            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);

            mnuSinhVien.Click += mnuSinhVien_Click;
            mnuKhoa.Click += (s, e) => OpenChildForm(new frmKhoa(_dsKhoa));
            mnuTimKiem.Click += mnuTimKiem_Click;
            mnuGioiThieu.Click += (s, e) => { using var f = new frmGioiThieu(); f.ShowDialog(this); };
            mnuThoat.Click += (s, e) => Close();     // đi qua FormClosing để xác nhận

            // Bài 10: đồng hồ
            tmrClock = new System.Windows.Forms.Timer { Interval = 1000 };
            tmrClock.Tick += (s, e) => lblClock.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            tmrClock.Start();
            lblClock.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            FormClosing += frmMain_FormClosing;
        }

        // Bài 5: mở Form con, tránh mở trùng
        private void OpenChildForm(Form child)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f.GetType() == child.GetType())
                {
                    f.Activate();
                    child.Dispose();
                    return;
                }
            }
            child.MdiParent = this;
            child.Show();
        }

        private void mnuSinhVien_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmSinhVien(_dsSinhVien, _dsKhoa));
        }

        private void mnuTimKiem_Click(object sender, EventArgs e)
        {
            using var f = new frmTimKiem(_dsSinhVien);
            if (f.ShowDialog(this) == DialogResult.OK && f.SinhVienChon != null)
            {
                using var ct = new frmChiTietSinhVien(f.SinhVienChon);
                ct.ShowDialog(this);
            }
        }

        // Bài 9: xác nhận khi đóng ứng dụng
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown) return;
            var r = MessageBox.Show("Bạn có chắc muốn thoát ứng dụng?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.No) e.Cancel = true;
            else tmrClock.Stop();
        }
    }
}
