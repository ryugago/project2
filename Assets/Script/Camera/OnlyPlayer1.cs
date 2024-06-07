using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlyPlayer1 : MonoBehaviour
{
    private GameObject player;
    public Quaternion Protation;

    public bool position=false;
    public Vector3 Pposition;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.rotation = Protation;
        }
        if (position)
        {
            player.transform.position = Pposition;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
