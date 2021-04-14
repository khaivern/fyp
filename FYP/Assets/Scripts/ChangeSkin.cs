using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeSkin : MonoBehaviour
{
    public AnimatorOverrideController soldierAnim;
    public AnimatorOverrideController kingAnim;

    // Game Session Reference
    GameSession myGameSession;
    // Costs and Upgrades
    Shop shop;
    Bullet bullet;
    Player player;
    // Score
    int score;
    int shortBy;

    private void Start()
    {
        bullet = FindObjectOfType<Bullet>();
        player = FindObjectOfType<Player>();
        shop = FindObjectOfType<Shop>();
        
    }

    private void Update()
    {
        myGameSession = FindObjectOfType<GameSession>();
        score = myGameSession.GetScore();
    }

    public void SoldierSkin()
    {
        if (score < shop.GetSoldierCost())
        {
            // Debug.Log("I am short by " + (shop.GetSoldierCost() - score));
            shortBy = shop.GetSoldierCost() - score;
            shop.SetReceipt(shortBy);
            return;
        }
        else
        {
            myGameSession.ReduceScore(shop.GetSoldierCost());
            GetComponent<Animator>().runtimeAnimatorController = soldierAnim;
        }
    }

    public void KingSkin()
    {
        if (score < shop.GetKingCost())
        {
            // Debug.Log("I am short by " + (shop.GetKingCost() - score));
            shortBy = shop.GetKingCost() - score;
            shop.SetReceipt(shortBy);
            return;

        }
        else
        {
            myGameSession.ReduceScore(shop.GetKingCost());
            GetComponent<Animator>().runtimeAnimatorController = kingAnim;
        }
    }

    public void DmgUP()
    {
        if (score < shop.GetDmgUpCost())
        {
            // Debug.Log("I am short by " + (shop.GetDmgUpCost() - score));
            shortBy = shop.GetDmgUpCost() - score;
            shop.SetReceipt(shortBy);
            return;
        }
        else
        {
            myGameSession.ReduceScore(shop.GetDmgUpCost());
            bullet.SetDamage(200);
        }
    }

    public void HpUP()
    {
        if (score < shop.GetHpUpCost())
        {
            // Debug.Log("I am short by " + (shop.GetHpUpCost() - score));
            shortBy = shop.GetHpUpCost() - score;
            shop.SetReceipt(shortBy);
            return;
        }
        else
        {
            myGameSession.ReduceScore(shop.GetHpUpCost());
            player.IncreaseMaxHealth(1000);
        }
    }
}
