using System;
using System.Drawing;
using System.Windows.Forms;
using MusicLibrary.Data;

namespace MusicLibrary.Forms
{
    class LoginForm : Form
    {
        TextBox txtUsername = new TextBox();
        TextBox txtPassword = new TextBox();
        Button btnLogin = new Button();
        Button btnGoRegister = new Button();
        Label lblTitle = new Label();

        public LoginForm()
        {
            Text = "Вхід";
            Size = new Size(320, 250);
            StartPosition = FormStartPosition.CenterScreen;

            lblTitle.Text = "🎵 MUSIC LIBRARY 🎵";
            lblTitle.Font = new Font("Arial", 14, FontStyle.Bold);
            lblTitle.Location = new Point(30, 15);
            lblTitle.Size = new Size(260, 30);
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;

            var lblUser = new Label { Text = "Логін:", Location = new Point(30, 60), Size = new Size(60, 25) };
            txtUsername.Location = new Point(100, 58);
            txtUsername.Size = new Size(170, 25);

            var lblPass = new Label { Text = "Пароль:", Location = new Point(30, 95), Size = new Size(60, 25) };
            txtPassword.Location = new Point(100, 93);
            txtPassword.Size = new Size(170,58);
            txtPassword.PasswordChar = '*';

            btnLogin.Text = "Вхід";
            btnLogin.Location = new Point(100, 130);
            btnLogin.Size = new Size(170, 30);
            btnLogin.Click += BtnLogin_Click;;
            btnGoRegister.Text = "Реестрація";
            btnGoRegister.Location = new Point(100, 168);
            btnGoRegister.Size = new Size(170, 25);
            btnGoRegister.Click += (s, e) => new RegisterForm().ShowDialog();

            Controls.AddRange(new Control[] { lblTitle, lblUser, txtUsername, lblPass, txtPassword, btnLogin, btnGoRegister });
        }

        void BtnLogin_Click(object? sender, EventArgs e)
        {
            string user = txtUsername.Text.Trim();
            string pass = txtPassword.Text;

            if (Database.Users.ContainsKey(user) && Database.Users[user] == pass)
            {
                MessageBox.Show("Ласкаво просимо, " + user + "!", "Вітаю в халупі");
                Hide();
                new MainForm(user).ShowDialog();
                Show();
            }
            else
            {
                MessageBox.Show("❌Невірний логін або пароль❌", "ПОМІЛКА");
            }
        }
    }
}