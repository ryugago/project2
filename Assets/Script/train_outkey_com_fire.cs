using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class train_outkey_com_fire : MonoBehaviour
{
    private bool istrigger = false;
    public GameObject card_remove;
    public GameObject fire;
    public float speed = 2f;
    private Transform playerTransform;

    // Update is called once per frame
    void Update()
    {
        CheckForCardRemove();

        if (istrigger)
        {
            MoveTowardsPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
        if (fire != null && card_remove == null)
        {
            // 플레이어의 위치를 가져오기
            if (playerTransform == null)
            {
                playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            }

            // 플레이어와 fire 사이의 거리 계산
            float distanceToPlayer = Vector3.Distance(fire.transform.position, playerTransform.position);

            // 거리가 5보다 작으면 이동 멈추기
            if (distanceToPlayer < 5f)
            {
                return;
            }

            // fire의 위치를 플레이어 쪽으로 이동시키기
            fire.transform.position = Vector3.MoveTowards(fire.transform.position, playerTransform.position, speed * Time.deltaTime);
        }
    }

    private void CheckForCardRemove()
    {
        if (card_remove == null && fire != null)
        {
            fire.SetActive(true); // fire 게임 오브젝트 활성화
            istrigger = true;
        }
    }
}
