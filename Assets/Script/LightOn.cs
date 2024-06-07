using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOn : MonoBehaviour
{
    public Light Light;

    public bool Lightcontrol;
    
    private bool control=true;

    private void Start()
    {
        Light.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (control)
            {
                Debug.Log("��������� ����");
                Light.gameObject.SetActive(Lightcontrol);
                control = false;
            }
                
        }
    }

}
