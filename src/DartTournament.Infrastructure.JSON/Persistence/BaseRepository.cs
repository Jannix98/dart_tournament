using DartTournament.Domain.Entities;
using DartTournament.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DartTournament.Infrastructure.JSON.Persistence
{
    public interface IBaseRepository<T> where T : class
    {
        Task SaveInFile(List<T> data);
        Task<List<T>> GetAllAsync();
    }

    public abstract class BaseRepository<T> where T : class
    {
        protected readonly string FilePath = null;
        protected abstract string FileName { get; }

        internal BaseRepository()
        {
            FilePath = PathManager.GetAndCreatePath(FileName);
        }

        protected async Task SaveInFile(List<T> data)
        {
            var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(FilePath, json);
        }

        protected async Task<List<T>> GetAllAsync()
        {
            if (!File.Exists(FilePath)) return new List<T>();
            var json = await File.ReadAllTextAsync(FilePath);
            if (String.IsNullOrEmpty(json))
                return new List<T>();
            return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
        }

    }
}
