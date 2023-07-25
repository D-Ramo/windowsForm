using System.Data;
using Classes;
using form.Data;
using YourNamespace;

using form.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace form
{


    public partial class Form1 : Form
    {
        private Label label;
        private ListBox editTab;
        private Label editTabLabel;
        private Button enterButton;
        private Button printButton;
        private Label nameLabel;
        private Label typeLabel;
        private Label departmentLabel;
        private Label adressLabel;
        private TextBox nameTextbox;
        private TextBox adressTextbox;
        private ComboBox typeOfuser;
        private ComboBox departmentBox;
        private Button editbutton;
        private Button deleteButon;
        private Button clearButton;
        public Form1()
        {
            InitializeComponent();

            try
            {

                using (EmployeeDbContext context = new EmployeeDbContext())


                    label = new Label();
                label.Text = "Employee Form";
                label.Location = new System.Drawing.Point(300, 30);
                label.AutoSize = true;

                typeOfuser = new ComboBox();
                typeOfuser.Location = new Point(330, 250);
                typeOfuser.Name = "Type of user";
                typeOfuser.Size = new Size(230, 700);
                string[] type = new string[] { "Employee", "Manager" };
                typeOfuser.DropDownStyle = ComboBoxStyle.DropDownList;
                typeOfuser.Items.AddRange(type);
                typeOfuser.SelectedIndexChanged += typeOfUserEventHandler;

                typeLabel = new Label();
                typeLabel.Location = new Point(330, 230);
                typeLabel.AutoSize = true;
                typeLabel.Text = "Please select the type of employee:";

                nameLabel = new Label();
                nameLabel.Location = new Point(330, 110);
                nameLabel.AutoSize = true;
                nameLabel.Text = "Please enter name:";

                nameTextbox = new TextBox();
                nameTextbox.Location = new Point(330, 130);
                nameTextbox.Size = new Size(300, 700);
                nameTextbox.KeyPress += nameBoxEventHandler;

                adressLabel = new Label();
                adressLabel.Location = new Point(330, 170);
                adressLabel.AutoSize = true;
                adressLabel.Text = "Please enter adress:";

                adressTextbox = new TextBox();
                adressTextbox.Location = new Point(330, 190);
                adressTextbox.Size = new Size(300, 700);

                departmentBox = new ComboBox();
                departmentBox.Location = new Point(330, 310);
                departmentBox.Name = "Type of user";
                departmentBox.Size = new Size(230, 700);
                string[] departments = new DepartmentClass().GetDepartments();
                departmentBox.Items.AddRange(departments);
                departmentBox.DropDownStyle = ComboBoxStyle.DropDownList;
                departmentBox.Visible = false;

                departmentLabel = new Label();
                departmentLabel.Location = new Point(330, 290);
                departmentLabel.AutoSize = true;
                departmentLabel.Text = "Select department:";
                departmentLabel.Visible = false;

                enterButton = new Button();
                enterButton.Text = "Submit";
                enterButton.Location = new Point(350, 350);
                enterButton.Size = new Size(70, 70);
                enterButton.Click += addToDb_click;

                printButton = new Button();
                printButton.Text = "Print Table";
                printButton.Location = new Point(460, 350);
                printButton.Size = new Size(70, 70);
                printButton.Click += printTableButton;

                editTabLabel = new Label();
                editTabLabel.Location = new Point(50, 110);
                editTabLabel.AutoSize = true;
                editTabLabel.Text = "Please pick which user to edit/delete:";

                editbutton = new Button();
                editbutton.Text = "Edit";
                editbutton.Location = new Point(160, 350);
                editbutton.Size = new Size(90, 30);
                editbutton.Click += editTableButtonEventHandler;

                deleteButon = new Button();
                deleteButon.Text = "Delete";
                deleteButon.Location = new Point(70, 350);
                deleteButon.Size = new Size(90, 30);
                deleteButon.Click += deleteButonHandler;

                editTab = new ListBox();
                editTab.Location = new Point(110, 130);
                editTab.Name = "Type of user";
                editTab.Size = new Size(100, 190);
                editTab.Items.AddRange(new EmployeeClass().EmployeeIds);
                editTab.SelectedIndexChanged += editTabEvnetHandler;

                clearButton = new Button();
                clearButton.Text = "Clear";
                clearButton.Location = new Point(670, 160);
                clearButton.Size = new Size(60, 30);
                clearButton.Click += clearButtonHandler;

                Controls.Add(label);
                Controls.Add(printButton);
                Controls.Add(enterButton);
                Controls.Add(adressLabel);
                Controls.Add(nameLabel);
                Controls.Add(nameTextbox);
                Controls.Add(adressTextbox);
                Controls.Add(typeOfuser);
                Controls.Add(typeLabel);
                Controls.Add(departmentBox);
                Controls.Add(departmentLabel);
                Controls.Add(editTab);
                Controls.Add(editTabLabel);
                Controls.Add(editbutton);
                Controls.Add(deleteButon);
                Controls.Add(clearButton);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while saving changes: " + ex.InnerException?.Message ?? ex.Message);
            }
        }




        private void deleteButonHandler(object sender, EventArgs e)
        {
            try
            {
                using (EmployeeDbContext context = new EmployeeDbContext())
                {
                    var emp = context.Employees
                        .Where(emp => emp.EmployeeId.ToString() == editTab.Text)
                        .FirstOrDefault();
                    if (emp is Employee)
                    {
                        context.Remove(emp);
                    }
                    context.SaveChanges();
                }
                MessageBox.Show("Deleted user!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            nameTextbox.Text = "";
            adressTextbox.Text = "";
            typeOfuser.SelectedIndex = -1;
            departmentBox.Text = "";
            editTab.Items.Clear();
            editTab.Items.AddRange(new EmployeeClass().EmployeeIds);
        }
        private void clearButtonHandler(object sender, EventArgs e)
        {
            editTab.Text = "";
            typeOfuser.SelectedIndex = -1;
            departmentBox.SelectedIndex = -1;
            nameTextbox.Text = "";
            adressTextbox.Text = "";
        }
        private void nameBoxEventHandler(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != '/' && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void editTabEvnetHandler(object sender, EventArgs e)
        {
            try
            {
                using (EmployeeDbContext context = new EmployeeDbContext())
                {
                    var query = context.Employees
                    .Where(e => e.EmployeeId.ToString() == editTab.Text)
                    .Select(e => e.Type).FirstOrDefault();
                    if (query == "Manager")

                    {
                        Manager managerClass = new Manager();
                        List<form.Models.Employee> employees = managerClass.GetManagers();
                        foreach (var man in employees)
                        {
                            if (man.EmployeeId.ToString() == editTab.Text)
                            {
                                nameTextbox.Text = man.EmployeeName;
                                adressTextbox.Text = man.EmployeeAddress;
                                typeOfuser.Text = man.Type;
                                departmentBox.Text = man.Department.DepartmentName;
                            }
                        }
                    }
                    else
                    {
                        EmployeeClass managerClass = new EmployeeClass();
                        List<form.Models.Employee> employees = managerClass.GetEmployees();
                        foreach (var emp in employees)
                        {
                            if (emp.EmployeeId.ToString() == editTab.Text)
                            {
                                nameTextbox.Text = emp.EmployeeName;
                                adressTextbox.Text = emp.EmployeeAddress;
                                typeOfuser.Text = emp.Type;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void checkNull()
        {
            if (nameTextbox.Text == "" || adressTextbox.Text == "" || typeOfuser.Text == "" || (typeOfuser.Text == "Manager" && departmentBox.Text == ""))
            {
                throw new ArgumentException("Inputs cannot be null");

            }
        }
        private void editTableButtonEventHandler(object sender, EventArgs e)
        {
            try
            {
                checkNull();
                using (EmployeeDbContext context = new EmployeeDbContext())
                {
                    var query = context.Employees
                    .Where(e => e.EmployeeId.ToString() == editTab.Text)
                    .Select(e => e.Type).FirstOrDefault();

                    var emp = context.Employees
                    .Where(emp => emp.EmployeeId.ToString() == editTab.Text)
                    .FirstOrDefault();

                    if (emp is Employee)
                    {
                        emp.EmployeeName = nameTextbox.Text;
                        emp.EmployeeAddress = adressTextbox.Text;
                        emp.Type = typeOfuser.Text;
                        if (query == "Manager" || typeOfuser.Text == "Manager")
                        {
                            Department department = context.Departments.FirstOrDefault(d => d.DepartmentName == departmentBox.Text);
                            emp.Department = department;
                        }
                        context.SaveChanges();
                    }
                }
                nameTextbox.Text = "";
                adressTextbox.Text = "";
                typeOfuser.SelectedIndex = -1;
                departmentBox.Text = "";
                editTab.Items.Clear();
                editTab.Items.AddRange(new EmployeeClass().EmployeeIds);
                MessageBox.Show("Edited user!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void typeOfUserEventHandler(object sender, EventArgs e)
        {
            if (typeOfuser.Text == "Manager")
            {
                departmentBox.Visible = true;
                departmentLabel.Visible = true;
            }
            else
            {
                departmentBox.Visible = false;
                departmentLabel.Visible = false;
            }
        }
        private void addToDb_click(object sender, EventArgs e)
        {

            string name = nameTextbox.Text;
            string adress = adressTextbox.Text;
            string type = typeOfuser.Text;
            string departmentname = departmentBox.Text;

            try
            {
                checkNull();
                using (EmployeeDbContext context = new EmployeeDbContext())
                {
                    Department department = context.Departments.FirstOrDefault(d => d.DepartmentName == departmentname);
                    if (typeOfuser.Text == "Manager")
                    {
                        Employee newMan = new Employee()
                        {
                            EmployeeName = name,
                            EmployeeAddress = adress,
                            Type = type,
                            Department = department,
                        };
                        context.Employees.Add(newMan);
                    }
                    else
                    {
                        Employee newEmp = new Employee()
                        {
                            EmployeeName = name,
                            EmployeeAddress = adress,
                            Type = type,
                        };
                        context.Employees.Add(newEmp);

                    }
                    context.SaveChanges();
                    var id = context.Employees.Select(d => d.EmployeeId).Max();
                    MessageBox.Show("Added user with ID: " + id.ToString());

                }
                nameTextbox.Text = "";
                adressTextbox.Text = "";
                typeOfuser.SelectedIndex = -1;
                departmentBox.Text = "";

                editTab.Items.Clear();
                editTab.Items.AddRange(new EmployeeClass().EmployeeIds);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while saving changes: " + ex.InnerException?.Message ?? ex.Message);
            }
        }

        private void printTableButton(object sender, EventArgs e)
        {
            DataTable data = new DataTable();
            EmployeeClass employee = new EmployeeClass();
            data.Merge(employee.EmployeeTable);

            Manager manager = new Manager();
            data.Merge(manager.ManagerTable);

            DataView view = data.DefaultView;
            view.Sort = "ID ASC, Type ASC";
            DataTable sortedData = view.ToTable();

            PrintForm printForm = new PrintForm(sortedData);
            printForm.Show();
        }
    }


}



