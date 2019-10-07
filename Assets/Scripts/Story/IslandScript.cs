using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandScript : MonoBehaviour
{
    public SpriteRenderer sprite;
    public SpriteRenderer islandSprite;

    public void EndOnboardAnimation()
    {
        sprite.sprite = null;
        ObjectReferencer.Instance.gameController.AssignCurrentBugToSlot();
        ObjectReferencer.Instance.isPlayingOnboardAnimation = false;
    }
}
