using System;
using System.Collections.Generic;
using System.Text;

namespace Lab05_MultiForm
{
    public class Khoa
    {
        public string MaKhoa { get; set; } = "";
        public string TenKhoa { get; set; } = "";
        public override string ToString() => TenKhoa;
    }

    public class SinhVien
    {
        public string MaSV { get; set; } = "";
        public string HoTen { get; set; } = "";
        public string TenKhoa { get; set; } = "";
        public double DiemTB { get; set; }
        public override string ToString() => $"{MaSV} - {HoTen} - {TenKhoa} - {DiemTB:N2}";
    }

    // Hàm dùng chung để tạo control nhanh (code UI thuần, không cần Designer)
    internal static class UI
    {
        public static Label Lbl(string t, int x, int y, int w = 100) =>
            new Label { Text = t, Left = x, Top = y + 3, Width = w, AutoSize = false };
        public static TextBox Txt(int x, int y, int w = 200) =>
            new TextBox { Left = x, Top = y, Width = w };
        public static Button Btn(string t, int x, int y, int w = 100) =>
            new Button { Text = t, Left = x, Top = y, Width = w, Height = 28 };
    }
}
