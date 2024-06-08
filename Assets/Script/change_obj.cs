using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class change_obj : MonoBehaviour
{
    //public GameObject change;
    public TMP_Text interac_txt;
    public Train_hallucination hall;
    public train_captain cap;

    public GameObject obj;
    public GameObject image;
    public GameObject checkimage;

    public Animator objanimator;

    private bool ispick = false;
    GameObject nearObject;

    public string objtext = "";
    public float Wtime;

    bool check = true;
    //bool check2 = true;
    bool check3 = false;
    public bool displayText = true;

    private bool isenabled = true;

    public AudioClip clip;

    private void Update()
    {
        if (!isenabled)
            return;

        if (ispick)
        {
            checkimage.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (nearObject != null)
                {
                    if (nearObject.tag == "ViewPoint")
                    {
                        if (!(clip == null))
                            SoundManager.instance.SFXPlay("objsound", clip);
                        isenabled = false;
                        //GameManager.isPause = true;
                        //obj.SetActive(true);
                        //image.SetActive(true);
                        //check2 = false;
                        objanimator.SetTrigger("trigger");
                        check3 = true;
                        StartCoroutine(Tcheck(Wtime));
                        checkimage.SetActive(false);
                        hall.istrigger++;
                    }
                }
            }
        }
        
    }

    IEnumerator Tcheck(float time)
    {
        yield return new WaitForSeconds(time);
        //if(displayText)
            //interac_txt.text = objtext;
    }


    public void exit()
    {
        if (check3)
        {
            if (check)
            {
                hall.istrigger++;
                check = false;
            }
        }
        displayText = false;
        obj.SetActive(false);
        image.SetActive(false);
        //check2 = true;
        interac_txt.text = " ";
        GameManager.isPause = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ViewPoint")
        {
            //if (cap.trap)
            {
                nearObject = other.gameObject;
                ispick = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "ViewPoint")
        {
            nearObject = null;
            checkimage.SetActive(false);
            ispick = false;
        }
    }
}
