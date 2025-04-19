using UnityEngine;

public class CustomCharacterSelectionList<T> : CircularDoubleLinkedList<T>
{
    private Node<T> currentNode;

    public void InitializeSelection()
    {
        currentNode = Head;
    }

    public void SelectNext()
    {
            currentNode = currentNode.Next;
            Debug.Log("Character selected: " + currentNode.Value.ToString());
    }

    public void SelectPrevious()
    {
            currentNode = currentNode.Prev;
            Debug.Log("Character selected: " + currentNode.Value.ToString());
    }

    public T GetCurrentSelection()
    {
        return currentNode != null ? currentNode.Value : default(T);
    }
}