using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleDilector : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI titleText;�@
    [SerializeField] private float speed = 2f;�@//�F���ω����鑬�x


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //�e�L�X�g�̐F��ω�������
        float t = (Mathf.Sin(Time.time * speed) + 1f) / 2f;
        Color newColor = Color.Lerp(Color.cyan, Color.magenta, t);
        titleText.color = newColor;
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("StageScene");
    }

    public void EndGame()
    {
#if UNITY_EDITOR�@//�f�o�b�N�p
        UnityEditor.EditorApplication.isPlaying = false;
#else //�A�v���p
Application.Quit();
#endif
    }
}
