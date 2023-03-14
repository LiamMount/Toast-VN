using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManagerDayThree : LevelManager
{
    public int progressionCounter = 1; // I numbered things wrong in the files, so I am using 1 instead of 0

    // 1
    public Conversation day3Start;

    // 2
    public Conversation day3Restaurant;
    public Conversation day3Park;
    public Conversation day3Fields;
    public Conversation day3Lawn;
    public Conversation day3Bridge;

    // 3
    public Conversation day3ToastingWhite;
    public Conversation day3ToastingBaige;
    public Conversation day3ToastingPumper;
    public Conversation day3ToastingCroissant;
    public Conversation day3ToastingMold;

    void Start()
    {
        progressionCounter = 1;

        // Start the first convo here
        // Also fade in
        DialogueManager.StartConversation(day3Start);
        FadeScreen.FadeIn();
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
                StartCoroutine(NextConvo(day3Start, 3f));
                break;
            case 2:
                foreach (SavesManager.ChoiceSet choice in SavesManager.instance.activeSave.choiceList)
                {
                    if (choice.choiceKey == "ParkOffice")
                    {
                        if (choice.choiceResult == "Park")
                        {
                            StartCoroutine(NextConvo(day3Restaurant, 3f)); // White
                            //DialogueManager.StartConversation(day3Restaurant); // White
                        }
                        if (choice.choiceResult == "Office")
                        {
                            StartCoroutine(NextConvo(day3Park, 3f)); // Baige
                            //DialogueManager.StartConversation(day3Park); // Baige
                        }
                    }
                    else if (choice.choiceKey == "GasOutside")
                    {
                        if (choice.choiceResult == "Gas")
                        {
                            StartCoroutine(NextConvo(day3Fields, 3f)); // Pumper
                            //DialogueManager.StartConversation(day3Fields); // Pumper
                        }
                        if (choice.choiceResult == "Outside")
                        {
                            StartCoroutine(NextConvo(day3Lawn, 3f)); // Croissant
                            //DialogueManager.StartConversation(day3Lawn); // Croissant
                        }
                    }
                    else if (choice.choiceKey == "BridgeCorn")
                    {
                        if (choice.choiceResult == "Bridge")
                        {
                            StartCoroutine(NextConvo(day3Bridge, 3f)); // Mold
                            //DialogueManager.StartConversation(day3Bridge); // Mold
                        }
                        if (choice.choiceResult == "Corn")
                        {
                            // Nothing
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
                            StartCoroutine(NextConvo(day3ToastingWhite, 3f)); // White
                            //DialogueManager.StartConversation(day3ToastingWhite); // White
                        }
                        if (choice.choiceResult == "Office")
                        {
                            StartCoroutine(NextConvo(day3ToastingBaige, 3f)); // Baige
                            //DialogueManager.StartConversation(day3ToastingBaige); // Baige
                        }
                    }
                    else if (choice.choiceKey == "GasOutside")
                    {
                        if (choice.choiceResult == "Gas")
                        {
                            StartCoroutine(NextConvo(day3ToastingPumper, 3f)); // Pumper
                            //DialogueManager.StartConversation(day3ToastingPumper); // Pumper
                        }
                        if (choice.choiceResult == "Outside")
                        {
                            StartCoroutine(NextConvo(day3ToastingCroissant, 3f)); // Croissant
                            //DialogueManager.StartConversation(day3ToastingCroissant); // Croissant
                        }
                    }
                    else if (choice.choiceKey == "BridgeCorn")
                    {
                        if (choice.choiceResult == "Bridge")
                        {
                            StartCoroutine(NextConvo(day3ToastingMold, 3f)); // Mold
                            //DialogueManager.StartConversation(day3ToastingMold); // Mold
                        }
                        if (choice.choiceResult == "Corn")
                        {
                            // Nothing
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
                            FadeScreen.FadeOut();
                            SavesManager.instance.Save();
                            Invoke("LoadEnding", 3f);
                        }
                        if (choice.choiceResult == "Office")
                        {
                            FadeScreen.FadeOut();
                            SavesManager.instance.Save();
                            Invoke("LoadEnding", 3f);
                        }
                    }
                    else if (choice.choiceKey == "GasOutside")
                    {
                        if (choice.choiceResult == "Gas")
                        {
                            FadeScreen.FadeOut();
                            SavesManager.instance.Save();
                            Invoke("LoadEnding", 3f);
                        }
                        if (choice.choiceResult == "Outside")
                        {
                            FadeScreen.FadeOut();
                            SavesManager.instance.Save();
                            Invoke("LoadEnding", 3f);
                        }
                    }
                    else if (choice.choiceKey == "BridgeCorn")
                    {
                        if (choice.choiceResult == "Bridge")
                        {
                            FadeScreen.FadeOut();
                            SavesManager.instance.Save();
                            Invoke("LoadEnding", 3f);
                        }
                        if (choice.choiceResult == "Corn")
                        {
                            // Nothing
                        }
                    }
                }
                break;
            default:
                //StartCoroutine(NextConvo(day3Start, 3f));
                break;
        }
    }

    public void LoadEnding()
    {
        SceneManager.LoadScene("Ending");
    }
}
