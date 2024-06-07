using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brokentraindoor : MonoBehaviour
{
    public GameObject door1;
    public GameObject door2;

    public TrainDoor trigger;

    public GameObject autokey;

    public GameObject Player;
    public PlayerController Maincamera;
    public GameObject PMaincamera;

    public Quaternion Protation;
    public Vector3 Pposition;
    
    bool door_open = false;
    bool door_trigger = true;
    bool triggerbool = true;
    public int open = 0;

    public AudioClip trinopensound;
    private void Start()
    {
        PMaincamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void Update()
    {
        //if( door_open)
        if(trigger.trigger_open && door_open && door_trigger)
        {
            autokey.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                open++;
                if (triggerbool)
                {
                    triggerbool = false;
                    Maincamera.SetCameraRotationX(0);
                    PMaincamera.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    StartCoroutine(MovePlayerToPosition(Player.transform, Pposition, Protation, 0.2f));

                    GameManager.canPlayerMove2 = false;
                }
            }
            if(open >= 10)
            {
                SoundManager.instance.SFXPlay("Trinopensound", trinopensound);
                door_trigger = false;
                StartCoroutine(OpenDoors());
                GameManager.canPlayerMove2 = true;
                autokey.SetActive(false);
                open = -9;
            }
        }
    }

    IEnumerator MovePlayerToPosition(Transform playerTransform, Vector3 targetPosition, Quaternion targetRotation, float duration)
    {
        Vector3 startPosition = playerTransform.position;
        Quaternion startRotation = playerTransform.rotation;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            playerTransform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            playerTransform.rotation = Quaternion.Lerp(startRotation, targetRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        playerTransform.position = targetPosition;
        playerTransform.rotation = targetRotation;
    }

    IEnumerator OpenDoors()
    {
        float elapsedTime = 0f;
        float duration = 0.04f;

        // Initial positions
        Vector3 door1StartPos = door1.transform.localPosition;
        Vector3 door2StartPos = door2.transform.localPosition;

        // Target positions
        Vector3 door1TargetPos = new Vector3(-7.87f, door1StartPos.y, door1StartPos.z);
        Vector3 door2TargetPos = new Vector3(-6.65f, door2StartPos.y, door2StartPos.z);

        while (elapsedTime < duration)
        {
            // Interpolate positions
            door1.transform.localPosition = Vector3.Lerp(door1StartPos, door1TargetPos, elapsedTime / duration);
            door2.transform.localPosition = Vector3.Lerp(door2StartPos, door2TargetPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure final positions are set
        door1.transform.localPosition = door1TargetPos;
        door2.transform.localPosition = door2TargetPos;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ViewPoint")
        {
            door_open = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "ViewPoint")
        {
            autokey.SetActive(false);
            door_open = false;
        }
    }
}
