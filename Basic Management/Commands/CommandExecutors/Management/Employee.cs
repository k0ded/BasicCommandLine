namespace Basic_Management.Commands.CommandExecutors
{
    public class Employee
    {
        public string Name { get; }
        public int Salary { get; }
        public int Revenue { get; }
        public int Profit { get; }
        
        public Employee(string name, int salary)
        {
            Name = name;
            Salary = salary;
        }
    }
}