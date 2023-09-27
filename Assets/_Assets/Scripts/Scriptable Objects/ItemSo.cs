using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Pickable Item", fileName = "new item")]
public class ItemSo : ScriptableObject
{
    public Sprite uiSprite;
    public GameObject itemPrefab;
    public string itemText;
}
