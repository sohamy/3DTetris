using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTetris : MonoBehaviour
{
    public GameObject[] Tetris;
    private GameObject[] randomTetris;
    private GameObject temp, target, next, hold, tmp;
    private bool isFirst = true;
    private int cnt = 0;
    private int height;

    // Start is called before the first frame update
    void Start()
    {

        Suffle();
        NewTetris();
        hold = null;
    }

    // Update is called once per frame
    void Update()
    {
        height = Mathf.RoundToInt(TetrisBlock.shadowHeight);
        Hold();
    }

    public void NewTetris()
    {    
        if (isFirst)
        {
            isFirst = false;
            if(next != null)
            {
                target = next;
                target.transform.position = transform.position;
                target.GetComponent<TetrisBlock>().enabled = true;
            }
            else
                target = Instantiate(randomTetris[cnt], transform.position, Quaternion.identity);
            cnt++;
            next = null;
            next = Instantiate(randomTetris[cnt], new Vector3(10, 14, 3), Quaternion.identity);
            next.GetComponent<TetrisBlock>().enabled = false;
        }    
        else
        {
            cnt++;
            target = next;
            target.transform.position = transform.position;
            target.GetComponent<TetrisBlock>().enabled = true;
            next = null;
            if (cnt < 7)
            {
                next = Instantiate(randomTetris[cnt], new Vector3(10, 14, 3), Quaternion.identity);
                next.GetComponent<TetrisBlock>().enabled = false;
            }
        }
        if(cnt >= 7)
        {
            isFirst = true;
            cnt = 0;
            Suffle();
            next = null;
            next = Instantiate(randomTetris[cnt], new Vector3(10, 14, 3), Quaternion.identity);
            next.GetComponent<TetrisBlock>().enabled = false;
        }      
    }
    
    public void Suffle()
    {
        int rd;
        randomTetris = Tetris;
        for (int i = 0; i < Tetris.Length; i++)
        {
            rd = Random.Range(0, Tetris.Length);
            temp = randomTetris[i];
            randomTetris[i] = randomTetris[rd];
            randomTetris[rd] = temp;
        }
    }

    public void Hold()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            if(hold == null)
            {
                hold = target;
                hold.transform.position = new Vector3(-5, 14, 3);
                hold.GetComponent<TetrisBlock>().enabled = false;
                NewTetris();
            }
            else
            {
                tmp = hold;
                hold = target;
                hold.transform.position = new Vector3(-5, 14, 3);
                hold.GetComponent<TetrisBlock>().enabled = false;
                target = tmp;
                target.transform.position = transform.position;
                target.GetComponent<TetrisBlock>().enabled = true;
            }
        }
    }
}
