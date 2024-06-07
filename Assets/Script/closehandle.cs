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

            // ���ο� �迭�� �����ϰ�, ���̸� 2�� ����ϴ�.
            Material[] newMaterials = new Material[currentLength + 1];

            // ���� �迭�� ��Ҹ� �� �迭�� �����մϴ�.
            for (int i = 0; i < currentLength; i++)
            {
                newMaterials[i] = currentMaterials[i];
            }

            // ���ο� ���׸����� �迭�� ������ ��ҷ� �߰��մϴ�.
            newMaterials[currentLength] = shader;

            // ������Ʈ�� ���׸��� �迭�� ���� ������ �迭�� ��ü�մϴ�.
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
