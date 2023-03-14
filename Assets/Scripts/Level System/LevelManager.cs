using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    /*
     * These scripts manage the whole game.
     * 
     * They call and start different conversations based on past choice results,
     * and they likely do other things too (Write more descriptive description later)
     * 
     * Unfortunately, they will probably be huge freakin' monoliths. Oh well.
     */

    [HideInInspector]
    public static LevelManager instance;

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
        
    }

    public virtual void AdvanceStory()
    {
        // Put logic here
    }

    protected IEnumerator NextConvo(Conversation _convo, float _waitTime, bool fadesOutAuto = true)
    {
        if (fadesOutAuto)
        {
            FadeScreen.FadeOut();
        }
        yield return new WaitForSeconds(_waitTime);
        DialogueManager.StartConversation(_convo);
        FadeScreen.FadeIn();
    }
}
