using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImgConverter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var con = new SqlConnection(@"Data Source=(local)\SQLEXPRESS;Initial Catalog=Employee;Integrated Security=True;");

            con.Open();

            var cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from recruit where recruitId = 1";

            var dr = cmd.ExecuteReader();
            if(dr.Read())
            {
                var cv = Convert.FromBase64String(dr["cv"].ToString());
                var img = Convert.FromBase64String(dr["face"].ToString());

                // save to folder
                var path = @"C:\Users\johnm\Desktop\Test\";
                var absoluteCv = $"{path}test.pdf";
                var absoluteImg = $"{path}test.png";
                File.WriteAllBytes(absoluteCv, cv);

                // fetch to picturebox
                var ms = new MemoryStream(img);
                pictureBox1.Image = Image.FromStream(ms);

                // save to folder with proper format
                Image imgStream = Image.FromStream(ms);
                imgStream.Save(absoluteImg, ImageFormat.Png);
            }
            dr.Close();
            con.Close();
        }
    }
}
