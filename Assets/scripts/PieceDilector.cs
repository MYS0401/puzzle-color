using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceDilector : MonoBehaviour
{
    [SerializeField] Material nomove;
    [SerializeField] Material move;

    public bool isMove = true;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(tag == "Player")
        {
            return;
        }

        if (!isMove)
        {
            tag = ("nomove");
            GetComponent<Renderer>().material.color = nomove.color;
        }
        else
        {
            tag = ("move");
            GetComponent<Renderer>().material.color = move.color;
        }
        
    }


}
