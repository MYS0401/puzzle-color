using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectDilector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // �X�e�[�W�ԍ��������Ɏ󂯎���ăV�[�������[�h
    public void LoadStage(int stageNumber)
    {
        // �X�e�[�W���ƂɃV�[���������߂Ă���
        string sceneName = "PuzzleScene" + stageNumber;
        SceneManager.LoadScene(sceneName);
    }
}
