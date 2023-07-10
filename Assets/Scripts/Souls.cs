using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Souls : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] PlayerStats playerStats;
    [SerializeField] int gain;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            playerStats.Souls += gain;
            playerStats.soulsText.text = playerStats.Souls.ToString();
            Destroy(gameObject);
        }
    }
}
