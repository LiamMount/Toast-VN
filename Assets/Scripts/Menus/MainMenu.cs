using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private bool onCreditsMenu = false;

    // Music goes here
    public AudioClip musicIntro;
    public AudioClip musicMain;
    public AudioClip musicDay1;
    public AudioClip musicDay2;

    // Main Menu
    public GameObject mainMenu;
    public GameObject mainMenuSelector;
    private int[] mainMenuXY = { 0, 0 }; // Co-ordinates note: X is left to right, Y is bottom to top

    public GameObject playDay1Button; // Pos 0, 0
    public GameObject playDay2Button; // Pos 0, 1
    public GameObject creditsButton; // Pos 0, 2
    public GameObject quitButton; // Pos 0, 3

    public GameObject cornIndicator;

    // Credits menu
    public GameObject creditsMenu;
    public GameObject creditsMenuSelector;
    private int[] creditsMenuXY = { 0, 0 }; // Co-ordinates note: X is left to right, Y is top to bottom

    public GameObject metriusDiscordButton; // Pos 0, 0
    public GameObject lukeTwitchButton; // Pos 1, 0
    public GameObject colinTwitchButton; // Pos 2, 0
    public GameObject colinYouTubeButton; // Pos 2, 1
    public GameObject timTwitchButton; // Pos 3, 0
    public GameObject timYouTubeButton; // Pos 3, 1
    public GameObject myTwitchButton; // Pos 4, 0
    public GameObject myYouTubeButton; // Pos 4, 1

    private float creditsMenuBuffer = 0.25f;

    void Start()
    {
        mainMenuSelector.transform.position = playDay1Button.transform.position;

        //creditsMenuBuffer = 0.25f;
        //onCreditsMenu = false;

        if (SavesManager.instance.activeSave.dayOneDone)
        {
            playDay2Button.SetActive(true);
        }
        else
        {
            playDay2Button.SetActive(false);
        }
        if (SavesManager.instance.activeSave.endingWhite && SavesManager.instance.activeSave.endingBaige && SavesManager.instance.activeSave.endingPumper &&
                    SavesManager.instance.activeSave.endingCroissant && SavesManager.instance.activeSave.endingMold)
        {
            cornIndicator.SetActive(true);
        }
        else
        {
            cornIndicator.SetActive(false);
        }

        // Start music
        SoundManager.instance.musicSource.Stop();
        SoundManager.instance.musicSource.loop = true;
        SoundManager.instance.musicSource.volume = 1f;
        SoundManager.instance.musicSource.PlayOneShot(musicIntro);
        SoundManager.instance.musicSource.clip = musicMain;
        SoundManager.instance.musicSource.PlayScheduled(AudioSettings.dspTime + musicIntro.length);
    }

    void Update()
    {
        MainMenuUINav();
        if (creditsMenuBuffer <= 0)
        {
            CreditsMenuUINav();
        }
        else if (onCreditsMenu)
        {
            creditsMenuBuffer -= Time.deltaTime;
        }
    }

    public void MainMenuUINav()
    {
        if (!onCreditsMenu && Input.GetKeyDown(KeyCode.UpArrow))
        {
            switch (mainMenuXY[1])
            {
                case 0: // Day 1 up to Quit
                    mainMenuSelector.transform.position = quitButton.transform.position;
                    mainMenuXY[1] = 3;
                    break;
                case 1: // Day 2 up to Day 1
                    mainMenuSelector.transform.position = playDay1Button.transform.position;
                    mainMenuXY[1] = 0;
                    break;
                case 2: // Credits up to Day 2 or Day 1
                    if (SavesManager.instance.activeSave.dayOneDone)
                    {
                        mainMenuSelector.transform.position = playDay2Button.transform.position;
                        mainMenuXY[1] = 1;
                    }
                    else
                    {
                        mainMenuSelector.transform.position = playDay1Button.transform.position;
                        mainMenuXY[1] = 0;
                    }
                    break;
                case 3: // Quit up to Credits
                    mainMenuSelector.transform.position = creditsButton.transform.position;
                    mainMenuXY[1] = 2;
                    break;
                default:
                    break;
            }
        }
        else if (!onCreditsMenu && Input.GetKeyDown(KeyCode.DownArrow))
        {
            switch (mainMenuXY[1])
            {
                case 0: // Day 1 down to Day 2 or Credits
                    if (SavesManager.instance.activeSave.dayOneDone)
                    {
                        mainMenuSelector.transform.position = playDay2Button.transform.position;
                        mainMenuXY[1] = 1;
                    }
                    else
                    {
                        mainMenuSelector.transform.position = creditsButton.transform.position;
                        mainMenuXY[1] = 2;
                    }
                    break;
                case 1: // Day 2 down to Credits
                    mainMenuSelector.transform.position = creditsButton.transform.position;
                    mainMenuXY[1] = 2;
                    break;
                case 2: // Credits down to Quit
                    mainMenuSelector.transform.position = quitButton.transform.position;
                    mainMenuXY[1] = 3;
                    break;
                case 3: // Quit down to Day 1
                    mainMenuSelector.transform.position = playDay1Button.transform.position;
                    mainMenuXY[1] = 0;
                    break;
                default:
                    break;
            }
        }
        else if (!onCreditsMenu && Input.GetKeyDown(KeyCode.Z))
        {
            switch (mainMenuXY[1])
            {
                case 0: // Day 1
                    SoundManager.instance.FadeMusic(musicDay1);
                    SceneManager.LoadScene("DayOne");
                    break;
                case 1: // Day 2
                    SoundManager.instance.FadeMusic(musicDay2);
                    SceneManager.LoadScene("DayTwo");
                    break;
                case 2: // Credits
                    mainMenu.SetActive(false);
                    creditsMenu.SetActive(true);
                    creditsMenuSelector.transform.position = metriusDiscordButton.transform.position;
                    creditsMenuXY[0] = 0;
                    creditsMenuXY[1] = 0;
                    onCreditsMenu = true;
                    break;
                case 3: // Quit
                    Application.Quit();
                    Debug.Log("Quit");
                    break;
                default:
                    break;
            }
        }
    }

    public void CreditsMenuUINav()
    {
        if (onCreditsMenu && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow)))
        {
            switch (creditsMenuXY[0], creditsMenuXY[1])
            {
                case (0, 0): // Nothing
                    break;
                case (1, 0): // Nothing
                    break;
                case (2, 0): // C Twitch to C YouTube
                    creditsMenuSelector.transform.position = colinYouTubeButton.transform.position;
                    creditsMenuXY[1] = 1;
                    break;
                case (2, 1): // C YouTube to C Twitch
                    creditsMenuSelector.transform.position = colinTwitchButton.transform.position;
                    creditsMenuXY[1] = 0;
                    break;
                case (3, 0): // T Twitch to T YouTube
                    creditsMenuSelector.transform.position = timYouTubeButton.transform.position;
                    creditsMenuXY[1] = 1;
                    break;
                case (3, 1): // T YouTube to T Twitch
                    creditsMenuSelector.transform.position = timTwitchButton.transform.position;
                    creditsMenuXY[1] = 0;
                    break;
                case (4, 0): // Liam Twitch to Liam YouTube
                    creditsMenuSelector.transform.position = myYouTubeButton.transform.position;
                    creditsMenuXY[1] = 1;
                    break;
                case (4, 1): // Liam YouTube to Liam Twitch
                    creditsMenuSelector.transform.position = myTwitchButton.transform.position;
                    creditsMenuXY[1] = 0;
                    break;
                default:
                    break;
            }
        }
        else if (onCreditsMenu && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            switch (creditsMenuXY[0], creditsMenuXY[1])
            {
                case (0, 0): // M Discord to Liam Twitch
                    creditsMenuSelector.transform.position = myTwitchButton.transform.position;
                    creditsMenuXY[0] = 4;
                    break;
                case (1, 0): // Luke Twitch to M Discord
                    creditsMenuSelector.transform.position = metriusDiscordButton.transform.position;
                    creditsMenuXY[0] = 0;
                    break;
                case (2, 0): // C Twitch to Luke Twitch
                    creditsMenuSelector.transform.position = lukeTwitchButton.transform.position;
                    creditsMenuXY[0] = 1;
                    break;
                case (2, 1): // C YouTube to Luke Twitch
                    creditsMenuSelector.transform.position = lukeTwitchButton.transform.position;
                    creditsMenuXY[0] = 1;
                    creditsMenuXY[1] = 0;
                    break;
                case (3, 0): // T Twitch to C Twitch
                    creditsMenuSelector.transform.position = colinTwitchButton.transform.position;
                    creditsMenuXY[0] = 2;
                    break;
                case (3, 1): // T YouTube to C YouTube
                    creditsMenuSelector.transform.position = colinYouTubeButton.transform.position;
                    creditsMenuXY[0] = 2;
                    break;
                case (4, 0): // Liam Twitch to T Twitch
                    creditsMenuSelector.transform.position = timTwitchButton.transform.position;
                    creditsMenuXY[0] = 3;
                    break;
                case (4, 1): // Liam YouTube to T YouTube
                    creditsMenuSelector.transform.position = timYouTubeButton.transform.position;
                    creditsMenuXY[0] = 3;
                    break;
                default:
                    break;
            }
        }
        else if (onCreditsMenu && Input.GetKeyDown(KeyCode.RightArrow))
        {
            switch (creditsMenuXY[0], creditsMenuXY[1])
            {
                case (0, 0): // M Discord to Luke Twitch
                    creditsMenuSelector.transform.position = lukeTwitchButton.transform.position;
                    creditsMenuXY[0] = 1;
                    break;
                case (1, 0): // Luke Twitch to C Twitch
                    creditsMenuSelector.transform.position = colinTwitchButton.transform.position;
                    creditsMenuXY[0] = 2;
                    break;
                case (2, 0): // C Twitch to T Twitch
                    creditsMenuSelector.transform.position = timTwitchButton.transform.position;
                    creditsMenuXY[0] = 3;
                    break;
                case (2, 1): // C YouTube to T YouTube
                    creditsMenuSelector.transform.position = timYouTubeButton.transform.position;
                    creditsMenuXY[0] = 3;
                    break;
                case (3, 0): // T Twitch to Liam Twitch
                    creditsMenuSelector.transform.position = myTwitchButton.transform.position;
                    creditsMenuXY[0] = 4;
                    break;
                case (3, 1): // T YouTube to Liam YouTube
                    creditsMenuSelector.transform.position = myYouTubeButton.transform.position;
                    creditsMenuXY[0] = 4;
                    break;
                case (4, 0): // Liam Twitch to M Discord
                    creditsMenuSelector.transform.position = metriusDiscordButton.transform.position;
                    creditsMenuXY[0] = 0;
                    break;
                case (4, 1): // Liam YouTube to M Discord
                    creditsMenuSelector.transform.position = metriusDiscordButton.transform.position;
                    creditsMenuXY[0] = 0;
                    creditsMenuXY[1] = 0;
                    break;
                default:
                    break;
            }
        }
        else if (onCreditsMenu && Input.GetKeyDown(KeyCode.Z))
        {
            creditsMenuSelector.GetComponent<Animator>().SetTrigger("Select");

            switch (creditsMenuXY[0], creditsMenuXY[1])
            {
                case (0, 0): // M Discord link
                    //Application.OpenURL("Discordapp.com/users/338524547391160322");
                    Application.OpenURL("https://discord.com/channels/@me/Metrius%20the%20bot#3696/");
                    break;
                case (1, 0): // Luke Twitch link
                    Application.OpenURL("https://www.twitch.tv/luckyboy527");
                    break;
                case (2, 0): // C Twitch link
                    Application.OpenURL("https://www.twitch.tv/owouch");
                    break;
                case (2, 1): // C YouTube link
                    Application.OpenURL("https://www.youtube.com/@owouchttv");
                    break;
                case (3, 0): // T Twitch link
                    Application.OpenURL("https://www.twitch.tv/h4tz_tv");
                    break;
                case (3, 1): // T YouTube link
                    Application.OpenURL("https://www.youtube.com/@HatStackMusic");
                    break;
                case (4, 0): // Liam Twitch link
                    Application.OpenURL("https://discord.gg/73SU3s3rWg");
                    break;
                case (4, 1): // Liam YouTube link
                    Application.OpenURL("https://www.youtube.com/channel/UC3Upr6sLnFXpiJcHSiUO9-A");
                    break;
                default:
                    break;
            }
        }
        else if (onCreditsMenu && Input.GetKeyDown(KeyCode.X))
        {
            creditsMenuBuffer = 0.25f;
            creditsMenu.SetActive(false);
            mainMenu.SetActive(true);
            onCreditsMenu = false;
        }
    }
}
