using System;
using System.Collections.Generic;
using System.Text;

namespace Lab05_MultiForm
{
    public class frmLogin : Form
    {
        private const int MaxLanThu = 3;
        private const int ThoiGianKhoa = 30;
        private int _soLanSai = 0;
        private int _giayConLai = 0;

        private TextBox txtUser, txtPass;
        private Button btnDangNhap, btnThoat;
        private Label lblThongBao;
        private ErrorProvider errorProvider1 = new ErrorProvider();
        private System.Windows.Forms.Timer tmrKhoa;

        public string TenDangNhap { get; private set; }

        public frmLogin()
        {
            Text = "Đăng nhập";
            ClientSize = new Size(360, 190);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false; MinimizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;

            txtUser = UI.Txt(120, 20, 200);
            txtPass = UI.Txt(120, 55, 200);
            txtPass.UseSystemPasswordChar = true;
            btnDangNhap = UI.Btn("Đăng nhập", 120, 95, 100);
            btnThoat = UI.Btn("Thoát", 230, 95, 90);
            lblThongBao = new Label { Left = 20, Top = 135, Width = 320, Height = 40, ForeColor = Color.Firebrick };
            Controls.AddRange(new Control[] {
            UI.Lbl("Tên đăng nhập", 20, 20), txtUser, UI.Lbl("Mật khẩu", 20, 55), txtPass,
            btnDangNhap, btnThoat, lblThongBao });

            AcceptButton = btnDangNhap;
            CancelButton = btnThoat;
            btnThoat.DialogResult = DialogResult.Cancel;
            btnDangNhap.Click += btnDangNhap_Click;

            tmrKhoa = new System.Windows.Forms.Timer { Interval = 1000 };
            tmrKhoa.Tick += tmrKhoa_Tick;
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (string.IsNullOrWhiteSpace(txtUser.Text))
            { errorProvider1.SetError(txtUser, "Nhập tên đăng nhập"); return; }
            if (string.IsNullOrEmpty(txtPass.Text))
            { errorProvider1.SetError(txtPass, "Nhập mật khẩu"); return; }

            // Tài khoản mẫu: admin / 123456
            if (txtUser.Text.Trim() == "admin" && txtPass.Text == "123456")
            {
                TenDangNhap = txtUser.Text.Trim();
                DialogResult = DialogResult.OK;
                return;
            }

            _soLanSai++;
            txtPass.Clear();
            if (_soLanSai >= MaxLanThu)
            {
                btnDangNhap.Enabled = false;
                _giayConLai = ThoiGianKhoa;
                lblThongBao.Text = $"Sai {MaxLanThu} lần. Thử lại sau {_giayConLai} giây.";
                tmrKhoa.Start();
            }
            else
            {
                lblThongBao.Text = $"Sai tên đăng nhập hoặc mật khẩu. Còn {MaxLanThu - _soLanSai} lần thử.";
                txtPass.Focus();
            }
        }

        private void tmrKhoa_Tick(object sender, EventArgs e)
        {
            _giayConLai--;
            if (_giayConLai <= 0)
            {
                tmrKhoa.Stop();
                btnDangNhap.Enabled = true;
                _soLanSai = 0;
                lblThongBao.Text = "Bạn có thể đăng nhập lại.";
            }
            else lblThongBao.Text = $"Sai {MaxLanThu} lần. Thử lại sau {_giayConLai} giây.";
        }
    }
}
