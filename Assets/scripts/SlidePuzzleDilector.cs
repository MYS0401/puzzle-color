using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlidePuzzleSceneDirector : MonoBehaviour
{
    // �s�[�X
    [SerializeField] List<GameObject> pieces;
    [SerializeField] GameObject targetPiece;   // �S�[���Ώۂ̃s�[�X
    [SerializeField] Vector2 goalPosition;     // �S�[�����W
    [SerializeField] float goalTolerance = 0.01f; // �덷���e�l

    // �Q�[���N���A���ɕ\�������{�^��
    [SerializeField] GameObject buttonRetry;
    [SerializeField] GameObject buttonEnd;
    // �V���b�t����
    [SerializeField] int shuffleCount;

    // �����ʒu
    List<Vector2> startPositions;

    //�n�C���C�g
    [SerializeField] GameObject highlight;

    //�{�^����������Ă��邩
    bool button = false;

    //�F��ς��邱�Ƃ��ł���s�[�X
    GameObject colorChangeablePiece;

    // Start is called before the first frame update
    void Start()
    {
        // �����ʒu��ۑ�
        startPositions = new List<Vector2>();
        foreach (var item in pieces)
        {
            startPositions.Add(item.transform.position);
        }

        // �w��񐔃V���b�t��
        for (int i = 0; i < shuffleCount; i++)
        {
            // 0�ԂƗאڂ���s�[�X
            List<GameObject> movablePieces = new List<GameObject>();

            // 0�ԂƗאڂ���s�[�X�����X�g�ɒǉ�
            foreach (var item in pieces)
            {
                if (GetEmptyPiece(item) != null)
                {
                    movablePieces.Add(item);
                }
            }

            // �אڂ���s�[�X�������_���œ��ꂩ����
            int rnd = Random.Range(0, movablePieces.Count);
            GameObject piece = movablePieces[rnd];
            SwapPiece(piece, pieces[0]);
        }

        // �{�^����\��
        buttonRetry.SetActive(false);
        buttonEnd.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // �^�b�`����
        if (Input.GetMouseButtonUp(0))
        {
            // �X�N���[�����W���烏�[���h���W�ɕϊ�
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // ���C���΂�
            RaycastHit2D hit2d = Physics2D.Raycast(worldPoint, Vector2.zero);

            // �����蔻�肪������
            if (hit2d)
            {
                // �q�b�g�����Q�[���I�u�W�F�N�g
                GameObject hitPiece = hit2d.collider.gameObject;

                //�܂��I�΂�Ă��Ȃ���΂��̃s�[�X��F�ύX�\�ɂ���
                if (colorChangeablePiece == null)
                {
                    colorChangeablePiece = hitPiece;
                }

                //�{�^���������ꂽ��I�΂ꂽ1�̃s�[�X�����F�ύX�\
                if (button)
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
                
                // 0�Ԃ̃s�[�X�Ɨאڂ��Ă���΃f�[�^������
                GameObject emptyPiece = GetEmptyPiece(hitPiece);
                // �I�񂾃s�[�X��0�Ԃ̃s�[�X����ꂩ����
                SwapPiece(hitPiece, emptyPiece);

                // �N���A����
                buttonRetry.SetActive(true);

                // �N���A����
                if (Vector2.Distance(targetPiece.transform.position, goalPosition) < goalTolerance)
                {
                    buttonEnd.SetActive(true);
                }
                else
                {
                    buttonEnd.SetActive(false);
                }


            }
        }

    }

    // �����̃s�[�X��0�Ԃ̃s�[�X�Ɨאڂ��Ă�����0�Ԃ̃s�[�X��Ԃ�
    GameObject GetEmptyPiece(GameObject piece)
    {

        PieceDilector pieceDilector = piece.GetComponent<PieceDilector>();

        // �������Ȃ��s�[�X�̓X�L�b�v
        if (pieceDilector != null && !pieceDilector.isMove)
            {
            return null;
        }

        // 2�_�Ԃ̋�������
        float dist =
            Vector2.Distance(piece.transform.position, pieces[0].transform.position);

        // ������1�Ȃ�0�Ԃ̃s�[�X��Ԃ��i2�ȏ㗣��Ă�����A�΂߂̏ꍇ��1���傫�������ɂȂ�j
        if (dist == 1)
        {
            return pieces[0];
        }

        return null;
    }

    // 2�̃s�[�X�̈ʒu����ꂩ����
    void SwapPiece(GameObject pieceA, GameObject pieceB)
    {
        // �ǂ��炩��null�Ȃ珈�������Ȃ�
        if (pieceA == null || pieceB == null)
        {
            return;
        }

        // A��B�̃|�W�V��������ꂩ����
        Vector2 position = pieceA.transform.position;
        pieceA.transform.position = pieceB.transform.position;
        pieceB.transform.position = position;
    }

    // ���g���C�{�^��
    public void OnClickRetry()
    {
        SceneManager.LoadScene("PuzzleScene");
    }

    //�V�[���ڍs
    public void ChangeScene()
    {
        SceneManager.LoadScene("ResultScene");
    }

    //�J���[�`�F���W
    public void ColorChange(GameObject gameObject)
    {
        //�I�΂ꂽ�s�[�X�ȊO�͖���
        if (gameObject != colorChangeablePiece) return;

        PieceDilector pieceDilector = gameObject.GetComponent<PieceDilector>();

        if(!pieceDilector.isMove)
        {
            pieceDilector.isMove = true;
        }
    }

    //�J���[�`�F���W�{�^��
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
