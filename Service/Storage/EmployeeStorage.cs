using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Services.Storage
{
    public class EmployeeStorage : IEmployeeStorage
    {

        public List<Employee> Data { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Add(Employee item)
        {
            throw new NotImplementedException();
        }

        public void Remove(Employee item)
        {
            throw new NotImplementedException();
        }

        public void Update(Employee item)
        {
            throw new NotImplementedException();
        }
    }
}