using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculadora
{
    public partial class Form1 : Form
    {
        public static readonly string[][] txtBotones = {
                                                        new string[] { "^", "/", "*", "-" },
                                                        new string[] { "7", "8", "9", "+" },
                                                        new string[] { "4", "5", "6", ")" },
                                                        new string[] { "1", "2", "3", "(" },
                                                        new string[] { ",", "0", "C", "=" }
                                                      };
        private Button[][] botones;

        public Form1()
        {
            InitializeComponent();

            botones = new Button[txtBotones.Length][];
            for (int n = 0; n < botones.Length; n++)
            {
                botones[n] = new Button[txtBotones[0].Length];
                for (int i = 0; i < botones[n].Length; i++)
                {
                    botones[n][i] = new Button();
                    botones[n][i].Text = txtBotones[n][i];
                    botones[n][i].Size = new Size(75, 75);
                    botones[n][i].Margin = new Padding(3, 3, 3, 3);
                    botones[n][i].Click += btnClick;
                    this.flowLayoutPanel1.Controls.Add(botones[n][i]);
                }
            }
        }

        private void btnClick(object sender, EventArgs e)
        {
            Button b = ((Button) sender);
            switch (b.Text)
            {
                case "C":
                    edVisor.Text = "";
                    break;

                case "=":
                    if (!combinarParentesis(edVisor.Text))
                        MessageBox.Show("Parentesis mal usadas");
                    else
                    {
                        /*
                         Comente el ep el cual se usaba para el posfixa 
                         */
                        Expresion ei = Expresion.Dicionarizar(edVisor.Text); //edVisor Ingresa lee el ejericico a resolver
                        
                        //Expresion ep = Expresion.TraduzirParaPosfixa(ei); // En estas lineas crea el posfixa

                        ei = Expresion.TraduzirParaPosfixa(ei); // esto no estaba.

                        edResultado.Text = Expresion.ResolverPosfixa(ei) + ""; //En el original se enviaba para solucionar "ep"
                        
                        //lbSequencias.Text = ep.getExpresion; // y con esta lo asigna a el label
                    }
                    break;

                default:
                    edVisor.Text += b.Text;
                    break;
            }
        }

        public bool combinarParentesis(string expresiones)
        {
            Pila<char> parentesis = new Pila<char>();

            foreach (char c in expresiones)
                if (c == '(')
                    parentesis.Apilar(c);
                else if (c == ')')
                    try
                    {
                        parentesis.Desapilar();
                    }
                    catch (Exception)
                    {
                        return false;
                    }

            if (!parentesis.EstaVacio())
                return false;

            return true;
        }

        private void FlowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }

    public class Expresion
    {
        public static readonly bool[,] precedenciaDeSeñal =
                                        {
                                            { false, false, false, false, false, false, true  },
                                            { false, true,  true,  true,  true,  true,  true  },
                                            { false, false, true,  true,  true,  true,  true  },
                                            { false, false, true,  true,  true,  true,  true  },
                                            { false, false, false, false, true,  true,  true  },
                                            { false, false, false, false, true,  true,  true  },
                                            { false, false, false, false, false, false, false }
                                        };

        public static readonly char[] señales = { '(', '^', '*', '/', '+', '-', ')' };

        private String _expresion;
        private Dictionary<char, double> _diccionario;

        public String getExpresion
        {
            get { return _expresion; }
        }

        public Dictionary<char, double> getDiccionario
        {
            get { return _diccionario; }
        }

        public Expresion(String expressao, Dictionary<char, double> diccionario)
        {
            this._expresion  = expressao;
            this._diccionario = diccionario;
        }

        public static Expresion Dicionarizar(string expressao)
        {
            Dictionary<char, double> objDictionary = new Dictionary<char, double>();
            StringBuilder nuevaExpresion = new StringBuilder(expressao);

            for (int i = 0; i < nuevaExpresion.Length; i++)
                if (!isOperador(nuevaExpresion[i]))
                {
                    //Si no es un operador, es un número, por lo que tomamos su comienzo y nos desplazamos hasta encontrar el siguiente operador, o el final de la cadena.

                    //Después de eso, sabemos su tamaño, así que lo subcadenamos y lo convertimos a double.
                    string numero = "";
                    int indexInicio = i;

                    while (indexInicio + numero.Length < nuevaExpresion.Length && !isOperador(nuevaExpresion[indexInicio + numero.Length]))
                        numero += nuevaExpresion[indexInicio + numero.Length];

                    double clave = Convert.ToDouble(numero);

                    nuevaExpresion.Remove( indexInicio, numero.Length);
                    objDictionary.Add(char.ConvertFromUtf32(65 + objDictionary.Count)[0], clave);

                    nuevaExpresion.Insert(indexInicio, char.ConvertFromUtf32(65 + objDictionary.Count - 1));

                    i = indexInicio;
                }

            return new Expresion(nuevaExpresion.ToString(), objDictionary);
        }

        public static Expresion TraduzirParaPosfixa(Expresion expressaoInfixa)
        {
            String infixa = expressaoInfixa.getExpresion;

            Pila<char> p  = new Pila<char>();
            String posfixa = "";

            for (int i = 0; i < infixa.Length; i++)
            {
                bool unario = false;

                if (isOperador(infixa[i]))
                {
                    if (infixa[i] == '-')
                        if (i == 0 || infixa[i - 1] == '(')
                        {
                            p.Apilar('@');
                            unario = true;
                        }

                    if (!unario)
                    {
                        bool parar = false;

                        while (!parar && !p.EstaVacio() && Precedencia(p.OTopo(), infixa[i]))
                        {
                            char operadorComMaiorPrec = p.OTopo();
                            if (operadorComMaiorPrec == '(')
                                parar = true;
                            else
                            {
                                posfixa += operadorComMaiorPrec;
                                p.Desapilar();
                            }
                        }

                        if (infixa[i] != ')')
                            p.Apilar(infixa[i]);
                        else
                            p.Desapilar();
                    }
                }
                else
                    posfixa += infixa[i];
            }

            while (!p.EstaVacio())
            {
                char operadorComMaiorPrec = p.Desapilar();
                if (operadorComMaiorPrec != '(')
                    posfixa += operadorComMaiorPrec;
            }

            return new Expresion(posfixa, expressaoInfixa.getDiccionario);
        }

        public static double ResolverPosfixa(Expresion ExpPosfixa)
        {
            String posfixa = ExpPosfixa.getExpresion;

            Pila<double> p = new Pila<double>();
            for (int i = 0; i < posfixa.Length; i++)
            {
                if (!isOperador(posfixa[i]))
                    if (posfixa[i] == '@')
                        p.Apilar(p.Desapilar() * -1);
                    else
                        p.Apilar(ExpPosfixa.getDiccionario[posfixa[i]]);
                else
                {
                    double operando2 = p.Desapilar(),
                           operando1 = p.Desapilar();

                    p.Apilar(SubExpressao(operando1, operando2, posfixa[i]));
                }
            }

            return p.Desapilar();
        }

        private static double SubExpressao(double operando1, double operando2, char sinal)
        {
            switch (sinal)
            {
                case '+': return operando1 + operando2;

                case '-': return operando1 - operando2;

                case '*': return operando1 * operando2;

                case '/': return operando1 / operando2;

                case '^': return Math.Pow(operando1, operando2);

                default : return 0; //Retorno estándar, solo para satisfacer al compilador

            }
        }

        private static bool isOperador(char c)
        {
            foreach (char siñal in señales)
                if (c == siñal)
                    return true;

            return false;
        }

        private static bool Precedencia(char c1, char c2)
        {
            if (c1 == '@' || c2 == '@')
                return true;

            int indiceC1 = Array.FindIndex(señales, x => x == c1),
                indiceC2 = Array.FindIndex(señales, y => y == c2);

            if (indiceC1 < 0 || indiceC2 < 0)
                throw new ArgumentException("Señal no existente");

            return precedenciaDeSeñal[indiceC1, indiceC2];
        }
    }
}
