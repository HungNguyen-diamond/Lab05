namespace Lab05_MultiForm
{
    internal static class Program
    {
  
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            string user;
            using (var login = new frmLogin())
            {
                if (login.ShowDialog() != DialogResult.OK) return;
                user = login.TenDangNhap;
            }
            Application.Run(new frmMain(user));
        }
    }
}