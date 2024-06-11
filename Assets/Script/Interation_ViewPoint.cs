using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interation_ViewPoint : MonoBehaviour
{

    public TextMeshProUGUI turnstiletext;
    public PlayerController playercontroll;
    public GameObject arm_phone;
    public Transform player;
    public Camera playercamera;

    public GameObject pickup;

    [Header("열쇠 찾음")]
    public TMP_Text quest;
    public findlever chatfindlever;
    [Header("애니메이션")]
    public Animator look_burn;
    GameObject nearObject;

    private bool trigger1 = true;

    // Update is called once per frame
    public void Interation()
    {
        //int weapon_Index = -1;

        if (Input.GetKeyDown(KeyCode.E) && nearObject != null)
        {
            /*if (nearObject.tag == "Weapon")
            {
                Item item = nearObject.GetComponent<Item>();
                int weaponIndex = item.value;
                hasWeapons[weaponIndex] = true;
                weapon_Index = 0;
                weapons[weapon_Index].SetActive(true);
                Destroy(nearObject);
            }*/

            if (nearObject.tag == "CardKey")
            {
                if (trigger1)
                {
                    trigger1 = false;
                    //quest.text = "기장실로 가보자";
                    Invoke("questde", 2f);
                }
                //StartCoroutine(ResetQuestText());
                Item item = nearObject.GetComponent<Item>();
                int keyIndex = item.value;
                playercontroll.hasKeys[keyIndex] = true;
                //weapon_Index = 0;
                // weapons[weapon_Index].SetActive(true);
                Destroy(nearObject);
            }
            if (nearObject.tag == "CardKey1")
            {
                chatfindlever.enabled = true;
                Item item = nearObject.GetComponent<Item>();
                int keyIndex = item.value;
                playercontroll.hasKeys[keyIndex] = true;
                //weapon_Index = 0;
                // weapons[weapon_Index].SetActive(true);
                GameManager.canPlayerMove2 = false;
                player.position = new Vector3(135.3309f, -1.885786f, 142.8212f);
                playercamera.transform.rotation = Quaternion.Euler(62.7f, playercamera.transform.
                    rotation.eulerAngles.y, playercamera.transform.rotation.eulerAngles.z);
                look_burn.SetTrigger("burn1");
                Destroy(nearObject);
            }
            if (nearObject.tag == "CardKey2")
            {
                Item item = nearObject.GetComponent<Item>();
                int keyIndex = item.value;
                playercontroll.hasKeys[keyIndex] = true;
                //weapon_Index = 0;
                // weapons[weapon_Index].SetActive(true);
                Destroy(nearObject);
            }
            if (nearObject.tag == "Letter")
            {
                Item item = nearObject.GetComponent<Item>();
                int letterIndex = item.value;
                playercontroll.hasLetter[letterIndex] = true;
                Destroy(nearObject);
                //Letter.SetActive(true);
                GameManager.isPause = true;
            }
            if (nearObject.tag == "Phone")
            {
                Item item = nearObject.GetComponent<Item>();
                playercontroll.hasPhone = true;
                arm_phone.SetActive(true);
                Destroy(nearObject);
                //Letter.SetActive(true);
            }

            pickup.SetActive(false);
            turnstiletext.text = " ";
        }

    }

    void questde()
    {
        quest.text = " ";
    }

    IEnumerator ResetQuestText()
    {
        // Wait for 3 seconds
        yield return new WaitForSeconds(2f);

        // Reset the quest text
        quest.text = " ";
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "CardKey")
        {
            nearObject = other.gameObject;
            pickup.SetActive(true);
        }
        if (other.tag == "CardKey1")
        {
            nearObject = other.gameObject;
            pickup.SetActive(true);
        }
        if (other.tag == "CardKey2")
        {
            nearObject = other.gameObject;
            pickup.SetActive(true);
        }
        if (other.tag == "Letter")
        {
            nearObject = other.gameObject;
            pickup.SetActive(true);
        }
        if (other.tag == "Phone")
        {
            nearObject = other.gameObject;
            pickup.SetActive(true);
        }


        // Debug.Log(nearObject.name);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "CardKey")
        {
            pickup.SetActive(false);
            nearObject = null;
        }
        if (other.tag == "CardKey1")
        {
            pickup.SetActive(false);
            nearObject = null;
        }
        if (other.tag == "CardKey2")
        {
            pickup.SetActive(false);
            nearObject = null;
        }
        if (other.tag == "Letter")
        {
            pickup.SetActive(false);
            nearObject = null;
        }
        if (other.tag == "Phone")
        {
            pickup.SetActive(false);
            nearObject = null;
        }

        pickup.SetActive(false);
        turnstiletext.text = " ";
    }


}
