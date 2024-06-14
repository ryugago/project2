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
            // �÷��̾��� ��ġ�� ��������
            if (playerTransform == null)
            {
                playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            }

            // �÷��̾�� fire ������ �Ÿ� ���
            float distanceToPlayer = Vector3.Distance(fire.transform.position, playerTransform.position);

            // �Ÿ��� 5���� ������ �̵� ���߱�
            if (distanceToPlayer < 5f)
            {
                return;
            }

            // fire�� ��ġ�� �÷��̾� ������ �̵���Ű��
            fire.transform.position = Vector3.MoveTowards(fire.transform.position, playerTransform.position, speed * Time.deltaTime);
        }
    }

    private void CheckForCardRemove()
    {
        if (card_remove == null && fire != null)
        {
            fire.SetActive(true); // fire ���� ������Ʈ Ȱ��ȭ
            istrigger = true;
        }
    }
}
