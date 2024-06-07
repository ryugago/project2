using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitPlayer : MonoBehaviour
{
    public float delete_obj;
    // Update is called once per frame
    void Start()
    {
        Destroy(gameObject, delete_obj);
    }
}
