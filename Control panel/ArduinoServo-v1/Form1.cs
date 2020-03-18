using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;


namespace ArduinoServo_v1
{
    
    public partial class Form1 : Form
    {

         
        SerialPort port;
        byte flagON = 0;
        int adress;
        int Forward = 2001; 
        int Backward = 2002;
        int toRight = 2003;
        int toLeft = 2004;
        int STOP = 2005;
        int powerflag;
        int Freq = 5;

        private void init()
        {

        }

        public Form1()
        {
            InitializeComponent();
            KeyPreview = true;
            init();
        }



       
        private void button3_Click(object sender, EventArgs e)                      // Кнопка "CONNECT"
        {
            port = new SerialPort();
            port.PortName = domainUpDown1.Text;
            port.BaudRate = 115200;

            try
            {

                port.Open();
                flagON = 1;
            }

            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
                flagON = 0;
            }

        }
        //Gorizont
        public void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (flagON == 1){

                if (adress != 1000){
                    adress = 1000;
                    port.WriteLine(adress.ToString());
                }
                else{
                    port.WriteLine(trackBar1.Value.ToString());
                    degree1.Text = trackBar1.Value.ToString();
                } 
            }   
            else{
                MessageBox.Show("О многоуважаемый, выберите последовательный COM порт, пожалуйста!");
                flagON = 0;
            }
         }

        //plecho
        public void trackBar2_Scroll(object sender, EventArgs e)
        {
            if (flagON == 1)
            {
                if (adress != 1003)
                {
                    adress = 1003;
                    port.WriteLine(adress.ToString());
                }
                else
                {
                    port.WriteLine(trackBar2.Value.ToString());
                    degree2.Text = trackBar2.Value.ToString();
                }
                }               
            
            else
            {
                MessageBox.Show("О многоуважаемый, выберите последовательный COM порт, пожалуйста!");
                flagON = 0;
            }
          
        }                     // ШКАЛА  
        //Os
        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            if (flagON == 1)
            {
                if (adress != 1006)
                {
                    adress = 1006;
                    port.WriteLine(adress.ToString());
                    

                }
                else
                {
                    port.WriteLine(trackBar3.Value.ToString());
                    label2.Text = trackBar3.Value.ToString();
                }
            }
            else
            {
                MessageBox.Show("О многоуважаемый, выберите последовательный COM порт, пожалуйста!");
                flagON = 0;
            }
  
        }
        //vrashenie osi
        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            if (flagON == 1)
            {
                if (adress != 1009)
                {
                    adress = 1009;
                    port.WriteLine(adress.ToString());
                }
                else
                {

                    port.WriteLine(trackBar4.Value.ToString());
                    label1.Text = trackBar4.Value.ToString();
                }
            }
            else
            {
                MessageBox.Show("О многоуважаемый, выберите последовательный COM порт, пожалуйста!");
                flagON = 0;
            }

           

        }
        //kleshnya
        private void trackBar5_Scroll(object sender, EventArgs e)
        {
            if (flagON == 1){
                if (adress != 1012){
                    adress = 1012;
                    port.WriteLine(adress.ToString());
                }
                else{
                    port.WriteLine(trackBar5.Value.ToString());
                    degree3.Text = trackBar5.Value.ToString();
                }
            }
            else
            {
                MessageBox.Show("О многоуважаемый, выберите последовательный COM порт, пожалуйста!");
                flagON = 0;
            }


        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            int restart = 3019;
            port.WriteLine(restart.ToString());
            trackBar5.Value = 40;
            trackBar4.Value = 40;
            trackBar3.Value = 70;
            trackBar2.Value = 85;
            trackBar1.Value = 210;

        }
       

        private void Form1_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Внимание, для начала работы  выберите последовательный COM порт, пожалуйста!");
            // отключение стрелок начинается
             foreach (Control control in this.Controls)
        {
            control.PreviewKeyDown += new PreviewKeyDownEventHandler(control_PreviewKeyDown);
        }
        }
        void control_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                e.IsInputKey = true;
            }
        }
        //отключение стрелок заканчивается
        private void trackBar5_MouseDown(object sender, MouseEventArgs e)
        {
            adress = 1012;
            port.WriteLine(adress.ToString());
         
        }

        private void trackBar4_MouseDown(object sender, MouseEventArgs e)
        {
            adress = 1009;
            port.WriteLine(adress.ToString());
         
        }

        private void trackBar1_MouseDown(object sender, MouseEventArgs e)
        {
            adress = 1000;
            port.WriteLine(adress.ToString());
         
        }

        private void trackBar2_MouseDown(object sender, MouseEventArgs e)
        {
            adress = 1003;
            port.WriteLine(adress.ToString());
       
        }

        private void trackBar3_MouseDown(object sender, MouseEventArgs e)
        {
            adress = 1006;
            port.WriteLine(adress.ToString());
          
        }
       private void button1_MouseUp(object sender, MouseEventArgs e)
        {
            port.WriteLine(STOP.ToString());
        }
        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            port.WriteLine(Forward.ToString());
        }


        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Up:
                    port.WriteLine(STOP.ToString());
                    break;
                case Keys.Down:
                    port.WriteLine(STOP.ToString());
                    break;
                case Keys.Left:
                    port.WriteLine(STOP.ToString());
                    break;
                case Keys.Right:
                    port.WriteLine(STOP.ToString());
                    break;
                case Keys.Space:
                    port.WriteLine(STOP.ToString());
                    break;

            }
        }
        private void button4_MouseDown(object sender, MouseEventArgs e)
        {
            port.WriteLine(toLeft.ToString());
        }

        private void button4_MouseUp(object sender, MouseEventArgs e)
        {
            port.WriteLine(STOP.ToString());
        }

        private void button2_MouseDown(object sender, MouseEventArgs e)
        {
            port.WriteLine(toRight.ToString());
        }

        private void button2_MouseUp(object sender, MouseEventArgs e)
        {
            port.WriteLine(STOP.ToString());
        }

        private void button5_MouseDown(object sender, MouseEventArgs e)
        {
            port.WriteLine(Backward.ToString());
        }

        private void button5_MouseUp(object sender, MouseEventArgs e)
        {
            port.WriteLine(STOP.ToString());
        }

        private void button7_MouseDown(object sender, MouseEventArgs e)
        {
            port.WriteLine(STOP.ToString());
        }

        private void button7_MouseUp(object sender, MouseEventArgs e)
        {
            port.WriteLine(STOP.ToString());
        }



        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
         
         switch (e.KeyData)
            {
                case Keys.Up:
                    port.WriteLine(Forward.ToString());
                    break;
                case Keys.Down:
                    port.WriteLine(Backward.ToString());
                    break;
                case Keys.Left:
                    port.WriteLine(toLeft.ToString());
                    break;
                case Keys.Right:
                    port.WriteLine(toRight.ToString());
                    break;
                case Keys.Space:
                    port.WriteLine(STOP.ToString());
                    break;

            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            adress = 1009;
            port.WriteLine(adress.ToString());
            trackBar4.Value = 180;
            port.WriteLine(trackBar4.Value.ToString());
            label1.Text = Convert.ToString(trackBar4.Value);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            adress = 1000;
            port.WriteLine(adress.ToString());
            trackBar1.Value = 220;
            port.WriteLine(trackBar1.Value.ToString());
            degree1.Text = Convert.ToString(trackBar1.Value);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            adress = 1000;
            port.WriteLine(adress.ToString());
            trackBar1.Value = 1;
            port.WriteLine(trackBar1.Value.ToString());
            degree1.Text = Convert.ToString(trackBar1.Value);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            adress = 1009;
            port.WriteLine(adress.ToString());
            trackBar4.Value = 1;
            port.WriteLine(trackBar4.Value.ToString());
            label1.Text = Convert.ToString(trackBar4.Value);
        }
        
      

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            adress = 1000;
            port.WriteLine(adress.ToString());
            trackBar1.Value = Convert.ToInt32(textBox1.Text);
            port.WriteLine(trackBar1.Value.ToString());
            degree1.Text = Convert.ToString(trackBar1.Value);
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            adress = 1009;
            port.WriteLine(adress.ToString());
            trackBar4.Value = Convert.ToInt32(textBox2.Text);
            port.WriteLine(trackBar4.Value.ToString());
            label1.Text = Convert.ToString(trackBar4.Value);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            adress = 1012;
            port.WriteLine(adress.ToString());
            trackBar5.Value = Convert.ToInt32(textBox3.Text);
            port.WriteLine(trackBar5.Value.ToString());
            degree3.Text = Convert.ToString(trackBar5.Value);
        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            adress = 1003;
            port.WriteLine(adress.ToString());
            trackBar2.Value = Convert.ToInt32(textBox4.Text);
            port.WriteLine(trackBar2.Value.ToString());
            degree2.Text = Convert.ToString(trackBar2.Value);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            adress = 1006;
            port.WriteLine(adress.ToString());
            trackBar3.Value = Convert.ToInt32(textBox5.Text);
            port.WriteLine(trackBar3.Value.ToString());
            label2.Text = Convert.ToString(trackBar3.Value);
        }


      
    }

    }


    


