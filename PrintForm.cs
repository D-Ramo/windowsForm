using System;
using System.Windows.Forms;
using System.Data;

namespace YourNamespace
{
    public partial class PrintForm : Form
    {
        private DataGridView dataGridView;

        public PrintForm(DataTable data)
        {
            InitializeDataGridView(data);
        }

        private void InitializeDataGridView(DataTable data)
        {
            dataGridView = new DataGridView();
            dataGridView.DataSource = data;
            dataGridView.Dock = DockStyle.Fill;
            this.Size = new Size(800, 600);

            this.Controls.Add(dataGridView);
        }
    }
}
