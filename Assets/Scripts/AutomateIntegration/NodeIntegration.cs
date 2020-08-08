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
#pragma warning disable 0649 
        [SerializeField] private Text nodeText;
        [SerializeField] private Node node;
        [SerializeField] private Node[] nodeTransition;
        [SerializeField] private GameObject selectedImage;
#pragma warning restore 0649
        
        public int NodeIndex { get; private set; }

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

        public void UnSelect()
        {
            selectedImage.SetActive(false);
        }

        public void Select()
        {
            selectedImage.SetActive(true);
        }
    }
}
