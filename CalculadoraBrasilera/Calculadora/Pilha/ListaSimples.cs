using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculadora
{
    public class ListaSimples<Dado> where Dado : IComparable<Dado>
    {
        private NoLista<Dado> primero;
        private NoLista<Dado> ultimo;
        private NoLista<Dado> anterior, actual;
        private int quantosNos;

        private bool primerRutaDeAcceso;

        public ListaSimples()
        {
            primero = ultimo = anterior = actual = null;
            quantosNos = 0;
            primerRutaDeAcceso = false;
        }

        public void ExaminarLista()
        {
            actual = primero;
            while (actual != null)
            {
                Console.WriteLine(actual.Info);
                actual = actual.Prox;
            }
        }

        public bool EstaVacia
        {
            get { return primero == null; }
        }

        public NoLista<Dado> Primero
        {
            get
            {
                return primero;
            }
        }

        public NoLista<Dado> Ultimo
        {
            get
            {
                return ultimo;
            }
        }

        public int QuantosNos
        {
            get { return quantosNos; }
        }

        public NoLista<Dado> Atual
        {
            get
            {
                return actual;
            }
        }

        public void InsertarAntesDeComenzar(Dado novoDado)
        {
            var novoNo = new NoLista<Dado>(novoDado, primero);

            if (ultimo == null)
                ultimo = novoNo;

            primero = novoNo;
            quantosNos++;
        }

        public Dado RemoverPrimero()
        {
            if (EstaVacia)
                throw new Exception("Lista vacia!");

            Dado o = primero.Info;
            primero = primero.Prox;
            if (primero == null)
                ultimo = null;
            quantosNos--;
            return o;
        }

        public void InsertarAlFinal(Dado nuevoDado)
        {
            var NuevoNo = new NoLista<Dado>(nuevoDado, null);

            if (EstaVacia)
                primero = NuevoNo;
            else
                ultimo.Prox = NuevoNo;
            ultimo = NuevoNo;
            quantosNos++;
        }

        public void Listar(ListBox umListBox)
        {
            umListBox.Items.Clear();
            actual = primero;
            while (actual != null)
            {
                umListBox.Items.Add(actual.Info.ToString());
                actual = actual.Prox;
            }
        }

        public bool existeDado(Dado outroProcurado)
        {
            anterior = null;
            actual = primero;
            // Em seguida, é verificado se a lista está vazia. Caso esteja, é
            // retornado false ao local de chamada, indicando que a chave não foi
            // encontrada, e actual e anterior ficam valendo null
            if (EstaVacia)
                return false;
            // a lista não está vazia, possui nós
            // dado procurado é menor que o primero dado da lista:
            // portanto, dado procurado não existe
            if (outroProcurado.CompareTo(primero.Info) < 0)
                return false;
            // dado procurado é maior que o último dado da lista:
            // portanto, dado procurado não existe
            if (outroProcurado.CompareTo(ultimo.Info) > 0)
            {
                anterior = ultimo;
                actual = null;
                return false;
            }
            
            // caso não tenha sido definido que a chave está fora dos limites de
            // chaves da lista, vamos procurar no seu interior
            // o apontador actual indica o primero nó da lista e consideraremos que
            // ainda não achou a chave procurada nem chegamos ao final da lista
            bool achou = false;
            bool fim = false;
            // repete os comandos abaixo enquanto não achou o RA nem chegou ao
            // final da lista
            while (!achou && !fim)
                // se o apontador actual vale null, indica final da lista
                if (actual == null)
                    fim = true;
                // se não chegou ao final da lista, verifica o valor da chave actual
                else
                // verifica igualdade entre chave procurada e chave do nó actual
                if (outroProcurado.CompareTo(actual.Info) == 0)
                    achou = true;
                else
                // se chave actual é maior que a procurada, significa que
                // a chave procurada não existe na lista ordenada e, assim,
                // termina a pesquisa indicando que não achou. Anterior
                // aponta o anterior ao actual, que foi acessado por
                // último
                if (actual.Info.CompareTo(outroProcurado) > 0)
                    fim = true;
                else
                {
                    // se não achou a chave procurada nem uma chave > que ela,
                    // então a pesquisa continua, de maneira que o apontador
                    // anterior deve apontar o nó actual e o apontador actual
                    // deve seguir para o nó seguinte
                    anterior = actual;
                    actual = actual.Prox;
                }
            // por fim, caso a pesquisa tenha terminado, o apontador actual
            // aponta o nó onde está a chave procurada, caso ela tenha sido
            // encontrada, ou o nó onde ela deveria estar para manter a
            // ordenação da lista. O apontador anterior aponta o nó anterior
            // ao actual
            return achou; // devolve o valor da variável achou, que indica
        } // se a chave procurada foi ou não encontrado

        public bool inserirEmOrdem(Dado dados)
        {
            if (!existeDado(dados)) // existeChave configura anterior e actual
            { // aqui temos certeza de que a chave não existe
                if (EstaVacia) // se a lista está vazia, então o
                    InsertarAntesDeComenzar(dados); // novo nó é o primero da lista
                else
                if (anterior == null && actual != null)
                    InsertarAntesDeComenzar(dados); // liga novo antes do primero
                else
                    InserirNoMeio(dados); // insere entre os nós anterior e actual

                return true;  // significa que incluiu
            }
            return false;   // significa que não incluiu

            //throw new Exception("Aluno já cadastrado!");
        }

        public bool removerDado(Dado aExcluir)
        {
            if (existeDado(aExcluir)) // existeDado configurou 
            {                         // actual e anterior
                quantosNos--;

                if (actual == primero)  // se vamos excluir o 1o nó
                {
                    primero = primero.Prox;
                    if (primero == null)  // esvaziou
                        ultimo = null;
                }
                else
                    if (actual == ultimo)    // se vamos excluir o último nó
                    {
                        ultimo = anterior;
                        ultimo.Prox = null;
                    }
                    else
                    {
                        anterior.Prox = actual.Prox;
                        actual.Prox = null;
                    }
                return true;
            }
            return false;
        }
        private void InserirNoMeio(Dado dados)
        {
            var novo = new NoLista<Dado>(dados, null); // guarda dados no
                                                       // novo nó
            // existeChave() encontrou intervalo de inclusão do novo nó
            anterior.Prox = novo; // liga anterior ao novo
            novo.Prox = actual; // e novo no actual
            if (anterior == ultimo) // se incluiu ao final da lista,
                ultimo = novo; // atualiza o apontador ultimo
            quantosNos++; // incrementa número de nós da lista
        }
      
        public void ordenar()
        {
            ListaSimples<Dado> ordenada = new ListaSimples<Dado>();
            while (!this.EstaVacia)
            {
                // achar o menor de todos
                // remover o menor de todos
                // incluir o menor de todos já removido na lista ordenada
            }
            this.primero = ordenada.primero;
            this.ultimo = ordenada.ultimo;
            this.quantosNos = ordenada.quantosNos;
        }

        private void InsertarAlFinal(NoLista<Dado> noAntigo)
        {
            if (EstaVacia)
                primero = noAntigo;
            else
                ultimo.Prox = noAntigo;
            ultimo = noAntigo;
            noAntigo.Prox = null;
            quantosNos++;
        }
    }
}
