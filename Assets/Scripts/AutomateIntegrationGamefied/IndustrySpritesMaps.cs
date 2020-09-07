using System.Collections.Generic;
using UnityEngine;

public class IndustrySpritesMaps : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private Sprite industryA;
    [SerializeField] private Sprite industryB;
    [SerializeField] private Sprite industryC;
#pragma warning restore 0649

    public Dictionary<string, Sprite> Map;

    public void Start()
    {
        Map = new Dictionary<string, Sprite>
            {
                { "A", industryA },
                { "B", industryB },
                { "C", industryC }
            };
    }
}
