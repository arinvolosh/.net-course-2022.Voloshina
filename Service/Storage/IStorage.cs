using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Storage
{
    public interface IStorage<T>
    {
        public void Add(T item);
        public void Remove(T item);
        public void Update(T item);
    }
}