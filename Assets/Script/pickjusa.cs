using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickjusa : MonoBehaviour
{
    public Train_hallucination jusa;

    public TMPro.TMP_Text viewPoint;
    public GameObject pickup;
    //bool targeton = false;

    GameObject nearObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (nearObject)
        {
            if(nearObject.tag == "ViewPoint")
            {
                pickup.SetActive(true);
                //viewPoint.text = "ащ╠Б";
                if (Input.GetKeyDown(KeyCode.E))
                {
                    jusa.pick_jusa = true;
                    pickup.SetActive(false);
                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ViewPoint")
        {
            nearObject = other.gameObject;

        }
        else if(other.tag != null)
        {
            nearObject = null;
        }
    }
}
