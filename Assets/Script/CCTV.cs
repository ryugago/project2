using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCTV : MonoBehaviour
{
    // �÷��̾� ������Ʈ�� Transform
    public Transform target;

    void Update()
    {
        // ������Ʈ�� �÷��̾ �ٶ󺸵��� ȸ��
        transform.LookAt(target);
    }
}