using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gotitle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void title()
    {
        SceneManager.LoadScene("TitleScene"); // Ÿ��Ʋ ���� �̸��� ���⿡ �Է��ϼ���.
    }
}
