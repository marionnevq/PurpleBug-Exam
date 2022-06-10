using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagPart : MonoBehaviour
{

    public enum Part
    {
        bot,
        mid,
        top
    }

    public Part flagPart;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            EndFlag.instance.RecieveSignal(flagPart);
        }
    }
}
