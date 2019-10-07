using Doozy.Engine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public SpriteRenderer bugSlot1;
    public SpriteRenderer bugSlot2;
    public SpriteRenderer bugSlot3;

    public bool bug1Complete = false;
    public bool bug2Complete = false;
    public bool bug3Complete = false;
    public bool bug4Complete = false;
    public bool bug5Complete = false;
    public bool bug6Complete = false;

    public GameObject Bug1Island;
    public GameObject Bug2Island;
    public GameObject Bug3Island;
    public GameObject Bug4Island;
    public GameObject Bug5Island;
    public GameObject Bug6Island;

    public TextAsset bug1Conversation;
    public TextAsset bug1Yes;
    public TextAsset bug1No;
    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    public TextAsset bug2Conversation;
    public TextAsset bug2Yes;
    public TextAsset bug2No;
    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    public TextAsset bug3Conversation;
    public TextAsset bug3Yes;
    public TextAsset bug3No;
    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    public TextAsset bug4Conversation;
    public TextAsset bug4Yes;
    public TextAsset bug4No;
    public TextAsset bug4Full;
    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    public TextAsset bug5Conversation;
    public TextAsset bug5Yes;
    public TextAsset bug5No;
    public TextAsset bug5Full;
    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    public TextAsset bug6Conversation;
    public TextAsset bug6Yes;
    public TextAsset bug6No;
    public TextAsset bug6Full;
    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    private int _currentBug = 0;
    [SerializeField] private int _currentSlot = 1;

    public UIView startView;

    private void Start()
    {
        ObjectReferencer.Instance.ship.GetComponent<Animator>().Play("ship-start");

        StartCoroutine(WaitForStart());
    }

    // ---------------------------------------------------------------------

    private void Update()
    {
        var distanceTravelled = ObjectReferencer.Instance.ship.distanceCovered;

        if (!bug1Complete && distanceTravelled > 2)
        {
            bug1Complete = true;
            _BugEvent(1);
        }
        else if (!bug2Complete && distanceTravelled > 5)
        {
            bug2Complete = true;
            _BugEvent(2);
        }
        else if (!bug3Complete && distanceTravelled > 8)
        {
            bug3Complete = true;
            _BugEvent(3);
        }
        else if (!bug4Complete && distanceTravelled > 11)
        {
             bug4Complete = true;
            _BugEvent(4);
        }
        else if (!bug5Complete && distanceTravelled > 14)
        {
             bug5Complete = true;
            _BugEvent(5);
        }
        else if (!bug6Complete && distanceTravelled > 17)
        {
              bug6Complete = true;
            _BugEvent(6);
        }
        else if (distanceTravelled > 20)
        {

        }
    }

    // ---------------------------------------------------------------------

    IEnumerator WaitForStart()
    {
        yield return new WaitForSeconds(4);
        startView.Show();
    }

    // ---------------------------------------------------------------------

    public void AssignBugSprite(int slot, Sprite sprite)
    {
        if (slot == 1) bugSlot1.sprite = sprite;
        else if (slot == 2) bugSlot2.sprite = sprite;
        else if (slot == 3) bugSlot3.sprite = sprite;
    }

    // ---------------------------------------------------------------------

    public void OnOkayButtonClicked()
    {
        startView.Hide();
        ObjectReferencer.Instance.CanMove = true;
    }

    // ---------------------------------------------------------------------

    public void OnYesButtonClicked()
    {
        switch (_currentBug)
        {
            case 1:
                
                ObjectReferencer.Instance.inkyController.StartConversation(bug1Yes, false);
                break;
            case 2:
                ObjectReferencer.Instance.inkyController.StartConversation(bug2Yes, false);
                break;
            case 3:
                ObjectReferencer.Instance.inkyController.StartConversation(bug3Yes, false);
                break;
            case 4:
                ObjectReferencer.Instance.inkyController.StartConversation(bug4Yes, false);
                break;
            case 5:
                ObjectReferencer.Instance.inkyController.StartConversation(bug5Yes, false);
                break;
            case 6:
                ObjectReferencer.Instance.inkyController.StartConversation(bug6Yes, false);
                break;
        }

        ObjectReferencer.Instance.inkyController.isShowingChoices = false;

        // Play onboard nimation
        _PlayOnboardAnimation();
    }

    // ---------------------------------------------------------------------

    public void OnNoButtonClicked()
    {
        switch (_currentBug)
        {
            case 1:
                ObjectReferencer.Instance.inkyController.StartConversation(bug1No, false);
                break;
            case 2:
                ObjectReferencer.Instance.inkyController.StartConversation(bug2No, false);
                break;
            case 3:
                ObjectReferencer.Instance.inkyController.StartConversation(bug3No, false);
                break;
            case 4:
                ObjectReferencer.Instance.inkyController.StartConversation(bug4No, false);
                break;
            case 5:
                ObjectReferencer.Instance.inkyController.StartConversation(bug5No, false);
                break;
            case 6:
                ObjectReferencer.Instance.inkyController.StartConversation(bug6No, false);
                break;
        }

        ObjectReferencer.Instance.inkyController.isShowingChoices = false;
    }

    // ---------------------------------------------------------------------

    public void AssignCurrentBugToSlot()
    {
        Sprite sprite = null;

        switch (_currentBug)
        {
            case 1: sprite = ObjectReferencer.Instance.inkyController.bug1Character.sprite; break;
            case 2: sprite = ObjectReferencer.Instance.inkyController.bug2Character.sprite; break;
            case 3: sprite = ObjectReferencer.Instance.inkyController.bug3Character.sprite; break;
            case 4: sprite = ObjectReferencer.Instance.inkyController.bug4Character.sprite; break;
            case 5: sprite = ObjectReferencer.Instance.inkyController.bug5Character.sprite; break;
            case 6: sprite = ObjectReferencer.Instance.inkyController.bug6Character.sprite; break;
        }

        AssignBugSprite(_currentSlot, sprite);
        _currentSlot++;
    }

    // ---------------------------------------------------------------------

    private void _PlayOnboardAnimation()
    {
        ObjectReferencer.Instance.isPlayingOnboardAnimation = true;

        GameObject islandGo = null;

        switch (_currentBug)
        {
            case 1: islandGo = Bug1Island; break;
            case 2: islandGo = Bug2Island; break;
            case 3: islandGo = Bug3Island; break;
            case 4: islandGo = Bug4Island; break;
            case 5: islandGo = Bug5Island; break;
            case 6: islandGo = Bug6Island; break;
        }

        if (_currentSlot == 1) islandGo.GetComponent<Animator>().Play("onboard_1");
        else if (_currentSlot == 2) islandGo.GetComponent<Animator>().Play("onboard_2");
        else if (_currentSlot == 3) islandGo.GetComponent<Animator>().Play("onboard_3");
    }

    // ---------------------------------------------------------------------

    private void _PlayChainAnimation(int num)
    {
        GameObject islandGo = null;

        switch (num)
        {
            case 4: islandGo = Bug4Island; break;
            case 5: islandGo = Bug5Island; break;
            case 6: islandGo = Bug6Island; break;
        }

        islandGo.GetComponent<Animator>().SetBool("Pass", true);
    }

    // ---------------------------------------------------------------------

    private void _BugEvent(int num)
    {
        GameObject islandGo = null;
        string convoString = "";

        switch (num)
        {
            case 1:
                islandGo = Bug1Island;
                convoString = "_Bug1Conversation";
                break;
            case 2:
                islandGo = Bug2Island;
                convoString = "_Bug2Conversation";
                break;
            case 3:
                islandGo = Bug3Island;
                convoString = "_Bug3Conversation";
                break;
            case 4:
                islandGo = Bug4Island;
                convoString = "_Bug4Conversation";
                break;
            case 5:
                islandGo = Bug5Island;
                convoString = "_Bug5Conversation";
                break;
            case 6:
                islandGo = Bug6Island;
                convoString = "_Bug6Conversation";
                break;
        }

        ObjectReferencer.Instance.CanMove = false;
        islandGo.SetActive(true);
        _DoDelayBugAnimation(num);
        Invoke(convoString, 2);
    }

    // ---------------------------------------------------------------------

    private void _DoDelayBugAnimation(int num)
    {
        GameObject islandGo = null;

        switch (num)
        {
            case 1: islandGo = Bug1Island; break;
            case 2: islandGo = Bug2Island; break;
            case 3: islandGo = Bug3Island; break;
            case 4: islandGo = Bug4Island; break;
            case 5: islandGo = Bug5Island; break;
            case 6: islandGo = Bug6Island; break;
        }

        islandGo.GetComponent<Animator>().Play("entrance");
    }

    // ---------------------------------------------------------------------

    public void DoExitBugAnimation()
    {
        GameObject islandGo = null;

        switch (_currentBug)
        {
            case 1: islandGo = Bug1Island; break;
            case 2: islandGo = Bug2Island; break;
            case 3: islandGo = Bug3Island; break;
            case 4: islandGo = Bug4Island; break;
            case 5: islandGo = Bug5Island; break;
            case 6: islandGo = Bug6Island; break;
        }

        islandGo.GetComponent<Animator>().Play("exit");
    }

    // ---------------------------------------------------------------------

    private void _Bug1Conversation()
    {
        _currentBug = 1;
        ObjectReferencer.Instance.inkyController.StartConversation(bug1Conversation, true);
    }

    // ---------------------------------------------------------------------

    private void _Bug2Conversation()
    {
        _currentBug = 2;
        ObjectReferencer.Instance.inkyController.StartConversation(bug2Conversation, true);
    }

    // ---------------------------------------------------------------------

    private void _Bug3Conversation()
    {
        _currentBug = 3;
        ObjectReferencer.Instance.inkyController.StartConversation(bug3Conversation, true);
    }

    // ---------------------------------------------------------------------

    private void _Bug4Conversation()
    {
        _currentBug = 4;
        if (_currentSlot >= 4) ObjectReferencer.Instance.inkyController.StartConversation(bug4Full, false);
        else ObjectReferencer.Instance.inkyController.StartConversation(bug4Conversation, true);
    }


    // ---------------------------------------------------------------------

    private void _Bug5Conversation()
    {
        _currentBug = 5;
        if (_currentSlot >= 4) ObjectReferencer.Instance.inkyController.StartConversation(bug5Full, false);
        else ObjectReferencer.Instance.inkyController.StartConversation(bug5Conversation, true);
    }

    // ---------------------------------------------------------------------

    private void _Bug6Conversation()
    {
        _currentBug = 6;
        if (_currentSlot >= 4) ObjectReferencer.Instance.inkyController.StartConversation(bug6Full, false);
        else ObjectReferencer.Instance.inkyController.StartConversation(bug6Conversation, true);
    }
}
