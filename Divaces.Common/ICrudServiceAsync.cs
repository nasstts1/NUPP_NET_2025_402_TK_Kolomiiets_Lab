using Devices.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Divaces.Common
{
    public class ICrudServiceAsync<T> : CrudServiceAsync<T> where T : Device{
        private readonly object _lock = new object();

        private readonly SemaphoreSlim _fileSemaphore = new SemaphoreSlim(1, 1);

        private readonly List<T> _data = new List<T>();
        private readonly string _filePath;

        public ICrudServiceAsync(string filePath)
        {
            _filePath = filePath;
        }


        public Task<bool> CreateAsync(T element)
        {
            lock (_lock)
            {
                _data.Add(element);
                return Task.FromResult(true);
            }
        }

        public Task<T> ReadAsync(Guid id)
        {
            lock (_lock)
            {
                var result = _data.FirstOrDefault(e => e.Id == id);
                return Task.FromResult(result);
            }
        }

        public Task<IEnumerable<T>> ReadAllAsync()
        {
            lock (_lock)
            {
                return Task.FromResult<IEnumerable<T>>(_data.ToList());
            }
        }

        public Task<IEnumerable<T>> ReadAllAsync(int page, int amount)
        {
            lock (_lock)
            {
                var result = _data
                    .Skip((page - 1) * amount)
                    .Take(amount)
                    .ToList();
                return Task.FromResult<IEnumerable<T>>(result);
            }
        }

        public Task<bool> UpdateAsync(T element)
        {
            lock (_lock)
            {
                var existingElement = _data.FirstOrDefault(e => e.Id == element.Id);
                if (existingElement == null) return Task.FromResult(false);

                var index = _data.IndexOf(existingElement);
                _data[index] = element;
                return Task.FromResult(true);
            }
        }

        public Task<bool> RemoveAsync(T element)
        {
            lock (_lock)
            {
                return Task.FromResult(_data.Remove(element));
            }
        }

        public async Task<bool> SaveAsync()
        {
            await _fileSemaphore.WaitAsync();
            try
            {
                var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                string jsonString;

                lock (_lock)
                {
                    jsonString = JsonSerializer.Serialize(_data, jsonOptions);
                }

                await File.WriteAllTextAsync(_filePath, jsonString);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка збереження: {ex.Message}");
                return false;
            }
            finally
            {
                _fileSemaphore.Release();
            }
        }
        public IEnumerator<T> GetEnumerator()
        {
            lock (_lock)
            {
                return _data.GetEnumerator();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
