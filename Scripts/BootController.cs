using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BootController : MonoBehaviour
{
    void Awake()
    {
        Debug.Log("Awake");
        SceneManager.LoadScene("Expenses", LoadSceneMode.Single);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
}
