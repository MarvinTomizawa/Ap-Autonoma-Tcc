using System.Collections.Generic;
using UnityEngine;

namespace AutomateBase
{
    public class WordProcessor : MonoBehaviour
    {
        private Node _actualNode;
        private Stack<string> _words;

        public Node ActualNode => _actualNode;

        public void Start()
        {
            _words = new Stack<string>();
            InnitNode();            
        }

        public void InnitNode()
        {
            var nodeGameObject = GameObject.FindGameObjectWithTag("innitialNode");

            if (nodeGameObject is null)
            {
                Debug.LogError("Não foi possivel encontrar um objeto com a tag innitialNode na cena");
                return;
            }

            _actualNode = nodeGameObject.GetComponent<Node>();
            _actualNode.ResetQueue();
        }

        public void ResetProcessor()
        {
            InnitNode();
            _words.Clear();
        }

        public void AddWord(string word)
        {
            _words.Push(word);
        }

        public bool ProcessAll()
        {
            var wordsToProcess = new Stack<string>(_words);

            while (_words.Count > 0)
            {
                var word = wordsToProcess.Pop();

                if (Process(word))
                {
                    return false;
                }

            }

            return true;
        }

        public bool Process(string word)
        {
            InnitNode();

            foreach (var letter in word)
            {
                var processed = ProcessLetter(letter);

                if (processed) continue;
            
                return false;
            }

            return _actualNode.FinishedProcessing;
        }

        public bool ProcessLetter(char letter)
        {
            return _actualNode.ProcessLetter(letter, out _actualNode);
        }
    }
}
