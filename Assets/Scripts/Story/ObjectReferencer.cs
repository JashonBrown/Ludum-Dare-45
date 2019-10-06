using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectReferencer : MonoBehaviour
{
    public static ObjectReferencer Instance;

    // Fields
    public StoryShip ship;
    public bool CanMove;
    public TextWriter textWriter;

    private void Awake()
    {
        if (Instance == null) Instance = this;

        // Default
        CanMove = true;
    }
}
