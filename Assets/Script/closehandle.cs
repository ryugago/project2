using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closehandle : MonoBehaviour
{


    public MeshRenderer doornob;
    public Material shader;

    public CapsuleCollider playerColl;

    private bool trigger1;
    private bool trigger2;

    private Material[] originalMaterials;
    // Start is called before the first frame update
    void Start()
    {
        originalMaterials = doornob.materials;
    }

    // Update is called once per frame
    void Update()
    {
        if (trigger1)
        {
            Material[] currentMaterials = doornob.materials;
            int currentLength = currentMaterials.Length;

            // 새로운 배열을 생성하고, 길이를 2로 만듭니다.
            Material[] newMaterials = new Material[currentLength + 1];

            // 기존 배열의 요소를 새 배열로 복사합니다.
            for (int i = 0; i < currentLength; i++)
            {
                newMaterials[i] = currentMaterials[i];
            }

            // 새로운 머테리얼을 배열의 마지막 요소로 추가합니다.
            newMaterials[currentLength] = shader;

            // 오브젝트의 머테리얼 배열을 새로 생성한 배열로 대체합니다.
            doornob.materials = newMaterials;
        }
        else if (!trigger1)
        {
            doornob.materials = originalMaterials;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            trigger1 = true;
            playerColl.radius = 0.2f;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            trigger1 = false;
            playerColl.radius = 0.4f;
        }
    }

}
