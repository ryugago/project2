using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermoveposition : MonoBehaviour
{

    [Header("플레이어,카메라 위치")]
    public Vector3 playervector;
    public Quaternion playerrotation;
    public Quaternion rollafterplayer;

    private Camera playercamera;
    private GameObject player; // player 변수를 클래스 수준으로 이동
    private bool trigger;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // player 변수 초기화
        playercamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        if (player == null)
        {
            Debug.LogError("플레이어를 찾을 수 없습니다. 'Player' 태그를 가진 게임 오브젝트를 확인하세요.");
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
