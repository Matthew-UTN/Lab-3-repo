using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculadora
{
    class Pila<Dado> : ListaSimples<Dado>,
                                    Stack<Dado>
          where Dado : IComparable<Dado>
    {
        public Dado Desapilar()
        {
            return RemoverPrimero();
        }

        public void Apilar(Dado elemento)
        {
            InsertarAntesDeComenzar(elemento);
        }

        public Dado OTopo()
        {
            if ( EstaVacio() )
               throw new Exception("Pila vacia al consultar arriba!");

            return Primero.Info;
        }

        public int Tamaño()
        {
            return QuantosNos;
        }

        new public bool EstaVacio()
        {
            return base.EstaVacia;
        }
    }
}
