using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefreshButton
{
     [RuntimeInitializeOnLoadMethod]
     static void _InitializeOnLoad()
    {
        Debug.Log("Initialize On Load");
    }
}
