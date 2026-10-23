using System;
using System.Collections.Generic;
using System.Text;

namespace Lab05_MultiForm
{
    public class frmChiTietSinhVien : Form
    {
        private readonly SinhVien _sinhVien;
        private Label lblMa, lblHoTen, lblKhoa, lblDiem;

        public frmChiTietSinhVien(SinhVien sinhVien)
        {
            _sinhVien = sinhVien;
            InitializeUI();
            Load += frmChiTietSinhVien_Load;
        }

        private void InitializeUI()
        {
            Text = "Chi tiết sinh viên";
            ClientSize = new Size(360, 190);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false; MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;

            lblMa = new Label { Left = 120, Top = 23, Width = 220 };
            lblHoTen = new Label { Left = 120, Top = 58, Width = 220 };
            lblKhoa = new Label { Left = 120, Top = 93, Width = 220 };
            lblDiem = new Label { Left = 120, Top = 128, Width = 220 };
            var btnDong = UI.Btn("Đóng", 130, 155, 90);
            btnDong.DialogResult = DialogResult.OK;
            CancelButton = btnDong;

            Controls.AddRange(new Control[] {
            UI.Lbl("Mã SV:", 20, 20), lblMa, UI.Lbl("Họ tên:", 20, 55), lblHoTen,
            UI.Lbl("Khoa:", 20, 90), lblKhoa, UI.Lbl("Điểm TB:", 20, 125), lblDiem, btnDong });
        }

        private void frmChiTietSinhVien_Load(object sender, EventArgs e)
        {
            lblMa.Text = _sinhVien.MaSV;
            lblHoTen.Text = _sinhVien.HoTen;
            lblKhoa.Text = _sinhVien.TenKhoa;
            lblDiem.Text = _sinhVien.DiemTB.ToString("N2");
        }
    }
}
