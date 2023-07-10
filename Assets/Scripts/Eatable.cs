using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eatable : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] PlayerStats playerStats;
    [SerializeField] int gain;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            playerStats.lifePoints = LifeGain(playerStats.lifePoints, playerStats.lifePointMax, gain);
            playerStats.LifeAff();
            Destroy(gameObject);
        }
    }

    int LifeGain(int life, int lifeMax, int gain)
    {
        if (life < lifeMax)
            life += gain;
        return life;
    }
}
