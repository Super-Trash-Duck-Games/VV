using UnityEngine;
using System;

[Serializable]
public class SaveData
{
    public int levelsUnlocked;
    public bool beachHatUnlocked;
    public bool chefHatUnlocked;
    public bool santaHatUnlocked;
    public bool cupcakeHatUnlocked;
    public bool pumpkinHatUnlocked;
    public bool mariachiHatUnlocked;
    public bool moustacheHatUnlocked;

    public SaveData(PersistentData data)
    {
        levelsUnlocked = data.levelsUnlocked;

        beachHatUnlocked = data.beachHatUnlocked;
        chefHatUnlocked = data.chefHatUnlocked;
        santaHatUnlocked = data.santaHatUnlocked;
        cupcakeHatUnlocked = data.cupcakeHatUnlocked;
        pumpkinHatUnlocked = data.pumpkinHatUnlocked;
        mariachiHatUnlocked = data.mariachiHatUnlocked;
        moustacheHatUnlocked = data.moustacheHatUnlocked;
    }
}
