using System;
using UnityEngine;
using UnityEngine.UI;

public class CommandIntegration : MonoBehaviour
{
    [SerializeField]
    private Text ProcessedWord;

    [SerializeField]
    private Text PoppedTicket;

    [SerializeField]
    private Text PushedTickets;

    [SerializeField]
    private Text OriginNode;

    [SerializeField]
    private Text DestinyNode;

    public Guid Id { get; private set; }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
    
    public void SetValues(Guid id, char processedWord, char poppedTicket, string pushedTickets, string destinyNode, string originNode)
    {
        Id = id;
        gameObject.SetActive(true);
        PushedTickets.text = pushedTickets;
        PoppedTicket.text = poppedTicket.ToString();
        ProcessedWord.text = processedWord.ToString();
        OriginNode.text = originNode;
        DestinyNode.text = destinyNode;
    }
}
