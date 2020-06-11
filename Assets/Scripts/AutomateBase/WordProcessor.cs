using System.Collections.Generic;
using UnityEngine;

public class WordProcessor : MonoBehaviour
{
    private Node actualNode;
    private Stack<string> words;

    void Start()
    {
        words = new Stack<string>();
        InnitNode();            
    }

    private void InnitNode()
    {
        var nodeGameObject = GameObject.FindGameObjectWithTag("innitialNode");

        if (nodeGameObject is null)
        {
            Debug.LogError("Não foi possivel encontrar um objeto com a tag innitialNode na cena");
        }

        actualNode = nodeGameObject.GetComponent<Node>();
    }

    public void ResetProcessor()
    {
        InnitNode();
        words.Clear();
    }

    public void AddWord(string word)
    {
        words.Push(word);
    }

    public bool ProcessAll()
    {
        var wordsToProcess = new Stack<string>(words);

        while (words.Count > 0)
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
        bool success = true;

        InnitNode();

        foreach (char letter in word)
        {
            var processed = actualNode.ProcessLetter(letter, out actualNode);

            if (!processed)
            {
                success = false;
                break;
            }
        }

        return success;
    }
}
