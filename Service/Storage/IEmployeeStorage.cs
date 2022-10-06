using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Services.Storage
{
    public interface IEmployeeStorage : IStorage<Employee>
    {
        public List<Employee> Data { get; set; }
    }
}
