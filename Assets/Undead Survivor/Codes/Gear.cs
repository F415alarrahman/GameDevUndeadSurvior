using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    public ItemData.ItemType type;
    public float rate;

    public void Init(ItemData data)
    {
        name = "Gear " + data.itemID;
        transform.parent = GameManager.Instance.player.transform;
        transform.localPosition = Vector3.zero; // Reset posisi

        type = data.itemType;
        rate = data.damages[0];
        ApplyGear(); // Terapkan gear saat inisialisasi
    }


    public void LevelUp(float rate)
    {
        this.rate = rate;
        ApplyGear(); 
    }

    public void ApplyGear()
    {
        switch (type)
        {
            case ItemData.ItemType.Glove:
                RateUp(); // Naikkan rate untuk glove
                break;
            case ItemData.ItemType.Shoe:
                SpeedUp(); // Naikkan rate untuk shoe
                break;
        }
    }

    void RateUp()
    {
        Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();

        foreach (Weapon weapon in weapons)
        {
            switch (weapon.id)
            {
                case 0: // Gear khusus untuk senjata melee
                    float speed = 150 * Character.WeaponSpeed; // Kecepatan dasar senjata melee
                    weapon.speed = speed + (speed * rate);
                    break;
                default:
                    speed = 0.5f * Character.WeaponSpeed; // Kecepatan dasar senjata range
                    weapon.speed = speed + (1f * rate);
                    break;

            }
        }
    }

    void SpeedUp()
    {
        float speed = 3 * Character.Speed; // Kecepatan dasar karakter
        GameManager.Instance.player.speed = speed + speed * rate;
    }
}
