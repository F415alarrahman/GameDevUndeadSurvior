using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public static float Speed
    {
        get
        {
            return GameManager.Instance.playerId == 0 ? 1.1f : 1f;
        }
    }

    public static float WeaponSpeed
    {
        get
        {
            return GameManager.Instance.playerId == 0 ? 1.1f : 1f;
        }
    }

    public static float WeaponRate
    {
        get
        {
            return GameManager.Instance.playerId == 0 ? 0.9f : 1f;
        }
    }

    public static float Damage
    {
        get
        {
            return GameManager.Instance.playerId == 0 ? 1.2f : 1f;
        }
    }

    public static int Count
    {
        get
        {
            return GameManager.Instance.playerId == 3 ? 1 : 0;
        }
    }
}
