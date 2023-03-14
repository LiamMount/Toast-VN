using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManagerDayOne : LevelManager
{
    public int progressionCounter = 1; // I numbered things wrong in the files, so I am using 1 instead of 0

    public Conversation convo1;
    public Conversation convo2;
    public Conversation convo3;
    public Conversation convo4;
    public Conversation convo5;
    public Conversation convo6;
    public Conversation convo7;
    public Conversation convo8;

    // Music will be done through this script
    public AudioClip musicDay2;

    void Start()
    {
        progressionCounter = 1;

        // Start the first convo here
        // Also fade in
        DialogueManager.StartConversation(convo1);
        FadeScreen.FadeIn();
    }

    void Update()
    {
        
    }

    public override void AdvanceStory() // This feels wrong for some reason, but I don't know why
    {
        progressionCounter += 1;

        // The rest of the logic
        switch (progressionCounter)
        {
            case 1: // Should never be called when 1, but here it is anyway
                StartCoroutine(NextConvo(convo1, 3f));
                break;
            case 2:
                StartCoroutine(NextConvo(convo2, 3f));
                break;
            case 3:
                StartCoroutine(NextConvo(convo3, 3f));
                break;
            case 4:
                StartCoroutine(NextConvo(convo4, 3f));
                break;
            case 5:
                StartCoroutine(NextConvo(convo5, 3f));
                break;
            case 6:
                StartCoroutine(NextConvo(convo6, 3f));
                break;
            case 7:
                StartCoroutine(NextConvo(convo7, 3f));
                break;
            case 8:
                StartCoroutine(NextConvo(convo8, 3f));
                break;
            case 9: // Go to next day
                FadeScreen.FadeOut();
                SavesManager.instance.activeSave.dayOneDone = true;
                SavesManager.instance.Save();
                SoundManager.instance.FadeMusic(musicDay2);
                Invoke("LoadDay2", 3f);
                break;
            default:
                //StartCoroutine(NextConvo(convo1, 3f));
                break;
        }
    }

    public void LoadDay2()
    {
        SceneManager.LoadScene("DayTwo");
    }
}
