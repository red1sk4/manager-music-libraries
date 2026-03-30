using System;
using System.Collections.Generic;
using System.Linq;
using MusicLibrary.Models;
using MusicLibrary.Data;

namespace MusicLibrary
{
    public class TodoService
    {
        private static int _nextId = 1;
        private static readonly List<TodoItem> _items = new List<TodoItem>();

        public TodoItem Add(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty", nameof(title));

            var item = new TodoItem { Id = _nextId++, Title = title.Trim() };
            _items.Add(item);
            return item;
        }

        public bool Delete(int id)
        {
            var existing = _items.FirstOrDefault(i => i.Id == id);
            if (existing == null) return false;
            _items.Remove(existing);
            return true;
        }

        public TodoItem MarkDone(int id)
        {
            if (id <= 0) throw new ArgumentException("Invalid id", nameof(id));
            var existing = _items.FirstOrDefault(i => i.Id == id);
            if (existing == null) throw new ArgumentException("Item not found", nameof(id));
            existing.IsDone = true;
            return existing;
        }
    }
}
