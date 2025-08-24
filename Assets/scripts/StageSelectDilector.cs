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

    // ステージ番号を引数に受け取ってシーンをロード
    public void LoadStage(int stageNumber)
    {
        // ステージごとにシーン名を決めておく
        string sceneName = "PuzzleScene" + stageNumber;
        SceneManager.LoadScene(sceneName);
    }
}
