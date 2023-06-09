using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DialogueLine
{
    // TO ADD List:
    // Background images (Done?)
    // Animations
    // Additional sounds to act as voice clips and sound effects
    // And maybe even cinematics eventually?

    [Header("Normal Node")]
    public string speaker;
    [TextArea]
    public string dialogue;
    public bool additive;
    public GameObject character; // Will create new character if not found.
    public CHARACTERPOSITIONS.LocationEnum stageDirection;
    public GameObject[] otherCharacters;
    public CHARACTERPOSITIONS.LocationEnum[] otherStageDirections;
    public Sprite speakerImage; // Will change character's image component, null will leave it as is.
    public AudioClip speakingSound;
    public float timeBetweenLetters = 0.04f;
    public AudioClip soundEffect; // Will play a sound in reaction to something

    public bool removeFromScene;
    public float moveSpeed = 3f;
    public bool reverseImage;

    //public bool triggerBoolPlaceholder; // Will be checked in the DialogueManager, and trigger whatever outside scripts we deem necessary

    public CHARACTERPOSITIONS.AnimationEnum animationDirection; //Implement later
    public CHARACTERPOSITIONS.AnimationEnum[] otherAnimationDirections; //Implement later

    [Header("Choice Node")]
    public bool isChoiceNode;
    public string choiceName; // Will get added to a dictionary as a unique choice (Potentially need to make this a unique object)
    [TextArea]
    public string choice1Text; // Choice text
    public Conversation choiceConvo1; // Conversation to run if it get selected
    public string choice1Result; // Result to be stored in dictionary, alongside choiceName
    [TextArea]
    public string choice2Text;
    public Conversation choiceConvo2;
    public string choice2Result;
    [TextArea]
    public string choice3Text;
    public Conversation choiceConvo3;
    public string choice3Result;

    [Header("Images and stuff")] // Change name of this later
    public Sprite bg;
}
