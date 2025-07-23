using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlidePuzzleSceneDirector : MonoBehaviour
{
    // ピース
    [SerializeField] List<GameObject> pieces;
    // ゲームクリア時に表示されるボタン
    [SerializeField] GameObject buttonRetry;
    [SerializeField] GameObject buttonEnd;
    // シャッフル回数
    [SerializeField] int shuffleCount;

    // 初期位置
    List<Vector2> startPositions;

    //ハイライト
    [SerializeField] GameObject highlight;

    //ボタンが押されているか
    bool button = false;

    // Start is called before the first frame update
    void Start()
    {
        // 初期位置を保存
        startPositions = new List<Vector2>();
        foreach (var item in pieces)
        {
            startPositions.Add(item.transform.position);
        }

        // 指定回数シャッフル
        for (int i = 0; i < shuffleCount; i++)
        {
            // 0番と隣接するピース
            List<GameObject> movablePieces = new List<GameObject>();

            // 0番と隣接するピースをリストに追加
            foreach (var item in pieces)
            {
                if (GetEmptyPiece(item) != null)
                {
                    movablePieces.Add(item);
                }
            }

            // 隣接するピースをランダムで入れかえる
            int rnd = Random.Range(0, movablePieces.Count);
            GameObject piece = movablePieces[rnd];
            SwapPiece(piece, pieces[0]);
        }

        // ボタン非表示
        buttonRetry.SetActive(false);
        buttonEnd.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // タッチ処理
        if (Input.GetMouseButtonUp(0))
        {
            // スクリーン座標からワールド座標に変換
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // レイを飛ばす
            RaycastHit2D hit2d = Physics2D.Raycast(worldPoint, Vector2.zero);

            // 当たり判定があった
            if (hit2d)
            {
                // ヒットしたゲームオブジェクト
                GameObject hitPiece = hit2d.collider.gameObject;

                if(button)
                {
                    ColorChange(hitPiece);
                    button = false;
                }
                
                if (hitPiece.tag == "nomove")
                {
                    highlight.transform.position = hitPiece.transform.position;

                    highlight.SetActive(true);
                }
                else
                {
                    highlight.SetActive(false);
                }
                
                // 0番のピースと隣接していればデータが入る
                GameObject emptyPiece = GetEmptyPiece(hitPiece);
                // 選んだピースと0番のピースを入れかえる
                SwapPiece(hitPiece, emptyPiece);

                // クリア判定
                buttonRetry.SetActive(true);

                // 正解の位置と違うピースを探す
                /*for (int i = 0; i < pieces.Count; i++)
                {
                    // 現在のポジション
                    Vector2 position = pieces[i].transform.position;
                    // 初期位置と違ったらボタンを非表示
                    if (position != startPositions[i])
                    {
                        buttonRetry.SetActive(false);
                    }
                }*/

                GameObject targetPiece = pieces[1]; 
                Vector2 goalPosition = new Vector2(1.5f, -1.5f); //右下の座標

                if ((Vector2)targetPiece.transform.position == goalPosition)
                {
                    //buttonRetry.SetActive(true);
                    buttonEnd.SetActive(true);
                }
                else
                {
                   // buttonRetry.SetActive(false);
                    buttonEnd.SetActive(false);
                }


            }
        }

    }

    // 引数のピースが0番のピースと隣接していたら0番のピースを返す
    GameObject GetEmptyPiece(GameObject piece)
    {

        PieceDilector pieceDilector = piece.GetComponent<PieceDilector>();

        // 動かせないピースはスキップ
        if (!pieceDilector.isMove && pieceDilector != null)
        {
            return null;
        }

        // 2点間の距離を代入
        float dist =
            Vector2.Distance(piece.transform.position, pieces[0].transform.position);

        // 距離が1なら0番のピースを返す（2個以上離れていたり、斜めの場合は1より大きい距離になる）
        if (dist == 1)
        {
            return pieces[0];
        }

        return null;
    }

    // 2つのピースの位置を入れかえる
    void SwapPiece(GameObject pieceA, GameObject pieceB)
    {
        // どちらかがnullなら処理をしない
        if (pieceA == null || pieceB == null)
        {
            return;
        }

        // AとBのポジションを入れかえる
        Vector2 position = pieceA.transform.position;
        pieceA.transform.position = pieceB.transform.position;
        pieceB.transform.position = position;
    }

    // リトライボタン
    public void OnClickRetry()
    {
        SceneManager.LoadScene("PuzzleScene");
    }

    //シーン移行
    public void ChangeScene()
    {
        SceneManager.LoadScene("ResultScene");
    }

    //カラーチェンジ
    public void ColorChange(GameObject gameObject)
    {
        PieceDilector pieceDilector = gameObject.GetComponent<PieceDilector>();

        if(!pieceDilector.isMove)
        {
            pieceDilector.isMove = true;
        }
    }

    //カラーチェンジボタン
    public void isChange()
    {
        if(button)
        {
            button = false;
        }
        else
        {
            button = true;
        }
    }
}
