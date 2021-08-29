using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Custom/Response", order = 1)]
public class Response : Node
{
    public Choice[] choices;
}

[System.Serializable]
public class Choice
{
    public string text;
    public Node next;
    public int attractionBonus = 0;
}
