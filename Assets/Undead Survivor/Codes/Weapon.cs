using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;

    void Start()
    {
        Init(damage, count); // Ambil nilai awal dari Inspector
    }

    void Update()
    {
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;
            default:
                break;
        }

        // Tombol lompat = upgrade senjata
        if (Input.GetButtonDown("Jump"))
        {
            LevelUp(5, 1); // Naikkan damage dan count sedikit demi sedikit
        }
    }

    public void Init(float damage, int count)
    {
        this.damage = damage;
        this.count = count;

        switch (id)
        {
            case 0:
                speed = 150;
                Batch();
                break;
            default:
                break;
        }
    }

    public void LevelUp(float damageIncrease, int countIncrease)
    {
        damage += damageIncrease;
        count += countIncrease;

        switch (id)
        {
            case 0:
                Batch();
                break;
            default:
                break;
        }
    }

    void Batch()
    {
        for (int index = 0; index < count; index++)
        {
            Transform bullet;

            if (index < transform.childCount)
            {
                bullet = transform.GetChild(index);
                bullet.gameObject.SetActive(true); // Re-activate if reused
            }
            else
            {
                bullet = GameManager.Instance.pool.Get(prefabId).transform;
                bullet.parent = transform;
            }

            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            float angle = 360f * index / count;
            bullet.Rotate(Vector3.forward * angle);
            bullet.Translate(Vector3.up * 1.5f, Space.Self);

            bullet.GetComponent<Bullet>().Init(damage, -1);
        }

        // Nonaktifkan peluru sisa jika count berkurang
        for (int i = count; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
