using System.Collections.Generic;
using UnityEngine;

namespace AutomateBase
{
    public class QueueBehaviour : MonoBehaviour
    {
        private Stack<Ticket> _tickets;
        public bool IsEmpty => _tickets.Count == 0;

        public const int LIMIT = 13;

        public void Start()
        {
            ResetQueue();
        }

        public void ResetQueue()
        {
            _tickets = new Stack<Ticket>();
            _tickets.Push(new Ticket('0'));
        }

        public bool ProcessItem(Ticket processedWord, IEnumerable<Ticket> insertedWords)
        {
            if (_tickets.Count == 0)
            {
                Debug.LogError($"Não é possivel processar a letra {processedWord.Letter} pois a fila está vazia");
                return false;
            }

            var nextItemInQueue = _tickets.Peek();
        
            if (!nextItemInQueue.Equals(processedWord))
            {
                Debug.Log($"Não é possivel processar a letra {processedWord.Letter} não é igual ao próximo a ser processado {nextItemInQueue.Letter}");
                return false;
            }

            _tickets.Pop();

            foreach (var word in insertedWords)
            {
                if (_tickets.Count + 1 > LIMIT)
                {
                    return false;
                }
                _tickets.Push(word);
            }

            return true;
        }

        public Ticket GetNextTicket()
        {
            if (IsEmpty)
            {
                return null;
            }

            return _tickets.Peek();
        }

        public List<Ticket> GetTicketsAsList()
        {
            return new List<Ticket>(_tickets  );
        }
    }
}
