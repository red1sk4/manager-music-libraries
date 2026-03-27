using System;
using System.Drawing;
using System.Windows.Forms;
using MusicLibrary.Data;

namespace MusicLibrary.Forms
{
    class RegisterForm : Form
    {
        TextBox txtUsername = new TextBox();
        TextBox txtPassword = new TextBox();
        Button btnRegister = new Button();

        public RegisterForm()
        {
            Text = "Реєстрація нового акаунту ";
            Size = new Size(300, 200);
            StartPosition = FormStartPosition.CenterScreen;

            var lblTitle = new Label { Text = "Новий Аккаунт", Font = new Font("Arial", 12, FontStyle.Bold), Location = new Point(80, 10), Size = new Size(150, 25), TextAlign = ContentAlignment.MiddleCenter };
            var lblUser = new Label { Text = "Логін:", Location = new Point(20, 50), Size = new Size(60, 25) };
            txtUsername.Location = new Point(90, 48);
            txtUsername.Size = new Size(170, 25);

            var lblPass = new Label { Text = "Пароль:", Location = new Point(20, 85), Size = new Size(65, 25) };
            txtPassword.Location = new Point(90, 83);
            txtPassword.Size = new Size(170, 25);
            txtPassword.PasswordChar = 'x';

            btnRegister.Text = "Ну давай, давай";
            btnRegister.Location = new Point(90, 120);
            btnRegister.Size = new Size(170, 30);
            btnRegister.Click += BtnRegister_Click;

            Controls.AddRange(new Control[] { lblTitle, lblUser, txtUsername, lblPass, txtPassword, btnRegister });
        }

        void BtnRegister_Click(object? sender, EventArgs e)
        {
            string user = txtUsername.Text.Trim();
            string pass = txtPassword.Text;

            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Заповніть всі поля!", "Помилка");
                return;
            }
            if (Database.Users.ContainsKey(user))
            {
                MessageBox.Show("Такий користувач вже існує!", "Помилка");
                return;
            }

            Database.Users[user] = pass;
            Storage.Save();
            MessageBox.Show("Реєстрація успішна!", "Успіх");
            Close();
        }
    }
}