using UnityEngine;

[System.Serializable] //Make viewable in unity editor
public class Item
{
    string mName;
    string mDescription;

    Sprite mItemImage;

    public string Name { get => mName; set => mName = value; }
    public string Description { get => mDescription; set => mDescription = value; }

    public Sprite ItemImage { get => mItemImage; set => mItemImage = value; }
}
