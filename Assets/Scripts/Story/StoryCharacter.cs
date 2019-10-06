using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Character")]
public class StoryCharacter : ScriptableObject
{
    public string Name;
    public Sprite sprite;
    public bool IsPlayer;
}
