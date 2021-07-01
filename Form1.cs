using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Drawing.Imaging;
using System.IO;

namespace GymManagementSystemAdvanced
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename="+Environment.CurrentDirectory+"\\Database1.mdf;Integrated Security=True;Connect Timeout=30");
        private bool _dragging;
        private Point _start_point;
        private int record_id;
        public int index = 0;

        String Name;
        String Father;
        String Fee;
        String Trainer_Fee;
        String subs;
        String TrainerServ;
        String phone;
        String feestatus;
        String date;


        String collector;


        String idForupdat;


        int suppQty;

        DataTable dt;

        //for profile
        String memid;
        String name;
        String father;
        String ph;
        String memshp;
        String trn;
        String dat;

        int ct = 1;
        
        

        public Form1()
        {
            InitializeComponent();

            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.ExitThread();
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            pictureBox2.Cursor = Cursors.Hand;
        }

        private void pictureBox2_DragLeave(object sender, EventArgs e)
        {
            pictureBox2.Cursor = Cursors.Default;
        }


        public void adminflg(bool a)
        {
         
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != null && textBox2.Text != null)
            {

               


                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Select * From [Credendials] Where Username ='" + textBox1.Text.Trim() + "' AND Password ='" + textBox2.Text.Trim() + "'";
                cmd.ExecuteNonQuery();
                dt = new DataTable();
                SqlDataAdapter sd = new SqlDataAdapter(cmd);
                sd.Fill(dt);
                con.Close();

                if (dt.Rows.Count > 0)
                {


                    displayDetailsOnHomeScreen();
                
                }
                else
                {
                    MessageBox.Show("Bad Credentials");
                    textBox1.Text = "";
                    textBox2.Text = "";
                }


            }

        }





        public void displayDetailsOnHomeScreen()
        {



           
            usernamePassPanel.Visible = false;
            panel4.Visible = true;
            HomePanel.Visible = true;
            LogoPanel.Visible = false;

            /**con.Open();
            SqlCommand c = con.CreateCommand();
            c.CommandType = CommandType.Text;
            c.CommandText = "Select * From [Credendials] ";
            c.ExecuteNonQuery();
            DataTable d = new DataTable();
            SqlDataAdapter s = new SqlDataAdapter(c);
            s.Fill(d);
            con.Close();*/


            label10.Text = dt.Rows[0][0].ToString();

            collector = dt.Rows[0][0].ToString();



            con.Open();
            SqlCommand cd = con.CreateCommand();
            cd.CommandType = CommandType.Text;
            cd.CommandText = "Select * From [FeeStructure] ";
            cd.ExecuteNonQuery();
            DataTable dtt = new DataTable();
            SqlDataAdapter sdd = new SqlDataAdapter(cd);
            sdd.Fill(dtt);
            con.Close();

            label22.Text = dtt.Rows[0][0].ToString();
            label23.Text = dtt.Rows[0][1].ToString();
            label31.Text = dtt.Rows[0][2].ToString();
            label59.Text = dtt.Rows[0][3].ToString();



            //-------- for displaying stocked supplements

            con.Open();
            SqlCommand cmd2 = con.CreateCommand();
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = "Select * From [Supplements] Where SupplementQty > 0";
            cmd2.ExecuteNonQuery();
            DataTable dt2 = new DataTable();
            SqlDataAdapter sd2 = new SqlDataAdapter(cmd2);
            sd2.Fill(dt2);
            con.Close();

            label18.Text = dt2.Rows.Count.ToString();




            //---------- for displaying outstocked Supplements

            con.Open();
            SqlCommand cmd3 = con.CreateCommand();
            cmd3.CommandType = CommandType.Text;
            cmd3.CommandText = "Select * From [Supplements] Where SupplementQty < 0";
            cmd3.ExecuteNonQuery();
            DataTable dt3 = new DataTable();
            SqlDataAdapter sd3 = new SqlDataAdapter(cmd3);
            sd3.Fill(dt3);
            con.Close();
            label17.Text = dt3.Rows.Count.ToString();

            //-------- for displaying all members

            con.Open();
            SqlCommand cmd4 = con.CreateCommand();
            cmd4.CommandType = CommandType.Text;
            cmd4.CommandText = "Select * From [Member_Table]";
            cmd4.ExecuteNonQuery();
            DataTable dt4 = new DataTable();
            SqlDataAdapter sd4 = new SqlDataAdapter(cmd4);
            sd4.Fill(dt4);
            con.Close();

            label15.Text = dt4.Rows.Count.ToString();



            //------ for displaying unpaying members


            con.Open();
            SqlCommand cmd5 = con.CreateCommand();
            cmd5.CommandType = CommandType.Text;
            cmd5.CommandText = "Select Subtype,TrainerService From [Member_Table] Where FeeStatus = 'Unpaid'";
            cmd5.ExecuteNonQuery();
            DataTable dt5 = new DataTable();
            SqlDataAdapter sd5 = new SqlDataAdapter(cmd5);
            sd5.Fill(dt5);
            con.Close();


            label16.Text = dt5.Rows.Count.ToString();



            //---for displaying active admins

            con.Open();
            SqlCommand cmd6 = con.CreateCommand();
            cmd6.CommandType = CommandType.Text;
            cmd6.CommandText = "Select * From [Credendials]";
            cmd6.ExecuteNonQuery();
            DataTable dt6 = new DataTable();
            SqlDataAdapter sd6 = new SqlDataAdapter(cmd6);
            sd6.Fill(dt6);
            con.Close();


            label61.Text = dt6.Rows.Count.ToString();




            //--for displaying total revenue


            con.Open();
            SqlCommand cmd7 = con.CreateCommand();
            cmd7.CommandType = CommandType.Text;
            cmd7.CommandText = "Select * From [Member_Table] Where Subtype = 'Gym Only' and TrainerService ='No'";
            cmd7.ExecuteNonQuery();
            DataTable dt7 = new DataTable();
            SqlDataAdapter sd7 = new SqlDataAdapter(cmd7);
            sd7.Fill(dt7);
            con.Close();

            int x = 0;
            for (int i = 0; i < dt7.Rows.Count; i++)
            {
                x = x + (int)dtt.Rows[0][0];// here

            }

            con.Open();
            SqlCommand cmd8 = con.CreateCommand();
            cmd8.CommandType = CommandType.Text;
            cmd8.CommandText = "Select * From [Member_Table] Where Subtype = 'Gym Only' and TrainerService ='Yes'";
            cmd8.ExecuteNonQuery();
            DataTable dt8 = new DataTable();
            SqlDataAdapter sd8 = new SqlDataAdapter(cmd8);
            sd8.Fill(dt8);
            con.Close();



            for (int i = 0; i < dt8.Rows.Count; i++)
            {
                x = x + (int)dtt.Rows[0][0];
                x = x + (int)dtt.Rows[0][2];
            }



            con.Open();
            SqlCommand cmd9 = con.CreateCommand();
            cmd9.CommandType = CommandType.Text;
            cmd9.CommandText = "Select * From [Member_Table] Where Subtype = 'Gym & Fitness' and TrainerService ='No'";
            cmd9.ExecuteNonQuery();
            DataTable dt9 = new DataTable();
            SqlDataAdapter sd9 = new SqlDataAdapter(cmd9);
            sd9.Fill(dt9);
            con.Close();



            for (int i = 0; i < dt9.Rows.Count; i++)
            {
                x = x + (int)dtt.Rows[0][0];
                x = x + (int)dtt.Rows[0][1];
            }


            //calculate the gym and fitness with trainers service yes


            con.Open();
            SqlCommand cmd10 = con.CreateCommand();
            cmd10.CommandType = CommandType.Text;
            cmd10.CommandText = "Select * From [Member_Table] Where Subtype = 'Gym & Fitness' and TrainerService ='Yes'";
            cmd10.ExecuteNonQuery();
            DataTable dt10 = new DataTable();
            SqlDataAdapter sd10 = new SqlDataAdapter(cmd10);
            sd10.Fill(dt10);
            con.Close();


            for (int i = 0; i < dt10.Rows.Count; i++)
            {
                x = x + (int)dtt.Rows[0][0];
                x = x + (int)dtt.Rows[0][1];
                x = x + (int)dtt.Rows[0][2];
            }


            con.Open();
            SqlCommand cmd12 = con.CreateCommand();
            cmd12.CommandType = CommandType.Text;
            cmd12.CommandText = "Select * From [Member_Table] Where Subtype = 'Fitness Only' and TrainerService ='No'";
            cmd12.ExecuteNonQuery();
            DataTable dt12 = new DataTable();
            SqlDataAdapter sd12 = new SqlDataAdapter(cmd12);
            sd12.Fill(dt12);
            con.Close();

            for (int i = 0; i < dt12.Rows.Count; i++)
            {
              
                x = x + (int)dtt.Rows[0][1];
         
            }




            con.Open();
            SqlCommand cmd13 = con.CreateCommand();
            cmd13.CommandType = CommandType.Text;
            cmd13.CommandText = "Select * From [Member_Table] Where Subtype = 'Fitness Only' and TrainerService ='Yes'";
            cmd13.ExecuteNonQuery();
            DataTable dt13 = new DataTable();
            SqlDataAdapter sd13 = new SqlDataAdapter(cmd13);
            sd13.Fill(dt13);
            con.Close();

            for (int i = 0; i < dt12.Rows.Count; i++)
            {

                x = x + (int)dtt.Rows[0][1];
                x = x + (int)dtt.Rows[0][2];
            }



            label63.Text = x.ToString(); //ultimate sum display




            con.Open();
            SqlCommand cmd11 = con.CreateCommand();
            cmd11.CommandType = CommandType.Text;
            cmd11.CommandText = "Select * From [money] Where Id = 1";
            cmd11.ExecuteNonQuery();
            DataTable dt11 = new DataTable();
            SqlDataAdapter sd11 = new SqlDataAdapter(cmd11);
            sd11.Fill(dt11);
            con.Close();


            label75.Text = dt11.Rows[0][1].ToString();


        }



        private void label4_MouseEnter(object sender, EventArgs e)
        {
            label4.Cursor = Cursors.Hand;
        }

        private void label4_MouseLeave(object sender, EventArgs e)
        {
            label4.Cursor = Cursors.Default;
        }

        private void label5_DragEnter(object sender, DragEventArgs e)
        {
            label5.Cursor = Cursors.Hand;
        }

        private void label5_DragLeave(object sender, EventArgs e)
        {
            label5.Cursor = Cursors.Default;
        }

        private void label6_MouseEnter(object sender, EventArgs e)
        {
            label6.Cursor = Cursors.Hand;
        }

        private void label6_MouseLeave(object sender, EventArgs e)
        {
            label6.Cursor = Cursors.Default;
        }

        private void label5_MouseEnter(object sender, EventArgs e)
        {
            label5.Cursor = Cursors.Hand;
        }

        private void label5_MouseLeave(object sender, EventArgs e)
        {
            label5.Cursor = Cursors.Default;
        }

        private void label7_MouseEnter(object sender, EventArgs e)
        {
            label7.Cursor = Cursors.Hand;
        }

        private void label7_MouseLeave(object sender, EventArgs e)
        {
            label7.Cursor = Cursors.Default;
        }

        private void label8_MouseEnter(object sender, EventArgs e)
        {
            label8.Cursor = Cursors.Hand;
        }

        private void label8_MouseLeave(object sender, EventArgs e)
        {
            label8.Cursor = Cursors.Default;
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox10_MouseEnter(object sender, EventArgs e)
        {
            pictureBox10.Cursor = Cursors.Hand;
        }

        private void pictureBox10_MouseLeave(object sender, EventArgs e)
        {
            pictureBox10.Cursor = Cursors.Default;
        }

        private void label5_Click(object sender, EventArgs e)
        {
            HomePanel.Visible = false;
            payFeePanel.Visible = false;
            panel6.Visible = false;
            panel5.Visible = true;
          

        }

        private void label4_Click(object sender, EventArgs e)
        {
            payFeePanel.Visible = false;
            panel5.Visible = false;
            panel6.Visible = false;
            HomePanel.Visible = true;

            

            con.Open();
            SqlCommand cd = con.CreateCommand();
            cd.CommandType = CommandType.Text;
            cd.CommandText = "Select * From [FeeStructure] ";
            cd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter sd = new SqlDataAdapter(cd);
            sd.Fill(dt);
            con.Close();

            label22.Text = dt.Rows[0][0].ToString();
            label23.Text = dt.Rows[0][1].ToString();
            label31.Text = dt.Rows[0][2].ToString();


            displayDetailsOnHomeScreen();

        }

        //delete
        private void button2_Click(object sender, EventArgs e)
        {


        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cd = con.CreateCommand();
            cd.CommandType = CommandType.Text;
            cd.CommandText = "Select * From [FeeStructure] ";
            cd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter sd = new SqlDataAdapter(cd);
            sd.Fill(dt);
            con.Close();
            if (textBox3.Text != null && textBox4.Text != null && textBox5.Text != null && comboBox1.SelectedIndex != -1)
            {
                if (checkBox1.Checked && !checkBox2.Checked)//for Gym ONly and not Fitness
                {

                    if (comboBox1.SelectedIndex == 0) // trainer is yes
                    {
                        con.Open();
                        SqlCommand cmd = con.CreateCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "INSERT into  [Member_Table] (Name,SubType,TrainerService,FeeStatus,Phone,AddDate,Father) values ('" + textBox3.Text.Trim() + "','Gym Only','" + comboBox1.Text.Trim() + "','Paid','" + textBox4.Text.Trim() + "','" + dateTimePicker1.Text.Trim() + "','" + textBox5.Text.Trim() + "')";
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Success");
                        clearOut();


                        //----------------------------------------------------------------updating with GYm and Trianer yes

                        con.Open();
                        SqlCommand cmd6 = con.CreateCommand();
                        cmd6.CommandType = CommandType.Text;
                        cmd6.CommandText = "Select * From [FeeStructure]";
                        cmd6.ExecuteNonQuery();
                        DataTable dt6 = new DataTable();
                        SqlDataAdapter sd6 = new SqlDataAdapter(cmd6);
                        sd6.Fill(dt6);
                        con.Close();



                        con.Open();
                        SqlCommand cmd11 = con.CreateCommand();
                        cmd11.CommandType = CommandType.Text;
                        cmd11.CommandText = "Select * From [money]";
                        cmd11.ExecuteNonQuery();
                        DataTable dt11 = new DataTable();
                        SqlDataAdapter sd11 = new SqlDataAdapter(cmd11);
                        sd11.Fill(dt11);
                        con.Close();

                        int a = (int) dt11.Rows[0][1];
                        int s = (int)dt6.Rows[0][3];
                        int b = (int)dt6.Rows[0][0];
                        int c = (int)dt6.Rows[0][2];
                        a = a + (s + b + c);
                        MessageBox.Show("Must Pay = "+(s+b+c).ToString());
                        con.Open();
                        SqlCommand cmd12 = con.CreateCommand();
                        cmd12.CommandType = CommandType.Text;
                        cmd12.CommandText = "update  [money]  set Amount = "+a;
                        cmd12.ExecuteNonQuery();        
                        con.Close();


                        //----------------------------------------------------------------updating with GYm and Trianer yes


                    }
                    else if (comboBox1.SelectedIndex == 1)//trainer is No
                    {
                        con.Open();
                        SqlCommand cmd = con.CreateCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "INSERT into  [Member_Table] (Name,SubType,TrainerService,FeeStatus,Phone,AddDate,Father) values ('" + textBox3.Text.Trim() + "','Gym Only','" + comboBox1.Text.Trim() + "','Paid','" + textBox4.Text.Trim() + "','" + dateTimePicker1.Text.Trim() + "','" + textBox5.Text.Trim() + "')";
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Success");

                        //-----------------------------------------for updating collecting funds without trainer

                        con.Open();
                        SqlCommand cmd6 = con.CreateCommand();
                        cmd6.CommandType = CommandType.Text;
                        cmd6.CommandText = "Select * From [FeeStructure]";
                        cmd6.ExecuteNonQuery();
                        DataTable dt6 = new DataTable();
                        SqlDataAdapter sd6 = new SqlDataAdapter(cmd6);
                        sd6.Fill(dt6);
                        con.Close();



                        con.Open();
                        SqlCommand cmd11 = con.CreateCommand();
                        cmd11.CommandType = CommandType.Text;
                        cmd11.CommandText = "Select * From [money]";
                        cmd11.ExecuteNonQuery();
                        DataTable dt11 = new DataTable();
                        SqlDataAdapter sd11 = new SqlDataAdapter(cmd11);
                        sd11.Fill(dt11);
                        con.Close();

                        int a = (int)dt11.Rows[0][1];
                      
                        int b = (int)dt6.Rows[0][0];
                        int c = (int)dt6.Rows[0][3];
                        a = a +( b +c);
                        MessageBox.Show("Must Pay = " + ( b + c).ToString());
                        con.Open();
                        SqlCommand cmd12 = con.CreateCommand();
                        cmd12.CommandType = CommandType.Text;
                        cmd12.CommandText = "update  [money]  set Amount = " + a;
                        cmd12.ExecuteNonQuery();
                        con.Close();

                        //----------------------------------------for updating collecting funds without trainer



                        clearOut();
                    }

                }
                else if (checkBox1.Checked && checkBox2.Checked)//if Gym and Fitness both aare yes
                {
                    if (comboBox1.SelectedIndex == 0)//trainer is yes
                    {
                        
                        con.Open();
                        SqlCommand cmd = con.CreateCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "INSERT into  [Member_Table] (Name,SubType,TrainerService,FeeStatus,Phone,AddDate,Father) values ('" + textBox3.Text.Trim() + "','Gym & Fitness','" + comboBox1.Text.Trim() + "','Paid','" + textBox4.Text.Trim() + "','" + dateTimePicker1.Text.Trim() + "','" + textBox5.Text.Trim() + "')";
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Success");
                        clearOut();

                        //----------------------------------------------------------------updating with GYm and Fitness and Trianer yes

                        con.Open();
                        SqlCommand cmd6 = con.CreateCommand();
                        cmd6.CommandType = CommandType.Text;
                        cmd6.CommandText = "Select * From [FeeStructure]";
                        cmd6.ExecuteNonQuery();
                        DataTable dt6 = new DataTable();
                        SqlDataAdapter sd6 = new SqlDataAdapter(cmd6);
                        sd6.Fill(dt6);
                        con.Close();



                        con.Open();
                        SqlCommand cmd11 = con.CreateCommand();
                        cmd11.CommandType = CommandType.Text;
                        cmd11.CommandText = "Select * From [money]";
                        cmd11.ExecuteNonQuery();
                        DataTable dt11 = new DataTable();
                        SqlDataAdapter sd11 = new SqlDataAdapter(cmd11);
                        sd11.Fill(dt11);
                        con.Close();

                        int a = (int)dt11.Rows[0][1];
                        int s = (int)dt6.Rows[0][3]; // add
                        int b = (int)dt6.Rows[0][0]; // gym
                        int c = (int)dt6.Rows[0][1]; // fitness
                        int x = (int)dt6.Rows[0][2]; // trainer
                        a = a + (s + b + c + x);
                        MessageBox.Show("Must Pay  = "+(s+b+c+x).ToString());
                        con.Open();
                        SqlCommand cmd12 = con.CreateCommand();
                        cmd12.CommandType = CommandType.Text;
                        cmd12.CommandText = "update  [money]  set Amount = " + a;
                        cmd12.ExecuteNonQuery();
                        con.Close();


                        //----------------------------------------------------------------updating with GYm and Fitness and Trianer yes




                    }
                    else
                    {
                       
                        con.Open();
                        SqlCommand cmd = con.CreateCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "INSERT into  [Member_Table] (Name,SubType,TrainerService,FeeStatus,Phone,AddDate,Father) values ('" + textBox3.Text.Trim() + "','Gym & Fitness','" + comboBox1.Text.Trim() + "','Paid','" + textBox4.Text.Trim() + "','" + dateTimePicker1.Text.Trim() + "','" + textBox5.Text.Trim() + "')";
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Success");


                        //----------------------------------------------------------------updating with GYm and Fitness and Trianer No

                        con.Open();
                        SqlCommand cmd6 = con.CreateCommand();
                        cmd6.CommandType = CommandType.Text;
                        cmd6.CommandText = "Select * From [FeeStructure]";
                        cmd6.ExecuteNonQuery();
                        DataTable dt6 = new DataTable();
                        SqlDataAdapter sd6 = new SqlDataAdapter(cmd6);
                        sd6.Fill(dt6);
                        con.Close();



                        con.Open();
                        SqlCommand cmd11 = con.CreateCommand();
                        cmd11.CommandType = CommandType.Text;
                        cmd11.CommandText = "Select * From [money]";
                        cmd11.ExecuteNonQuery();
                        DataTable dt11 = new DataTable();
                        SqlDataAdapter sd11 = new SqlDataAdapter(cmd11);
                        sd11.Fill(dt11);
                        con.Close();

                        int a = (int)dt11.Rows[0][1];
                        int s = (int)dt6.Rows[0][3]; // add
                        int b = (int)dt6.Rows[0][0]; // gym
                        int c = (int)dt6.Rows[0][1]; // fitness

                        a = a + (s + b + c );
                        MessageBox.Show("Must Pay  = " + (s + b + c ).ToString());
                        con.Open();
                        SqlCommand cmd12 = con.CreateCommand();
                        cmd12.CommandType = CommandType.Text;
                        cmd12.CommandText = "update  [money]  set Amount = " + a;
                        cmd12.ExecuteNonQuery();
                        con.Close();


                        //----------------------------------------------------------------updating with GYm and Fitness and Trianer No





                        clearOut();
                    }
                }
                else if (checkBox2.Checked && !checkBox1.Checked) // if fitness only 
                {
                    if (comboBox1.SelectedIndex == 0) // trainer is yes
                    {
                        con.Open();
                        SqlCommand cmd = con.CreateCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "INSERT into  [Member_Table] (Name,SubType,TrainerService,FeeStatus,Phone,AddDate,Father) values ('" + textBox3.Text.Trim() + "','Fitness Only','" + comboBox1.Text.Trim() + "','Paid','" + textBox4.Text.Trim() + "','" + dateTimePicker1.Text.Trim() + "','" + textBox5.Text.Trim() + "')";
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Success");


                        //----------------------------------------------------------------updating with fitness and Trianer yes

                        con.Open();
                        SqlCommand cmd6 = con.CreateCommand();
                        cmd6.CommandType = CommandType.Text;
                        cmd6.CommandText = "Select * From [FeeStructure]";
                        cmd6.ExecuteNonQuery();
                        DataTable dt6 = new DataTable();
                        SqlDataAdapter sd6 = new SqlDataAdapter(cmd6);
                        sd6.Fill(dt6);
                        con.Close();



                        con.Open();
                        SqlCommand cmd11 = con.CreateCommand();
                        cmd11.CommandType = CommandType.Text;
                        cmd11.CommandText = "Select * From [money]";
                        cmd11.ExecuteNonQuery();
                        DataTable dt11 = new DataTable();
                        SqlDataAdapter sd11 = new SqlDataAdapter(cmd11);
                        sd11.Fill(dt11);
                        con.Close();

                        int a = (int)dt11.Rows[0][1];
                        int s = (int)dt6.Rows[0][3];
                        int b = (int)dt6.Rows[0][1];
                        int c = (int)dt6.Rows[0][2];
                        a = a + (s + b + c);
                        MessageBox.Show("Must Pay = " + (s + b + c).ToString());
                        con.Open();
                        SqlCommand cmd12 = con.CreateCommand();
                        cmd12.CommandType = CommandType.Text;
                        cmd12.CommandText = "update  [money]  set Amount = " + a;
                        cmd12.ExecuteNonQuery();
                        con.Close();


                        //----------------------------------------------------------------updating with fitness and Trianer yes




                        clearOut();
                    }
                    else
                    {
                        con.Open();
                        SqlCommand cmd = con.CreateCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "INSERT into  [Member_Table] (Name,SubType,TrainerService,FeeStatus,Phone,AddDate,Father) values ('" + textBox3.Text.Trim() + "','Fitness Only','" + comboBox1.Text.Trim() + "','Paid','" + textBox4.Text.Trim() + "','" + dateTimePicker1.Text.Trim() + "','" + textBox5.Text.Trim() + "')";
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Success");


                        //----------------------------------------------------------------updating with fitness and Trianer No

                        con.Open();
                        SqlCommand cmd6 = con.CreateCommand();
                        cmd6.CommandType = CommandType.Text;
                        cmd6.CommandText = "Select * From [FeeStructure]";
                        cmd6.ExecuteNonQuery();
                        DataTable dt6 = new DataTable();
                        SqlDataAdapter sd6 = new SqlDataAdapter(cmd6);
                        sd6.Fill(dt6);
                        con.Close();



                        con.Open();
                        SqlCommand cmd11 = con.CreateCommand();
                        cmd11.CommandType = CommandType.Text;
                        cmd11.CommandText = "Select * From [money]";
                        cmd11.ExecuteNonQuery();
                        DataTable dt11 = new DataTable();
                        SqlDataAdapter sd11 = new SqlDataAdapter(cmd11);
                        sd11.Fill(dt11);
                        con.Close();

                        int a = (int)dt11.Rows[0][1];
                        int s = (int)dt6.Rows[0][3];
                        int b = (int)dt6.Rows[0][1];
                        
                        a = a + (s + b );
                        MessageBox.Show("Must Pay = " + (s + b ).ToString());
                        con.Open();
                        SqlCommand cmd12 = con.CreateCommand();
                        cmd12.CommandType = CommandType.Text;
                        cmd12.CommandText = "update  [money]  set Amount = " + a;
                        cmd12.ExecuteNonQuery();
                        con.Close();


                        //----------------------------------------------------------------updating with fitness and Trianer No




                        clearOut();
                    }

                }
            }
            else
            {
                MessageBox.Show("Error, Try Again");
            }
            button7.Enabled = false; //for load image button 
            button8.Enabled = false; //for upload image button 
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Select * From [FeeStructure] ";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter sd = new SqlDataAdapter(cmd);
                sd.Fill(dt);
                con.Close();



                label32.Text = dt.Rows[0][2].ToString();
            }
            else
            {
                label32.Text = "0";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button4.Enabled = true;
            clearOut();
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "Select MemberId,Name,Father,Phone,SubType,TrainerService,FeeStatus,AddDate From [Member_Table] ";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            sd.Fill(dt);
            con.Close();
            dataGridView1.DataSource = dt;

            button7.Enabled = false; //for load image button 
            button8.Enabled = false; //for upload image button 
        }
        //delete
        private void pictureBox18_Click(object sender, EventArgs e)
        {

        }
        //delete
        private void pictureBox17_Click(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            _dragging = true;  // _dragging is your variable flag
            _start_point = new Point(e.X, e.Y);
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            _dragging = false;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_dragging)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - this._start_point.X, p.Y - this._start_point.Y);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                button4.Enabled = false;
                record_id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                memid = record_id.ToString();
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                name = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                father = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                ph = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                String s = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                memshp = s;
                String tr = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                trn = tr;
                dat = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                if (s.Equals("Gym Only"))
                {
                    checkBox1.Checked = true;
                    checkBox2.Checked = false;

                }
                else if (s.Equals("Fitness Only"))
                {
                    checkBox2.Checked = true;
                    checkBox1.Checked = false;
                }
                else if (s.Equals("Gym & Fitness"))
                {
                    checkBox1.Checked = true;
                    checkBox2.Checked = true;
                }

                //for trainer
                if (tr.Equals("Yes"))
                {
                    comboBox1.SelectedIndex = 0;

                }
                else if (tr.Equals("No"))
                {
                    comboBox1.SelectedIndex = 1;
                }



                updateButton.Enabled = true;
                button7.Enabled = true;
                button9.Enabled = true;

            }
            catch (Exception)
            {
                
            }

        }

        private void clearOut()
        {
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            comboBox1.SelectedIndex = -1;
        }

        private void updateButton_Click(object sender, EventArgs e)
            {
                if (textBox3.Text != null && textBox4.Text != null && textBox5.Text != null && comboBox1.SelectedIndex != -1) {
                    if (checkBox1.Checked && !checkBox2.Checked)
                    {


                        if (comboBox1.SelectedIndex == 0)
                        {




                            con.Open();
                            SqlCommand cd = con.CreateCommand();
                            cd.CommandType = CommandType.Text;
                            cd.CommandText = "Update [Member_Table] set Name = '" + textBox3.Text.Trim() + "', Father= '" + textBox5.Text.Trim() + "',SubType ='Gym Only', Phone ='" + textBox4.Text.Trim() + "', TrainerService = '" + comboBox1.Text.Trim() + "' WHERE MemberId = " + record_id + "";
                            cd.ExecuteNonQuery();
                            con.Close();
                            MessageBox.Show("Update Successful ");
                            clearOut();

                        }
                        else
                        {

                            con.Open();
                            SqlCommand cd = con.CreateCommand();
                            cd.CommandType = CommandType.Text;
                            cd.CommandText = "Update Member_Table set Name = '" + textBox3.Text.Trim() + "', Father= '" + textBox5.Text.Trim() + "', Subtype = 'Gym Only', Phone ='" + textBox4.Text.Trim() + "', TrainerService ='" + comboBox1.Text.Trim() + "' WHERE MemberId = '" + record_id + "'";
                            cd.ExecuteNonQuery();
                            con.Close();
                            MessageBox.Show("Update Successful");
                            clearOut();

                        }
                    }
                    else if (checkBox1.Checked && checkBox2.Checked)
                    {




                        if (comboBox1.SelectedIndex == 0)
                        {
                            con.Open();
                            SqlCommand cd = con.CreateCommand();
                            cd.CommandType = CommandType.Text;
                            cd.CommandText = "Update Member_Table set Name = '" + textBox3.Text.Trim() + "', Father= '" + textBox5.Text.Trim() + "',SubType ='Gym & Fitness', Phone ='" + textBox4.Text.Trim() + "', TrainerService = '" + comboBox1.Text.Trim() + "' WHERE MemberId = " + record_id + "";
                            cd.ExecuteNonQuery();
                            con.Close();

                            MessageBox.Show("Update Successful");
                            clearOut();
                        }
                        else
                        {

                            con.Open();
                            SqlCommand cd = con.CreateCommand();
                            cd.CommandType = CommandType.Text;
                            cd.CommandText = "Update Member_Table set Name = '" + textBox3.Text.Trim() + "', Father= '" + textBox5.Text.Trim() + "',SubType ='Gym & Fitness', Phone ='" + textBox4.Text.Trim() + "', TrainerService = '" + comboBox1.Text.Trim() + "' WHERE MemberId = " + record_id + "";
                            cd.ExecuteNonQuery();
                            con.Close();
                            MessageBox.Show("Update Successful");
                            clearOut();

                        }
                    }
                    else if (!checkBox1.Checked && checkBox2.Checked)
                    {



                        if (comboBox1.SelectedIndex == 0)
                        {
                            con.Open();
                            SqlCommand cd = con.CreateCommand();
                            cd.CommandType = CommandType.Text;
                            cd.CommandText = "Update Member_Table set Name = '" + textBox3.Text.Trim() + "', Father= '" + textBox5.Text.Trim() + "',SubType ='Fitness Only', Phone ='" + textBox4.Text.Trim() + "', TrainerService = '" + comboBox1.Text.Trim() + "'  WHERE MemberId = " + record_id + "";
                            cd.ExecuteNonQuery();
                            con.Close();
                            MessageBox.Show("Success");
                            clearOut();
                        }
                        else
                        {
                            con.Open();
                            SqlCommand cd = con.CreateCommand();
                            cd.CommandType = CommandType.Text;
                            cd.CommandText = "Update Member_Table set Name = '" + textBox3.Text.Trim() + "', Father= '" + textBox5.Text.Trim() + "',SubType ='Fitness Only', Phone ='" + textBox4.Text.Trim() + "', TrainerService = '" + comboBox1.Text.Trim() + "' WHERE MemberId = " + record_id + "";
                            cd.ExecuteNonQuery();
                            con.Close();
                            MessageBox.Show("Success");
                            clearOut();
                        }



                    }
                } else
                {
                    MessageBox.Show("Error, try Again");
                }
            button7.Enabled = false; //for load image button 
            button8.Enabled = false; //for upload image button 
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            PrintDialog pd = new PrintDialog();
          
        }

        private void label6_Click(object sender, EventArgs e)
        {
            comboBox2.SelectedIndex = 0;

            HomePanel.Visible = false;
            panel5.Visible = false;
            panel6.Visible = false;
            payFeePanel.Visible = true;

           


            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "Select MemberId,Name,Father,SubType,FeeStatus,Phone,TrainerService,AddDate From [Member_Table] WHERE FeeStatus = 'Unpaid' ";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            sd.Fill(dt);
            con.Close();
            dataGridView2.DataSource = dt;

            DataGridViewColumn column = dataGridView2.Columns[0];
            DataGridViewColumn nameColumn = dataGridView2.Columns[1];
            DataGridViewColumn fatherColumn = dataGridView2.Columns[2];
            DataGridViewColumn column2 = dataGridView2.Columns[6];
            DataGridViewColumn column3 = dataGridView2.Columns[4];
            fatherColumn.Width = 130;
            nameColumn.Width = 130;
            column.Width = 40;
            column2.Width = 45;
            column3.Width = 55;

        }

        private void comboBox2_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex>=0)
            {
                textBox6.Enabled = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {






            if (comboBox2.SelectedIndex==0)
            {

                try
                {
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "Select  MemberId,Name,Father,SubType,FeeStatus,Phone,TrainerService,AddDate From [Member_Table] WHERE MemberId = '" + textBox6.Text.Trim() + "'";
                    cmd.ExecuteNonQuery();
                    DataTable dt = new DataTable();
                    SqlDataAdapter sd = new SqlDataAdapter(cmd);
                    sd.Fill(dt);
                    con.Close();
                    dataGridView2.DataSource = dt;


                    DataGridViewColumn column = dataGridView2.Columns[0];
                    DataGridViewColumn nameColumn = dataGridView2.Columns[1];
                    DataGridViewColumn fatherColumn = dataGridView2.Columns[2];
                    DataGridViewColumn column2 = dataGridView2.Columns[6];
                    DataGridViewColumn column3 = dataGridView2.Columns[4];
                    fatherColumn.Width = 130;
                    nameColumn.Width = 130;
                    column.Width = 40;
                    column2.Width = 45;
                    column3.Width = 55;

                }
                catch (Exception)
                {
                    MessageBox.Show("Sorry, something went wrong. Try again");
                    con.Close();
                }





            }
            else if (comboBox2.SelectedIndex==1)
            {

                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "Select  MemberId,Name,Father,SubType,FeeStatus,Phone,TrainerService From [Member_Table] WHERE Name like  '%" + textBox6.Text.Trim() + "%'";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter sd = new SqlDataAdapter(cmd);
                sd.Fill(dt);
                con.Close();
                dataGridView2.DataSource = dt;



            }
            else if (comboBox2.SelectedIndex==2)
            {

                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "Select  MemberId,Name,Father,SubType,FeeStatus,Phone,TrainerService From [Member_Table] WHERE Father like '%" + textBox6.Text.Trim() + "%'";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter sd = new SqlDataAdapter(cmd);
                sd.Fill(dt);
                con.Close();
                dataGridView2.DataSource = dt;



            }
            else
            {

            }


        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void checkBox3_CheckedChanged_1(object sender, EventArgs e)
        {
            
        }

        private void button2_Click_2(object sender, EventArgs e)
        {
           
        }

        private void checkBox3_CheckedChanged_2(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)//For Search Query in the Registration Panel
            {
                comboBox3.SelectedIndex = 0;
                comboBox3.Enabled = true;
                textBox7.Enabled = true;
                button2.Enabled = true;
            }
            else
            {
                comboBox3.Enabled = false;
                textBox7.Enabled = false;
                button2.Enabled = false;
            }
        }

        private void button2_Click_3(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex == 0)//for memberID
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "Select MemberId,Name,Father,Phone,SubType,TrainerService,FeeStatus,AddDate From [Member_Table] Where MemberId = '" + textBox7.Text.Trim() + "'";
                    cmd.ExecuteNonQuery();
                    DataTable dt = new DataTable();
                    SqlDataAdapter sd = new SqlDataAdapter(cmd);
                    sd.Fill(dt);
                    con.Close();
                    dataGridView1.DataSource = dt;
                }
                catch (Exception)
                {
                    con.Close();
                }

            }
            else if (comboBox3.SelectedIndex == 1)//for Name
            {
                
                
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "Select MemberId,Name,Father,Phone,SubType,TrainerService,FeeStatus,AddDate From [Member_Table] Where Name Like  '%" + textBox7.Text.Trim() + "%'";
                    cmd.ExecuteNonQuery();
                    DataTable dt = new DataTable();
                    SqlDataAdapter sd = new SqlDataAdapter(cmd);
                    sd.Fill(dt);
                    con.Close();
                    dataGridView1.DataSource = dt;
              
               
            }
            else if (comboBox3.SelectedIndex == 2)//for Father
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Select MemberId,Name,Father,Phone,SubType,TrainerService,FeeStatus,AddDate From [Member_Table] Where Father Like  '%" + textBox7.Text.Trim() + "%'";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter sd = new SqlDataAdapter(cmd);
                sd.Fill(dt);
                con.Close();
                dataGridView1.DataSource = dt;
            }
            else if (comboBox3.SelectedIndex == 3)//for Phone
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Select MemberId,Name,Father,Phone,SubType,TrainerService,FeeStatus,AddDate From [Member_Table] Where Phone =  '" + textBox7.Text.Trim() + "'";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter sd = new SqlDataAdapter(cmd);
                sd.Fill(dt);
                con.Close();
                dataGridView1.DataSource = dt;
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                record_id = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString());
                Name = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
                Father = dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString();
                subs = dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString();
                phone = dataGridView2.Rows[e.RowIndex].Cells[5].Value.ToString();
                TrainerServ = dataGridView2.Rows[e.RowIndex].Cells[6].Value.ToString();
                feestatus = dataGridView2.Rows[e.RowIndex].Cells[4].Value.ToString();
                date = dataGridView2.Rows[e.RowIndex].Cells[7].Value.ToString();


                button6.Enabled = true;
            }catch (Exception)
            {

            }

            



            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //button for pay fee

            payfeeForm pf = new payfeeForm(record_id.ToString(), Name, Father, phone, subs, TrainerServ, date,dateTimePicker2.Text.Trim()) ;
            pf.ShowDialog();

            label6_Click(sender, e);
            
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "JPG Files(*.jpg)*.jpg|PNG Files (*.png)*.png|All Files(*.*)|*.*";
            dlg.Title = "Member Picture Upload";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                String picloc = dlg.FileName.ToString();
                textBox8.Text = picloc;
                pictureBox17.ImageLocation = picloc;
                button8.Enabled = true;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {


            con.Open();
            SqlCommand cd = con.CreateCommand();
            cd.CommandType = CommandType.Text;
            cd.CommandText = "Select * from [Table] Where ImageId ="+record_id;
            cd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter sd = new SqlDataAdapter(cd);
            sd.Fill(dt);
            con.Close();

            if (dt.Rows.Count > 0)
            {
                
                  
                
                    byte[] imgbyt = null;
                    FileStream fst = new FileStream(this.textBox8.Text, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fst);
                    imgbyt = br.ReadBytes((int)fst.Length);

                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "Update  [Table]  set  image = @IMG where ImageId = "+record_id;
                    cmd.Parameters.Add(new SqlParameter("@IMG", imgbyt));
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Image Updated");
                    pictureBox17.Image.Dispose();
                    pictureBox17.Image = null;
                    textBox8.Text = "";
                    button7.Enabled = false; //for load image button 
                    button8.Enabled = false; //for upload image button 

                }
            else
            {
                byte[] imgbyt = null;
                FileStream fst = new FileStream(this.textBox8.Text, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fst);
                imgbyt = br.ReadBytes((int)fst.Length);


                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Insert into [Table] (ImageId,image) values (@id,@IMG)  ";
                cmd.Parameters.Add(new SqlParameter("@id", record_id));
                cmd.Parameters.Add(new SqlParameter("@IMG", imgbyt));
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Image Inserted");
                pictureBox17.Image.Dispose();
                pictureBox17.Image = null;
                textBox8.Text = "";
                button7.Enabled = false; //for load image button 
                button8.Enabled = false; //for upload image button 
            }
            





         

        }

        private void button9_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2(memid,name,father,ph,memshp,trn,dat);
            f.Show();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            HomePanel.Visible = false;
            panel5.Visible = false;
            payFeePanel.Visible = false;
            panel6.Visible = true;
            
               
        }

        private void label35_MouseEnter(object sender, EventArgs e)
        {
            label35.Cursor = Cursors.Hand;
        }

        private void label35_MouseLeave(object sender, EventArgs e)
        {
            label35.Cursor = Cursors.Default;
        }

        private void label36_MouseEnter(object sender, EventArgs e)
        {
        }

        private void label36_MouseLeave(object sender, EventArgs e)
        {
            
        }

        private void label37_MouseEnter(object sender, EventArgs e)
        {
            label37.Cursor = Cursors.Hand;
        }

        private void label37_MouseLeave(object sender, EventArgs e)
        {
            label37.Cursor = Cursors.Default;
        }

        private void label38_MouseEnter(object sender, EventArgs e)
        {
            label38.Cursor = Cursors.Hand;
        }

        private void label38_MouseLeave(object sender, EventArgs e)
        {
            label38.Cursor = Cursors.Default;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            //for checking existing credentials 
           


        }

        private void label35_Click(object sender, EventArgs e)
        {
            panel9.Visible = false;
            panel10.Visible = false;
            panel12.Visible = false;
            panel8.Visible = true;
            

        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Select * From Credendials Where Username = '" + textBox12.Text.Trim() + "'and  Password = '" + textBox11.Text.Trim() + "'";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter sd = new SqlDataAdapter(cmd);
                sd.Fill(dt);
                con.Close();
            
           

            if (dt.Rows.Count > 0)
            {

                idForupdat = dt.Rows[0][2].ToString();

                label48.Text = "Exists";
                label48.ForeColor = System.Drawing.Color.Green;
                newPassText.Visible = true;
                newUserText.Visible = true;
                button12.Visible = true;
                label45.Visible = true;
                label46.Visible = true;

            }
            else
            {
                label48.Text = "No IDs";
                label48.ForeColor = System.Drawing.Color.Red;
                textBox11.Text = null;
                textBox12.Text = null;
                textBox11.Text = null;
                textBox12.Text = null;
              
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {

            if (newUserText.Text.Length > 0 && newPassText.Text.Length > 0)
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Update Credendials set Username = '" + newUserText.Text.Trim() + "', Password ='" + newPassText.Text.Trim() + "' Where id = " + idForupdat;
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Credentials Changed");
                newPassText.Visible = false;
                newUserText.Visible = false;
                button12.Visible = false;
                label45.Visible = false;
                label46.Visible = false;
                textBox11.Text = null;
                textBox12.Text = null;
                label48.Text = "-";
            }
            else
            {
                MessageBox.Show("The Crendentials were Entered, Try Again", "Credentials Error",
    MessageBoxButtons.OK, MessageBoxIcon.Error);
                newPassText.Visible = false;
                newUserText.Visible = false;
                button12.Visible = false;
                label45.Visible = false;
                label46.Visible = false;
                textBox11.Text = null;
                textBox12.Text = null;
                label48.Text = "-";
            }
        }

        private void checkBox4_CheckedChanged_1(object sender, EventArgs e)
        {
            if(checkBox4.Checked == true)
            {
                textBox9.Enabled = true;
                textBox10.Enabled = true;
                button10.Enabled = true;
            }
            else
            {
                textBox9.Enabled = false;
                textBox10.Enabled = false;
                button10.Enabled = false;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (ct == 1)

            {

                if (textBox9.Text.Length > 0 && textBox10.Text.Length > 0)
                {


                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "Select * From Credendials Where Username = '" + textBox9.Text.Trim() + "'and  Password = '" + textBox10.Text.Trim() + "'";
                    cmd.ExecuteNonQuery();
                    DataTable dt = new DataTable();
                    SqlDataAdapter sd = new SqlDataAdapter(cmd);
                    sd.Fill(dt);
                    con.Close();


                    if (dt.Rows.Count > 0)
                    {
                        ct = 2;
                        button10.Text = "ADD";

                        MessageBox.Show("Admin Id is Confirmed, You can now add new user", "Admin ID Verified",
 MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        textBox9.Text = null;
                        textBox10.Text = null;
                        label41.Text = "New User";
                        label42.Text = "New Pass";
                        label41.ForeColor = System.Drawing.Color.Green;
                        label42.ForeColor = System.Drawing.Color.Green;

                    }
                    else
                    {
                        MessageBox.Show("Admin Identification Failed", "Admin Unidentified",
MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBox9.Text = null;
                        textBox10.Text = null;
                    }




                }
                else
                {
                    MessageBox.Show("The Crendentials Were not Entered, Try Again", "Credentials Error",
     MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else if (ct == 2)
            {
                if (textBox9.Text.Length > 0 && textBox10.Text.Length > 0)
                {
                    con.Open();
                    SqlCommand cmd2 = con.CreateCommand();
                    cmd2.CommandType = CommandType.Text;
                    cmd2.CommandText = "Insert into [Credendials] (Username,Password) values (@Username,@Password)  ";
                    cmd2.Parameters.Add(new SqlParameter("@Username", textBox9.Text.Trim()));
                    cmd2.Parameters.Add(new SqlParameter("@Password", textBox10.Text.Trim()));
                    cmd2.ExecuteNonQuery();
                    con.Close();

                    textBox10.Text = null;
                    textBox9.Text = null;
                    MessageBox.Show("New User was Added");
                    ct = 1;

                    button10.Text = "Verify";
                    textBox9.Text = null;
                    textBox10.Text = null;

                    label41.Text = "Username";
                    label42.Text = "Password";
                    label41.ForeColor = System.Drawing.Color.Blue;
                    label42.ForeColor = System.Drawing.Color.Blue;

                }
                else
                {

                    MessageBox.Show("The Crendentials Were not Entered, Try Again", "Credentials Error",
     MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void label37_Click(object sender, EventArgs e)
        {
            panel8.Visible = false;
            panel12.Visible = false;
            panel10.Visible = false;
            panel9.Visible = true;
            dataGridView3.ForeColor = Color.Black;
           
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox5.Checked == true)
            {

                textBox13.Enabled = true;
                textBox14.Enabled = true;
                textBox15.Enabled = true;
                textBox16.Enabled = true;
                button16.Enabled = true;
            }
            else
            {
                textBox13.Enabled = false;
                textBox14.Enabled = false;
                textBox15.Enabled = false;
                textBox16.Enabled = false;
                button16.Enabled = false;
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {


            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select Id, SupplementName as Name, SupplementWeight as Weight, SupplementQty as Qty, SupplementPrice as Price From Supplements";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            sd.Fill(dt);
            con.Close();
            dataGridView3.DataSource = dt;



            
            DataGridViewColumn nameColumn = dataGridView3.Columns[1];
    
            nameColumn.Width = 130;
           

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                checkBox5.Checked = true;
                button16.Enabled = false;
                button14.Enabled = true;
                button15.Enabled = true;
                record_id = Convert.ToInt32(dataGridView3.Rows[e.RowIndex].Cells[0].Value.ToString());
                textBox13.Text = dataGridView3.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBox14.Text = dataGridView3.Rows[e.RowIndex].Cells[2].Value.ToString();
                textBox15.Text = dataGridView3.Rows[e.RowIndex].Cells[3].Value.ToString();
                suppQty = Convert.ToInt32(dataGridView3.Rows[e.RowIndex].Cells[3].Value.ToString());
                textBox16.Text = dataGridView3.Rows[e.RowIndex].Cells[4].Value.ToString();
            }
            catch (Exception)
            {
                checkBox5.Checked = false;
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {

            if (textBox13.Text.Length >0 && textBox14.Text.Length >0 && textBox15.Text.Length > 0 && textBox16.Text.Length >0)
            {

                try
                {
                    float weight = float.Parse(textBox14.Text);
                    float price = float.Parse(textBox16.Text);
                    int qty = int.Parse(textBox15.Text);


                    con.Open();
                    SqlCommand cmd2 = con.CreateCommand();
                    cmd2.CommandType = CommandType.Text;
                    cmd2.CommandText = "Insert into [Supplements] (SupplementName,SupplementWeight,SupplementQty,SupplementPrice) values (@Name,@Weight,@Qty,@price)  ";


                    cmd2.Parameters.Add(new SqlParameter("@Name", textBox13.Text.Trim()));
                    cmd2.Parameters.Add(new SqlParameter("@Weight",weight));
                    cmd2.Parameters.Add(new SqlParameter("@Qty", qty));
                    cmd2.Parameters.Add(new SqlParameter("@price", price));
                    cmd2.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Supplement Stock Added");

                    textBox13.Text = null;
                    textBox14.Text = null;
                    textBox15.Text = null;
                    textBox16.Text = null;

                }
                catch (Exception)
                {
                    MessageBox.Show("Make Sure Weight is in (0.0), Price is in (0.0) Format", "Details Format Error",
    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }



            }
            else
            {

            }

          







        }

        private void button17_Click(object sender, EventArgs e)
        {



            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select Id, SupplementName as Name, SupplementWeight as Weight, SupplementQty as Qty, SupplementPrice as Price From [Supplements] Where SupplementName like '%"+textBox17.Text.Trim()+"%'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            sd.Fill(dt);
            con.Close();
            dataGridView3.DataSource = dt;

            DataGridViewColumn nameColumn = dataGridView3.Columns[1];

            nameColumn.Width = 130;



        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

            float pr = float.Parse(comboBox4.SelectedItem.ToString());


            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select Id, SupplementName as Name, SupplementWeight as Weight, SupplementQty as Qty, SupplementPrice as Price From [Supplements] Where SupplementPrice <"+pr;
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            sd.Fill(dt);
            con.Close();
            dataGridView3.DataSource = dt;

            DataGridViewColumn nameColumn = dataGridView3.Columns[1];

            nameColumn.Width = 130;

        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {



            float pr = float.Parse(comboBox5.SelectedItem.ToString());


            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select Id, SupplementName as Name, SupplementWeight as Weight, SupplementQty as Qty, SupplementPrice as Price From [Supplements] Where SupplementWeight <" + pr;
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            sd.Fill(dt);
            con.Close();
            dataGridView3.DataSource = dt;

            DataGridViewColumn nameColumn = dataGridView3.Columns[1];

            nameColumn.Width = 130;





        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                checkBox5.Checked = true;
                button16.Enabled = false;
                button14.Enabled = true;
                button15.Enabled = true;
                record_id = Convert.ToInt32(dataGridView3.Rows[e.RowIndex].Cells[0].Value.ToString());
                textBox13.Text = dataGridView3.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBox14.Text = dataGridView3.Rows[e.RowIndex].Cells[2].Value.ToString();
                textBox15.Text = dataGridView3.Rows[e.RowIndex].Cells[3].Value.ToString();
                suppQty = Convert.ToInt32(dataGridView3.Rows[e.RowIndex].Cells[3].Value.ToString());
                textBox16.Text = dataGridView3.Rows[e.RowIndex].Cells[4].Value.ToString();
            }
            catch (Exception)
            {
                checkBox5.Checked = false;
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {




            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Update  [Supplements]  set  SupplementName = @name , SupplementWeight = @wgt , SupplementQty =@qty, SupplementPrice=@prc Where Id =" +record_id;
            cmd.Parameters.Add(new SqlParameter("@name", textBox13.Text.Trim() ));
            cmd.Parameters.Add(new SqlParameter("@wgt", float.Parse(textBox14.Text)));
            cmd.Parameters.Add(new SqlParameter("@qty", int.Parse(textBox15.Text)));
            cmd.Parameters.Add(new SqlParameter("@prc", float.Parse(textBox16.Text)));
            cmd.ExecuteNonQuery();
            con.Close();


            MessageBox.Show("Records Updated");

            textBox13.Text = null;
            textBox14.Text = null;
            textBox15.Text = null;
            textBox16.Text = null;
            checkBox5.Checked = false;
            button14.Enabled = false;
            button15.Enabled = false;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to sell Item: "+textBox13.Text.Trim()+" For Price: "+textBox16.Text.Trim()+"", "Supplements Sales", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {

                if (suppQty >= 0)
                {
                    suppQty = suppQty - 1;



                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "Update  [Supplements]  set   SupplementQty =@qty  Where Id =" + record_id;
                    cmd.Parameters.Add(new SqlParameter("@qty", suppQty));
                    cmd.ExecuteNonQuery();
                    con.Close();

                    

                    textBox13.Text = null;
                    textBox14.Text = null;
                    textBox15.Text = null;
                    textBox16.Text = null;
                    checkBox5.Checked = false;

                }
                else
                {
                    MessageBox.Show("Not Enough Stocks Available");

                }


            }
            else if (dialogResult == DialogResult.No)
            {
                textBox13.Text = null;
                textBox14.Text = null;
                textBox15.Text = null;
                textBox16.Text = null;
                checkBox5.Checked = false;
            }

        }

        private void label74_MouseEnter(object sender, EventArgs e)
        {
            label74.Cursor = Cursors.Hand;
        }

        private void label74_DragLeave(object sender, EventArgs e)
        {

        }

        private void label74_MouseLeave(object sender, EventArgs e)
        {
            label74.Cursor = Cursors.Hand;
        }

        private void label38_Click(object sender, EventArgs e)
        {
            panel9.Visible = false;
            panel8.Visible = false;
            panel12.Visible = false;
            panel10.Visible = true;

            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "Select  * From [FeeStructure]";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            sd.Fill(dt);
            con.Close();


            



            textBox18.Text = dt.Rows[0][0].ToString();
            textBox21.Text = dt.Rows[0][2].ToString();
            textBox19.Text = dt.Rows[0][1].ToString();
            textBox22.Text = dt.Rows[0][3].ToString();
        }

        private void panel10_Click(object sender, EventArgs e)
        {
            
        }

        private void button18_Click(object sender, EventArgs e)
        {


            if(textBox18.Text.Length > 0)
            {


                try
                {

                    int a = int.Parse(textBox18.Text);

                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "Update [FeeStructure] set Gym = "+a;
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Gym Only Fee Changed to Rs "+a+" Monthly");

                }
                catch(Exception E)
                {
                    MessageBox.Show("Fee Must be Proper Amounts(500,1000..)");
                  
                }



            }
            else
            {
                MessageBox.Show("Error, Try Again");
            }




        }

        private void button19_Click(object sender, EventArgs e)
        {
            if(textBox19.Text.Length > 0)
            {

                try
                {
                   int x = int.Parse(textBox19.Text);

                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "Update [FeeStructure] set Fitness = " + x;
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Fitness Only Fee Changed to Rs " + x + " Monthly");


                }
                catch (Exception)
                {
                    MessageBox.Show("Fee Must be Proper Amounts(500,1000..)");
                }

            }
            else
            {
                MessageBox.Show("Error, Try Again");
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {


            if (textBox21.Text.Length > 0)
            {

                try
                {
                    int x = int.Parse(textBox21.Text);

                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "Update [FeeStructure] set Trainer = " + x;
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Trainer Fee Changed to Rs " + x + " Monthly");


                }
                catch (Exception)
                {
                    MessageBox.Show("Fee Must be Proper Amounts(500,1000..)");
                }

            }
            else
            {
                MessageBox.Show("Error, Try Again");
            }

        }

        private void button22_Click(object sender, EventArgs e)
        {


            if (textBox22.Text.Length > 0)
            {

                try
                {
                    int x = int.Parse(textBox22.Text);

                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "Update [FeeStructure] set Admission = " + x;
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Admission Fee Changed to Rs " + x + " Monthly");


                }
                catch (Exception)
                {
                    MessageBox.Show("Fee Must be Proper Amounts(500,1000..)");
                }

            }
            else
            {
                MessageBox.Show("Error, Try Again");
            }




        }

        private void button20_Click(object sender, EventArgs e)
        {


            


        }

        private void label74_Click(object sender, EventArgs e)
        {
            panel8.Visible = false;
            panel9.Visible = false;
            panel10.Visible = false;
            panel12.Visible = true;
        }

        private void label8_Click(object sender, EventArgs e)
        {
            panel4.Visible = false;
            HomePanel.Visible = false;
            payFeePanel.Visible = false;
            panel6.Visible = false;
            panel5.Visible = false;
            LogoPanel.Visible = true;

            usernamePassPanel.Visible = true;
            textBox1.Text = null;
            textBox2.Text = null;
            button1.Visible = true;
        }

        private void dataGridView3_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                checkBox5.Checked = true;
                button16.Enabled = false;
                button14.Enabled = true;
                button15.Enabled = true;
                record_id = Convert.ToInt32(dataGridView3.Rows[e.RowIndex].Cells[0].Value.ToString());
                textBox13.Text = dataGridView3.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBox14.Text = dataGridView3.Rows[e.RowIndex].Cells[2].Value.ToString();
                textBox15.Text = dataGridView3.Rows[e.RowIndex].Cells[3].Value.ToString();
                suppQty = Convert.ToInt32(dataGridView3.Rows[e.RowIndex].Cells[3].Value.ToString());
                textBox16.Text = dataGridView3.Rows[e.RowIndex].Cells[4].Value.ToString();
            }
            catch (Exception)
            {
                checkBox5.Checked = false;
            }
        }

        private void dataGridView3_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                checkBox5.Checked = true;
                button16.Enabled = false;
                button14.Enabled = true;
                button15.Enabled = true;
                record_id = Convert.ToInt32(dataGridView3.Rows[e.RowIndex].Cells[0].Value.ToString());
                textBox13.Text = dataGridView3.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBox14.Text = dataGridView3.Rows[e.RowIndex].Cells[2].Value.ToString();
                textBox15.Text = dataGridView3.Rows[e.RowIndex].Cells[3].Value.ToString();
                suppQty = Convert.ToInt32(dataGridView3.Rows[e.RowIndex].Cells[3].Value.ToString());
                textBox16.Text = dataGridView3.Rows[e.RowIndex].Cells[4].Value.ToString();
            }
            catch (Exception)
            {
                checkBox5.Checked = false;
            }
        }

        private void button20_Click_1(object sender, EventArgs e)
        {
            if (textBox20.Text.Length > 0 && textBox23.Text.Length > 0)
            {

                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Select * From [Credendials] Where Username = @usr and Password = @ps";
                cmd.Parameters.Add(new SqlParameter("@usr", textBox20.Text.Trim()));
                cmd.Parameters.Add(new SqlParameter("@ps", textBox23.Text.Trim()));
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter sd = new SqlDataAdapter(cmd);
                sd.Fill(dt);
                con.Close();


                if (dt.Rows.Count > 0)
                {

                    DialogResult dialogResult = MessageBox.Show("Are You Sure you want to start a new month?", "New Month Commencement", MessageBoxButtons.YesNo);

                    if (dialogResult == DialogResult.Yes)
                    {


                        con.Open();
                        SqlCommand cmd2 = con.CreateCommand();
                        cmd2.CommandType = CommandType.Text;
                        cmd2.CommandText = "Update  [Member_Table]  set  FeeStatus = @s ";
                        cmd2.Parameters.Add(new SqlParameter("@s", "Unpaid"));
                        cmd2.ExecuteNonQuery();
                        con.Close();

                        con.Open();
                        SqlCommand cmd3 = con.CreateCommand();
                        cmd3.CommandType = CommandType.Text;
                        cmd3.CommandText = "Update  [money]  set  amount = 0 Where Id = 1 ";

                        cmd3.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("New Month Has Started");
                        textBox20.Text = null;
                        textBox23.Text = null;

                    }


                }
                else
                {
                    MessageBox.Show("Bad Credentials");
                    textBox20.Text = null;
                    textBox23.Text = null;
                }


            }
            else
            {
                MessageBox.Show("Empty Fields");
            }
        }
    }
    }




 













