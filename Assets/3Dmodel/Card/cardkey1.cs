using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cardkey1 : MonoBehaviour
{
    public cartrigger isAnima;
    public Animator Trap3;
    public GameObject customer_room_Light;
    public MeshDestroy[] Galss_Destroy;
    

    private void Update()
    {
        
        if (isAnima.istrigger)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Trap3.SetBool("open", false); // �ִϸ��̼��� Ʈ���Ÿ� �ߵ���Ŵ
                Invoke("ActivateTrap", 2f); // 2�� �Ŀ� ActivateTrap �޼ҵ� ȣ��

                for (int i = 0; i < Galss_Destroy.Length; i++)
                {
                    Galss_Destroy[i].DestroyMesh();
                }

                GameManager.is_Glass_Break = true;
                isAnima.istrigger = false;

            }
        }
    }

    

    void ActivateTrap()
    {
        customer_room_Light.SetActive(true);
    }
}
