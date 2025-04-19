using UnityEngine;

public class CircularDoubleLinkedList<T> : MonoBehaviour
{
    #region Settings
    private Node<T> head = null;
    private Node<T> last = null;

    private int count = 0;
    #endregion
    #region Getters
    public Node<T> Head => head;
    public Node<T> Last => last;

    public int Count => count;
    #endregion

    #region Methods
    #region Add
    public virtual void Add(T value)
    {
        Node<T> newNode = new Node<T>(value);
        count++;

        if (head == null)
        {
            head = newNode;
            last = newNode;

            head.SetNext(head);
            head.SetPrev(head);

            return;
        }

        last.SetNext(newNode);
        newNode.SetPrev(last);
        newNode.SetNext(head);

        head.SetPrev(newNode);

        last = newNode;
    }
    #endregion
    #region Seek
    public Node<T> Seek(T objective, Node<T> _head = null, int deep = 0)
    {
        if (head == null || deep >= count)
        {
            print("No hay elementos en la lista o No se encontr� el elemento");
            return null;
        }
        if (_head == null)
            _head = head;

        if (_head.Value.Equals(objective))
        {
            print("Elemento encontrado: " + _head.Value.ToString());
            print("Se encontr� en la posici�n: " + deep);
            return _head;
        }
        else
            return Seek(objective, _head.Next, deep + 1);
    }
    public Node<T> Seek(int _pos, Node<T> _head = null, int deep = 0)
    {
        if (head == null || deep >= count)
        {
            print("No hay elementos en la lista o No se encontr� el elemento");
            return null;
        }
        if (_head == null)
            _head = head;

        if (_pos == deep && _pos <= count)
        {
            print("Elemento encontrado: " + _head.Value.ToString());
            print("Se encontr� en la posici�n: " + deep);
            return _head;
        }
        else
            return Seek(_pos, _head.Next, deep + 1);
    }
    public Node<T> SeekPrev(T objective)
    {
        return Seek(objective).Prev;
    }
    public Node<T> SeekPrev(int _pos)
    {
        return Seek(_pos).Prev;
    }
    public Node<T> SeekNext(T objective)
    {
        return Seek(objective).Next;
    }
    public Node<T> SeekNext(int _pos)
    {
        return Seek(_pos).Next;
    }
    #endregion
    #region Read
    public virtual void ReadFromStart(Node<T> _head = null, int deep = 0)
    {
        if (head == null || deep >= count) return;

        if (_head == null)
        {
            _head = head;
        }
        print("" + _head.Value.ToString());
        print(" ? ");

        ReadFromStart(_head.Next, deep + 1);
    }
    public virtual void ReadFromEnd(Node<T> _last = null, int deep = 0)
    {
        if (last == null || deep >= count) return;

        if (_last == null)
        {
            _last = last;
        }
        print("" + _last.Value.ToString());
        print(" ? ");

        ReadFromEnd(_last.Prev, deep + 1);
    }
    #endregion

    #region Remove
    public virtual void Remove(T objective)
    {
        Node<T> node = Seek(objective);

        if (node == null)
        {
            Debug.Log("No existe elemento");
            return;
        }
        #region NodoEsElPrimero
        if (node == head && count >= 1)
        {
            head = node.Next;
            head.SetPrev(last);
            count--;
            return;
        }
        else if (node == head && count == 1)
        {
            RemoveAll();
            return;
        }
        #endregion
        #region NodoEnElMedio
        if (SeekPrev(objective) != null && node.Next != null)
        {
            SeekPrev(objective).SetNext(node.Next);

            node.Next.SetPrev(SeekPrev(objective));

            count--;
            return;
        }
        #endregion
        #region NodoEsElUltimo
        if (SeekPrev(objective) != null && node.Next == null)
        {
            SeekPrev(objective).SetNext(head);
            last = SeekPrev(objective);
            count--;
            return;
        }
        #endregion
    }

    public virtual void RemoveAll()
    {
        head = null;
        last = null;
        count = 0;
    }
    #endregion
    #endregion
}