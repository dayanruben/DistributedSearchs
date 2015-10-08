// Developed by doiTTeam => devdoiTTeam@gmail.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DHT
{
    /// <summary>
    ///   AVL implementation
    /// </summary>
    /// <typeparam name = "T"></typeparam>
    public class OrderedList<T> : ICollection<T>
    {
        private readonly Func<T, T, int> _comparer;
        private int _altura = -1;
        private int _fb;

        public OrderedList(Func<T, T, int> comparer)
        {
            _comparer = comparer;
            _altura = 0;
        }

        private OrderedList(T value, OrderedList<T> dad, Func<T, T, int> comparer)
            : this(comparer)
        {
            Count = 1;
            Value = value;
            Dad = dad;
        }

        #region Miembros de ICollection<T>

        /// <summary>
        ///   devuelve true si el elemento es una hoja
        /// </summary>
        private bool IsLeaf
        {
            get { return Left == null && Rigth == null; }
        }

        private OrderedList<T> MenorMayores
        {
            get
            {
                if (Left != null)
                    return Left.MenorMayores;
                else return this;
            }
        }

        /// <summary>
        ///   devuelve true si el nodo especificado es el hijo derecho de su padre
        /// </summary>
        /// <returns></returns>
        private bool SoyElDerecho
        {
            get { return (Dad != null && Dad.Rigth != null && _comparer(Dad.Rigth.Value, Value) == 0); }
        }

        /// <summary>
        ///   elimina todos los elementos de la coleccion
        /// </summary>
        public void Clear()
        {
            Value = default(T);
            Left = null;
            Rigth = null;
        }

        /// <summary>
        ///   devuelve true si item esta en la coleccion
        ///   tiempo: O(log count)
        /// </summary>
        /// <param name = "item"></param>
        /// <returns></returns>
        public bool Contains(T item)
        {
            //(comparer( value,item) < 0)//item es mayor que value
            if (Value == null) return false;
            else if (_comparer(Value, item) == 0)
                return true;
            else if (_comparer(Value, item) < 0 && Rigth != null)
                return Rigth.Contains(item);
            else if (_comparer(Value, item) > 0 && Left != null)
                return Left.Contains(item);
            else return false;
        }

        public int Count { get; private set; }

        /// <summary>
        ///   elimina a item de la coleccion
        ///   tiempo: O(log count)
        /// </summary>
        /// <param name = "item"></param>
        /// <returns></returns>
        public bool Remove(T item)
        {
            if (_comparer(Value, item) < 0 && Rigth != null)
            {
                if (Rigth.Remove(item))
                {
                    Count--;
                    return true;
                }
                else return false;
            }
            else if (_comparer(Value, item) > 0 && Left != null)
            {
                if (Left.Remove(item))
                {
                    Count--;
                    return true;
                }
                else return false;
            }
            else if (_comparer(Value, item) == 0) //si es igual entonces este es el nodo que hay que eliminar
            {
                Remove();
                Count--;
                return true;
            }
            else return false;
        }

        public void Add(T item)
        {
            if (AddR(item)) Count++;
        }

        void ICollection<T>.CopyTo(T[] array, int arrayIndex)
        {
            this.ToList().CopyTo(array, arrayIndex);
        }

        bool ICollection<T>.IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        ///   devuelve true si se annadio el elemento
        /// </summary>
        /// <param name = "item"></param>
        /// <returns></returns>
        private bool AddR(T item)
        {
            if (Value == null)
            {
                //el nodo no existia y se inserta en la raiz
                Value = item;
                return true;
            }
            else if (_comparer(Value, item) < 0) //insertar a la derecha
            {
                if (Rigth == null)
                {
                    //este nodo nunca va a tener desbalance pues
                    //no tiene hijo derecho
                    Rigth = new OrderedList<T>(item, this, _comparer);
                    ReOrder();
                    return true;
                }
                else //lo inserto a la derecha
                    return Rigth.AddR(item);
            }
            else if (_comparer(Value, item) > 0)
            {
                if (Left == null)
                {
                    Left = new OrderedList<T>(item, this, _comparer);
                    ReOrder();
                    return true;
                }
                else
                {
                    return Left.AddR(item);
                }
            }
            else return false;
        }

        /// <summary>
        ///   despues de annadir llamar a este metodo para reordenar el tree y realizar todas las rotaciones
        /// </summary>
        private void ReOrder()
        {
            int oldFb = _fb;
            UpdateFb();
            int newFb = _fb;

            if (oldFb == 0 && (newFb == 1 || newFb == -1) && Dad != null)
                Dad.ReOrder();
            else if ((oldFb == 1 || oldFb == -1) && newFb == 0)
                return;
            else if ((oldFb == 1 || oldFb == -1) && (newFb < -1 || newFb > 1))
                Rotar(this);
        }

        private void Rotar(OrderedList<T> node)
        {
            if (node._fb >= 1)
            {
                if (node.Rigth._fb == -1)
                    RotarDerIzq(node);
                else if (node.Rigth._fb == 1)
                    RotarIzq(node);
            }
            else if (node._fb <= -1)
            {
                if (node.Left._fb == 1)
                    RotarIzqDer(node);
                else if (node.Left._fb == -1)
                    RotarDer(node);
            }
        }

        private void RotarDer(OrderedList<T> node)
        {
            var newA = new OrderedList<T>(node.Value, node, _comparer);
            newA.Left = node.Left.Rigth;
            newA.Rigth = node.Rigth;

            //asegura que los hijos de NewA sepan quien es su padre
            if (newA.Left != null) newA.Left.Dad = newA;
            if (newA.Rigth != null) newA.Rigth.Dad = newA;

            newA.UpdateFb();

            node.Value = node.Left.Value;
            node.Left = node.Left.Left;
            node.Rigth = newA;

            if (node.Left != null) node.Left.Dad = node;

            //node.fb = 0;

            node.UpdateFb();
        }

        private void RotarIzq(OrderedList<T> node)
        {
            var newA = new OrderedList<T>(node.Value, node, _comparer);
            newA.Left = node.Left;
            newA.Rigth = node.Rigth.Left;

            //asegura que los hijos de NewA sepan quien es su padre
            if (newA.Left != null) newA.Left.Dad = newA;
            if (newA.Rigth != null) newA.Rigth.Dad = newA;

            newA.UpdateFb();

            node.Value = node.Rigth.Value;
            node.Left = newA;
            node.Rigth = node.Rigth.Rigth;

            if (node.Rigth != null)
                node.Rigth.Dad = node;

            //node.fb = 0;

            node.UpdateFb();
        }

        private void RotarIzqDer(OrderedList<T> node)
        {
            RotarIzq(node.Left);
            RotarDer(node);
        }

        private void RotarDerIzq(OrderedList<T> node)
        {
            RotarDer(node.Rigth);
            RotarIzq(node);
        }

        /// <summary>
        ///   despues de eliminar llamar a este metodo para reordenar el tree y realizar todas las rotaciones
        /// </summary>
        private void ReOrderRemove()
        {
            int oldFb = _fb;
            UpdateFb();

            if (oldFb == 0 && (_fb == 1 || _fb == -1)) return;
            else if (_fb < -1 || _fb > 1) //hay desbalance

                if (RotarRemove(this)) return;

            if (Dad != null)
                Dad.ReOrderRemove();
        }

        /// <summary>
        ///   elimina el nodo seleccionado de arbol
        /// </summary>
        private void Remove()
        {
            #region Caso 1 (Hoja)

            if (IsLeaf)
            {
                if (Dad != null)
                {
                    if (SoyElDerecho) //soy el hijo derecho de mi padre
                        Dad.Rigth = null;
                    else //soy el hijo izquierdo de mi padre
                        Dad.Left = null;
                }
                else Value = default(T);
            }
                #endregion
                #region Caso 2 (nodo con 1 hijo)

            else if (Left == null)
            {
                if (Dad != null)
                {
                    if (SoyElDerecho)
                        Dad.Rigth = Rigth; //el derecho de mi padre es mi derecho
                    else //soy izquierdo
                        Dad.Left = Rigth;
                    Rigth.Dad = Dad;
                }
                else
                {
                    //elimina la raiz con un hijo derecho
                    Value = Rigth.Value;
                    Rigth = null;
                    UpdateFb();
                }
            }
            else if (Rigth == null)
            {
                if (Dad != null)
                {
                    if (SoyElDerecho)
                        Dad.Rigth = Left; //el derecho de mi padre es mi derecho
                    else //soy izquierdo

                        Dad.Left = Left;
                    Left.Dad = Dad;
                }
                else
                {
                    //elimina la raiz con un hijo derecho
                    Value = Left.Value;
                    Left = null;
                    UpdateFb();
                }
            }
                #endregion
                #region Caso 3

            else
            {
                OrderedList<T> m = Rigth.MenorMayores;
                Value = m.Value;
                m.Remove();
                return;
            }

            #endregion

            if (Dad != null)
                Dad.ReOrderRemove();
        }

        private bool RotarRemove(OrderedList<T> node)
        {
            if (node._fb >= 1 && node.Rigth._fb == 0)
            {
                RotarIzq(node);
                return true;
            }
            else if (node._fb <= -1 && node.Left._fb == 0)
            {
                RotarDer(node);
                return true;
            }
            else
            {
                Rotar(node);
                return false;
            }
        }

        #endregion

        #region Miembros de IEnumerable<T>

        public IEnumerator<T> GetEnumerator()
        {
            return InOrder().GetEnumerator();
        }

        #endregion

        #region Miembros de IEnumerable

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        public T Value { get; private set; }

        public OrderedList<T> Dad { get; protected set; }

        public OrderedList<T> Rigth { get; protected set; }

        public OrderedList<T> Left { get; protected set; }

        /// <summary>
        ///   actualiza la altura del nodo y su factor de balance a 
        ///   partie de la de sus hijos
        /// </summary>
        private void UpdateFb()
        {
            int leftHeigth = -1, rigthHeigth = -1;
            if (Left != null) leftHeigth = Left._altura;
            if (Rigth != null) rigthHeigth = Rigth._altura;

            _altura = Math.Max(leftHeigth, rigthHeigth) + 1;

            _fb = rigthHeigth - leftHeigth;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        private IEnumerable<T> InOrder()
        {
            if (Left != null)
                foreach (T item in Left.InOrder())
                    yield return item;

            yield return Value;

            if (Rigth != null)
                foreach (T item in Rigth.InOrder())
                    yield return item;
        }
    }
}