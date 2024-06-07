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

            // Player 태그를 가진 오브젝트를 찾아서 PlayerController 스크립트에 접근
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            maincamer = GameObject.FindGameObjectWithTag("MainCamera");
            if (player != null && maincamer != null) 
            {
                maincamer.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                PlayerController playerController = player.GetComponent<PlayerController>();
                if (playerController != null)
                {
                    // currentCameraRotationX 값을 0으로 설정
                    playerController.SetCameraRotationX(0f);
                }
                else
                {
                    Debug.LogError("PlayerController 스크립트를 찾을 수 없습니다!");
                }
            }
            else
            {
                Debug.LogError("Player를 찾을 수 없습니다!");
            }
        }
    }
    
}
