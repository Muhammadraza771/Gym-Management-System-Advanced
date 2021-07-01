using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GymManagementSystemAdvanced
{
    public partial class Form2 : Form
    {


        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Environment.CurrentDirectory + "\\Database1.mdf;Integrated Security=True;Connect Timeout=30");
        public Form2(String id, String name, String father, String phone, String type,String trainer,String dat)
        {
            InitializeComponent();


            label9.Text = id;
            label10.Text = name;
            label11.Text = father;
            label12.Text = phone;
            label13.Text = type;
            label14.Text = trainer;
            label15.Text = dat;


            SqlDataReader sdr;
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * From [Table] Where ImageId = " + id; ;
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            sd.Fill(dt);
            con.Close();


            byte[] img;

            if (dt.Rows.Count > 0)
            {

                img = (byte[])(dt.Rows[0][1]);




                if (img == null)
                {
                    pictureBox2.Image = null;
                }
                else
                {
                    MemoryStream ms = new MemoryStream(img);
                    pictureBox2.Image = System.Drawing.Image.FromStream(ms);
                }

            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
