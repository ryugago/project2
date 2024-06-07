using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class train_outkey_com_fire : MonoBehaviour
{
    private bool istrigger=false;
    public GameObject card_remove;
    public GameObject fire;
    public float speed = 2f;

    // Update is called once per frame
    void Update()
    {
        OnDestroy();

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
            Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

            // fire의 위치를 플레이어 쪽으로 이동시키기
            fire.transform.position = Vector3.MoveTowards(fire.transform.position, playerPosition, speed * Time.deltaTime);
        }
    }

    private void OnDestroy()
    {
        if (card_remove == null && fire != null)
        {
            fire.SetActive(true); // fire 게임 오브젝트 활성화
            istrigger=true;
        }
    }
}
