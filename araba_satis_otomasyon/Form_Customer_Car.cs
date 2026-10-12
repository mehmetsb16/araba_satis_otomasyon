using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Data.Entity;



namespace araba_satis_otomasyon
{
    public partial class Form_Customer_Car : Form
    {
        public Form_Customer_Car()
        {
            InitializeComponent();
        }
        CustomerDbContext db = new CustomerDbContext();


        private void Form_Customer_Car_Load(object sender, EventArgs e)
        {

            try
            {
                combo_customer.DataSource = db.Customers 
                .OrderBy(c => c.Customer_Name) 
                .Select(c => new
                {
                    c.Customer_Id, 
                    FullName = c.Customer_Name + " " + c.Customer_Surname 
                })
                .ToList();
                combo_customer.DisplayMember = "FullName"; 
                combo_customer.ValueMember = "Customer_Id"; 
                                                            
                combo_car.DataSource = db.Cars
                    .Include(p => p.Category)
                    .OrderBy(p => p.Car_Name)
                    .Select(p => new
                    {
                        p.Car_Id,
                        CarInfo = p.Car_Name + " - " + p.Car_Price + "₺ (" + p.Category.Category_Name + ")"
                    }).ToList();
                combo_car.DisplayMember = "CarInfo";
                combo_car.ValueMember = "Car_Id";
                btn_listele.PerformClick();
                
            }
            catch (Exception ex)
            { MessageBox.Show($"Hata = {ex.Message}"); }


        }

        private void btn_listele_Click(object sender, EventArgs e)
        {
            try
            {
                var list = db.CustomerCars
    .Include(cc => cc.Customer)
    .Include(cc => cc.Car.Category)
    .Select(cc => new
    {
        cc.Cuscar_Id,
        MüşteriAdı = cc.Customer.Customer_Name,
        MüşteriSoyadııı = cc.Customer.Customer_Surname,
        ÜrünAdı = cc.Car.Car_Name,
        ÜrünFiyatı = cc.Car.Car_Price,
        Kategori = cc.Car.Category.Category_Name
    })
    .ToList();
                dataGridView1.DataSource = list;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.Columns["Cuscar_Id"].Visible = false;



            }
            catch (Exception ex)
            {

                { MessageBox.Show($"Hata = {ex.Message}"); }
            }

        }

        private void btn_ekle_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedCustomerId = (int)combo_customer.SelectedValue;
                int selectedcarid = (int)combo_car.SelectedValue;


                var newRecord = new CustomerCar
                {
                    Customer_Id = selectedCustomerId,
                    Car_Id = selectedcarid
                };

                db.CustomerCars.Add(newRecord);
                db.SaveChanges();

                MessageBox.Show("Kayıt başarıyla eklendi.");


                btn_listele.PerformClick();
            }
            catch (Exception ex)
            { MessageBox.Show($"Hata = {ex.Message}"); }

        }

        private void btn_sil_Click(object sender, EventArgs e)
        {

            try
            {
                DialogResult sonuc = MessageBox.Show(
                            "Silmek istediğinize emin misiniz?",
                            "Silme Onayı",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);
                if (sonuc == DialogResult.Yes)
                {
                    if (dataGridView1.CurrentRow != null)
                    {

                        int selectedId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["CusCar_Id"].Value);

                        var recordToDelete = db.CustomerCars.Find(selectedId);

                        if (recordToDelete != null)
                        {

                            db.CustomerCars.Remove(recordToDelete);
                            db.SaveChanges();

                            MessageBox.Show("Kayıt başarıyla silindi.");

                            btn_listele.PerformClick();
                        }
                        else
                        {
                            MessageBox.Show("Kayıt bulunamadı.");
                        }

                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show($"Hata = {ex.Message}");
            }

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rd_1.Checked)
                {
                    var result = db.CustomerCars
                        .GroupBy(cc => new
                        {
                            cc.Customer.Customer_Id,
                            cc.Customer.Customer_Name,
                            cc.Customer.Customer_Surname
                        })
                        .Select(g => new
                        {
                            FullName = g.Key.Customer_Name + " " + g.Key.Customer_Surname,
                            TotalCount = g.Count()
                        })
                        .OrderByDescending(x => x.TotalCount)
                        .FirstOrDefault();

                    lbl_1.Text = result != null
                        ? $"{result.FullName} {result.TotalCount} araç ile en çok aracı satın almıştır."
                        : "Kayıt bulunamadı.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata = {ex.Message}");
            }

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

            try
            {
                if (rd_2.Checked)
                {   
                    var result = db.CustomerCars
                        .Include(cc => cc.Car) 
                        .GroupBy(cc => new
                        {
                            cc.Customer.Customer_Id,
                            cc.Customer.Customer_Name,
                            cc.Customer.Customer_Surname
                        })
                        .Select(g => new
                        {
                            FullName = g.Key.Customer_Name + " " + g.Key.Customer_Surname,
                            TotalPrice = g.Sum(cc => cc.Car.Car_Price)
                        })
                        
                        .OrderByDescending(x => x.TotalPrice)          
                        .FirstOrDefault();
                    if (result != null)
                    { lbl_2.Text = $"{result.FullName} {result.TotalPrice} TL ile en çok para harcamıştır."; }
                    else
                    { lbl_2.Text = "Kayıt bulunamadı."; }
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show($"Hata = {ex.Message}");
            }

        }

        private void btn_form_customer_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            form1.ShowDialog();
            this.Close();
        }

        private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
        {
            try
            {
                if (rd_3.Checked) 
                {
                    var result = db.CustomerCars
                        .Include(cc => cc.Car)
                        .Include(cc => cc.Customer)
                        .GroupBy(cc => new
                        {
                            cc.Customer.Customer_Id,
                            cc.Customer.Customer_Name,
                            cc.Customer.Customer_Surname
                        })
                        .Select(g => new
                        {
                            FullName = g.Key.Customer_Name + " " + g.Key.Customer_Surname,
                            TotalPrice = g.Sum(cc => cc.Car.Car_Price)
                        })
                        .OrderBy(x => x.TotalPrice) 
                        .FirstOrDefault();

                    if (result != null)
                    {
                        lbl_3.Text = $"{result.FullName} sadece {result.TotalPrice} TL harcamıştır (en az harcayan).";
                    }
                    else
                    {
                        lbl_3.Text = "Kayıt bulunamadı.";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void radioButton1_CheckedChanged_2(object sender, EventArgs e)
        {

            try
            {
                if (radioButton1.Checked)
                {
                    var result = db.CustomerCars
                        .Include(cc => cc.Car.Category) 
                        .GroupBy(cc => cc.Car.Category.Category_Name)
                        .Select(g => new
                        {
                            CategoryName = g.Key,
                            TotalSold = g.Count()
                        })
                        .OrderByDescending(x => x.TotalSold)
                        .FirstOrDefault();

                    if (result != null)
                    {
                        lbl_4.Text = $"{result.CategoryName} kategorisi {result.TotalSold} satış ile en çok satılan kategoridir.";
                    }
                    else
                    {
                        lbl_4.Text = "Satış yapılmış kategori bulunamadı.";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void lbl_1_Click(object sender, EventArgs e)
        {

        }

        private void lbl_2_Click(object sender, EventArgs e)
        {

        }

        
    }
    }

