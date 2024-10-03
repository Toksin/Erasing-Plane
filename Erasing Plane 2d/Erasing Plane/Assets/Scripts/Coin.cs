using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1;
    private static string PLAYER_TAG = "Player";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(PLAYER_TAG))
        {            
            GameStates gameStates = FindObjectOfType<GameStates>();           
            gameStates.AddTemporaryCoins(coinValue);            
            Destroy(gameObject);
        }
    }
}
