using System.Data;
using form.Data;
using form.Migrations;
using form.Models;

namespace Classes;

public class EmployeeClass
{
    public DataTable EmployeeTable { get; set; }
    public string[] EmployeeIds { get; set; }
    public EmployeeClass()
    {
        EmployeeTable = new DataTable();
        CreateEmployeeTable();
        GetIds();
    }


    private void GetIds()
    {
        try
        {
            using (EmployeeDbContext context = new EmployeeDbContext())
            {
                var ids = context.Employees
                .OrderBy(e => e.EmployeeId)
                .Select(e => e.EmployeeId.ToString())
                .ToArray();

                EmployeeIds = ids;
            }

        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
    public List<form.Models.Employee> GetEmployees()
    {
        try
        {
            using (EmployeeDbContext context = new EmployeeDbContext())
            {
                return context.Employees
                 .Where(Employee => Employee.Type == "Employee").ToList();
            }

        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
        return null;

    }
    private void CreateEmployeeTable()
    {
        try
        {
            List<form.Models.Employee> employees = GetEmployees();

            EmployeeTable.Columns.Add("ID", typeof(int));
            EmployeeTable.Columns.Add("Name", typeof(string));
            EmployeeTable.Columns.Add("Adress", typeof(string));
            EmployeeTable.Columns.Add("Type", typeof(string));

            foreach (var emp in employees)
            {
                EmployeeTable.Rows.Add(emp.EmployeeId, emp.EmployeeName, emp.EmployeeAddress, emp.Type);
            }

        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
        }


    }


}