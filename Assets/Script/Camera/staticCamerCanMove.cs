using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class staticCamerCanMove : MonoBehaviour
{
    public bool playercanmove = false;
    public bool playercantmove = false;
    // Update is called once per frame
    void Update()
    {
        if (playercanmove)
        {
            GameManager.canPlayerMove2 = true;
            playercanmove = false;
        }
        if (playercantmove)
        {
            GameManager.canPlayerMove2 = false;
            playercantmove = false;
        }
    }
}
