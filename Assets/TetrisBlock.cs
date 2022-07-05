using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TetrisBlock : MonoBehaviour
{
    private float previousTime;
    public float fallTime = 0.5f;
    public static int y = 20;
    public static int x = 7;
    public static int z = 7;
    public static int shadowHeight = 0;
    public Vector3 rotationPoint;
    private static Transform[,,] grid = new Transform[x, y, z];
    public static bool isGameOver = false;
    private float gameScore = 1f;

    void Start()
    {
    }
    
    // Update is called once per frame
    void Update()
    {
        if (GameOver.isPause)
        {
            Time.timeScale = 0f;
        }
        else
            Time.timeScale = gameScore;
        switch (ShowScore.score/6000)
        {
            case 0:
                {
                    gameScore = 1f;
                    Time.timeScale = gameScore;
                    break;
                }
            case 1:
                {
                    gameScore = 2f;
                    ShowScore.level=2;
                    Time.timeScale = gameScore;
                    break;
                }
            case 2:
                {
                    gameScore = 3f;
                    ShowScore.level=3;
                    Time.timeScale = gameScore;
                    break;
                }
            case 3:
                {
                    gameScore = 4f;
                    ShowScore.level=4;
                    Time.timeScale = gameScore;
                    break;
                }
            case 4:
                {
                    gameScore = 5f;
                    ShowScore.level=5;
                    Time.timeScale = gameScore;
                    break;
                }
            case 5:
                {
                    ShowScore.level=6;
                    Time.timeScale = 6f;
                    break;
                }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            if (!ValidMove())
            {
                transform.position -= new Vector3(-1, 0, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            if (!ValidMove())
            {
                transform.position -= new Vector3(1, 0, 0);
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.position += new Vector3(0, 0, -1);
            if (!ValidMove())
            {
                transform.position -= new Vector3(0, 0, -1);
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.position += new Vector3(0, 0, 1);
            if (!ValidMove())
            {
                transform.position -= new Vector3(0, 0, 1);
            }
        }

        else if (Input.GetKeyDown(KeyCode.Q))
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(1, 0, 0), 90);
            if (!ValidMove())
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(1, 0, 0), -90);
            }
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 1, 0), 90);
            if (!ValidMove())
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, -1, 0), -90);
            }
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
            if (!ValidMove())
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
            }
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(-1, 0, 0), 90);
            if (!ValidMove())
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(-1, 0, 0), -90);
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, -1, 0), 90);
            if (!ValidMove())
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, -1, 0), -90);
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, -1), 90);
            if (!ValidMove())
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, -1), -90);
            }
        }

        if (Time.time - previousTime > (Input.GetKey(KeyCode.Space) ? fallTime / 10 : fallTime))
        {
            transform.position += new Vector3(0, -1, 0);
            if (!ValidMove())
            {
                transform.position -= new Vector3(0, -1, 0);
                AddToGrid();
                deleteFullLayer();
                this.enabled = false;
                ShowScore.score += 10;
                if (!isGameOver)
                    FindObjectOfType<SpawnTetris>().NewTetris();
            }
            previousTime = Time.time;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            dropBlock();
        }
    }

    void dropBlock()
    {
        fallTime = 0;
    }

    bool ValidMove()
    {
        for (int i = 0; i < y; i++)
            for (int j = 0; j < x; j++)
                for (int k = 0; k < z; k++)
                {
                    if (grid[j, i, k] != null)
                        if (grid[j, i, k].parent == transform)
                            grid[j, i, k] = null;
                }
        foreach (Transform children in transform)
        {
            int roundX = Mathf.RoundToInt(children.transform.position.x);
            int roundY = Mathf.RoundToInt(children.transform.position.y);
            int roundZ = Mathf.RoundToInt(children.transform.position.z);

            if (roundX < 0 || roundY < 0 || roundX >= x || roundY >= y || roundZ < 0 || roundZ >= z)
                return false;
            if (grid[roundX, roundY, roundZ] != null && grid[roundX,roundY,roundZ].parent != transform)
            {
                return false;
            }
        }
        return true;
    }

    void AddToGrid()
    {
        foreach (Transform children in transform)
        {
            int roundX = Mathf.RoundToInt(children.transform.position.x);
            int roundY = Mathf.RoundToInt(children.transform.position.y);
            int roundZ = Mathf.RoundToInt(children.transform.position.z);

            if (roundY <= 16)
                grid[roundX, roundY, roundZ] = children;
            else
                isGameOver = true;
        }
    }

    
    public static void DeleteLayer(int y)
    {
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < z; j++)
            {
                Destroy(grid[i, y, j].gameObject);
                grid[i, y, j] = null;
            }
        }
    }

    public static void decreaseLayer(int y)
    {
        for (int i = 0; i < x; ++i)
        {
            for (int j = 0; j < z; j++)
            {
                if (grid[i, y, j] != null)
                {
                    grid[i, y - 1, j] = grid[i, y, j];
                    grid[i, y, j] = null;
                    grid[i, y - 1, j].position += new Vector3(0, -1, 0);
                }
            }
        }
    }
    public static void decreaseLayerAbove(int k)
    {
        for (int i = k; i < y; ++i)
            decreaseLayer(i);
    }

    public static bool isLayerFull(int y)
    {
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < z; j++)
            {
                if (grid[i, y, j] == null)
                    return false;
            }
        }
        return true;
    }

    public static void deleteFullLayer()
    {
        for (int k = 0; k < y; k++)
        {
            if (isLayerFull(k))
            {
                DeleteLayer(k);
                ShowScore.line++;
                ShowScore.score += ShowScore.line * 1000;
                decreaseLayerAbove(k + 1);
                --k;
            }
        }
    }
}
