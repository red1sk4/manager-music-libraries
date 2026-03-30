using System.Collections.Generic;
using MusicLibrary.Models;

namespace MusicLibrary.Data
{
    static class Database
    {
        public static Dictionary<string, string> Users = new Dictionary<string, string>();
        public static List<Song> Songs = new List<Song>();
    }
}
