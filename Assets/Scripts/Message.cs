using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Custom/Message", order = 1)]
public class Message : Node
{
    public Speaker speaker;
    public SpeakerEmotion emotion;
    public string text;
}

