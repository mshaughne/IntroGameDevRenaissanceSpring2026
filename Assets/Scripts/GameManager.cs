using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int coinsObtained = 0;
    [SerializeField] TextMeshProUGUI coinsText;

    private void Awake()
    {
        if(Instance == null) // if we don't already have an instance
        {
            Instance = this; // this becomes the instance
        }
        else // if we already have one
        {
            Destroy(gameObject); // destroy this new one
        }
    }

    public void PickUpCoin()
    {
        coinsObtained++;
        coinsText.text = "Coins: " + coinsObtained;
    }
}
