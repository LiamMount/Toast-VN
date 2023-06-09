using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueBoxUI, dialogueChoiceUI;
    private bool justOpened = false; // Controls the delay for text starting

    public Text speakerName, speech;
    public GameObject continueIndicator;

    public RectTransform characterPanel;
    public Image backgroundImage;

    public Text choiceOne, choiceTwo, choiceThree;
    public Transform choice1Pos, choice2Pos, choice3Pos;
    public GameObject chooser;
    public GameObject twoChoiceBox, threeChoiceBox;
    private bool choiceBuffer = false; // To stop the input from double-triggering

    private int currentIndex;
    private Conversation currentConvo;
    [HideInInspector]
    public static DialogueManager instance; // Formerly private, needed access for other scripts

    //public Animator anim;
    //[HideInInspector]
    public bool fadedOut; // Used to trigger level scripts or whatever when a scene is finished
    public AudioSource audioSource;

    private Coroutine typing;

    // Start Character Test
    private GameObject prefabCharacter; // Used to spawn character in
    private GameObject activeCharacter;
    private DialogueLine currentLine;
    // End Character Test

    public List<GameObject> charactersInScene = new List<GameObject>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        dialogueBoxUI.SetActive(true);
        dialogueChoiceUI.SetActive(false); //Maybe switch these to animations in the future
    }

    void Update()
    {
        if (!PauseMenu.instance.gameIsPaused)
        {
            if (Input.GetButtonDown("Fire1") && (currentConvo != null ? !currentLine.isChoiceNode : true))
            {
                ReadNext();
            }

            if (currentConvo != null ? currentLine.isChoiceNode : false)
            {
                ChoiceControls();
            }
        }
    }

    public void ChoiceControls()
    {
        // Modify to allow for controllers later
        if (Input.GetKeyDown(KeyCode.UpArrow)) // TODO - Add axis controls
        {
            if (chooser.transform.position == choice1Pos.position)
            {
                if (currentLine.choiceConvo3 != null)
                {
                    chooser.transform.position = choice3Pos.position;
                }
                else
                {
                    chooser.transform.position = choice2Pos.position;
                }
            }
            else if (chooser.transform.position == choice2Pos.position)
            {
                chooser.transform.position = choice1Pos.position;
            }
            else if (chooser.transform.position == choice3Pos.position)
            {
                chooser.transform.position = choice2Pos.position;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow)) // TODO - Add axis controls
        {
            if (chooser.transform.position == choice1Pos.position)
            {
                chooser.transform.position = choice2Pos.position;
            }
            else if (chooser.transform.position == choice2Pos.position)
            {
                if (currentLine.choiceConvo3 != null)
                {
                    chooser.transform.position = choice3Pos.position;
                }
                else
                {
                    chooser.transform.position = choice1Pos.position;
                }
            }
            else if (chooser.transform.position == choice3Pos.position)
            {
                chooser.transform.position = choice1Pos.position;
            }
        }
        else if (Input.GetButtonDown("Fire1"))
        {
            bool choiceFound = false;
            SavesManager.ChoiceSet currentChoice = new SavesManager.ChoiceSet(currentLine.choiceName, null);
            foreach (SavesManager.ChoiceSet choice in SavesManager.instance.activeSave.choiceList)
            {
                if (choice.choiceKey == currentChoice.choiceKey)
                {
                    currentChoice = choice;
                    choiceFound = true;
                    if (chooser.transform.position == choice1Pos.position)
                    {
                        choice.choiceResult = currentLine.choice1Result;
                    }
                    else if (chooser.transform.position == choice2Pos.position)
                    {
                        choice.choiceResult = currentLine.choice2Result;
                    }
                    else if (chooser.transform.position == choice3Pos.position)
                    {
                        choice.choiceResult = currentLine.choice3Result;
                    }
                }
            }
            if (!choiceFound)
            {
                if (chooser.transform.position == choice1Pos.position)
                {
                    currentChoice.choiceResult = currentLine.choice1Result;
                }
                else if (chooser.transform.position == choice2Pos.position)
                {
                    currentChoice.choiceResult = currentLine.choice2Result;
                }
                else if (chooser.transform.position == choice3Pos.position)
                {
                    currentChoice.choiceResult = currentLine.choice3Result;
                }

                SavesManager.instance.activeSave.choiceList.Add(currentChoice);
            }

            if (!choiceBuffer)
            {
                SavesManager.instance.Save();
            }

            if (choiceBuffer)
            {
                choiceBuffer = false;
            }
            else if (chooser.transform.position == choice1Pos.position)
            {
                DialogueManager.StartConversation(currentLine.choiceConvo1);
                //Debug.Log("Uno");
            }
            else if (chooser.transform.position == choice2Pos.position)
            {
                DialogueManager.StartConversation(currentLine.choiceConvo2);
                //Debug.Log("Dos");
            }
            else if (chooser.transform.position == choice3Pos.position)
            {
                DialogueManager.StartConversation(currentLine.choiceConvo3);
                //Debug.Log("Tres");
            }
        }
    }

    public static void StartConversation(Conversation convo)
    {
        //instance.gm.isCutscene = true;

        //instance.anim.SetBool("isOpen", true);

        if (!instance.dialogueBoxUI.activeInHierarchy) //Temp
        {
            instance.justOpened = true;
            instance.dialogueBoxUI.SetActive(true); //Temp
        }
        instance.dialogueChoiceUI.SetActive(false); //Temp

        instance.currentIndex = 0;
        instance.currentConvo = convo;
        instance.speakerName.text = "";
        instance.speech.text = "";

        instance.FirstLine();
    }

    // FirstLine() is the way all lines are read
    public void FirstLine() // It was the only way to avoid pulling from outside the bounds of the convo array
    {
        if (typing != null)
        {
            instance.StopCoroutine(typing);
            typing = null;
        }

        currentLine = currentConvo.GetLineByIndex(currentIndex);

        // Backgrounds and whatnot
        if (currentLine.bg != null)
        {
            backgroundImage.sprite = currentLine.bg;
        }
        if (currentLine.soundEffect != null)
        {
            SoundManager.instance.PlaySound(currentLine.soundEffect); // Plays sound sting
        }

        // Normal Node
        if (!currentLine.isChoiceNode)
        {
            speakerName.text = currentLine.speaker;

            bool charJustMade = false;
            // Character check
            if (currentLine.character != null)
            {
                prefabCharacter = currentLine.character;
                bool characterAlreadyExists = false;

                foreach (GameObject existingCharacter in charactersInScene)
                {
                    if (existingCharacter.GetComponent<Character>().characterName == prefabCharacter.GetComponent<Character>().characterName)
                    {
                        characterAlreadyExists = true;
                        activeCharacter = existingCharacter;
                    }
                }
                if (!characterAlreadyExists)
                {
                    activeCharacter = GameObject.Instantiate(prefabCharacter, characterPanel);
                    charJustMade = true;
                    charactersInScene.Add(activeCharacter);
                    if (currentLine.stageDirection != CHARACTERPOSITIONS.LocationEnum.Stay)
                    {
                        // Code for snapping character to non-center immediately
                        activeCharacter.GetComponent<Character>().MoveTo(
                    CHARACTERPOSITIONS.characterPositionsList[(int)currentLine.stageDirection], currentLine.moveSpeed); // Problem!
                        activeCharacter.GetComponent<Character>().StopMoving(true);
                        // WTF (Why was this the solution?)
                    }
                }

                // Extra Character Check
                if (currentLine.otherCharacters.Length > 0) // Modify to accept stage directions for already-existing characters later
                {
                    int i = 0;
                    foreach (GameObject extraCharacter in currentLine.otherCharacters)
                    {
                        GameObject newCharacter = GameObject.Instantiate(extraCharacter, characterPanel);
                        charactersInScene.Add(newCharacter);
                        if (currentLine.otherStageDirections[i] != CHARACTERPOSITIONS.LocationEnum.Stay)
                        {
                            // Code for snapping character to non-center immediately
                            newCharacter.GetComponent<Character>().MoveTo(
                        CHARACTERPOSITIONS.characterPositionsList[(int)currentLine.otherStageDirections[i]], currentLine.moveSpeed);
                            newCharacter.GetComponent<Character>().StopMoving(true);
                        }

                        i++; // This just wound up being a poorly formatted for loop
                    }
                }
            }

            // Image check
            if (currentLine.speakerImage != null && activeCharacter != null)
            {
                activeCharacter.GetComponent<Character>().imageRenderer.sprite = currentLine.speakerImage;
            }

            // Removal check
            if (currentLine.removeFromScene)
            {
                charactersInScene.Remove(activeCharacter);
                //Destroy(activeCharacter.gameObject); // Modify later to trigger animations instead
                activeCharacter.GetComponent<Character>().FadeAway();
                activeCharacter = null; // Make sure to set a thing that prevents moving to the next line until the animation is done
            }

            // Stage Direction check
            if (!charJustMade && currentLine.stageDirection != CHARACTERPOSITIONS.LocationEnum.Stay && activeCharacter != null)
            {
                activeCharacter.GetComponent<Character>().MoveTo(
                    CHARACTERPOSITIONS.characterPositionsList[(int)currentLine.stageDirection], currentLine.moveSpeed);
            }

            // Animation Direction check
            if (!charJustMade && currentLine.animationDirection != CHARACTERPOSITIONS.AnimationEnum.Stay && activeCharacter != null)
            {
                // Do the thing somehow
            }

            // Flip Image check
            if (currentLine.reverseImage && activeCharacter != null ? activeCharacter.transform.rotation.y != 180 : false)
            {
                activeCharacter.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else if (!currentLine.reverseImage && activeCharacter != null ? activeCharacter.transform.rotation.y != 0 : false)
            {
                activeCharacter.transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            // End of checks
            if (currentLine.additive) // Except for this check, of course
            {
                typing = instance.StartCoroutine(TypeText(currentConvo.GetLineByIndex(currentIndex).dialogue, true));
            }
            else
            {
                typing = instance.StartCoroutine(TypeText(currentConvo.GetLineByIndex(currentIndex).dialogue));
            }
            currentIndex += 1;
        }
        // Choice Node
        else
        {
            choiceBuffer = true;
            instance.dialogueChoiceUI.SetActive(true); //Temp (Turn into animation later)

            if (currentLine.choiceConvo3 != null)
            {
                twoChoiceBox.SetActive(false);
                threeChoiceBox.SetActive(true);
            }
            else
            {
                twoChoiceBox.SetActive(true);
                threeChoiceBox.SetActive(false);
            }

            chooser.transform.position = choice1Pos.position; // Start at choice 1

            choiceOne.text = currentLine.choice1Text;
            choiceTwo.text = currentLine.choice2Text;
            choiceThree.text = currentLine.choiceConvo3 != null ? currentLine.choice3Text : "";

            //currentIndex += 1; // Maybe this isn't necessary?
        }
    }

    public void ReadNext()
    {
        // Close conversation if finished
        if (currentConvo != null && currentIndex > currentConvo.GetLength() && typing == null)
        {
            //instance.anim.SetBool("isOpen", false);
            //gm.isCutscene = false;

            // Code to clear existing characters, in case we don't have animations set
            activeCharacter = null;
            List<GameObject> tempChars = new List<GameObject>();
            foreach (GameObject existingCharacter in charactersInScene)
            {
                tempChars.Add(existingCharacter);
            }
            foreach (GameObject existingCharacter in tempChars)
            {
                charactersInScene.Remove(existingCharacter);
                //Destroy(existingCharacter);
                existingCharacter.GetComponent<Character>().FadeAway();
            }
            tempChars.Clear();

            currentConvo = null;
            instance.dialogueBoxUI.SetActive(false); //Temp
            instance.dialogueChoiceUI.SetActive(false); //Temp

            // MAYBE CALL A FUNCTION OF THE LEVEL MANAGER OR SOMETHING HERE IDK
            LevelManager.instance.AdvanceStory();


            return;
        }

        // Carry on
        if (currentConvo != null && typing == null && (activeCharacter == null || !activeCharacter.GetComponent<Character>().isMoving)) // Modified from ternary statement
        {
            FirstLine(); // Replaced the original ReadNext() function that was here
        }
        else if (typing != null)
        {
            if (activeCharacter != null)
            {
                activeCharacter.GetComponent<Character>().StopMoving(true);
            }
            instance.StopCoroutine(typing);
            typing = null;
            speech.text = currentConvo.GetLineByIndex(currentIndex - 1).dialogue;

            continueIndicator.SetActive(true);
        }
    }

    private IEnumerator TypeText(string text, bool additive = false)
    {
        if (!additive)
        {
            speech.text = "";
        }

        bool complete = false;
        continueIndicator.SetActive(false);
        int index = 0;

        string startTag = string.Empty;
        string endTag = string.Empty;

        if (currentIndex == 0 && justOpened)
        {
            justOpened = false;
            yield return new WaitForSeconds(0.35f);
        }

        while (!complete)
        {
            char c = text[index];

            // Speaking sound
            if (c != ' ' && currentLine.speakingSound != null)
            {
                SoundManager.instance.PlaySound(currentLine.speakingSound);
            }

            //Rich Text
            if (c == '<')
            {
                if (string.IsNullOrEmpty(startTag))
                {
                    int indexNow = index;

                    for (int j = indexNow; j < text.Length; j += 1)
                    {
                        startTag += text[j].ToString();

                        if (text[j] == '>')
                        {
                            indexNow = j + 1;

                            index = indexNow;

                            for (int k = indexNow; k < text.Length; k++)
                            {
                                char next = text[k];

                                if (next == '<')
                                {
                                    break;
                                }
                                else
                                {
                                    indexNow += 1;
                                }
                            }
                            break;
                        }
                    }

                    for (int j = indexNow; j < text.Length; j += 1)
                    {
                        endTag += text[j].ToString();

                        if (text[j] == '>')
                        {
                            break;
                        }
                    }
                }
                else
                {
                    for (int j = index; j < text.Length; j++)
                    {
                        if (text[j] == '>')
                        {
                            index = j + 1;
                            break;
                        }
                    }

                    startTag = string.Empty;
                    endTag = string.Empty;
                }

                continue;
            }

            speech.text += string.Format("{0}{1}{2}", startTag, c, endTag);

            index += 1;
            //yield return new WaitForSeconds(currentLine.timeBetweenLetters);
            yield return new WaitForSeconds(0.03f); // 0.04 took too long

            if (index == text.Length)
            {
                complete = true;
                continueIndicator.SetActive(true);
            }
        }

        typing = null;
    }
}
