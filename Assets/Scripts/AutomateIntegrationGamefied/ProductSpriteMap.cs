using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AutomateIntegrationGamefied
{
    internal class ProductSpriteMap: MonoBehaviour
    {
#pragma warning disable 0649
        [SerializeField] private Sprite Product0;
        [SerializeField] private Sprite Product1;
        [SerializeField] private Sprite Product2;
        [SerializeField] private Sprite Product3;
        [SerializeField] private Sprite Product4;
#pragma warning restore 0649

        public Dictionary<int, Sprite> Map;

        public void Start()
        {
            Map = new Dictionary<int, Sprite>
            {
                { 0 , Product0 },
                { 1, Product1 },
                { 2, Product2 },
                { 3, Product3 },
                { 4, Product4 }
            };
        }
    }
}