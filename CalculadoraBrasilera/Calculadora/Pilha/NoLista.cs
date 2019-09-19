using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculadora
{
    public class NoLista<Dado> where Dado : IComparable<Dado>
    {
        private Dado info;
        private NoLista<Dado> prox;

        public NoLista(Dado informacion, NoLista<Dado> puntero)
        {
            info = informacion;
            prox = puntero;
        }

        public Dado Info
        {
            get
            {
                return info;
            }

            set
            {
                info = value;
            }
        }

        public NoLista<Dado> Prox
        {
            get
            {
                return prox;
            }

            set
            {
                prox = value;
            }
        }
    }
}
