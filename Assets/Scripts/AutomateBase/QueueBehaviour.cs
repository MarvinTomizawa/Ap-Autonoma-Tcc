using Assets.Scripts.AutomateBase;
using System.Collections.Generic;
using UnityEngine;

public class QueueBehaviour : MonoBehaviour
{
    private Stack<Ticket> tickets;

    void Start()
    {
        ResetQueue();
    }

    public void ResetQueue()
    {
        tickets = new Stack<Ticket>();
        tickets.Push(new Ticket('Z'));
    }

    public bool ProcessItem(Ticket processedWord, IList<Ticket> insertedWords)
    {
        if (tickets.Count == 0)
        {
            Debug.LogError($"Não é possivel processar a letra {processedWord.Letter} pois a fila está vazia");
            return false;
        }

        var nextItemInQueue = tickets.Peek();
        
        if (!nextItemInQueue.Equals(processedWord))
        {
            Debug.Log($"Não é possivel processar a letra {processedWord.Letter} não é igual ao próximo a ser processado {nextItemInQueue.Letter}");
            return false;
        }

        tickets.Pop();

        foreach (var word in insertedWords)
        {
            tickets.Push(word);
        }

        return true;
    }

    public Ticket GetNextTicket()
    {
        return tickets.Peek();
    }
}
