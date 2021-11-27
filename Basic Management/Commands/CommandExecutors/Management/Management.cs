using System;
using System.Collections.Generic;
using System.Linq;

namespace Basic_Management.Commands.CommandExecutors
{
    public class Management : Command
    {
        public Management() : base("management", AccessType.SUPERUSER, "Launch the management application") { }

        public override void RunCommand(string[] args)
        {
            Console.Clear();

            int screenx = Console.WindowWidth - 3;
            int screeny = Console.WindowHeight - 2;

            string Title = "Management GUI";
            
            for (int y = 0; y < Console.WindowHeight; y++)
            {
                for (int x = 0; x < Console.WindowWidth - 1; x++)
                {
                    if (!(y == 0 || y == Console.WindowHeight - 1 || x == 0 || x == Console.WindowWidth - 2)) continue;
                    Console.SetCursorPosition(x,y);
                    if (y == Console.WindowHeight - 1)
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write('#');
                }
            }
            Console.SetCursorPosition((screenx+2) / 2 - Title.Length / 2 - 4, 0);
            Console.Write("( " + Title + " )");
            
            Console.ForegroundColor = ConsoleColor.White;

            string eTitle = "Employees -= | =- Revenue -= | =- Salary -= | =- Profit";
            string bar = "===============================================================";

            int titleX = (screenx + 2) / 2 - eTitle.Length / 2 - 2;
            int barX = (screenx + 2) / 2 - bar.Length / 2 - 2;
            
            Console.SetCursorPosition(titleX, 4);
            Console.Write(eTitle);
            Console.SetCursorPosition(barX, 5);
            Console.Write(bar);

            BogoEmployee e = new BogoEmployee(8, 1);
            List<String> employeeStrings = new List<string>();

            for (int i = 0; i < e.Employees.Count; i++)
            {
                int y = 6 + i;
                Employee employee = e.Employees[i];
                double revenue = 0;
                
                foreach (var p in e.Projects)
                {
                    revenue += p.ProjectRevenue(employee, e.features[employee]);
                }

                Console.SetCursorPosition(barX, y);
                Console.Write(employee.Name + " | " + revenue + " | " + employee.Salary + " | " + (revenue - employee.Salary));
            }
        }
    }
}