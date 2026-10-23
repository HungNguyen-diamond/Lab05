using System;
using System.Collections.Generic;
using System.Text;

namespace Lab05_MultiForm
{
    public class frmGioiThieu : Form
    {
        public frmGioiThieu()
        {
            Text = "Giới thiệu";
            ClientSize = new Size(380, 190);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false; MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;

            var lbl = new Label
            {
                Left = 20,
                Top = 20,
                Width = 340,
                Height = 110,
                Text = "ỨNG DỤNG QUẢN LÝ SINH VIÊN\r\nHọc phần: Lập trình .NET (IT3212)\r\nLab 5 - Xử lý sự kiện, kiểm tra dữ liệu, đa Form\r\n\r\n" +
                       "Sinh viên: Nguyễn Tiến Hùng\r\nMã SV: 24000091 - Lớp: Lập trình .NET-1-1-26(N03)"
            };
            var btn = UI.Btn("Đóng", 140, 145, 100);
            btn.DialogResult = DialogResult.OK;
            AcceptButton = btn;
            Controls.AddRange(new Control[] { lbl, btn });
        }
    }
}
