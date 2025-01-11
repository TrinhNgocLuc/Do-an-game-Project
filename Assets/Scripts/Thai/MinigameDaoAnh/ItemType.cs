using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IItemType
{
    public float GetItemPickUpSpeedModifier();
    public void SetBePickedUp(Transform parent);
}
