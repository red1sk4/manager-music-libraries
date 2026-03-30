using System;
using System.Drawing;
using System.Windows.Forms;
using MusicLibrary.Data;
using MusicLibrary.Models;

namespace MusicLibrary.Forms
{
    class MainForm : Form
    {
        ListView listView = new ListView();
        TextBox txtTitle = new TextBox();
        TextBox txtArtist = new TextBox();
        Button btnAdd = new Button();
        Button btnLike = new Button();
        Button btnBlacklist = new Button();
        Button btnDelete = new Button();
        Label lblFilter = new Label();
        ComboBox cmbFilter = new ComboBox();

        public MainForm(string username)
        {
            Text = "LIBRARY" + username;
            Size = new Size(620, 500);
            StartPosition = FormStartPosition.CenterScreen;

            var lblTitle = new Label { Text = "Назва треку",Location = new Point(10, 15), Size = new Size(55, 22) };
            txtTitle.Location = new Point(70, 13);
            txtTitle.Size = new Size(150, 22);

            var lblArtist = new Label { Text = "Виконавець:", Location = new Point(10, 45), Size = new Size(80, 22) };
            txtArtist.Location = new Point(95, 43);
            txtArtist.Size = new Size(125, 22);

            btnAdd.Text = "Додати пісню";
            btnAdd.Location = new Point(10, 75);
            btnAdd.Size = new Size(210, 28);
            btnAdd.BackColor = Color.LightGreen;
            btnAdd.Click += BtnAdd_Click;

            btnLike.Text = "❤ ";
            btnLike.Location = new Point(10, 120);
            btnLike.Size = new Size(100, 28);
            btnLike.BackColor = Color.LightPink;
            btnLike.Click += BtnLike_Click;

            btnBlacklist.Text = "🚫";
            btnBlacklist.Location = new Point(120, 120);
            btnBlacklist.Size = new Size(100, 28);
            btnBlacklist.BackColor = Color.LightGray;
            btnBlacklist.Click += BtnBlacklist_Click;

            btnDelete.Text = "🗑";
            btnDelete.Location = new Point(10, 158);
            btnDelete.Size = new Size(210, 28);
            btnDelete.BackColor = Color.IndianRed;
            btnDelete.ForeColor = Color.White;
            btnDelete.Click += BtnDelete_Click;

            lblFilter.Text = "Фільтр:";
            lblFilter.Location = new Point(10, 200);
            lblFilter.Size = new Size(55, 22);

            cmbFilter.Location = new Point(70, 198);
            cmbFilter.Size = new Size(150, 22);
            cmbFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFilter.Items.AddRange(new object[] { "Всі", "Лайкнуті", "Чорний список" });
            cmbFilter.SelectedIndex = 0;
            cmbFilter.SelectedIndexChanged += (s, e) => RefreshList();

            listView.Location = new Point(240, 10);
            listView.Size = new Size(355, 440);
            listView.View = View.Details;
            listView.FullRowSelect = true;
            listView.GridLines = true;
            listView.Columns.Add("Назва", 120);
            listView.Columns.Add("Виконавець", 100);
            listView.Columns.Add("❤", 40);
            listView.Columns.Add("🚫", 40);

            Controls.AddRange(new Control[] {
                lblTitle, txtTitle, lblArtist, txtArtist,
                btnAdd, btnLike, btnBlacklist, btnDelete,
                lblFilter, cmbFilter, listView
            });

            RefreshList();
        }

        void BtnAdd_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text) || string.IsNullOrEmpty(txtArtist.Text))
            {
                MessageBox.Show("Введіть назву і виконавця!", "Помилка");
                return;
            }
            Database.Songs.Add(new Song { Title = txtTitle.Text.Trim(), Artist = txtArtist.Text.Trim() });
            txtTitle.Clear();
            txtArtist.Clear();
            RefreshList();
            Storage.Save();
        }

        void BtnLike_Click(object? sender, EventArgs e)
        {
            var song = GetSelected();
            if (song == null) return;
            song.IsLiked = !song.IsLiked;
            RefreshList();
            Storage.Save();
        }

        void BtnBlacklist_Click(object? sender, EventArgs e)
        {
            var song = GetSelected();
            if (song == null) return;
            song.IsBlacklisted = !song.IsBlacklisted;
            RefreshList();
            Storage.Save();
        }

        void BtnDelete_Click(object? sender, EventArgs e)
        {
            var song = GetSelected();
            if (song == null) return;
            Database.Songs.Remove(song);
            RefreshList();
            Storage.Save();
        }

        Song? GetSelected()
        {
            if (listView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Оберіть пісню!", "Увага");
                return null;
            }
            return listView.SelectedItems[0].Tag as Song;
        }

        void RefreshList()
        {
            listView.Items.Clear();
            foreach (var song in Database.Songs)
            {
                string? filter = cmbFilter.SelectedItem?.ToString();
                if (filter == "Лайкнуті" && !song.IsLiked) continue;
                if (filter == "Чорний список" && !song.IsBlacklisted) continue;

                var item = new ListViewItem(song.Title);
                item.SubItems.Add(song.Artist);
                item.SubItems.Add(song.IsLiked ? "❤" : "");
                item.SubItems.Add(song.IsBlacklisted ? "🚫" : "");
                item.Tag = song;

                if (song.IsBlacklisted) item.BackColor = Color.LightGray;
                else if (song.IsLiked) item.BackColor = Color.MistyRose;

                listView.Items.Add(item);
            }
        }
    }
}