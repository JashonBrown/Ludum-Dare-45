using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using Doozy.Engine.UI;
using UnityEngine.SceneManagement;

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
    public StoryCharacter ghostChar;

    public bool isInConvo = false;
    public bool isShowingChoices = false;
    public bool doShowChoices = true;
    public bool isEnd = false;

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

                if (_inkStory.currentTags.Contains("1"))
                {
                    if (ObjectReferencer.Instance.gameController.bugsInBoat.Count < 1) return;

                    int firstBug = ObjectReferencer.Instance.gameController.bugsInBoat[0];

                    ObjectReferencer.Instance.textWriter.PrintText(text, _GetCharacter(firstBug));
                }
                else if (_inkStory.currentTags.Contains("2"))
                {
                    if (ObjectReferencer.Instance.gameController.bugsInBoat.Count < 2) return;

                    int secondBug = ObjectReferencer.Instance.gameController.bugsInBoat[1];

                    ObjectReferencer.Instance.textWriter.PrintText(text, _GetCharacter(secondBug));
                }
                else if (_inkStory.currentTags.Contains("3"))
                {
                    if (ObjectReferencer.Instance.gameController.bugsInBoat.Count < 2) return;

                    int thirdBug = ObjectReferencer.Instance.gameController.bugsInBoat[2];

                    ObjectReferencer.Instance.textWriter.PrintText(text, _GetCharacter(thirdBug));
                }
                else
                {
                    ObjectReferencer.Instance.textWriter.PrintText(text, _GetCurrentCharacter());
                }

            }
            // Otherwise, player is making decision
            else if (!isShowingChoices && doShowChoices)
            {
                ObjectReferencer.Instance.textWriter.ShowChoices();
                isShowingChoices = true;
            }
            else if (!isShowingChoices && !isEnd)
            {
                EndConversation();
                ObjectReferencer.Instance.IsForcedMoving = true;
                Invoke("_ForceMoveStopper", 3);
                ObjectReferencer.Instance.gameController.DoExitBugAnimation();
            }
            else if (isEnd && ObjectReferencer.Instance.gameController.bugsInBoat.Count == 3)
            {
                SceneManager.LoadScene("Win Screen");
            }
            else if (isEnd && ObjectReferencer.Instance.gameController.bugsInBoat.Count < 3)
            {
                // LOSE
            }
        }
    }

    // ---------------------------------------------------------------------

    private void _ForceMoveStopper()
    {
        ObjectReferencer.Instance.IsForcedMoving = false;
    }

    // ---------------------------------------------------------------------

    public void StartConversation(TextAsset inkAsset, bool hasChoices, bool isGhost = false)
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

            if (!isGhost)
            {
                ObjectReferencer.Instance.textWriter.PrintText(text, _GetCurrentCharacter());
            }
            else
            {
                ObjectReferencer.Instance.textWriter.PrintText(text, ghostChar);
                
            }
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
        if (tags.Contains("g")) return ghostChar;

        return playerCharacter;
    }

    // ---------------------------------------------------------------------

    private StoryCharacter _GetCharacter(int num)
    {
        if (num == 1) return bug1Character;
        if (num == 2) return bug2Character;
        if (num == 3) return bug3Character;
        if (num == 4) return bug4Character;
        if (num == 5) return bug5Character;
        if (num == 6) return bug6Character;

        return null;
    }
}
