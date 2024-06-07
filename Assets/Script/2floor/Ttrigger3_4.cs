using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ttrigger3_4 : MonoBehaviour
{
    //public Light exLight;

    private bool trigger1;
    private bool trigger2 = true;

    public Ttrigger3 trigger;
    public Animator mop;
    public MeshRenderer doornob;
    public Material shader;

    public Collider doornobcollider;
    // Update is called once per frame
    void Update()
    {
        if (trigger1&& trigger2)
        {
            //trigger.trigger
            trigger2 = false;
            mop.SetTrigger("move");
            StartCoroutine(anidoor());
        }
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (trigger.trigger)
        {
            if (other.tag == "Player")
            {
                trigger1 = true;
            }
        }
    }
    IEnumerator anidoor()
    {
        yield return new WaitForSeconds(1f);
        doornobcollider.enabled = true;
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
}
