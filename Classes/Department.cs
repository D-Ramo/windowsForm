using form.Data;
using System.Data;


namespace Classes;
public class DepartmentClass
{

    public int Id { get; set; }
    public string Name { get; set; }

    public string[] Departments { get; set; }
    public string[] GetDepartments()
    {
        using (EmployeeDbContext context = new EmployeeDbContext())
        {
            var departmentNames = context.Departments
                .OrderBy(department => department.DepartmentId)
                .Select(department => department.DepartmentName)
                .ToArray();

            return departmentNames;
        }
    }
    public DepartmentClass()
    {

    }
    public DepartmentClass(string name)
    {
        using (EmployeeDbContext context = new EmployeeDbContext())
        {
            this.Name = name;
            var departmentId = context.Departments
                .Where(department => department.DepartmentName == name)
                .Select(department => department.DepartmentId)
                .FirstOrDefault();
            this.Id = departmentId;
        }
    }

    public DepartmentClass(int id)
    {
        using (EmployeeDbContext context = new EmployeeDbContext())
        {
            this.Id = id;

            var departmentName = context.Departments
            .Where(department => department.DepartmentId == id)
            .Select(department => department.DepartmentName).FirstOrDefault();

            this.Name = departmentName;
            MessageBox.Show($"Department ID: {id}, Name: {departmentName}");

        }
    }


}