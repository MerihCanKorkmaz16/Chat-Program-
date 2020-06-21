using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RemoteObject;
using System.Runtime.Remoting.Channels;
using System.Collections;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting;

namespace Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            label6.Text = Form2.isim.ToString();
            
            durums(true);
        }
        Graphics g;
        messObject mess;
        remotObject remo;
        alertObject alert;
        drawObject draw;

        public void durums(bool durm)
        {
            txtSunucu.Enabled = durm;
            label6.Enabled = durm;
            btnBaglan.Enabled = durm;
            LVMess.Enabled = !durm;
            panel1.Enabled = !durm;
            txtMesaj.Enabled = !durm;
            button1.Enabled = !durm;
            button2.Enabled = !durm;
        }

        private void btnBaglan_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Application.StartupPath + @"\Sunucu.exe");

            durums(false);
//MessageBox.Show(Application.StartupPath+@"\dosyaismi.ini");

            BinaryClientFormatterSinkProvider cli = new BinaryClientFormatterSinkProvider();
            BinaryServerFormatterSinkProvider slv = new BinaryServerFormatterSinkProvider();
            slv.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;
            Hashtable ht = new Hashtable();
            ht["port"] = 0;
            TcpChannel c = new TcpChannel(ht, cli, slv);
            ChannelServices.RegisterChannel(c, false);
            RemotingConfiguration.RegisterWellKnownClientType(typeof(remotObject),"tcp://" + txtSunucu.Text.Trim() + ":1797/chatUri");
            mess = new messObject();
            remo = new remotObject();
            alert = new alertObject();
            draw = new drawObject();

            remo.messEvent += new messDelegate(mess.SendMessage);
            remo.alertEvent += new alertDelegate(alert.alert);
            remo.drawEvent += new drawDelegate(draw.draw);
            
            mess.messEvent += new messDelegate(mess_messEvent);
            alert.alertEvent += new alertDelegate(alert_alertEvent);
            draw.drawEvent += new drawDelegate(draw_drawEvent);
            this.Text = remo.getMachineName();
            remo.alert(label6.Text);
        }

        void draw_drawEvent(int x, int y, Color renk)
        {
            g = panel1.CreateGraphics();
            SolidBrush bru = new SolidBrush(renk);
            g.FillEllipse(bru, x, y, 10, 10);
        }
        void alert_alertEvent(string kisi)
        {
            if (kisi != label6.Text.Trim())
            {
                MessageBox.Show(kisi + " baglandi");
            }
        }
        void mess_messEvent(string text)
        {
            ListViewItem li = new ListViewItem();
            li.Text = text.Split(',')[0];
            li.SubItems.Add(text.Split(',')[1]);
            LVMess.Items.Add(li);
        }

        private void txtMesaj_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                remo.SendMessage(label6.Text + "," + txtMesaj.Text);
                txtMesaj.Clear();
                txtMesaj.Focus();
            }
        }

        bool durum;

        private void basla(object sender, MouseEventArgs e)
        {
            durum = true;
        }

        private void devam(object sender, MouseEventArgs e)
        {
            if (durum)
            {
                Graphics g = panel1.CreateGraphics();
                Color renk = label3.BackColor;
                SolidBrush b = new SolidBrush(renk);
                g.FillEllipse(b, e.X, e.Y, 10, 10);
                remo.draw(e.X, e.Y, renk);

            }
        }
        private void bitir(object sender, MouseEventArgs e)
        {
            durum = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(colorDialog1.ShowDialog() == DialogResult.OK)
             {
                 label3.BackColor = colorDialog1.Color;
             }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {


        }

        private void LVMess_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtSunucu_TextChanged(object sender, EventArgs e)
        {

        }
        
        private void kullanıcı_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
