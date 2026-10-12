using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace araba_satis_otomasyon
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        CustomerDbContext db = new CustomerDbContext();

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
                        int selectedId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Customer_Id"].Value);
                        Customer customer = db.Customers.Find(selectedId);
                        if (customer != null)
                        {
                            db.Customers.Remove(customer);
                            db.SaveChanges();
                            MessageBox.Show("Müşteri Silindi!");
                            btn_listele.PerformClick();
                        }
                    }
                }
            }
            catch (Exception ex)
            { MessageBox.Show($"Hata = {ex.Message}"); }


        }

        private void btn_listele_Click(object sender, EventArgs e)
        {
            try
            {
                var customers = db.Customers
                    .Select(c => new
                    {
                        c.Customer_Id,
                        c.Customer_Name,
                        c.Customer_Surname,
                        c.Customer_Email,
                        c.Customer_Telephone
                    })
                    .ToList();

                dataGridView1.DataSource = customers;

                
                dataGridView1.Columns["Customer_Id"].HeaderText = "Müşteri ID";
                dataGridView1.Columns["Customer_Name"].HeaderText = "Müşteri Adı";
                dataGridView1.Columns["Customer_Surname"].HeaderText = "Müşteri Soyadı";
                dataGridView1.Columns["Customer_Email"].HeaderText = "E-Posta";
                dataGridView1.Columns["Customer_Telephone"].HeaderText = "Telefon";

                dataGridView1.Columns["Customer_Id"].Visible = false;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata = {ex.Message}");
            }
        }

        private void btn_ekle_Click(object sender, EventArgs e)
        {
            try
            {
                Customer newCustomer = new Customer()
                {
                    Customer_Name = txt_isim.Text,
                    Customer_Surname = txt_soyisim.Text,
                    Customer_Email = txt_email.Text,
                    Customer_Telephone = txt_telefon.Text
                };

                db.Customers.Add(newCustomer);
                db.SaveChanges();

                MessageBox.Show("Yeni Müşteri Eklendi!");
                btn_listele.PerformClick();

            }
            catch (Exception ex)
            {

                MessageBox.Show($"Hata = {ex.Message}");
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txt_isim.Text = dataGridView1.Rows[e.RowIndex].Cells["Customer_Name"].Value.ToString();
                txt_soyisim.Text = dataGridView1.Rows[e.RowIndex].Cells["Customer_Surname"].Value.ToString();
                txt_email.Text = dataGridView1.Rows[e.RowIndex].Cells["Customer_Email"].Value.ToString();
                txt_telefon.Text = dataGridView1.Rows[e.RowIndex].Cells["Customer_Telephone"].Value.ToString();
            }

        }

        private void btn_guncelle_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    int selectedId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Customer_Id"].Value);
                    Customer customer = db.Customers.Find(selectedId);
                    if (customer != null)
                    {
                        customer.Customer_Name = txt_isim.Text;
                        customer.Customer_Surname = txt_soyisim.Text;
                        customer.Customer_Email = txt_email.Text;
                        customer.Customer_Telephone = txt_telefon.Text;
                        db.SaveChanges();
                        MessageBox.Show("Müşteri güncellendi!");
                        btn_listele.PerformClick();
                    }
                }
            }
            catch (Exception ex)
            { MessageBox.Show($"Hata = {ex.Message}"); }

        }

        private void btn_form_cp_Click(object sender, EventArgs e)
        {
            Form_Customer_Car form2_cp = new Form_Customer_Car();
            this.Hide();
            form2_cp.ShowDialog();
            this.Close();

        }
    }
}

