using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using demo.ServiceReference1;
using System.IO;

namespace demo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataSet dsResult = new DataSet();
            try
            {

                var demdata = new Service1SoapClient();

                DataSet ds = demdata.GetDocumentazione();

                DataTable dt = ds.Tables[0];
                byte[] bytes = (byte[])dt.Rows[0]["Documento"];
                    


                ByteToImage(bytes);

                //System.Drawing.ImageConverter imageConverter = new System.Drawing.ImageConverter();
                //System.Drawing.Image image = imageConverter.ConvertFrom(bytes) as System.Drawing.Image;
                //System.Drawing.Bitmap b = new System.Drawing.Bitmap(image);
                //b.Save("D:\tets", System.Drawing.Imaging.ImageFormat.Jpeg);
                //b.Dispose();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Bitmap ByteToImage(byte[] blob)
        {
            MemoryStream mStream = new MemoryStream();
            byte[] pData = blob;
            mStream.Write(pData, 0, Convert.ToInt32(pData.Length));
            Bitmap bm = new Bitmap(mStream, false);
            mStream.Dispose();
            return bm;

        }

        public void byteArrayToImage(byte[] byteArrayIn)
        {
            //Stream ms = new MemoryStream(byteArrayIn);
            //Image returnImage = Image.FromStream(ms);
            //return returnImage;


            System.Drawing.Image newImage;

            string strFileName = GetTempFolderName() + "yourfilename.jpeg";

            if (byteArrayIn != null)
            {

                //MemoryStream stream = new MemoryStream(byteArrayIn);
                //stream.Seek(0, SeekOrigin.Begin);

                MemoryStream streamBitmap = new MemoryStream(byteArrayIn);
                streamBitmap.Position = 0;
                pictureBox1.Image = Image.FromStream((Stream)streamBitmap);

                //newImage = System.Drawing.Image.FromStream(stream);
                //newImage.Save(strFileName);

            }
        }

        private static string GetTempFolderName()
        {

            string strTempFolderName = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache) + @"\";

            if (Directory.Exists(strTempFolderName))
            {
                return strTempFolderName;
            }
            else
            {
                Directory.CreateDirectory(strTempFolderName);
                return strTempFolderName;
            }

        }

    }
}
