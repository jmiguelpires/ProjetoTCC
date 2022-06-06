using ProjetoTCC.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dal
{
    public class AcessoDados<T> where T : new()
    {
        private readonly SQLiteAsyncConnection _database;

        public AcessoDados()
        {
            _database = new SQLiteAsyncConnection(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MiaumeApp.db3"));
            _database.CreateTableAsync<T>(CreateFlags.ImplicitPK).Wait();
        }

        public Task<List<T>> GetItemsAsync()
        {
            return _database.Table<T>().ToListAsync();
        }

        public Task<T> GetItemAsync()
        {
            return _database.Table<T>().FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(T item)
        {
            return _database.InsertAsync(item);
        }

        public Task<int> SaveAllItemAsync(List<T> items)
        {
            return _database.InsertAllAsync(items);
        }

        public Task<int> UpdateItemAsync(T item)
        {
            return _database.UpdateAsync(item);
        }

        public Task<int> DeleteItemAsync(T item)
        {
            return _database.DeleteAsync(item);
        }

        public void DropTableAsync(bool recriarTabela)
        {
            _database.DropTableAsync<T>().Wait();
            if (recriarTabela)
            {
                _database.CreateTableAsync<T>().Wait();
            }
        }

        public void DeleteAllAsync()
        {
            _database.DropTableAsync<T>().Wait();
        }
    }
}
