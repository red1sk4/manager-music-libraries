using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using MusicLibrary.Models;

namespace MusicLibrary.Data
{
    static class Storage
    {
        static string usersFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "users.json");
        static string songsFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "songs.json");

        public static void Save()
        {
            File.WriteAllText(usersFile, JsonSerializer.Serialize(Database.Users));
            File.WriteAllText(songsFile, JsonSerializer.Serialize(Database.Songs));
        }

        public static void Load()
        {
            if (File.Exists(usersFile))
            {
                var users = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(usersFile));
                if (users != null) Database.Users = users;
            }

            if (File.Exists(songsFile))
            {
                var songs = JsonSerializer.Deserialize<List<Song>>(File.ReadAllText(songsFile));
                if (songs != null) Database.Songs = songs;
            }
        }
    }
}