using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class phoneController : MonoBehaviour
{

    public TMP_Text phone_battery;
    
    public GameObject Phone_Light;

    public int phone_battery_percent;

    public int battery_second;

    private bool isPhoneLight = false;

    public PlayerController Player;
    public GameObject hasPhone;

    public AudioClip clip;

    float count;
    private bool Phone = true;

    
    // Start is called before the first frame update
    void Start()
    {
        hasPhone.SetActive(false);
        //phone_battery.text = phone_battery_percent.ToString() + "%";
        count = battery_second;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.hasPhone)
        {
            PhoneLight();
            if (Phone)
            {
                phone_battery.text = phone_battery_percent.ToString() + "%";
                hasPhone.SetActive(true);
                Phone = false;
            }
        }

    }

    private void PhoneLight()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (phone_battery_percent > 0)
            {
                if (!isPhoneLight)
                {
                    SoundManager.instance.SFXPlay("PhoneLighton", clip, false);
                    isPhoneLight = true;
                }
                else if (isPhoneLight)
                {
                    SoundManager.instance.SFXPlay("PhoneLightonoff", clip, false);
                    isPhoneLight = false;
                }
            }
            else
                isPhoneLight = false;
        }

        /*if (isPhoneLight)
        {
            if (phone_battery_percent > 0)
            {
                count -= Time.deltaTime; // 시간 감소
                if (count <= 0)
                {
                    phone_battery_percent -= 1; // 1퍼센트 감소
                    phone_battery.text = phone_battery_percent.ToString() + "%";
                    count = battery_second;
                }
            }
            else
            {
                isPhoneLight = false;
                Phone_Light.SetActive(false);
            }
        }*/

        Phone_Light.SetActive(isPhoneLight);
    }
}
