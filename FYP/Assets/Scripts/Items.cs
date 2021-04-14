using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    public enum ItemType
    {
        Armor_1, // soldier
        Armor_2, 
        HealthPotion,
        Sword_1,
        Sword_2
    }

    public static int GetCost(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.Armor_1: return 30;
            case ItemType.Armor_2: return 60;
            case ItemType.HealthPotion: return 90;
            case ItemType.Sword_1: return 120;
            case ItemType.Sword_2: return 150;

        }

    }

   
}
