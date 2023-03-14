using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    private float timer = 5f;

    public Image bgImage;
    public GameObject returnToMenuButton;
    private bool returningToMenu = false;

    public Sprite whiteEnding;
    public Sprite baigeEnding;
    public Sprite pumperEnding;
    public Sprite croissantEnding;
    public Sprite moldEnding;

    public AudioClip musicEnding;

    // Music would go here if we had any

    void Start()
    {
        returnToMenuButton.SetActive(false);
        returningToMenu = false;

        timer = 5f;

        foreach (SavesManager.ChoiceSet choice in SavesManager.instance.activeSave.choiceList)
        {
            if (choice.choiceKey == "ParkOffice")
            {
                if (choice.choiceResult == "Park")
                {
                    bgImage.sprite = whiteEnding;
                    SavesManager.instance.activeSave.endingWhite = true;
                }
                if (choice.choiceResult == "Office")
                {
                    bgImage.sprite = baigeEnding;
                    SavesManager.instance.activeSave.endingBaige = true;
                }
            }
            else if (choice.choiceKey == "GasOutside")
            {
                if (choice.choiceResult == "Gas")
                {
                    bgImage.sprite = pumperEnding;
                    SavesManager.instance.activeSave.endingPumper = true;
                }
                if (choice.choiceResult == "Outside")
                {
                    bgImage.sprite = croissantEnding;
                    SavesManager.instance.activeSave.endingCroissant = true;
                }
            }
            else if (choice.choiceKey == "BridgeCorn")
            {
                if (choice.choiceResult == "Bridge")
                {
                    bgImage.sprite = moldEnding;
                    SavesManager.instance.activeSave.endingMold = true;
                }
                if (choice.choiceResult == "Corn")
                {
                    // Nothing
                }
            }
        }

        SavesManager.instance.Save();

        FadeScreen.FadeIn();

        SoundManager.instance.FadeMusic(musicEnding);
        SoundManager.instance.musicSource.loop = false;
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                returnToMenuButton.SetActive(true);
            }
        }
        else if (timer < 0 && !returningToMenu)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                FadeScreen.FadeOut();
                returningToMenu = true;
                Invoke("LoadMainMenu", 3f);
            }
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
