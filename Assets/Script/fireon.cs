using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireon : MonoBehaviour
{
    public GameObject cigarette;
    public GameObject onfire;

    public Animator cigarttestick;

    private void Update()
    {
        if (cigarttestick.GetCurrentAnimatorStateInfo(0).IsName("cigarette2"))
        {
            // Activate the cigarette if the 'cigarette2' animation is playing
            if (!cigarette.activeSelf)
            {
                cigarette.SetActive(true);
            }

            // Invoke the ActivateFire method after 2 seconds if not already invoked
            if (!IsInvoking("ActivateFire"))
            {
                Invoke("ActivateFire", 2f);
            }
        }
        else
        {
            onfire.SetActive(false);
        }
    }

    private void ActivateFire()
    {
        onfire.SetActive(true);
    }
}