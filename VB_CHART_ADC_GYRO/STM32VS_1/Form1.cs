using System;
using System.IO.Ports;
using System.Text;
using System.Windows.Forms;

namespace STM32VS_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            comboBox1.Text = "";
            comboBox1.Items.Clear();
            if (ports.Length != 0)
            {
                comboBox1.Items.AddRange(ports);
                comboBox1.SelectedIndex = 0;
            }
        }
        string str = "";

        private void mySerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e) // получаем данные из порта
        {
            byte[] inputbuffer = new byte[256];
            int count = mySerialPort.Read(inputbuffer, 0, inputbuffer.Length);
            if (count > 0)
            {
                char[] chars = Encoding.UTF8.GetChars(inputbuffer);
                str = new string(chars, 0, count);
                BeginInvoke(new EventHandler(ShowData)); // асинхронно
            }
        }

        private void ShowData(object sender, EventArgs e) // Собираем сообщение
        {
            label1.Text = str;
            if (chart1.Series[0].Points.Count == 1000) chart1.Series[0].Points.Clear();
            chart1.Series[0].Points.AddY(str);
            chart1.ChartAreas[0].AxisY.Minimum = -100;
            chart1.ChartAreas[0].AxisY.Maximum = 100;
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Maximum = 1000;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            byte[] writeCommand = { 0x02 };
            mySerialPort.Write(writeCommand, 0, 1);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (button1.Text == "Подключиться")
            {
                try
                {
                    mySerialPort.PortName = comboBox1.Text; // выбор порта
                    mySerialPort.Open();      // открываем порт
                    mySerialPort.ReadTimeout = 500;
                    button1.Text = "Отключиться";
                    comboBox2.Enabled = true;
                    checkBox1.Enabled = true;
                }
                catch
                {
                    MessageBox.Show("Ошибка подключения");
                }
            }
            else if (button1.Text == "Отключиться")
            {
                mySerialPort.Close();
                button1.Text = "Подключиться";
                comboBox2.Enabled = false;
                comboBox3.Enabled = false;
                button2.Enabled = false;
                checkBox1.Enabled = false;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text == "Через РС")
            {
                byte[] writeCommand = { 0x10 };
                mySerialPort.Write(writeCommand, 0, 1);
                comboBox3.Enabled = true;
                button2.Enabled = false;
                comboBox3.Text = "0";
            }
            else
            {
                byte[] writeCommand = { 0x09 };
                mySerialPort.Write(writeCommand, 0, 1);
                comboBox3.Enabled = false;
                button2.Enabled = true;
                comboBox3.Text = "";
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.Text == "0")
            {
                byte[] writeCommand = { 0x10 };
                mySerialPort.Write(writeCommand, 0, 1);
            }
            else if (comboBox3.Text == "1")
            {
                byte[] writeCommand = { 0x01 };
                mySerialPort.Write(writeCommand, 0, 1);
            }
            else if (comboBox3.Text == "2")
            {
                byte[] writeCommand = { 0x02 };
                mySerialPort.Write(writeCommand, 0, 1);
            }
            else if (comboBox3.Text == "3")
            {
                byte[] writeCommand = { 0x03 };
                mySerialPort.Write(writeCommand, 0, 1);
            }
            else if (comboBox3.Text == "4")
            {
                byte[] writeCommand = { 0x04 };
                mySerialPort.Write(writeCommand, 0, 1);
            }
            else if (comboBox3.Text == "5")
            {
                byte[] writeCommand = { 0x05 };
                mySerialPort.Write(writeCommand, 0, 1);
            }
            else if (comboBox3.Text == "6")
            {
                byte[] writeCommand = { 0x06 };
                mySerialPort.Write(writeCommand, 0, 1);
            }
            else if (comboBox3.Text == "7")
            {
                byte[] writeCommand = { 0x07 };
                mySerialPort.Write(writeCommand, 0, 1);
            }
            else if (comboBox3.Text == "8")
            {
                byte[] writeCommand = { 0x08 };
                mySerialPort.Write(writeCommand, 0, 1);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            byte[] writeCommand = { 0x00 };
            mySerialPort.Write(writeCommand, 0, 1);
        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            byte[] writeCommand = { 0x00 };
            mySerialPort.Write(writeCommand, 0, 1);
        }

        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                byte[] writeCommand = { 0x12 };
                mySerialPort.Write(writeCommand, 0, 1);
                label3.Text = "Gyro";
            }
            else
            {
                byte[] writeCommand = { 0x09 };
                mySerialPort.Write(writeCommand, 0, 1);
                label3.Text = "АЦП";
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
