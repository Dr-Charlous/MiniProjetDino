using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] int lifePoints;
    [SerializeField] int lifePointMax;
    [SerializeField] GameObject[] lifes;
    [SerializeField] Transform respawn;

    private void Start()
    {
        lifePoints = lifePointMax;

        foreach (var item in lifes)
        {
            item.SetActive(true);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            lifePoints = GetHurt(lifePoints, 1);

            if (lifePointMax - lifePoints > 0)
            {
                for (int i = 0; i < lifePointMax - lifePoints; i++)
                {
                    lifes[lifes.Length - 1 - i].SetActive(false);
                }
            }
        }

        if (lifePoints <= 0)
        {
            Dead();
        }
    }

    public int GetHurt(int life, int damage)
    {
        life -= damage;
        return life;
    }

    public void Dead()
    {
        print("You are dead !");

        lifePoints = lifePointMax;

        foreach (var item in lifes)
        {
            item.SetActive(true);
        }

        gameObject.transform.position = respawn.position;
    }
}
