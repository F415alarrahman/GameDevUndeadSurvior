using UnityEngine;
using System.Collections;

public class Reposition : MonoBehaviour
{
    Collider2D coll;
    void Awake()
    {
        coll = GetComponent<Collider2D>();
    }
    private bool hasRepositioned = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Area") || hasRepositioned)
            return;

        Vector3 playerPos = GameManager.Instance.player.transform.position;
        Vector3 myPos = transform.position;
        Vector3 playerDir = GameManager.Instance.player.inputVec;

        // Skip kalau player diam
        if (playerDir == Vector3.zero) return;

        float diffX = Mathf.Abs(playerPos.x - myPos.x);
        float diffY = Mathf.Abs(playerPos.y - myPos.y);

        // Skip kalau belum terlalu jauh
        if (diffX < 15 && diffY < 15) return;

        float dirX = playerDir.x < 0 ? -1 : 1;
        float dirY = playerDir.y < 0 ? -1 : 1;

        switch (transform.tag)
        {
            case "Ground":
                hasRepositioned = true;

                if (diffX > diffY)
                    transform.Translate(Vector3.right * dirX * 40);
                else
                    transform.Translate(Vector3.up * dirY * 40);

                StartCoroutine(ResetFlag());
                break;
            case "Enemy":
                if (coll.enabled)
                {
                    transform.Translate(playerDir * 20 + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0f));
                }
                break;
        }
    }

    IEnumerator ResetFlag()
    {
        yield return new WaitForSeconds(0.3f);
        hasRepositioned = false;
    }
}
