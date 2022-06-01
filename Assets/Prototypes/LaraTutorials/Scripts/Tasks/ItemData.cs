using UnityEngine;

public enum ItemType
{
    Collectable,
}

[System.Serializable]
public class ItemData
{
    #region Private Variables
    private int id;
    [SerializeField] private string name;
    [SerializeField, TextArea(5,5)] private string description;
    [SerializeField] private int value;
    [SerializeField] private int amount;
    [SerializeField] private Sprite icon;
    [SerializeField] private GameObject mesh;
    [SerializeField] private ItemType type;
    #endregion

    #region Public Properties
    public int ID //allowing id to be modified outside script
    {
        get { return id; }
        set { id = value; }
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public string Description
    {
        get { return description; }
        set { description = value; }
    }

    public int Value
    {
        get { return value; }
        set { this.value = value; }
    }

    public int Amount
    {
        get { return amount; }
        set { amount = value; }
    }

    public Sprite Icon
    {
        get { return icon; }
        set { icon = value; }
    }

    public GameObject Mesh
    {
        get { return mesh; }
        set { mesh = value; }
    }

    public ItemType Type
    {
        get { return type; }
        set { type = value; }
    }
    
    #endregion

    public static ItemData CreateItem(int itemID)
    {
        string description = "";
        string name = "";
        int value = 0;
        int amount = 0;
        string icon = "";
        string mesh = "";
        ItemType type = ItemType.Collectable;

        switch (itemID) //Keys on keyboard
        {
            case 0:
                name = "Fuse";
                description = "Gives electricity to fuse box";
                value = 1;
                amount = 1;
                icon = "";
                mesh = "";
                type = ItemType.Collectable;
                break;
        }

        ItemData temp = new ItemData
        {
            ID = itemID,
            Name = name,
            Description = description,
            Value = value,
            Amount = amount,
            Type = type,
            Icon = Resources.Load("Icon/" + icon) as Sprite,
            Mesh = Resources.Load("Mesh/" + mesh) as GameObject,
        };
        return temp;
    }
}

