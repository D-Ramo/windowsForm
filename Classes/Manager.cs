
using form.Data;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Classes;

public class Manager : EmployeeClass
{
    public DataTable ManagerTable { get; set; }

    public Manager()
    {
        ManagerTable = new DataTable();
        PopulatManagers();
    }
    public List<form.Models.Employee> GetManagers()
    {
        try
        {
            using (EmployeeDbContext context = new EmployeeDbContext())
            {
                return context.Employees.Include(e => e.Department)
                 .Where(Employee => Employee.Type == "Manager").ToList();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
        return null;
    }

    public void PopulatManagers()
    {
        try
        {
            List<form.Models.Employee> managers = GetManagers();

            ManagerTable.Columns.Add("ID", typeof(int));
            ManagerTable.Columns.Add("Name", typeof(string));
            ManagerTable.Columns.Add("Adress", typeof(string));
            ManagerTable.Columns.Add("Type", typeof(string));
            ManagerTable.Columns.Add("Department", typeof(string));


            foreach (var man in managers)
            {
                ManagerTable.Rows.Add(man.EmployeeId, man.EmployeeName, man.EmployeeAddress, man.Type, man.Department?.DepartmentName);
            }

        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
        }

    }
}