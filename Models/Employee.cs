namespace form.Models;

public class Employee
{
    public int EmployeeId { get; set; }

    public string EmployeeName { get; set; }

    public string EmployeeAddress { get; set; }

    public string Type { get; set; }

    public Department Department { get; set; } 
}