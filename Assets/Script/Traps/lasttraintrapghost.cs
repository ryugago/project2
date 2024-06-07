using System.Collections;
using UnityEngine;

public class lasttraintrapghost : MonoBehaviour
{
    public GameObject objectToSpawn; // Ȱ��ȭ�� ������Ʈ�� ���� ����
    public Transform cameraTransform; // ī�޶��� Transform ������Ʈ

    private bool playerEntered = false; // �÷��̾ �ݶ��̴��� �����ߴ��� Ȯ���ϱ� ���� ����

    public nolever nolever;

    public AudioClip clip;

    // �÷��̾ �ݶ��̴��� �������� �� ȣ��Ǵ� �Լ�
    private void OnTriggerEnter(Collider other)
    {
        if (nolever.trigger)
        {
            if (other.CompareTag("Player") && !playerEntered)
            {
                playerEntered = true; // �÷��̾ ���Դٰ� ǥ��
                SoundManager.instance.SFXPlay("shadow", clip);
                // ������Ʈ Ȱ��ȭ
                objectToSpawn.SetActive(true);
                GameManager.canPlayerMove2 = false;
                // ī�޶� ȸ�� ���� ����
                StartCoroutine(FollowObjectRotation());
                StartCoroutine(removeobj());

            }
        }
    }
    IEnumerator removeobj()
    {
        yield return new WaitForSeconds(2.5f); // 1�� ���
        objectToSpawn.SetActive(false); // ������Ʈ ��Ȱ��ȭ
        GameManager.canPlayerMove2 = true;
        yield return null;
    }
    // ������Ʈ�� �ٶ󺸴� ȸ�� ���� �ڷ�ƾ
    IEnumerator FollowObjectRotation()
    {
        while (true)
        {
            if (objectToSpawn.activeSelf)
            {
                // ī�޶� ������Ʈ�� �ٶ󺸰� ȸ��
                Vector3 directionToLook = objectToSpawn.transform.position - cameraTransform.position;
                Quaternion rotationToLookAt = Quaternion.LookRotation(directionToLook);
                cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, rotationToLookAt, Time.deltaTime * 5f);

                yield return null;
            }
            else
            {

                break; // �ڷ�ƾ ����
            }
        }
    }
}
