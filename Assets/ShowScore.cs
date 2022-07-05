using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowScore : MonoBehaviour
{
    // Start is called before the first frame update
    public static int line, score, level;
    public Text text;
    void Start()
    {
        line = 0;
        score = 0;
        level = 1;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Level: " + level.ToString() + "\n" + "Score: " + score.ToString() + "\n" + "Lines: " + line.ToString() + "\n";
    }
}
