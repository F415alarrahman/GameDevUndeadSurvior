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

    float timer;
    Player player; // Referensi ke komponen Player

    void Awake()
    {
        player = GameManager.Instance.player; // Ambil referensi Player dari GameManager
    }

    void Update()
    {
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;
            default:
                timer += Time.deltaTime; // Tambah timer untuk senjata lain

                if (timer > speed)
                {
                    timer = 0f;
                    Fire();
                }
                break;
        }

        // Tombol lompat = upgrade senjata
        if (Input.GetButtonDown("Jump"))
        {
            LevelUp(10, 1); // Naikkan damage dan count sedikit demi sedikit
        }
    }

    public void Init(ItemData data)
    {
        name = "Weapon" + data.itemID;
        transform.parent = player.transform; // Set parent ke Player
        transform.localPosition = Vector3.zero; // Reset posisi

        id = data.itemID;
        damage = data.baseDamage;
        count = (int)data.baseCount;

        for (int index = 0; index < GameManager.Instance.pool.prefabs.Length; index++)
        {
            if (data.projectile == GameManager.Instance.pool.prefabs[index])
            {
                prefabId = index;
                break;
            }
        }
        switch (id)
        {
            case 0:
                speed = 150;
                Batch();
                break;
            default:
                speed = 0.3f; // Set kecepatan untuk senjata lain
                break;
        }
    }

    public void LevelUp(float damage, int count)
    {
        this.damage += damage;
        this.count += count;

        if (id == 0)
            Batch();
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

            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero);
        }

        // Nonaktifkan peluru sisa jika count berkurang
        for (int i = count; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    void Fire()
    {
        if (!player.scanner.nearestTarget)

            return; // Tidak ada target terdekat

        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        Transform bullet = GameManager.Instance.pool.Get(prefabId).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(damage, count, dir);
    }
}
