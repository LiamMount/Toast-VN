using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManagerDayTwo : LevelManager
{
    public int progressionCounter = 1; // I numbered things wrong in the files, so I am using 1 instead of 0


    // 1
    public Conversation day2Start;
    public Conversation day2StartSecret; // Make this one have acces to the corn one

    // 2
    public Conversation day2Park;

    public Conversation day2Office;

    public Conversation day2Gas;

    public Conversation day2Outside;

    public Conversation day2UnderBridge;

    public Conversation day2Cornfield;

    // 3
    public Conversation day2ParkGoose;

    public Conversation day2OfficeCoffee;

    public Conversation day2BreadBowl;

    public Conversation day2OutsideAgain;

    // 3 || 4
    public Conversation day2End;

    // Music
    public AudioClip musicDay3;

    void Start()
    {
        progressionCounter = 1;

        // Start the first convo here
        // Also fade in
        if (SavesManager.instance.activeSave.endingWhite && SavesManager.instance.activeSave.endingBaige && SavesManager.instance.activeSave.endingPumper &&
                    SavesManager.instance.activeSave.endingCroissant && SavesManager.instance.activeSave.endingMold)
        {
            DialogueManager.StartConversation(day2StartSecret);
        }
        else
        {
            DialogueManager.StartConversation(day2Start);
        }

        FadeScreen.FadeIn();

        SavesManager.instance.activeSave.choiceList.Clear();
    }

    void Update()
    {
        
    }

    public override void AdvanceStory()
    {
        progressionCounter += 1;

        switch (progressionCounter)
        {
            case 1: // Should never be called when 1, but here it is anyway
                StartCoroutine(NextConvo(day2Start, 3f));
                break;
            case 2:
                foreach (SavesManager.ChoiceSet choice in SavesManager.instance.activeSave.choiceList)
                {
                    if (choice.choiceKey == "ParkOffice")
                    {
                        if (choice.choiceResult == "Park")
                        {
                            StartCoroutine(NextConvo(day2Park, 3f));
                            //DialogueManager.StartConversation(day2Park);
                        }
                        if (choice.choiceResult == "Office")
                        {
                            StartCoroutine(NextConvo(day2Office, 3f));
                            //DialogueManager.StartConversation(day2Office);
                        }
                    }
                    else if (choice.choiceKey == "GasOutside")
                    {
                        if (choice.choiceResult == "Gas")
                        {
                            StartCoroutine(NextConvo(day2Gas, 3f));
                            //DialogueManager.StartConversation(day2Gas);
                        }
                        if (choice.choiceResult == "Outside")
                        {
                            StartCoroutine(NextConvo(day2Outside, 3f));
                            //DialogueManager.StartConversation(day2Outside);
                        }
                    }
                    else if (choice.choiceKey == "BridgeCorn")
                    {
                        if (choice.choiceResult == "Bridge")
                        {
                            StartCoroutine(NextConvo(day2UnderBridge, 3f));
                            //DialogueManager.StartConversation(day2UnderBridge);
                        }
                        if (choice.choiceResult == "Corn")
                        {
                            StartCoroutine(NextConvo(day2Cornfield, 3f));
                            //DialogueManager.StartConversation(day2Cornfield);
                        }
                    }
                }
                break;
            case 3:
                foreach (SavesManager.ChoiceSet choice in SavesManager.instance.activeSave.choiceList)
                {
                    if (choice.choiceKey == "ParkOffice")
                    {
                        if (choice.choiceResult == "Park")
                        {
                            StartCoroutine(NextConvo(day2ParkGoose, 3f));
                            //DialogueManager.StartConversation(day2ParkGoose);
                        }
                        if (choice.choiceResult == "Office")
                        {
                            StartCoroutine(NextConvo(day2OfficeCoffee, 3f));
                            //DialogueManager.StartConversation(day2OfficeCoffee);
                        }
                    }
                    else if (choice.choiceKey == "GasOutside")
                    {
                        if (choice.choiceResult == "Gas")
                        {
                            StartCoroutine(NextConvo(day2BreadBowl, 3f));
                            //DialogueManager.StartConversation(day2BreadBowl);
                        }
                        if (choice.choiceResult == "Outside")
                        {
                            StartCoroutine(NextConvo(day2OutsideAgain, 3f));
                            //DialogueManager.StartConversation(day2OutsideAgain);
                        }
                    }
                    else if (choice.choiceKey == "BridgeCorn")
                    {
                        if (choice.choiceResult == "Bridge")
                        {
                            StartCoroutine(NextConvo(day2End, 3f));
                            //DialogueManager.StartConversation(day2End);
                        }
                        if (choice.choiceResult == "Corn")
                        {
                            FadeScreen.FadeOut();
                            SavesManager.instance.Save();
                            Invoke("LoadMainMenu", 2f);
                            //DialogueManager.StartConversation(day2End);
                        }
                    }
                }
                break;
            case 4:
                foreach (SavesManager.ChoiceSet choice in SavesManager.instance.activeSave.choiceList)
                {
                    if (choice.choiceKey == "ParkOffice")
                    {
                        if (choice.choiceResult == "Park")
                        {
                            StartCoroutine(NextConvo(day2End, 3f));
                            //DialogueManager.StartConversation(day2End);
                        }
                        if (choice.choiceResult == "Office")
                        {
                            StartCoroutine(NextConvo(day2End, 3f));
                            //DialogueManager.StartConversation(day2End);
                        }
                    }
                    else if (choice.choiceKey == "GasOutside")
                    {
                        if (choice.choiceResult == "Gas")
                        {
                            StartCoroutine(NextConvo(day2End, 3f));
                            //DialogueManager.StartConversation(day2End);
                        }
                        if (choice.choiceResult == "Outside")
                        {
                            StartCoroutine(NextConvo(day2End, 3f));
                            //DialogueManager.StartConversation(day2End);
                        }
                    }
                    else if (choice.choiceKey == "BridgeCorn")
                    {
                        if (choice.choiceResult == "Bridge")
                        {
                            FadeScreen.FadeOut();
                            SavesManager.instance.Save();
                            SoundManager.instance.FadeMusic(musicDay3);
                            Invoke("LoadDay3", 3f);
                        }
                        if (choice.choiceResult == "Corn")
                        {
                            // Nothing
                        }
                    }
                }
                break;
            case 5:
                foreach (SavesManager.ChoiceSet choice in SavesManager.instance.activeSave.choiceList)
                {
                    if (choice.choiceKey == "ParkOffice")
                    {
                        if (choice.choiceResult == "Park")
                        {
                            FadeScreen.FadeOut();
                            SavesManager.instance.Save();
                            SoundManager.instance.FadeMusic(musicDay3);
                            Invoke("LoadDay3", 3f);
                        }
                        if (choice.choiceResult == "Office")
                        {
                            FadeScreen.FadeOut();
                            SavesManager.instance.Save();
                            SoundManager.instance.FadeMusic(musicDay3);
                            Invoke("LoadDay3", 3f);
                        }
                    }
                    else if (choice.choiceKey == "GasOutside")
                    {
                        if (choice.choiceResult == "Gas")
                        {
                            FadeScreen.FadeOut();
                            SavesManager.instance.Save();
                            SoundManager.instance.FadeMusic(musicDay3);
                            Invoke("LoadDay3", 3f);
                        }
                        if (choice.choiceResult == "Outside")
                        {
                            FadeScreen.FadeOut();
                            SavesManager.instance.Save();
                            SoundManager.instance.FadeMusic(musicDay3);
                            Invoke("LoadDay3", 3f);
                        }
                    }
                    else if (choice.choiceKey == "BridgeCorn")
                    {
                        if (choice.choiceResult == "Bridge")
                        {
                            // Nothing
                        }
                        if (choice.choiceResult == "Corn")
                        {
                            // Nothing
                        }
                    }
                }
                break;
            default:
                //StartCoroutine(NextConvo(day2Start, 3f));
                break;
        }
    }

    public void LoadDay3()
    {
        SceneManager.LoadScene("DayThree");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
