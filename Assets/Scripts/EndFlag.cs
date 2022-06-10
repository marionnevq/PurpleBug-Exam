using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndFlag : MonoBehaviour
{
    private bool isFin;

    public static EndFlag instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {

    }

    public void RecieveSignal(FlagPart.Part part)
    {
        if (!isFin)
        {
            switch (part)
            {
                case FlagPart.Part.top:
                    EndGame(5000);
                    break;
                case FlagPart.Part.mid:
                    EndGame(4000);
                    break;
                case FlagPart.Part.bot:
                    EndGame(3000);
                    break;

            }
        }

    }

    private void EndGame(int score)
    {
        isFin = true;
        GameManager.instance.score += score;
        GameManager.instance.Endlevel();
    }
}
