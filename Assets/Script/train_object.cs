using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class train_object : MonoBehaviour
{
    
    public GameObject objpanel;
    public GameObject[] obj;
    public change_obj[] close;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            foreach (change_obj i in close)
            {
                i.exit();
            }
            objpanel.SetActive(false);
            foreach (GameObject o in obj)
            {
                o.SetActive(false);
            }
        }
    }
}
