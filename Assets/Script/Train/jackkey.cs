using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jackkey : MonoBehaviour
{
    public float minScale = 1.0f;
    public float maxScale = 1.1f;
    public float scaleSpeed = 1.0f;
    public float waitTime = 0.5f;

    private bool isScalingUp = true;
    private Vector3 originalScale;
    private float timer = 0.0f;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= waitTime)
        {
            if (isScalingUp)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, originalScale * maxScale, scaleSpeed * Time.deltaTime);
                if (transform.localScale.x >= originalScale.x * maxScale * 0.99f)
                {
                    isScalingUp = false;
                    timer = 0.0f;
                }
            }
            else
            {
                transform.localScale = Vector3.Lerp(transform.localScale, originalScale * minScale, scaleSpeed * Time.deltaTime);
                if (transform.localScale.x <= originalScale.x * minScale * 1.01f)
                {
                    isScalingUp = true;
                    timer = 0.0f;
                }
            }
        }
    }
}
