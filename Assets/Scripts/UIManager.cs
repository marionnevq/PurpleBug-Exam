using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public Text coinText, livesText;

    private void Awake() 
    {
        instance = this;    
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateCoins()
    {
        coinText.text = GameManager.instance.coins.ToString();
    }

    public void UpdateLives()
    {
        livesText.text = GameManager.instance.lives.ToString();
    }
}
