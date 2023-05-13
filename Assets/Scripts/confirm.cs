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
    
    /*public void recup()
    {
        
        switch (selecttrees.playerid)
        {
            case 0:
                choice = PlayerPrefs.GetInt("ArbreJ1");
                Debug.Log(choice);
                break;
            case 1:
                choice = PlayerPrefs.GetInt("ArbreJ2");
                Debug.Log(choice);
                break;
            case 2:
                choice = PlayerPrefs.GetInt("ArbreJ3");
                Debug.Log(choice);
                break;
            case 3:
                choice = PlayerPrefs.GetInt("ArbreJ4");
                Debug.Log(choice);
                break;
        }
    }*/

    /*public void valider()
    {
            leftArrow.SetActive(false);
            rightArrow.SetActive(false);
            switch (selecttrees.playerid)
            {
                case 0:
                    choice = PlayerPrefs.GetInt("ArbreJ1");
                    J1Valide = true;
                    Debug.Log(choice + " J1 valide");
                    PlayTreeSound();
                    break;
                case 1:
                    choice = PlayerPrefs.GetInt("ArbreJ2");
                    J2Valide = true;
                    Debug.Log(choice + " J2 valide");
                    PlayTreeSound();
                    break;
                case 2:
                    choice = PlayerPrefs.GetInt("ArbreJ3");
                    J3Valide = true;
                    Debug.Log(choice + " J3 valide");
                    PlayTreeSound();
                    break;
                case 3:
                    choice = PlayerPrefs.GetInt("ArbreJ4");
                    J4Valide = true;
                    Debug.Log(choice + " J4 valide");
                    PlayTreeSound();
                    break;
            }
            
    }*/

    public void CheckValidated()
    {

        CheckJ1Validated();
        CheckJ2Validated();
    }
    
    public void CheckJ1Validated()
    {
        if (!J1Valide)
        {
            PlayerPrefs.SetInt("ArbreJ1", 4);
        }
    }
    public void CheckJ2Validated()
    {
        if (!J2Valide)
        {
            PlayerPrefs.SetInt("ArbreJ2", 4);
        }
    }
}
