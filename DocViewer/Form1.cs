using OutsideIn;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DocViewer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {

               

            }
            catch (Exception er)
            {


            }
        }
        private Image GetCopyImage(string path)
        {
            try
            {
  using (Image im = Image.FromFile(path))
            {
                Bitmap bm = new Bitmap(im);
                return bm;
            }
            }
            catch (Exception er)
            {

                return null;
            }
          
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                pictureBox1.Image = null;

                 var inputFilename = textBox1.Text;
                var outfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                outfolder = Path.Combine(outfolder, "DocViewer");
                var outfile = Path.Combine(outfolder, "r.png");
                Exporter exporter = OutsideIn.OutsideIn.NewLocalExporter();

                exporter.SetPerformExtendedFI(true );
                int timezoneOffset = exporter.GetTimeZoneOffset();
                Text = timezoneOffset + "";

                if (Directory.Exists(outfolder))
                    Directory.Delete(outfolder, true);
                Directory.CreateDirectory(outfolder);
              
                exporter.SetSourceFile(inputFilename);

                exporter.SetDestinationFile(outfile);

                // subdocId is the sequential number of the node in the archive file



                exporter.SetDestinationFormat(FileFormat.FI_PNG   );
                //exporter.SetCallbackHandler(callback);

                exporter.ExtractEmbeddedFiles=  OutsideIn.Options.Options.ExtractEmbeddedFilesValue.Binary  ;
               // exporter.
                exporter.Export();
                listBox1.Items.Clear();
                foreach (var item in Directory.EnumerateFiles(outfolder))
                {
                    listBox1.Items.Add(item + "");
                }
                //pictureBox1.ImageLocation=(outfile);
                //pictureBox1.Load();

                if (listBox1.Items.Count > 0)
                    listBox1.SelectedIndex = 0;
            }
            catch (Exception er)
            {

                MessageBox.Show(er.Message);
            }



        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            Image im = GetCopyImage(listBox1.Text);
            pictureBox1.Image = im;
          
        }



        //   CallbackHandler callback = new CallbackHandler();


    }
    //class CallbackHandler : Callback

    //{
    //    public override CreateNewFileResponse CreateNewFile(FileFormat ParentOutputId, FileFormat OutputId, AssociationValue Association, string FilePath)
    //    {
    //        return base.CreateNewFile(ParentOutputId, OutputId, Association, FilePath);
    //    }
    //    public override NewPageResponse PrepareNewPage(long PageNumber)
    //    {
    //        return base.PrepareNewPage(PageNumber);
    //    }
    //}
}
