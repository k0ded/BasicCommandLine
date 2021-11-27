using System.Collections.Generic;

namespace Basic_Management.Commands.CommandExecutors
{
    public class Project
    {
        private Dictionary<Employee, double> _workerHours;

        private int totalHours;
        private int totalRevenue;
        private int totalFeatures;
        
        public Project(int hours, int revenue, int features, Dictionary<Employee, double> workerHours)
        {
            totalHours = hours;
            totalRevenue = revenue;
            totalFeatures = features;
            _workerHours = workerHours;
        }

        public double EmployeeExpectation(Employee employee, double featuresMade)
        {
            double employeeFeatureExpectation = (double)totalFeatures / totalHours * _workerHours[employee];
            double performance = featuresMade / employeeFeatureExpectation;
            return performance;
        }

        public double ProjectRevenue(Employee employee, double featuresMade)
        {
            return totalRevenue / _workerHours[employee] * EmployeeExpectation(employee, featuresMade);
        }
    }
}