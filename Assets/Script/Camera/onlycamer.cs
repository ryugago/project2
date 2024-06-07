using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onlycamer : MonoBehaviour
{
    private GameObject maincamer;

    // Start is called before the first frame update
    void Start()
    {
        //if (mainCamera != null)
        {
            //mainCamera.transform.rotation = Quaternion.Euler(0f, 0f, 0f);

            // Player �±׸� ���� ������Ʈ�� ã�Ƽ� PlayerController ��ũ��Ʈ�� ����
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            maincamer = GameObject.FindGameObjectWithTag("MainCamera");
            if (player != null && maincamer != null) 
            {
                maincamer.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                PlayerController playerController = player.GetComponent<PlayerController>();
                if (playerController != null)
                {
                    // currentCameraRotationX ���� 0���� ����
                    playerController.SetCameraRotationX(0f);
                }
                else
                {
                    Debug.LogError("PlayerController ��ũ��Ʈ�� ã�� �� �����ϴ�!");
                }
            }
            else
            {
                Debug.LogError("Player�� ã�� �� �����ϴ�!");
            }
        }
    }
    
}
