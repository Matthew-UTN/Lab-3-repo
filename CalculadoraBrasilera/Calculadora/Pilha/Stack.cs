using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculadora
{
    interface Stack<Dado> where Dado : IComparable<Dado>
    {
        int Tamaño();
        bool EstaVacio();
        void Apilar(Dado elemento);
        Dado Desapilar();
        Dado OTopo();

    }
}
