using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using Doozy.Engine.UI;

public class InkyController : MonoBehaviour
{
    private Story _inkStory;

    public StoryCharacter playerCharacter;
    public StoryCharacter bug1Character;
    public StoryCharacter bug2Character;
    public StoryCharacter bug3Character;
    public StoryCharacter bug4Character;
    public StoryCharacter bug5Character;
    public StoryCharacter bug6Character;

    public bool isInConvo = false;
    public bool isShowingChoices = false;
    public bool doShowChoices = true;

    public UIView chatView;

    private void Update()
    {
        if (!isInConvo) return;

        if (ObjectReferencer.Instance.isPlayingOnboardAnimation) return;

        if (Input.GetMouseButtonDown(0))
        {
            // Skip if no more story
            if (_inkStory.canContinue)
            {
                var text = _inkStory.Continue();
                ObjectReferencer.Instance.textWriter.PrintText(text, _GetCurrentCharacter());
            }
            // Otherwise, player is making decision
            else if (!isShowingChoices && doShowChoices)
            {
                ObjectReferencer.Instance.textWriter.ShowChoices();
                isShowingChoices = true;
            }
            else if (!isShowingChoices)
            {
                EndConversation();
                ObjectReferencer.Instance.IsForcedMoving = true;
                Invoke("_ForceMoveStopper", 3);
                ObjectReferencer.Instance.gameController.DoExitBugAnimation();
            }
        }
    }

    // ---------------------------------------------------------------------

    private void _ForceMoveStopper()
    {
        ObjectReferencer.Instance.IsForcedMoving = false;
    }

    // ---------------------------------------------------------------------

    public void StartConversation(TextAsset inkAsset, bool hasChoices)
    {
        doShowChoices = hasChoices;
        ObjectReferencer.Instance.CanMove = false;

        if (!chatView.IsActive()) {
            chatView.Show();
        }

        _inkStory = new Story(inkAsset.text);

        isInConvo = true;

        if (_inkStory.canContinue)
        {
            var text = _inkStory.Continue();
            ObjectReferencer.Instance.textWriter.PrintText(text, _GetCurrentCharacter());
        }
    }

    // ---------------------------------------------------------------------

    public void EndConversation()
    {
        isInConvo = false;
        isShowingChoices = false;
        chatView.Hide();
        ObjectReferencer.Instance.CanMove = true;
    }

    // ---------------------------------------------------------------------

    private StoryCharacter _GetCurrentCharacter()
    {
        var tags = _inkStory.currentTags;

        if (tags.Contains("p")) return playerCharacter;
        if (tags.Contains("b1")) return bug1Character;
        if (tags.Contains("b2")) return bug2Character;
        if (tags.Contains("b3")) return bug3Character;
        if (tags.Contains("b4")) return bug4Character;
        if (tags.Contains("b5")) return bug5Character;
        if (tags.Contains("b6")) return bug6Character;

        return playerCharacter;
    }
}
