using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class irondoor : MonoBehaviour
{

    private bool trigger1;
    private bool trigger2=true;
    public TMPro.TMP_Text intertxt;


    public GameObject openimg;

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
            openimg.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                SoundManager.instance.SFXPlay("ironlock", clip);
                intertxt.text = "문이 열리지 않습니다.";
                Invoke("lockdoor", 2f);
            }
        }
    }

    void lockdoor()
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
            openimg.SetActive(false);
        }
    }
}
