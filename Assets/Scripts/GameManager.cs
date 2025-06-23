using System;
using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private string[] Items = {"NestLevel", "XP", "Flower1", "Flower2", "Flower3", "Fairy1", "Fairy2", "Fairy3", "Branch"};

    void Start()
    {
        SetInventory();
    }

    void Update()
    {
        PrintTest(); //temp
    }

    void PrintTest(){
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

    private void SetInventory(){
        PlayerPrefs.SetInt("NestLevel", 1);
        for (int i = 1; i < Items.Length; i++)
        {
            PlayerPrefs.SetInt(Items[i], 0);        
        }    
    }
    public static void AddCollectable(string str, int num){
        PlayerPrefs.SetInt(str, (PlayerPrefs.GetInt(str) + num));
        AddXP(str, num);
    }

    private static void AddXP(string str, int num){
        if (str == "Fairy1" || str == "Fairy2" || str == "Fairy3") 
        {
            PlayerPrefs.SetInt("XP", (PlayerPrefs.GetInt("XP") + (20*num)));
        }
        else 
        {
            PlayerPrefs.SetInt("XP", (PlayerPrefs.GetInt("XP") + (5*num)));
        }
        if (NestUpgradeNeeded()){
            UpgradeNest();
        }
    }

    //Checks if the nest need to be upgraded based on the XP count. 
    private static bool NestUpgradeNeeded(){
        switch (PlayerPrefs.GetInt("XP"))
        {
            case var n when n < 20: return (PlayerPrefs.GetInt("NestLevel") == 1);
            case var n when n < 50: return (PlayerPrefs.GetInt("NestLevel") == 2);
            case var n when n < 90: return (PlayerPrefs.GetInt("NestLevel") == 3);
            case var n when n < 140: return (PlayerPrefs.GetInt("NestLevel") == 4);
        }
        return (PlayerPrefs.GetInt("NestLevel") == 5);
    }

    private static void UpgradeNest(){
        PlayerPrefs.SetInt("NestLevel", (PlayerPrefs.GetInt("NestLevel") + 1));
        //upgrade nest's sprite
    }
}
