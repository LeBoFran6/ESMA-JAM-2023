using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingInputs : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Scores.Instance.Testing();

        if (Input.GetAxis("Fire1")>0)
            Debug.Log(Input.GetAxis("Fire1"));

        if (Input.GetAxis("Fire12")>0)
            Debug.Log(Input.GetAxis("Fire12"));
    }
}
