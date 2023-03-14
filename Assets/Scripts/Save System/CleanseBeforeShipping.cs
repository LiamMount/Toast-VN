using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanseBeforeShipping : MonoBehaviour
{
    void Start()
    {
        SavesManager.instance.DeleteSaveData();
    }

    void Update()
    {
        
    }
}
