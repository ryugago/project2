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
}
