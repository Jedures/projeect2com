using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Static 
{
    public static int level
    {
        get => PlayerPrefs.GetInt("Level", 1);
        set => PlayerPrefs.SetInt("Level", value);
    }
}
