using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsUI.Abstract;
using WinFormsUI.Concrete;

namespace WinFormsUI
{
    public partial class Form1 : Form 
    {
        CarManager carManager = new CarManager(new EfCarDal());
        BrandManager brandManager = new BrandManager(new EfBrandDal());
        ColorManager colorManager = new ColorManager(new EfColorDal());
        Car car = new Car();
        Brand brand = new Brand();
        SqlManager con = new SqlManager();

        public Form1()
        {
            InitializeComponent();
            
        }

        public void Clear()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
        }

        public void Listele()
        {
            
            DataTable dataTable = new DataTable();
            SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM Cars", con.Connection());
            dataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;

        }
        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand sqlCommand1 = new SqlCommand("INSERT INTO Brands(BrandName) values (@p2)", con.Connection());
            
            sqlCommand1.Parameters.AddWithValue("@p2", textBox1.Text);
            sqlCommand1.ExecuteNonQuery();

            SqlCommand sqlCommand2 = new SqlCommand("INSERT INTO Cars(BrandId, ColorId, ModelYear, DailyPrice, Description) values (@p2,@p3,@p4,@p5,@p6)", con.Connection());

            sqlCommand2.Parameters.AddWithValue("@p2", textBox2.Text);
            sqlCommand2.Parameters.AddWithValue("@p3", textBox3.Text);
            sqlCommand2.Parameters.AddWithValue("@p4", textBox4.Text);
            sqlCommand2.Parameters.AddWithValue("@p5", decimal.Parse(textBox5.Text));
            sqlCommand2.Parameters.AddWithValue("@p6", textBox6.Text);          

            sqlCommand2.ExecuteNonQuery();
            con.Connection().Close();
            MessageBox.Show("Araba Sisteme Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
            Clear();
            

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            brand.BrandName = textBox1.Text;
            car.BrandId = Convert.ToInt32(textBox2.Text);
            car.ColorId = Convert.ToInt32(textBox3.Text);
            car.ModelYear = textBox4.Text;
            car.DailyPrice = decimal.Parse(textBox5.Text); 
            car.Description = textBox6.Text;
            using (EfContext efContext = new EfContext())
            {
                
                efContext.Cars.Add(car);
                efContext.SaveChanges();        
                
            }
            
            Clear();
            MessageBox.Show("Araç Sisteme Kayıt Oldu.");
            ListDataGridView();
        }

        public void ListDataGridView()
        {
            dataGridView1.AutoGenerateColumns = false;

            using (EfContext efContext = new EfContext())
            {
                dataGridView1.DataSource = efContext.Cars.ToList<Car>();
            }
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.CurrentRow.Index != 1)
            {
                car.CarId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["CarId"].Value);
                car.BrandId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["BrandId"].Value);
                car.ColorId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ColorId"].Value);
                car.DailyPrice = Convert.ToInt32(dataGridView1.CurrentRow.Cells["DailyPrice"].Value);
                car.ModelYear = Convert.ToString(dataGridView1.CurrentRow.Cells["ModelYear"].Value);
                car.Description = Convert.ToString(dataGridView1.CurrentRow.Cells["Description"].Value);
                using (EfContext efContext = new EfContext())
                {
                    car = efContext.Cars.Where(c => c.CarId == car.CarId).FirstOrDefault();
                    label7.Text = Convert.ToString(car.CarId);
                    textBox2.Text = Convert.ToString(car.BrandId);
                    textBox3.Text = Convert.ToString(car.ColorId);
                    textBox4.Text = Convert.ToString(car.ModelYear);
                    textBox5.Text = Convert.ToString(car.DailyPrice);
                    textBox6.Text = Convert.ToString(car.Description);
                }


            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Index != 1)
            {
                DataGridViewRow dr = new DataGridViewRow();

                //label7.Text = dr.Cells[1].Value.ToString();
                textBox2.Text = dr.Cells[2].Value.ToString();
                textBox3.Text = dr.Cells[3].Value.ToString();
                textBox4.Text = dr.Cells[4].Value.ToString();
                textBox5.Text = dr.Cells[5].Value.ToString();
                textBox6.Text = dr.Cells[6].Value.ToString();

                using (EfContext efContext = new EfContext())
                {
                    car = efContext.Cars.Where(c => c.CarId == car.CarId).FirstOrDefault();
                    label7.Text = Convert.ToString(car.CarId);
                    textBox2.Text = Convert.ToString(car.BrandId);
                    textBox3.Text = Convert.ToString(car.ColorId);
                    textBox4.Text = Convert.ToString(car.ModelYear);
                    textBox5.Text = Convert.ToString(car.DailyPrice);
                    textBox6.Text = Convert.ToString(car.Description);
                    efContext.Entry(car).State = EntityState.Modified;
                    efContext.SaveChanges();
                }

            }
            
            
            MessageBox.Show("Araç Güncellendi");
            ListDataGridView();
            Clear();


        }
    }
}
