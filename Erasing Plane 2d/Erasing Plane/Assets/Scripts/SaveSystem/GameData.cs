using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int totalCoins;    

    public bool[] skinsUnlocked;
    public bool[] backgroundUnlocked;
    public bool[] musicUnlocked;

    public int activeSkinIndex;
    public int activeBackgroundIndex;

    public int islandLevel;
    public int requiredBricks;
    public int totalBricks;
    public int requiredWood;
    public int totaledWood;
    public int requiredConcrete;
    public int totalConcrete;

    public bool isTutorialEnd;

    public GameData() 
    {
        totalBricks = 3;
        totaledWood = 5;
        totalConcrete = 2;

        requiredBricks = 3;
        requiredConcrete = 2;
        requiredWood = 5;

        totalCoins = 220;
          
        
        skinsUnlocked = new bool[5];
        skinsUnlocked[0] = true;

        backgroundUnlocked = new bool[5];
        backgroundUnlocked[0] = true;

        musicUnlocked = new bool[5];       

        activeSkinIndex = 0;
        activeBackgroundIndex = 0;
        islandLevel = 1;

        isTutorialEnd = false;
    }
  
}
