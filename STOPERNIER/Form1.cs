using SharpDX.XInput;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Media;
using System.IO;

namespace STOPERNIER
{

    public partial class Form1 : Form
    {
        private int negative_offset = 0;
        private int debouncing_offset_value = 0;
        Controller controller;
        int Reset_button_selected;
        public bool connected, pad_control,negative_offset_button_state,beep_enable = false;
        int min, sec, msec;
        private int buttonreset = 0;

        private List<string> previousTimes = new List<string>();
        
        public Form1()
        { 
            InitializeComponent();
            
        }

        private void Transparent_background()
        {
            label1.BackColor = System.Drawing.Color.Transparent;
            label2.BackColor = System.Drawing.Color.Transparent;
            label4.BackColor = System.Drawing.Color.Transparent;
            label5.BackColor = System.Drawing.Color.Transparent;
            label6.BackColor = System.Drawing.Color.Transparent;
            label7.BackColor = System.Drawing.Color.Transparent;
            //toolStrip1.BackColor = System.Drawing.Color.Transparent;

        }
        private void button2_Click(object sender, EventArgs e)//start
        {
            timer1.Enabled = true;
        }
        public void xone_controller()
        {
            controller = new Controller(UserIndex.One);
        }

        private void button3_Click(object sender, EventArgs e)//reset
        {
            min = 0;
            sec = negative_offset;
            msec = 0;
            Time_disp();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            msecup();
            Time_disp();
        }

        private void label1_Click(object sender, EventArgs e)//minutues
        {

        }

        private void saveDataLocally()
        {
            try
            {
                string time = $"{min.ToString()}:{sec.ToString()}:{msec.ToString()}";
                previousTimes.Add(time);
            }
            catch
            {
                // error
            }
        }



        private void button1_Click(object sender, EventArgs e) //stop
        {
            saveDataLocally();
            timer1.Enabled = false;

        }

        private void label2_Click(object sender, EventArgs e)//seconds
        {

        }

        private void label4_Click(object sender, EventArgs e)//mseconds
        {

        }
        private void Time_disp()
        {
            label1.Text = min.ToString("00");
            label2.Text = sec.ToString("00");
            label4.Text = msec.ToString("00");
        }
        private void minup()
        {
            min++;
        }
        private void secup()
        {
            if (sec == -1 && beep_enable == true)
            {
                SystemSounds.Beep.Play();
            }
            if (sec == 59)
            {
                msec = 0;
                minup();
            }
            else
                sec++;
        }
        private void msecup()
        {
            if (msec == 99)
            {
                msec = 0;
                secup();
            }
            else
                msec++;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Button_reset();
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {

        }



        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {

            controller.GetState(out var state);
            switch (Reset_button_selected)
            {
                case 0:
                    if (state.Gamepad.Buttons == GamepadButtonFlags.Start)
                    {
                        Thread.Sleep(100);
                        Button_reset(debouncing_offset_value);
                        Thread.Sleep(100);
                    }
                    break;
                case 1:
                    if (state.Gamepad.Buttons == GamepadButtonFlags.Back)
                    {
                        Thread.Sleep(100);
                        Button_reset(debouncing_offset_value);
                        Thread.Sleep(100);
                    }
                    break;
                case 2:
                    if (state.Gamepad.Buttons == GamepadButtonFlags.DPadUp)
                    {
                        Thread.Sleep(100);
                        Button_reset(debouncing_offset_value);
                        Thread.Sleep(100);
                    }
                    break;
                case 3:
                    if (state.Gamepad.Buttons == GamepadButtonFlags.DPadDown)
                    {
                        Thread.Sleep(100);
                        Button_reset(debouncing_offset_value);
                        Thread.Sleep(100);
                    }
                    break;
                case 4:
                    if (state.Gamepad.Buttons == GamepadButtonFlags.DPadLeft)
                    {
                        Thread.Sleep(100);
                        Button_reset(debouncing_offset_value);
                        Thread.Sleep(100);
                    }
                    break;
                case 5:
                    if (state.Gamepad.Buttons == GamepadButtonFlags.DPadRight)
                    {
                        Thread.Sleep(100);
                        Button_reset(debouncing_offset_value);
                        Thread.Sleep(100);
                    }
                    break;
            }
            



        }
        private void timer_update (int _min = 0, int _sec = 0, int _msec = 0)
        {
            min = _min;
            sec = _sec;
            msec = _msec;
            Time_disp();
        }

        private void dpadUpToolStripMenuItem1_Click(object sender, EventArgs e)//dpaddown
        {
            Reset_button_selected = 3;
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)//start
        {
            Reset_button_selected = 0;
        }

        private void selectToolStripMenuItem_Click(object sender, EventArgs e)//select
        {
            Reset_button_selected = 1;
        }

        private void dpadUpToolStripMenuItem_Click(object sender, EventArgs e)//dpadup
        {
            Reset_button_selected = 2;
        }

        private void dpadUpToolStripMenuItem2_Click(object sender, EventArgs e)//dpadleft
        {
            Reset_button_selected = 4;
        }

        private void dpadUpToolStripMenuItem3_Click(object sender, EventArgs e)//dpadright
        {
            Reset_button_selected = 5;
        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Transparent_background();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void toolStripTextBox1_Click_1(object sender, EventArgs e)
        {
            if (negative_offset_button_state == true)
            {
                try
                {
                    negative_offset = -Int32.Parse(toolStripTextBox1.Text);
                    timer_update(0, negative_offset, debouncing_offset_value);
                }
                catch
                {
                    negative_offset_button_state = false;
                    MessageBox.Show("Offset has to be an integer");
                }
            }
        }

        private void beepToolStripMenuItem_Click(object sender, EventArgs e)
        {
            beep_enable = !beep_enable;
        }

        private void negativeOffsetToolStripMenuItem_Click(object sender, EventArgs e)//set negative offset
        {
            negative_offset_button_state = !negative_offset_button_state;
            if (negative_offset_button_state == true)
            {
                try
                {
                    negative_offset = -Int32.Parse(toolStripTextBox1.Text);
                    timer_update(0, negative_offset, debouncing_offset_value);
                }
                catch
                {
                    negative_offset_button_state = false;
                    MessageBox.Show("Offset has to be an integer");
                    negativeOffsetToolStripMenuItem.Checked = false;
                }
            }
            else
            {
                negative_offset = 0;
                timer_update(0, negative_offset, debouncing_offset_value);
            }


            
        }

        private void toolStripButton2_Click(object sender, EventArgs e)//init pad key
        {
            pad_control = !pad_control;
            if (pad_control)
            {
                xone_controller();
                toolStripLabel1.Text = "Pad Control: ON";
                timer2.Enabled = true;
                debouncing_offset_value = 10;
                timer_update(0, negative_offset, debouncing_offset_value);

            }
            else
            {
                toolStripLabel1.Text = "Pad Control: OFF";
                timer2.Enabled = false;
                debouncing_offset_value = 0;
                timer_update(0, negative_offset, debouncing_offset_value);
            }
                
        }

        private void Button_reset(int debouncing_offset = 0)
        {
            if (buttonreset == 0)
            {
                timer1.Enabled = true;
                buttonreset++;
            }
            else if (buttonreset == 1)
            {
                timer1.Enabled = false;
                buttonreset++;
            }
            else if(buttonreset == 2){
                buttonreset = 1;
                startover(debouncing_offset);
            }
        }
        private void startover(int debouncing_offset = 0)
        {
            min = 0;
            sec = negative_offset;
            msec = debouncing_offset;
            timer1.Enabled = true;

            
        }
    }
}
