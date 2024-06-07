using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stroyesc : MonoBehaviour
{
    public PauseMenu phonemenu;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //phonemenu.CallMenu();
            gameObject.SetActive(false);
        }
    }
}
