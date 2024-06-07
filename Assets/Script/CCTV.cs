using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCTV : MonoBehaviour
{
    // 플레이어 오브젝트의 Transform
    public Transform target;

    void Update()
    {
        // 오브젝트가 플레이어를 바라보도록 회전
        transform.LookAt(target);
    }
}