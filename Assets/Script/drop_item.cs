using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FronkonGames.SpiceUp.DoubleVision;

public class drop_item : MonoBehaviour
{
    public train_captain trap;
    public Animator anicamera;
    //public Animator trapani;
    public PlayerController player;
    public train_drop chat;

    public GameObject[] dropobj;

    private bool train_trap;

    public AudioClip clip;

    private void Start()
    {
        DoubleVision.AddRenderFeature();
        DoubleVision.Settings settings = DoubleVision.GetSettings();
        settings.strength.z = 0f;
        foreach (GameObject obj in dropobj)
        {
            obj.SetActive(false);
        }
    }

    private void Update()
    {
        if (!enabled)
            return;
        
        //if (train_trap)
        if (train_trap && player.hasPhone && trap.trap)
        {
            SoundManager.instance.SFXPlay("DropPlayersound", clip);
            DoubleVision.AddRenderFeature();
            DoubleVision.Settings settings = DoubleVision.GetSettings();
            settings.strength.z = 1f;
            chat.enabled = true;
            GameManager.canPlayerMove2 = false;
            anicamera.SetTrigger("trap");
            foreach(GameObject obj in dropobj)
            {
                obj.SetActive(true);
            }
            //trapani.SetTrigger("drop");
            enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //if (player.hasPhone)
            {

                train_trap = true;

            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        train_trap = false;
    }
}
