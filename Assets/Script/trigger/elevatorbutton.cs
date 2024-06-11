using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevatorbutton : MonoBehaviour
{
    private bool trigger1;
    //public bool trigger2;


    public GameObject Eimg;

    public TMPro.TMP_Text intertxt;

    public string elevatxt;

    public AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (trigger1)
        {
            Eimg.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                SoundManager.instance.SFXPlay("evebutton", clip);
                intertxt.text = elevatxt;
                Invoke("txtno", 2f);
            }
        }
    }

    void txtno()
    {
        intertxt.text = " ";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ViewPoint")
        {
            trigger1 = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "ViewPoint")
        {
            trigger1 = false;
            Eimg.SetActive(false);
        }
    }
}
