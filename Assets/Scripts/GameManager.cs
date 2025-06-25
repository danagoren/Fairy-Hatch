using System;
using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static string[] Items = { "NestLevel", "XP", "Egg", "Flower1", "Flower2", "Flower3", "Fairy1", "Fairy2", "Fairy3", "Branch" };
    private static int[] Price = { 3, 3, 3 };
    //public static bool egg = false; // a flag to know if the is an egg in the nest
    [SerializeField] public GameObject egg;
    SpriteRenderer eggSprite;
    [SerializeField] Sprite WholeEgg;
    [SerializeField] Sprite HatchedEgg;

    public static bool isEggAvailable = false; // a flag to know if we have enough collectables to purchase an egg


    void Start()
    {
        eggSprite = egg.GetComponent<SpriteRenderer>();
        eggSprite.sprite = WholeEgg;
        egg.SetActive(false);
        SetInventory();
        //flags:
        PlayerPrefs.SetInt("PendingEgg", 0);
        PlayerPrefs.SetInt("EggHatched", 0);
        PlayerPrefs.SetInt("FairyCollected", 0);
        //tests:
        Test();
    }

    void Update()
    {
        isEggAvailable = IsEggAvailable(); // update flag
        if (PlayerPrefs.GetInt("PendingEgg") == 1)
        {
            PlayerPrefs.SetInt("PendingEgg", 0);
            BuyEgg();
        }
        if (PlayerPrefs.GetInt("EggHatched") == 1)
        {
            PlayerPrefs.SetInt("EggHatched", 0);
            HatchEgg();
        }
        if (PlayerPrefs.GetInt("FairyCollected") == 1)
        {
            PlayerPrefs.SetInt("FairyCollected", 0);
            CollectEgg();
        }
        //PrintTest(); //temp
    }

    void Test()
    {
        PlayerPrefs.SetInt("Flowers1", 1000);
        PlayerPrefs.SetInt("Flowers2", 1000);
        PlayerPrefs.SetInt("Flowers3", 1000);
    }
    void PrintTest()
    {
        string testStr = "";
        for (int i = 0; i < Items.Length; i++)
        {
            testStr += Items[i] + ": " + PlayerPrefs.GetInt(Items[i]);
            if (i < Items.Length - 1)
            {
                testStr += ",    ";
            }
        }
        Debug.Log(testStr);
    }

    private void SetInventory()
    {
        PlayerPrefs.SetInt("NestLevel", 1);
        for (int i = 1; i < Items.Length; i++)
        {
            PlayerPrefs.SetInt(Items[i], 0);
        }
    }
    public static void AddCollectable(string str, int num)
    {
        PlayerPrefs.SetInt(str, (PlayerPrefs.GetInt(str) + num));
        if (str == "Fairy1" || str == "Fairy2" || str == "Fairy3")
        {
            PlayerPrefs.SetInt("FairyCollected", 1);
            AddXP(20 * num);
        }
        else
        {
            AddXP(5 * num);
        }
    }

    private static void AddXP(int num)
    {
        PlayerPrefs.SetInt("XP", (PlayerPrefs.GetInt("XP") + (num)));
        if (NestUpgradeNeeded())
        {
            UpgradeNest();
        }
    }

    //Checks if the nest need to be upgraded based on the XP count. 
    private static bool NestUpgradeNeeded()
    {
        switch (PlayerPrefs.GetInt("XP"))
        {
            case var n when n < 20: return (PlayerPrefs.GetInt("NestLevel") == 1);
            case var n when n < 50: return (PlayerPrefs.GetInt("NestLevel") == 2);
            case var n when n < 90: return (PlayerPrefs.GetInt("NestLevel") == 3);
            case var n when n < 140: return (PlayerPrefs.GetInt("NestLevel") == 4);
        }
        return (PlayerPrefs.GetInt("NestLevel") == 5);
    }

    private static void UpgradeNest()
    {
        PlayerPrefs.SetInt("NestLevel", (PlayerPrefs.GetInt("NestLevel") + 1));
        //upgrade nest's sprite
    }

    public bool IsEggAvailable()
    {
        if (egg.activeSelf)
        {
            return false;
        }
        for (int i = 0; i < Price.Length; i++)
        {
            if (PlayerPrefs.GetInt(Items[i + 3]) < (PlayerPrefs.GetInt("Egg") * Price[i]))
            {
                return false;
            }
        }
        return true;
    }

    public void BuyEgg()
    {
        //add an egg sprite to the nest
        for (int i = 0; i < Price.Length; i++)
        {
            PlayerPrefs.SetInt(Items[i + 3], (PlayerPrefs.GetInt(Items[i + 3]) - PlayerPrefs.GetInt("NestLevel") * Price[i]));
        }
        egg.SetActive(true);
        eggSprite.sprite = WholeEgg;
        PlayerPrefs.SetInt("Egg", PlayerPrefs.GetInt("Egg") + 1);
    }

    public void HatchEgg()
    {
        eggSprite.sprite = HatchedEgg;
        //add gift prefab
        //add fairy prefab
    }

    //when the player collects the gift:
    public void CollectEgg()
    {
        eggSprite.sprite = WholeEgg;
        egg.SetActive(false);  //remove egg's sprite
    }
}
