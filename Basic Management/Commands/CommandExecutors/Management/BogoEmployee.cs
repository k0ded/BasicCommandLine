using System;
using System.Collections.Generic;

namespace Basic_Management.Commands.CommandExecutors
{
    public class BogoEmployee
    {
        public List<Project> Projects = new List<Project>();
        public List<Employee> Employees = new List<Employee>();
        public Dictionary<Employee, double> features = new Dictionary<Employee, double>();
        private Dictionary<Employee, int> hours = new Dictionary<Employee, int>();
        
        private Random _random = new Random();
        
        private const int MIN_SALARY = 2000;
        private const int MAX_SALARY = 10000;
        private const int MIN_FEATURES = 1;
        private const int MAX_FEATURES = 100;
        private const int MIN_TOTAL_HOURS = 100;
        private const int MAX_TOTAL_HOURS = 1000;
        private const int MIN_REVENUE = 100000;
        private const int MAX_REVENUE = 10000000;
        
        public BogoEmployee(int amountOfEmployees, int amountOfProjects)
        {

            for (int i = 0; i < amountOfEmployees; i++)
            {
                Employees.Add(new Employee("Bob " + i, _random.Next(MIN_SALARY, MAX_SALARY)));
            }
            
            for (int i = 0; i < amountOfProjects; i++)
            {
                int features = _random.Next(MIN_FEATURES, MAX_FEATURES);
                int hours = _random.Next(MIN_TOTAL_HOURS, MAX_TOTAL_HOURS);
                int revenue = _random.Next(MIN_REVENUE, MAX_REVENUE);

                double totalHours = 0;
                double totalFeatures = 0;
                Dictionary<Employee, double> employeeDic = new Dictionary<Employee, double>();
                for (int j = 0; j < amountOfEmployees; j++)
                {
                    double hoursWorked = (hours - totalHours) * _random.NextDouble();
                    double featuresMade = (features - totalFeatures) * _random.NextDouble();
                    employeeDic.Add(Employees[j], hoursWorked);
                    this.features.Add(Employees[j], featuresMade);
                    totalHours += hoursWorked;
                    totalFeatures += featuresMade;
                }
                Projects.Add(new Project(hours, revenue, features, employeeDic));
            }
        }
    }
}