using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]   
public struct Character
{
    public Dictionary<string,Sprite> sprites;
    [SerializeField] string characterName;
    [SerializeField] Color color;

    public Color Color
    {
        get { return color; }
    }
    public string Name
    {
        get { return characterName; }
    }

}
