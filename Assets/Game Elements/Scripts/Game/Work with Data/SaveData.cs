using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData
{
    private int fish;
    private int food;
    private int recordPath;
    private int recordLevel;
    private int health;
    private int armor;
    private int upgradePoints;

    private CatSkinsEnum selectedCatSkinType;
    private List<CatSkinsEnum> openedCatSkinTypes;


    public int Fish
    {
        get => fish;
        set
        {
            if(value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));
            fish = value;
        }
    }

    public int Food
    {
        get => food;
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));
            food = value;
        }
    }

    public int RecordPath
    {
        get => recordPath;
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));
            recordPath = value;
        }
    }

    public int RecordLevel
    {
        get => recordLevel;
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));
            recordLevel = value;
        }
    }

    public int Health
    {
        get => health;
        set
        {
            if (value < 1)
                throw new ArgumentOutOfRangeException(nameof(value));
            health = value;
        }
    }

    public int Armor
    {
        get => armor;
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));
            armor = value;
        }
    }

    public int UpgradePoints
    {
        get => upgradePoints;
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));
            upgradePoints = value;
        }
    }

    public CatSkinsEnum SelectedCatSkinType
    {
        get => selectedCatSkinType;
        set
        {
            if (openedCatSkinTypes.Contains(value) == false)
                throw new ArgumentException(nameof(value));

            selectedCatSkinType = value;
        }
    }

    public IEnumerable<CatSkinsEnum> OpenedCatSkinTypes => openedCatSkinTypes;

    [JsonConstructor]
    public SaveData(int fish, int food, int recordPath, int recordLevel, int health, int armor, int upgradePoints, CatSkinsEnum selectedCatSkinType, List<CatSkinsEnum> openedCatSkinTypes)
    {
        Fish = fish;
        Food = food;
        RecordPath = recordPath;
        RecordLevel = recordLevel;
        Health = health;
        Armor = armor;
        UpgradePoints = upgradePoints;
        if (openedCatSkinTypes != null)
        {
            this.openedCatSkinTypes = new List<CatSkinsEnum>(openedCatSkinTypes);
            SelectedCatSkinType = selectedCatSkinType;
        }
        else
        {
            this.openedCatSkinTypes = new List<CatSkinsEnum>() { CatSkinsEnum.ginger };
            selectedCatSkinType = CatSkinsEnum.ginger;
        }
    }

    public SaveData()
    {
        fish = 0;
        food = 0;
        recordPath = 0;
        recordLevel = 0;
        health = 5;
        armor = 0;
        upgradePoints = 0;
        openedCatSkinTypes = new List<CatSkinsEnum>() { CatSkinsEnum.ginger };
        selectedCatSkinType = CatSkinsEnum.ginger;

    }


    public void OpenCatSkin(CatSkinsEnum skin)
    {
        if(openedCatSkinTypes.Contains(skin))
            throw new ArgumentException(nameof(skin));

        openedCatSkinTypes.Add(skin);
    }

}
