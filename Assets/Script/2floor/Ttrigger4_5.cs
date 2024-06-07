using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ttrigger4_5 : MonoBehaviour
{
    private bool trigger1;
    private bool trigger2=true;
    private bool trigger3;


    public Light LStaffRoom;
    private float Lintensity;

    public GameObject doorhandle;
    // Start is called before the first frame update
    // Update is called once per frame

    private void Start()
    {
        Lintensity = LStaffRoom.intensity;
        LStaffRoom.intensity = 0;
        doorhandle.SetActive(false);
    }
    void Update()
    {
        if (trigger1 && trigger2)
        {
            trigger2 = false;
            LStaffRoom.intensity = Lintensity;
            doorhandle.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
