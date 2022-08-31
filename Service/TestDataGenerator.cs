﻿using System;
using System.Collections.Generic;
using System.Linq;
using Models;


namespace Services
{
    public class TestDataGenerator
    {
        
        public List<Client> GetClientsList()
        {
            DateTime start = new DateTime(1950, 1, 1);
            Random rand = new Random();
            int range = (DateTime.Today - start).Days;
            var clientsList = new List<Client>();
            for (int i = 0; i < 1000; i++)
            {
                clientsList.Add(new Client
                {
                    Name = "Name_" + i,
                    DOB = start.AddDays(rand.Next(range)),
                    PasportNum = rand.Next(100000, 999999),
                    Phone = 77500000 + i
                }) ;
            }
                return clientsList;
        }
        public Dictionary<int, Client> GetClientsDictionary()
        {
            DateTime start = new DateTime(1950, 1, 1);
            Random rand = new Random();
            int range = (DateTime.Today - start).Days;
            Client client = new Client();
            var clientsDictionary = new Dictionary<int, Client>();
            for (int i = 0; i < 1000; i++)
            {
                clientsDictionary.Add(
                client.Phone,
                new Client
                {
                    Name = "Name_" + i,
                    DOB= start.AddDays(rand.Next(range)),
                    PasportNum = rand.Next(100000, 999999),
                    Phone = 77500000 + i
                });
            }

            return clientsDictionary;
        }
        public List<Employee> GetEmployeesList()
        {
            DateTime start = new DateTime(1950, 1, 1);
            Random rand = new Random();
            int range = (DateTime.Today - start).Days;
            var employeesList = new List<Employee>();

            for (int i = 0; i < 1000; i++)
            {
                employeesList.Add(new Employee
                {
                    Name = "Name_" + i,
                    DOB = start.AddDays(rand.Next(range)),
                    PasportNum = rand.Next(100000, 999999),
                    Salary = rand.Next(1000,9999)
                });
            }
            return employeesList;
        }

    }
}
