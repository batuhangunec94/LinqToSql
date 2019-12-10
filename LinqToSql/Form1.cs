using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.SqlServer;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinqToSql
{
    public partial class Form1 : Form
    {
        NorthwindEntities db = new NorthwindEntities();
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSorgu1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Employees.OrderByDescending(x => SqlFunctions.DateDiff("Year", x.BirthDate, DateTime.Now)).Select(x => new {FirstName = x.FirstName,LastName = x.LastName,Title = x.Title,Age = SqlFunctions.DateDiff("Year",x.BirthDate,DateTime.Now) }).ToList();
        }

        private void btnSorgu2_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Employees.Where(x => SqlFunctions.DatePart("Year",x.BirthDate) >= 1925 && SqlFunctions.DatePart("Year",x.BirthDate) <= 1958).ToList();
        }

        private void btnSorgu3_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Employees.Where(x => x.TitleOfCourtesy == "Mr." || x.TitleOfCourtesy == "Dr.").ToList();
        }

        private void btnSorgu4_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Categories.ToList();
        }

        private void btnSorgu5_Click(object sender, EventArgs e)
        {
            Employee employee = db.Employees.Find(5);
            MessageBox.Show($"ID:{ employee.EmployeeID}\nFirst Name: { employee.FirstName}\nLast Name: {employee.LastName}" );
        }

        private void btnSorgu6_Click(object sender, EventArgs e)
        {
            try
            {
                Category category = db.Categories.First(x => x.CategoryID < 12);
                MessageBox.Show(category.CategoryName);
            }
            catch (Exception)
            {

                MessageBox.Show("Böyle Bir ID Bulunmamaktadır");
            }
        }

        private void btnSorgu7_Click(object sender, EventArgs e)
        {
            Employee employee = db.Employees.FirstOrDefault(x => x.EmployeeID < 15);
            if (employee == null)
            {
                MessageBox.Show("Böyle Bir ID Bulunmamaktadır...");
            }
            else
            {
                MessageBox.Show(employee.FirstName);
            }
        }

        private void btnSorgu8_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Employees.Where(x => SqlFunctions.DateDiff("YY", x.BirthDate, DateTime.Now) > 60).ToList();
        }

        private void btnSorgu9_Click(object sender, EventArgs e)
        {
            bool sonuc = db.Categories.Any(x => x.CategoryName == "Batuhan");
            MessageBox.Show(sonuc.ToString());
        }

        private void btnSorgu10_Click(object sender, EventArgs e)
        {
            bool sonuc = db.Employees.Any(x => x.FirstName.StartsWith("Ce"));
            MessageBox.Show(sonuc.ToString());
        }

        private void btnSorgu11_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Products.OrderByDescending(x => x.UnitPrice).Skip(3).Take(9).ToList();
        }

        private void btnSorgu12_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Products.Where(x => x.UnitPrice >= 50).OrderByDescending(x => x.UnitPrice).Select(x => new
            {
                ProductName = x.ProductName,
                UnitPrice = x.UnitPrice,
                UnitStock = x.UnitsInStock
            }).ToList();
        }
    }
}
