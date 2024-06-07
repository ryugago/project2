using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NovaSamples.Effects;

public class PlayerHit : MonoBehaviour
{
    public Material hit_effect;

    public float[] trapdamage;
    public float[] traptime;
    public Transform mainCamera;
    public PlayerController player;
    [Header("카메라 흔들림")]
    public CameraShake cameraShake;

    public float downCamera = 0.02f;
    public float upCamera = 2f;

    [Header("화면블러")]
    public BlurEffect blurEffect;


    private bool isHit = false;

    int i;



    private void Start()
    {
        blurEffect.BlurRadius = 0f;
        hit_effect.SetFloat("_Fullscreenintensity", 0);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Hit0" && !isHit)
        {
            //Debug.Log("사람맞음");
            i = 0;
            StartCoroutine(ApplyHitEffect());
        }

        if (other.tag == "Hit1" && !isHit)
        {
            //Debug.Log("사람맞음");
            i = 1;
            StartCoroutine(ApplyHitEffect());
        }
    }



    IEnumerator ApplyHitEffect()
    {
        isHit = true;

        // BlurRadius를 8로 설정
        blurEffect.BlurRadius = 8f;

        // BlurRadius를 7.5초 동안 3으로 감소
        float startTime = Time.time;
        float duration = 7.5f;
        while (Time.time < startTime + duration)
        {
            float t = (Time.time - startTime) / duration;
            blurEffect.BlurRadius = Mathf.Lerp(8f, 3f, t);
            yield return null;
        }
        blurEffect.BlurRadius = 3f;
        isHit = false;
    }
}
    /*
     * IEnumerator HitSequence()
    {
        //GameManager.isPause = true;
        StartCoroutine(ApplyHitEffect());
        //cameraShake.Shake(2f, 0.1f);
        //yield return StartCoroutine(RotateCamera90Degrees());
        player.currentCameraRotationX = 0f;

        GameManager.canPlayerMove2 = true;
        //GameManager.isPause = false;
        yield return null;
    }
    IEnumerator RotateCamera90Degrees()
    {
        Vector3 startPosition = mainCamera.localPosition;
        Quaternion startRotation = mainCamera.localRotation;

        Vector3 targetPosition = new Vector3(startPosition.x, 0f, startPosition.z);
        Quaternion targetRotation = Quaternion.Euler(90f, startRotation.eulerAngles.y, startRotation.eulerAngles.z);

        float elapsedTime = 0f;

        while (elapsedTime < downCamera)
        {
            mainCamera.localRotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime / downCamera);
            mainCamera.localPosition = Vector3.Lerp(startPosition, targetPosition, elapsedTime / downCamera);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mainCamera.localRotation = targetRotation;
        mainCamera.localPosition = targetPosition;

        // Reset rotation back to 0 over 1 second
        float resetTime = 0f;
        Quaternion originalRotation = mainCamera.localRotation;
        Vector3 originalPosition = mainCamera.localPosition;

        while (resetTime < upCamera)
        {
            mainCamera.localRotation = Quaternion.Slerp(originalRotation, Quaternion.identity, resetTime / upCamera);
            mainCamera.localPosition = Vector3.Lerp(originalPosition, startPosition, resetTime / upCamera);
            resetTime += Time.deltaTime;
            yield return null;
        }

        mainCamera.localRotation = Quaternion.identity;
        mainCamera.localPosition = startPosition;
    }*/

