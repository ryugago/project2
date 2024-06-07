using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class delete_obj : MonoBehaviour
{
    public TrainDoor Trap;
    
    // Update is called once per frame
    void Update()
    {
        if (Trap.trap)
        {
            Destroy(gameObject);
        }
    }
}
