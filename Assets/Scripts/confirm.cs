using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class confirm : MonoBehaviour
{
    public GameObject Menu;
    public GameObject TitleScreen;

    private bool J1Valide;
    private bool J2Valide;

    public AudioSource AS_Confirm;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteAll();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            StartCoroutine(OpenAccueil());
        }
    }

    IEnumerator OpenAccueil()
    {
        AS_Confirm.Play();
        Menu.SetActive(true);
        TitleScreen.SetActive(false);
        yield return (this);
    }
    
}
