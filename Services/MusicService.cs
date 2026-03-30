using System;
using MusicLibrary.Models;
using MusicLibrary.Data;

namespace MusicLibrary
{
    public class MusicService
    {
        public Song AddTrack(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty", nameof(title));

            var song = new Song { Title = title.Trim() };
            Database.Songs.Add(song);
            return song;
        }
    }
}
