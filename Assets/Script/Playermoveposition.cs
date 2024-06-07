using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermoveposition : MonoBehaviour
{

    [Header("�÷��̾�,ī�޶� ��ġ")]
    public Vector3 playervector;
    public Quaternion playerrotation;
    public Quaternion rollafterplayer;

    private Camera playercamera;
    private GameObject player; // player ������ Ŭ���� �������� �̵�
    private bool trigger;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // player ���� �ʱ�ȭ
        playercamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        if (player == null)
        {
            Debug.LogError("�÷��̾ ã�� �� �����ϴ�. 'Player' �±׸� ���� ���� ������Ʈ�� Ȯ���ϼ���.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (trigger && player != null)
        {
            GameManager.canPlayerMove2 = false;
            player.transform.position = playervector;
            player.transform.rotation = playerrotation;
            playercamera.transform.rotation = rollafterplayer;
            trigger = false;
            //Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            trigger = true;
        }
    }
}
