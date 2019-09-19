using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculadora_csharp
{
    public partial class Form1 : Form
    {
        Double valorResultante = 0;
        String accionTomada = "";
        bool accionFueEjecutado = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void button_click(object sender, EventArgs e)
        {
            if ((textBox_Result.Text == "0") || (accionFueEjecutado))
                textBox_Result.Clear();

            accionFueEjecutado = false;
            Button button = (Button)sender;
            if (button.Text == ".")
            {
                if (!textBox_Result.Text.Contains("."))
                    textBox_Result.Text = textBox_Result.Text + button.Text;

            }
            else
                textBox_Result.Text = textBox_Result.Text + button.Text;


        }

        private void operator_click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if (valorResultante != 0)
            {
                button15.PerformClick();
                accionTomada = button.Text;
                labelCurrentOperation.Text = valorResultante + " " + accionTomada;
                accionFueEjecutado = true;
            }
            else
            {

                accionTomada = button.Text;
                valorResultante = Double.Parse(textBox_Result.Text);
                labelCurrentOperation.Text = valorResultante + " " + accionTomada;
                accionFueEjecutado = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox_Result.Text = "0";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox_Result.Text = "0";
            valorResultante = 0;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            switch (accionTomada)
            {
                case "+":
                    textBox_Result.Text = (valorResultante + Double.Parse(textBox_Result.Text)).ToString();
                    break;
                case "-":
                    textBox_Result.Text = (valorResultante - Double.Parse(textBox_Result.Text)).ToString();
                    break;
                case "*":
                    textBox_Result.Text = (valorResultante * Double.Parse(textBox_Result.Text)).ToString();
                    break;
                case "/":
                    textBox_Result.Text = (valorResultante / Double.Parse(textBox_Result.Text)).ToString();
                    break;
                default:
                    break;
            }
            valorResultante = Double.Parse(textBox_Result.Text);
            labelCurrentOperation.Text = "";
        }
    }
}
