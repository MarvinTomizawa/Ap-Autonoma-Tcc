using System;
using System.Collections.Generic;
using System.Linq;
using AutomateBase;
using Exception;
using UnityEngine;
using UnityEngine.UI;

namespace AutomateIntegration
{
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

            var success = node.AddCommand(processedWord, poppedTicket, pushedTicket, selectedDestinyNode, NodeIndex);
        
            if (success)
            {
                NodeIndex++;
            }

            return success;
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
            if (!(nodeText is null))
            {
                return nodeText.text;
            }

            Debug.LogError(BaseException.FieldNotSetted(nameof(nodeText), gameObject.name));
            return "";

        }

        public void ClearCommands()
        {
            node.ClearCommands();
            NodeIndex = 0;
        }

        public IEnumerable<Command> GetCommands()
        {
            return node.Commands;
        }

        public IEnumerable<string> GetNodes()
        {
            return nodeTransition.Select(x => x.NodeName).ToList();
        }

        public string GetNodeName()
        {
            return node.NodeName;
        }
    }
}
