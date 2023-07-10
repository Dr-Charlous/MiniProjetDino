using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public int lifePoints;
    public int lifePointMax;
    [SerializeField] GameObject[] lifes;

    [SerializeField] Transform respawn;

    public int Souls;
    public TextMeshProUGUI soulsText;


    private void Start()
    {
        lifePoints = lifePointMax;
        soulsText.text = "0";

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
            LifeAff();
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

    public void LifeAff()
    {
        for (int i = 0; i < lifePointMax - lifePoints; i++)
        {
            lifes[lifes.Length - 1 - i].SetActive(false);
        }
        for (int i = 0; i < lifePoints; i++)
        {
            lifes[i].SetActive(true);
        }
    }
}
