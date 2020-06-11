using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class NodeIntegration : MonoBehaviour
{
    [SerializeField]
    private Text nodeText = null;

    [SerializeField]
    private Node node = null;

    [SerializeField]
    private Node[] nodeTransition = new Node[0];

    public int NodeIndex { get; private set; } = 0;

    public bool AddCommand(char processedWord, char poppedTicket, string pushedTicket, int nodeIndex)
    {
        var selectedDestinyNode = nodeTransition[nodeIndex];

        var sucess = node.AddCommand(processedWord, poppedTicket, pushedTicket, selectedDestinyNode, NodeIndex);
        
        if (sucess)
        {
            NodeIndex++;
        }

        return sucess;
    }

    public void RemoveCommand(Guid guid)
    {
        if(node.RemoveCommand(guid))
        {
            NodeIndex--;
        }
    }

    public string GetNodeText()
    {
        if (nodeText is null)
        {
            Debug.LogError(BaseException.FieldNotSetted(nameof(nodeText), gameObject.name));
            return "";
        }

        return nodeText.text;
    }

    public void ClearCommands()
    {
        node.ClearCommands();
        NodeIndex = 0;
    }

    public IList<Command> GetCommands()
    {
        return node.Commands;
    }

    public IList<string> GetNodes()
    {
        return nodeTransition.Select(x => x.NodeName).ToList();
    }

    public string GetNodeName()
    {
        return node.NodeName;
    }
}
