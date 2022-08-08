using Novid.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Message = Novid.Model.Message;

namespace Novid
{
    public partial class Form1 : Form
    {
        private System.Drawing.Color vert = System.Drawing.ColorTranslator.FromHtml("#05664f");
        public Form1()
        {
            InitializeComponent();


            
            panel1.Visible = true;

            Button btn = button1;
            btn.BackColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderColor = vert;
            btn.FlatAppearance.BorderSize = 1;

            Button btn2 = button2;
            btn2.BackColor = Color.White;
            btn2.FlatStyle = FlatStyle.Flat;
            btn2.FlatAppearance.BorderColor = vert;
            btn2.FlatAppearance.BorderSize = 1;



            panel2.Visible = true;
            panel3.Visible = false;
            panel4.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel3.Visible = false;
            panel4.Visible = false;
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            // var dataToUse = new Service().GetAsync("");
            //var JsonArray = JsonConvert.Deserial­izeObject<dynamic>(d­ataToUse);

          
            if(this.comboBox2.DataSource == null)
            {
                var personnes = await new Personne().GetAllPersonne();
                Dictionary<string, string> comboSource = new Dictionary<string, string>();

                foreach (var p in  personnes)
                {
                    comboSource.Add(p._id, p.Nom + " " + p.Prenom);
                }

                this.comboBox2.DataSource = new BindingSource(comboSource, null);
                this.comboBox2.DisplayMember = "Value";
                this.comboBox2.ValueMember = "Key";
            }

            if (this.comboBox3.DataSource == null)
            {
                var centres = await new Centre().GetAllCentre();
                Dictionary<string, string> comboSource = new Dictionary<string, string>();

                foreach (var c in centres)
                {
                    comboSource.Add(c._id, c.Nom_centre );
                }

                this.comboBox3.DataSource = new BindingSource(comboSource, null);
                this.comboBox3.DisplayMember = "Value";
                this.comboBox3.ValueMember = "Key";
            }

            if (this.comboBox1.DataSource == null)
            {
                Dictionary<string, string> comboSource = new Dictionary<string, string>();
                comboSource.Add("1", "positif");
                comboSource.Add("2", "négatif");

                this.comboBox1.DataSource = new BindingSource(comboSource, null);
                this.comboBox1.DisplayMember = "Value";
                this.comboBox1.ValueMember = "Key";
            }



            panel3.Visible = true;
            panel4.Visible = false;
        }


        private async void panel3_Paint(object sender, PaintEventArgs e)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle,
                                                            ColorTranslator.FromHtml("#05664f"),
                                                            ColorTranslator.FromHtml("#192806"),
                                                            90F))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }



        
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle,
                                                              ColorTranslator.FromHtml("#05664f"),
                                                              ColorTranslator.FromHtml("#192806"),
                                                              90F))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }

            this.LabelIDCarte.BackColor = System.Drawing.Color.Transparent;
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label1.BackColor = System.Drawing.Color.Transparent;

        }


        private void button3_Click(object sender, EventArgs e)
        {
            button2_Click(sender,e);
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }
        private async void panel4_Paint(object sender, PaintEventArgs e)
        {

                this.pictureBox4.Visible = false;
                this.pictureBox6.Visible = false;
            this.comboBox2.Visible = false;
                this.comboBox3.Visible = false;

            var test = new Test
                {
                    PersonneID = this.comboBox2.SelectedValue.ToString(),
                    DateTest = this.dateTimePicker1.Value,
                    CentreID = this.comboBox3.SelectedValue.ToString(),
                    EtatTest = Int32.Parse(this.comboBox1.SelectedValue.ToString())
                };

            string id =  await test.Insert(test);
                if (!String.IsNullOrEmpty(id))
                {
                    Zen.Barcode.CodeQrBarcodeDraw qrcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
                    pictureBox5.Image = qrcode.Draw(id, 235);
                    pictureBox5.BorderStyle = BorderStyle.FixedSingle;
                }

            if(test.EtatTest == 2)
            {
                this.label5.Text = "Test du " + test.DateTest + " négatif";
            }
            

            if (test.EtatTest == 1)
            {
                this.label5.Text = "Test du " + test.DateTest + " positif";
                var passage = await new HistoriquePassage().GetHistoriquePersonne(test.PersonneID);
                var lieux = passage.Select(x => x.Lieu_id).Distinct().ToList();
                List<String> idPersonne = new List<String>();
                HistoriquePassage p = new HistoriquePassage();
                foreach (var l in lieux)
                {
                   var hlieu = await p.GetHistoriqueLieu(l);
                    foreach(var h in hlieu)
                    {
                        idPersonne.Add(h.Personne_id);
                    }
                }

                idPersonne = idPersonne.Distinct().ToList();
                var notif = new Message();
                var text = " Un lieu que vous avez fréquentez récemment a recencé un cas positif. Pour plus de certitudes, veuillez effectuer un test et ne pas vous mélanger.";
                foreach (var per in idPersonne){
                    var temp = new Message
                    {
                        MessageContenu = text,
                        PersonneID = per,
                        CentreID = test.CentreID
                    };
                    await notif.Insert(temp);
                }
            }

        }

        private void  button4_Click(object sender, EventArgs e)
        {
           // button5.Visible = false;
            panel4.Visible = true;

        }
 
        private void resetTest()
        {
            this.comboBox2.SelectedIndex = 0;
            this.comboBox1.SelectedIndex = 0;
            this.comboBox3.SelectedIndex = 0;
            this.dateTimePicker1.Value = DateTime.Now;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

       
    }
}
