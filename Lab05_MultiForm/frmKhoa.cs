using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Lab05_MultiForm
{
    public class frmKhoa : Form
    {
        private readonly BindingList<Khoa> _dsKhoa;
        private TextBox txtMa, txtTen;
        private ListBox lstKhoa;
        private ErrorProvider errorProvider1 = new ErrorProvider();

        public frmKhoa(BindingList<Khoa> dsKhoa)
        {
            _dsKhoa = dsKhoa;
            Text = "Danh mục khoa";
            ClientSize = new Size(400, 330);

            txtMa = UI.Txt(120, 20, 120);
            txtTen = UI.Txt(120, 55, 250);
            var btnThem = UI.Btn("Thêm", 120, 90, 90);
            lstKhoa = new ListBox { Left = 20, Top = 130, Width = 360, Height = 180, DataSource = _dsKhoa };
            Controls.AddRange(new Control[] {
            UI.Lbl("Mã khoa", 20, 20), txtMa, UI.Lbl("Tên khoa", 20, 55), txtTen, btnThem, lstKhoa });

            btnThem.Click += (s, e) =>
            {
                errorProvider1.Clear();
                bool ok = true;
                if (string.IsNullOrWhiteSpace(txtMa.Text)) { errorProvider1.SetError(txtMa, "Nhập mã khoa"); ok = false; }
                if (string.IsNullOrWhiteSpace(txtTen.Text)) { errorProvider1.SetError(txtTen, "Nhập tên khoa"); ok = false; }
                if (!ok) return;
                _dsKhoa.Add(new Khoa { MaKhoa = txtMa.Text.Trim(), TenKhoa = txtTen.Text.Trim() });
                txtMa.Clear(); txtTen.Clear();
            };
        }
    }
}
