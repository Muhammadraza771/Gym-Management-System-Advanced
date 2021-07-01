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
    public partial class payfeeForm : Form
    {
        String id2;
        int updateValue;
        String curl;
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Environment.CurrentDirectory + "\\Database1.mdf;Integrated Security=True;Connect Timeout=30");
        public payfeeForm(String id,String name,String father,String phone,String sub,String trainerserv,String date,String cur)
        {
            InitializeComponent();
            this.curl = cur;

            label14.Text = id.ToString();
            label15.Text = name;
            label16.Text = father;
            label17.Text = phone;
            label18.Text = sub;
            label20.Text = trainerserv;
            label22.Text = date;
            label23.Text = cur;


            this.id2 = id;

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




            con.Open();
            SqlCommand cmd2 = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * From [FeeStructure] ";
            cmd.ExecuteNonQuery();
            DataTable dt2 = new DataTable();
            SqlDataAdapter sd2 = new SqlDataAdapter(cmd);
            sd.Fill(dt2);
            con.Close();




            if(sub.Equals("Gym Only") )
            {
                int x = (int) dt2.Rows[0][0];
                if (trainerserv.Equals("Yes"))
                {
                    int y= (int) dt2.Rows[0][2];
                    label19.Text = x.ToString();
                    label21.Text = y.ToString();
                    int z = x + y;
                    updateValue = z;
                    label25.Text = z.ToString()+"/-";
                    
                    
                }
                else
                {
                    label19.Text = x.ToString();
                    label21.Text = "0";
                    label25.Text = x.ToString()+"/-";
                    updateValue = x;
                }
            }
            else if(sub.Equals("Gym & Fitness"))
            {
                int x = (int)dt2.Rows[0][0];
                int y = (int)dt2.Rows[0][1];
                if (trainerserv.Equals("Yes"))
                {
                    int z = x + y;
                    
                    label19.Text = z.ToString();
                    z = z + (int)dt2.Rows[0][2];
                    label25.Text = z.ToString()+"/-";
                    label21.Text = dt2.Rows[0][2].ToString();
                    updateValue = z;
                }
                else
                {

                    int z = x + y;
                    label19.Text = z.ToString();

                    label25.Text = z.ToString()+"/-";
                    label21.Text = "0";
                    updateValue = z;
                }
            }
            else if(sub.Equals("Fitness Only"))
            {
                label19.Text = dt2.Rows[0][2].ToString();
                if (trainerserv.Equals("Yes"))
                {
                    label21.Text = dt2.Rows[0][2].ToString();

                    int z = (int)dt2.Rows[0][2] + (int)dt2.Rows[0][1];
                    label25.Text = z.ToString()+"/-";
                    label19.Text = dt2.Rows[0][1].ToString();
                    updateValue = z;
                }
                else
                {
                    label21.Text = "0";
                    label25.Text = dt2.Rows[0][1].ToString()+"/-";
                    label19.Text = dt2.Rows[0][1].ToString();
                    updateValue = (int) dt2.Rows[0][1];
                }
            }



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

        private void payfeeForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {


            con.Open();
            SqlCommand cmd2 = con.CreateCommand();
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = "Update  [Member_Table]  set  FeeStatus = @s, AddDate = @ad  Where MemberId = @d ";
            cmd2.Parameters.Add(new SqlParameter("@s", "Paid"));
            cmd2.Parameters.Add(new SqlParameter("@d", id2));
            cmd2.Parameters.Add(new SqlParameter("@ad",curl));
            cmd2.ExecuteNonQuery();
            con.Close();
       





            con.Open();
            SqlCommand cmd4 = con.CreateCommand();
            cmd4.CommandType = CommandType.Text;
            cmd4.CommandText = "Select * From [money] ";
            cmd4.ExecuteNonQuery();
            DataTable dt4 = new DataTable();
            SqlDataAdapter sd4 = new SqlDataAdapter(cmd4);
            sd4.Fill(dt4);
            con.Close();

            int a = (int) dt4.Rows[0][1];





            con.Open();
            SqlCommand cmd3 = con.CreateCommand();
            cmd3.CommandType = CommandType.Text;
          
            int sum = a + updateValue;
            cmd3.CommandText = "Update  [money]  set  amount = @mon Where Id =1 ";
            cmd3.Parameters.Add(new SqlParameter("@mon", sum));
            cmd3.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Fee Paid");

          


            this.Close();


        }
    }
}
