using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class InkyController : MonoBehaviour
{
    public TextAsset inkAsset;

    private Story _inkStory;

    public StoryCharacter playerCharacter;
    public StoryCharacter bugBeardCharacter;
    public StoryCharacter bug2Character;

    private void Awake()
    {
        _inkStory = new Story(inkAsset.text);
    }

    private void Start()
    {
        var text = _inkStory.Continue();

        ObjectReferencer.Instance.textWriter.PrintText(text, _GetCurrentCharacter());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Skip if no more story
            if (!_inkStory.canContinue) return;

            var text = _inkStory.Continue();

            ObjectReferencer.Instance.textWriter.PrintText(text, _GetCurrentCharacter());
        }
    }

    private StoryCharacter _GetCurrentCharacter()
    {
        var tags = _inkStory.currentTags;

        if (tags.Contains("p")) return playerCharacter;
        if (tags.Contains("b1")) return bugBeardCharacter;
        if (tags.Contains("b2")) return bug2Character;

        return playerCharacter;
    }
}
