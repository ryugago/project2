using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ttrigger3_3 : MonoBehaviour
{
    public Light exLight;

    private bool trigger1 = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (trigger1)
        {
            trigger1 = false;
            StartCoroutine(Elight());
        }
    }

    IEnumerator Elight()
    {

        yield return new WaitForSeconds(1f);
        exLight.gameObject.SetActive(false);
    }
}
