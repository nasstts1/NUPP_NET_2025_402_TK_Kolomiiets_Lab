using System;
using System.Collections.Generic;
using System.Linq;

namespace Devices.Common
{
    public class CrudService<T> : ICrudService<T> where T : Device
    {
        private readonly List<T> _data = new List<T>();

        public void Create(T element)
        {
            _data.Add(element);
        }

        public T Read(Guid id)
        {
            return _data.FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<T> ReadAll()
        {
            return _data;
        }

        public void Update(T element)
        {
            var existingElement = Read(element.Id);
            if (existingElement != null)
            {
                var index = _data.IndexOf(existingElement);
                _data[index] = element;
            }
        }

        public void Remove(T element)
        {
            _data.Remove(element);
        }
    }
}
