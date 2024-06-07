using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnstile_out : MonoBehaviour
{
    public Animator turnstileAnim1; // 애니메이터 컴포넌트를 가리키는 변수
    public Animator turnstileAnim2; // 애니메이터 컴포넌트를 가리키는 변수
    //private bool Isplayer = true;

    // Start is called before the first frame update
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // 플레이어가 콜라이더에서 나갈 때
        {
            Debug.Log("문닫힘");
            turnstileAnim1.SetBool("open", false); // 애니메이션의 bool 변수를 false로 설정하여 정지
            turnstileAnim2.SetBool("open", false); // 애니메이션의 bool 변수를 false로 설정하여 정지
        }
    }
}
