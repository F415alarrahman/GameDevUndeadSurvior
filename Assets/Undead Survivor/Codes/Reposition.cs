using UnityEngine;
using System.Collections;

public class Reposition : MonoBehaviour
{
    Collider2D coll;

    void Awake()
    {
        coll = GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Hanya reposisi jika objek ini adalah Enemy
        if (!CompareTag("Enemy"))
            return;

        // Ambil posisi dan arah pemain
        Vector3 playerPos = GameManager.Instance.player.transform.position;
        Vector3 myPos = transform.position;
        Vector3 playerDir = GameManager.Instance.player.inputVec;

        // Lewati kalau player diam
        if (playerDir == Vector3.zero)
            return;

        float diffX = Mathf.Abs(playerPos.x - myPos.x);
        float diffY = Mathf.Abs(playerPos.y - myPos.y);

        // Lewati kalau jaraknya belum cukup jauh
        if (diffX < 15 && diffY < 15)
            return;

        // Pindahkan musuh menjauh dari pemain dengan sedikit variasi posisi
        if (coll.enabled)
        {
            Vector3 offset = new Vector3(
                Random.Range(-3f, 3f),
                Random.Range(-3f, 3f),
                0f
            );

            transform.Translate(playerDir * 20 + offset);
        }
    }
}
