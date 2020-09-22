using Assets.Scripts.Exception;
using AutomateBase;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace AutomateIntegration
{
    public class NodeIntegration : IntegrationFieldsValidator
    {
#pragma warning disable 0649 
        [SerializeField] private Text nodeText;
        [SerializeField] private Node node;
        [SerializeField] private Node[] nodeTransition;
        [SerializeField] private GameObject selectedImage;
        [SerializeField] private int[] productValues;
#pragma warning restore 0649
        
        public int NodeIndex { get; private set; }

        public IEnumerable<Command> GetCommands()
            => node.Commands;

        public IEnumerable<string> GetNodes()
            => nodeTransition.Select(x => x.NodeName).ToList();

        public string GetNodeName()
            => node.NodeName;

        public IEnumerable<int> GetProducts()
            => productValues;

        public bool AddCommand(char processedWord, char poppedTicket, string pushedTicket, int nodeIndex)
        {
            if (IsNotValid) return false;

            var selectedDestinyNode = nodeTransition[nodeIndex];

            node.AddCommand(processedWord, poppedTicket, pushedTicket, selectedDestinyNode, NodeIndex);
            NodeIndex++;
            return true;
        }

        public void RemoveCommand(Guid guid)
        {
            if (IsNotValid) return;

            if(node.RemoveCommand(guid))
            {
                NodeIndex--;
            }
        }

        public string GetNodeText()
        {
            if (IsNotValid) return "";

            return nodeText.text;
        }

        public void ClearCommands()
        {
            if (IsNotValid) return;

            node.ClearCommands();
            NodeIndex = 0;
        }

        public void UnSelect()
        {
            selectedImage.SetActive(false);
        }

        public void Select()
        {
            selectedImage.SetActive(true);
        }

        protected override List<(object, string)> FieldsToBeValidated()
            => new List<(object, string)> { 
                (nodeText, nameof(nodeText)),
                (node, nameof(node)),
                (selectedImage, nameof(selectedImage))
            };
    }
}
