using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacoonItemTye : MonoBehaviour, IItemType
{
    [SerializeField] [Range(0, 10)] private float pickUpSpeedModifier;
    public float GetItemPickUpSpeedModifier()
    {
        return pickUpSpeedModifier;
    }

    public void SetBePickedUp(Transform parent)
    {
        GetComponent<RacoonMovement>().SetBePickedUp(parent);
    }
}
