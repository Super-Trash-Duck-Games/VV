using UnityEngine;

public  class PersistentData 
{
    public  int levelsUnlocked;

    public bool beachHatUnlocked;
    public bool chefHatUnlocked;
    public bool santaHatUnlocked;
    public bool cupcakeHatUnlocked;
    public bool pumpkinHatUnlocked;
    public bool mariachiHatUnlocked;
    public bool moustacheHatUnlocked;

    public  void SaveData()
    {
        SaveSystem.SaveData(this);
    }

    public  void LoadData()
    {
        SaveData data = SaveSystem.LoadData();

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
