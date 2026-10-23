using System;
using System.Collections.Generic;
using System.Text;

namespace Lab05_MultiForm
{
    public class frmChonKhoa : Form
    {
        private ListBox lstKhoa;
        public Khoa KhoaChon { get; private set; }

        public frmChonKhoa(IEnumerable<Khoa> dsKhoa)
        {
            Text = "Chọn khoa";
            ClientSize = new Size(320, 280);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false; MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;

            lstKhoa = new ListBox { Left = 20, Top = 20, Width = 280, Height = 200 };
            lstKhoa.Items.AddRange(dsKhoa.Cast<object>().ToArray());
            var btnOk = UI.Btn("OK", 120, 235, 80);
            var btnHuy = UI.Btn("Hủy", 215, 235, 80);
            btnHuy.DialogResult = DialogResult.Cancel;
            CancelButton = btnHuy;
            Controls.AddRange(new Control[] { lstKhoa, btnOk, btnHuy });

            btnOk.Click += (s, e) => Xong();
            lstKhoa.DoubleClick += (s, e) => Xong();
        }

        private void Xong()
        {
            if (lstKhoa.SelectedItem is Khoa k)
            {
                KhoaChon = k;
                DialogResult = DialogResult.OK;
            }
            else MessageBox.Show("Hãy chọn một khoa.");
        }
    }
}
