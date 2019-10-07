using Doozy.Engine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectReferencer : MonoBehaviour
{
    public static ObjectReferencer Instance;

    // Fields
    public StoryShip ship;
    public bool CanMove = false;
    public bool IsForcedMoving = false;
    public TextWriter textWriter;
    public InkyController inkyController;
    public GameController gameController;
    public UIView instructionView;
    public bool isPlayingOnboardAnimation = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    // ---------------------------------------------------------------------

    private void Update()
    {
        if (CanMove && !IsForcedMoving && !Input.GetMouseButton(1))
        {
            instructionView.Show();
        }
        else
        {
            instructionView.Hide();
        }
    }
}
