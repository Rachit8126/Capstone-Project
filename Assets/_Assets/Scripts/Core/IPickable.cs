using UnityEngine;

public interface IPickable
{
    public void Pickup(Transform pickupParent);
    public ItemSo GetItemSo();
}
