using System.Collections;
using UnityEngine;

public class first1AF : MonoBehaviour
{
    private bool trigger1;
    private bool trigger2 = true;

    public GameObject spider;
    public Camera Pcamera;

    private void Update()
    {
        if (trigger1 && trigger2)
        {
            trigger2 = false;
            GameManager.canPlayerMove2 = false;

            // �Ź̸� ī�޶� ���� �̵���Ű�� �ڷ�ƾ�� �����մϴ�.
            StartCoroutine(HandleSpiderMovement());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            trigger1 = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            trigger1 = false;
        }
    }

    private IEnumerator HandleSpiderMovement()
    {
        if (Pcamera != null && spider != null)
        {
            // �Ź̸� ī�޶� ���� ��ġ�ϰ� ī�޶� ȸ���� ���󰡵��� ����
            Vector3 cameraPosition = Pcamera.transform.position;
            Vector3 spawnPosition = cameraPosition + Vector3.up * 2.0f; // ī�޶� �� 2.0f��ŭ ��ġ
            spider.transform.position = spawnPosition;
            spider.transform.rotation = Pcamera.transform.rotation;

            // 2�� ���� �̵�
            float elapsedTime = 0f;
            float duration = 0.3f;
            Vector3 startPosition = spider.transform.position;
            Vector3 endPosition = cameraPosition + Pcamera.transform.forward * 0.8f; // ī�޶� �� 0.8f��ŭ ��ġ

            while (elapsedTime < duration)
            {
                spider.transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / duration);
                spider.transform.rotation = Pcamera.transform.rotation;
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // ��ġ�� ���� �������� ����
            spider.transform.position = endPosition;
            spider.transform.rotation = Pcamera.transform.rotation;

            // 1�� ���� �̵�
            elapsedTime = 0f;
            duration = 0.3f;
            startPosition = spider.transform.position;
            endPosition = startPosition + Vector3.down * 2.0f; // �Ʒ��� 2.0f��ŭ �̵�

            while (elapsedTime < duration)
            {
                spider.transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / duration);
                spider.transform.rotation = Pcamera.transform.rotation;
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // ��ġ�� ���� �������� ����

            spider.transform.position = endPosition;
            spider.transform.rotation = Pcamera.transform.rotation;

            GameManager.canPlayerMove2 = true;
        }
        else
        {
            Debug.LogWarning("Pcamera �Ǵ� spider GameObject�� �Ҵ���� �ʾҽ��ϴ�.");
        }
    }
}
