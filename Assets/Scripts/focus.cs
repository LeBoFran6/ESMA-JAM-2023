using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class focus : MonoBehaviour
{
    [SerializeField]
    GameObject _focusable;

    private void OnEnable()
    {
        //_focusable.GetComponent<Button>().Focus();
        if(_focusable != null)
        {
            EventSystem.current.SetSelectedGameObject(_focusable);
        }
           
    }


    private void Update()
    {
        Debug.Log(EventSystem.current);
    }
}
