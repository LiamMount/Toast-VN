using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScreen : MonoBehaviour
{
    [HideInInspector]
    public static FadeScreen instance;

    public Animator anim;

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
    }

    void Update()
    {
        // We were testing with these
        //if (Input.GetKeyDown(KeyCode.O))
        //{
        //    instance.FadeOut();
        //}
        //if (Input.GetKeyDown(KeyCode.I))
        //{
        //    instance.FadeIn();
        //}
    }

    public static void FadeOut()
    {
        instance.anim.SetTrigger("FadeOut");
    }

    public static void FadeIn()
    {
        instance.anim.SetTrigger("FadeIn");
    }

    public void TriggerEndOfScene()
    {
        DialogueManager.instance.fadedOut = true;
    }
}
