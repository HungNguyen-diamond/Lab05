using System;
using System.Collections.Generic;
using System.Text;

namespace Lab05_MultiForm
{
    public class frmTimKiem : Form
    {
        private readonly IEnumerable<SinhVien> _nguon;
        private TextBox txtTuKhoa;
        private ListBox lstKetQua;
        private Button btnChon, btnDong;

        public string TuKhoa => txtTuKhoa.Text.Trim();
        public SinhVien SinhVienChon { get; private set; }

        public frmTimKiem(IEnumerable<SinhVien> nguon, string tuKhoaBanDau = "")
        {
            _nguon = nguon;
            Text = "Tìm kiếm sinh viên";
            ClientSize = new Size(460, 300);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false; MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;

            txtTuKhoa = UI.Txt(120, 15, 350);
            lstKetQua = new ListBox { Left = 20, Top = 50, Width = 420, Height = 190 };
            btnChon = UI.Btn("Chọn", 250, 255, 90);
            btnDong = UI.Btn("Đóng", 350, 255, 90);
            btnDong.DialogResult = DialogResult.Cancel;
            CancelButton = btnDong;
            Controls.AddRange(new Control[] { UI.Lbl("Từ khóa", 20, 15), txtTuKhoa, lstKetQua, btnChon, btnDong });

            txtTuKhoa.TextChanged += (s, e) => Loc();
            lstKetQua.DoubleClick += (s, e) => ChonKetQua();
            btnChon.Click += (s, e) => ChonKetQua();

            txtTuKhoa.Text = tuKhoaBanDau;
            Loc();
        }

        private void Loc()
        {
            string k = TuKhoa;
            lstKetQua.DataSource = _nguon
                .Where(s => k == "" ||
                            s.MaSV.Contains(k, StringComparison.OrdinalIgnoreCase) ||
                            s.HoTen.Contains(k, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        private void ChonKetQua()
        {
            if (lstKetQua.SelectedItem is SinhVien sv)
            {
                SinhVienChon = sv;
                DialogResult = DialogResult.OK;
            }
            else MessageBox.Show("Chưa có kết quả nào được chọn.");
        }
    }
}
