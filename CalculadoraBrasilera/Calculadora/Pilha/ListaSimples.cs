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

        private bool primerRutaDeAcceso; // no se usa nunca

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

        public NoLista<Dado> Actual
        {
            get
            {
                return actual;
            }
        }

        public void InsertarAntesDeComenzar(Dado nuevoDado)
        {
            var novoNo = new NoLista<Dado>(nuevoDado, primero);

            if (ultimo == null)
                ultimo = novoNo;

            primero = novoNo;
            quantosNos++;
        }

        public Dado RemoverPrimero()
        {
            if (EstaVacia)
            {
                throw new Exception("Lista vacia!");
            }
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
            //--------------------------Traduccion-------------------------
            // Luego se verifica para ver si la lista está vacía. Si es así, es
            // se devuelve falso a la ubicación de la llamada, 
            // lo que indica que no se encontró la clave y que actual y anterior son nulos

            if (EstaVacia)
                return false;

            // a lista não está vazia, possui nós
            // dado procurado é menor que o primero dado da lista:
            // portanto, dado procurado não existe
            //---------------------------Traduccion--------------------------------------------------
            // la lista no está vacía, los datos buscados             
            // en los nodos son más pequeños que los primeros datos de la lista:          
            // por lo tanto, los datos buscados no existen

            if (outroProcurado.CompareTo(primero.Info) < 0)
                return false;
            // dado procurado é maior que o último dado da lista:
            // portanto, dado procurado não existe
            //---------------------------Traduccion--------------------------------------------------
            // Los datos de búsqueda son mayores que los últimos datos de la lista:
            //por lo tanto, los datos de búsqueda no existen
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
            //---------------------------Traduccion--------------------------------------------------
            //Si no se ha definido que la clave está fuera de los límites de 
            //la clave de la lista, miraremos dentro del puntero actual para
            //indicar el primer nodo de la lista y asumiremos que aún no ha encontrado 
            //la clave que buscó y no ha llegado al final de la lista.

            bool achou = false;
            bool fim = false;
            // repete os comandos abaixo enquanto não achou o RA nem chegou ao
            // final da lista
            //---------------------------Traduccion--------------------------------------------------
            //repita los comandos a continuación sin encontrar el RA o llegar al final de la lista
            while (!achou && !fim)
                // se o apontador actual vale null, indica final da lista
                //---------------------------Traduccion--------------------------------------------------
                //si el puntero actual es nulo, indica el final de la lista
                if (actual == null)
                    fim = true;
                // se não chegou ao final da lista, verifica o valor da chave actual
                //---------------------------Traduccion--------------------------------------------------
                //si no ha llegado al final de la lista, verifique el valor de la clave actual
                else
                // verifica igualdade entre chave procurada e chave do nó actual
                //---------------------------Traduccion--------------------------------------------------
                // comprueba la igualdad entre la clave buscada y la clave de nodo actual
                if (outroProcurado.CompareTo(actual.Info) == 0)
                    achou = true;
                else
                // se chave actual é maior que a procurada, significa que
                // a chave procurada não existe na lista ordenada e, assim,
                // termina a pesquisa indicando que não achou. Anterior
                // aponta o anterior ao actual, que foi acessado por
                // último
                //---------------------------Traduccion--------------------------------------------------
                /* Si la clave actual es mayor que la buscada, significa que 
                 * la clave buscada no existe en la lista ordenada y, por lo tanto, 
                 * finaliza la búsqueda, lo que indica que no la encontró.
                 * Puntos anteriores al anterior al actual,
                 * al que se accedió por última */

                if (actual.Info.CompareTo(outroProcurado) > 0)
                    fim = true;
                else
                {
                    // se não achou a chave procurada nem uma chave > que ela,
                    // então a pesquisa continua, de maneira que o apontador
                    // anterior deve apontar o nó actual e o apontador actual
                    // deve seguir para o nó seguinte
                    //---------------------------Traduccion--------------------------------------------------
                    /* si no encontró la clave buscada o una> clave, entonces 
                     * la búsqueda continúa para que el puntero anterior apunte 
                     * al nodo actual y el puntero actual vaya al siguiente nodo*/
                    anterior = actual;
                    actual = actual.Prox;
                }
            // por fim, caso a pesquisa tenha terminado, o apontador actual
            // aponta o nó onde está a chave procurada, caso ela tenha sido
            // encontrada, ou o nó onde ela deveria estar para manter a
            // ordenação da lista. O apontador anterior aponta o nó anterior
            // ao actual
            //---------------------------Traduccion--------------------------------------------------
            /* Finalmente, si la búsqueda se ha completado, el puntero actual apunta 
             * al nodo donde se encuentra la clave buscada, si se encontró, 
             * o al nodo donde debería estar para mantener el orden de la lista. 
             * El puntero anterior apunta al nodo anterior al actual.*/

            return achou; // devolve o valor da variável achou, que indica
            //---------------------------Traduccion--------------------------------------------------
            //devuelve el valor de la variable encontrada, que indica
        } // se a chave procurada foi ou não encontrado
        //---------------------------Traduccion--------------------------------------------------
        //si se encontró o no la clave de búsqueda
        public bool inserirEmOrdem(Dado dados)
        {
            if (!existeDado(dados)) // existeChave configura anterior e actual -- configura anteriores y actuales
            { // aqui temos certeza de que a chave não existe -- aquí estamos seguros de que la clave no existe
                if (EstaVacia) // se a lista está vazia, então o -- si la lista está vacía, entonces el

                    InsertarAntesDeComenzar(dados); // novo nó é o primero da lista -- nuevo nodo es el primero en la lista

                else
                if (anterior == null && actual != null)
                    InsertarAntesDeComenzar(dados); // liga novo antes do primero -- llamar nuevo antes del primero
                else
                    InserirNoMeio(dados); // insere entre os nós anterior e actual -- inserta entre los nodos anterior y actual

                return true;  // significa que incluiu -- significa que incluye
            }
            return false;   // significa que não incluiu -- significa que no incluyó

            //throw new Exception("Aluno já cadastrado!");
        }

        public bool removerDado(Dado aExcluir)
        {
            if (existeDado(aExcluir)) // existeDado configurou 
            {                         // actual e anterior
                quantosNos--;

                if (actual == primero)  // se vamos excluir o 1o nó -- si eliminamos el primer nodo
                {
                    primero = primero.Prox;
                    if (primero == null)  // esvaziou -- vaciado
                        ultimo = null;
                }
                else
                    if (actual == ultimo)    // se vamos excluir o último nó -- si eliminamos el último nodo
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
            var novo = new NoLista<Dado>(dados, null); // guarda dados no -- almacenar datos en
                                                       // novo nó -- nuevo nudo

            // existeChave() encontrou intervalo de inclusão do novo nó
            //---------------------------Traduccion--------------------------------------------------
            //               encontrou intervalo de inclusão do novo nó
            anterior.Prox = novo; // liga anterior ao novo -- liga anterior a nueva
            novo.Prox = actual; // e novo no actual -- y nuevo en el actual
            if (anterior == ultimo) // se incluiu ao final da lista, -- incluido al final de la lista,
                ultimo = novo; // atualiza o apontador ultimo -- incrementa número de nós da lista
            quantosNos++; // incrementa número de nós da lista -- incrementar el número de nodos en la lista
        }
      
        public void ordenar()
        {
            ListaSimples<Dado> ordenada = new ListaSimples<Dado>();
            while (!this.EstaVacia)
            {
                // achar o menor de todos
                // remover o menor de todos
                // incluir o menor de todos já removido na lista ordenada
                //---------------------------Traduccion--------------------------------------------------
                // encuentra el más pequeño de todos
                // eliminar el más pequeño de todos
                // incluir el más pequeño que se haya eliminado de la lista ordenada
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
