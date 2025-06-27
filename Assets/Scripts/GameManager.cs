//using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

//using System.Diagnostics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static string[] Items = { "NestLevel", "XP", "EggCount", "Flowers1", "Flowers2", "Flowers3", "Fairy1", "Fairy2", "Fairy3", "Branch" };
    private static int[] Price = { 3, 3, 3 };
    [SerializeField] GameObject[] Fairies;
    [SerializeField] GameObject[] Flowers;
    [SerializeField] GameObject egg;
    SpriteRenderer eggSprite;

    public static bool isEggAvailable = false; // a flag to know if we have enough collectibles to purchase an egg
    public bool tutorialFlag = false;


    void Start()
    {
        StartCoroutine(SpawnFlowers());
        eggSprite = egg.GetComponent<SpriteRenderer>();
        egg.SetActive(false);
        SetInventory();

        Test(); // test

        //initiate flags:
        PlayerPrefs.SetInt("NewEggFlag", 0);
        PlayerPrefs.SetInt("PendingEggFlag", 0);
        PlayerPrefs.SetInt("EggHatchedFlag", 0);
        PlayerPrefs.SetInt("FairyCollectedFlag", 0);

        //PlayerPrefs.SetInt("FairyQueue", 0);  //for an alternative implemantation of the method SpawnFairy
    }

    void Update()
    {
        //PrintTest(); //temp

        //check flags:
        isEggAvailable = IsEggAvailable(); // update flag
        if (PlayerPrefs.GetInt("PendingEggFlag") == 1)
        {
            PlayerPrefs.SetInt("PendingEggFlag", 0);
            BuyEgg();
        }
        if (PlayerPrefs.GetInt("EggHatchedFlag") == 1)
        {
            PlayerPrefs.SetInt("EggHatchedFlag", 0);
            SpawnFairy();
        }
        if (PlayerPrefs.GetInt("FairyCollectedFlag") == 1)
        {
            PlayerPrefs.SetInt("FairyCollectedFlag", 0);
            CollectEgg();
        }
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

    public static void AddCollectible(string str, int num)
    {
        PlayerPrefs.SetInt(str, (PlayerPrefs.GetInt(str) + num)); //update inventory
        if (str == "Fairy1" || str == "Fairy2" || str == "Fairy3")
        {
            PlayerPrefs.SetInt("FairyCollectedFlag", 1);
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

    //Checks if the nest need to be upgraded based on the XP count 
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

    //a new egg is available only if there isn't already one in the nest, and the player can afford its price
    public bool IsEggAvailable()
    {
        if (egg.activeSelf)
        {
            return false;
        }
        for (int i = 0; i < Price.Length; i++)
        {
            if (PlayerPrefs.GetInt("Flowers" + (i + 1).ToString()) < ((PlayerPrefs.GetInt("EggCount") + 1) * Price[i]))
            {
                return false;
            }
        }
        return true;
    }

    public void BuyEgg()
    {
        for (int i = 0; i < Price.Length; i++)
        {
            PlayerPrefs.SetInt("Flowers" + (i + 1).ToString(), (PlayerPrefs.GetInt("Flowers" + (i + 1).ToString()) - (PlayerPrefs.GetInt("EggCount") + 1) * Price[i]));
        }
        PlayerPrefs.SetInt("EggCount", PlayerPrefs.GetInt("EggCount") + 1);
        egg.SetActive(true);
        PlayerPrefs.SetInt("NewEggFlag", 1);
    }

    //Spawn fairies. fairy1, fairy2, and fairy3 are being spawned randomly with a ratio of 4:2:1
    public void SpawnFairy()
    {
        int i = Random.Range(0, 7);
        switch (i)
        {
            case var n when n < 4: Instantiate(Fairies[0]); Debug.Log("0"); return;
            case var n when n < 6: Instantiate(Fairies[1]); return;
            case var n when n < 7: Instantiate(Fairies[2]); return;
        }

        //another implementation with a queue:
        //Instantiate(Fairies[PlayerPrefs.GetInt("FairyQueue") % 3]); //add a prefab of the next fairy in line
        //PlayerPrefs.SetInt("FairyQueue", PlayerPrefs.GetInt("FairyQueue") + 1); //fairy line ++
    }

    //When the player collects a fairy, delete egg
    public void CollectEgg()
    {
        egg.SetActive(false);
    }


    public IEnumerator SpawnFlowers()
    {
        while (true)
        {
            int i = Random.Range(0, 7); //picks a flower type
            float j = Random.Range(-7, 7); //picks the flower's x position
            GameObject flower;
            if (i < 4)
            {
                flower = Instantiate(Flowers[0], new Vector3(j, 4.6f, 0), Quaternion.identity);
            }
            else if (i < 6)
            {
                flower = Instantiate(Flowers[1], new Vector3(j, 4.6f, 0), Quaternion.identity);
            }
            else
            {
                flower = Instantiate(Flowers[2], new Vector3(j, 4.6f, 0), Quaternion.identity);
            }
            flower.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -2f);
            StartCoroutine(DestroyOutOfFrame(flower));
            //await Task.Delay(2000);
            yield return new WaitForSeconds(2f);
        }
    }

    IEnumerator DestroyOutOfFrame(GameObject flower)
    {
        yield return new WaitForSeconds(6);
        Destroy(flower);
    }
}