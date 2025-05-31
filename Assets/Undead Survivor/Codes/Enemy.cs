using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float speed;
    public Rigidbody2D target;

    bool isLive = true;
    Rigidbody2D rigit;
    SpriteRenderer spriter;
    // Start is called before the first frame update
    void Start()
    {
        rigit = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (!isLive)
            return;


        Vector2 dirVec = target.position - rigit.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigit.MovePosition(rigit.position + nextVec);
        rigit.velocity = Vector2.zero;
    }


    void LateUpdate()
    {
        if (!isLive)
            return;

        spriter.flipX = target.position.x < rigit.position.x;
    }

    void OnEnable()
    {
        target = GameManager.Instance.player.GetComponent<Rigidbody2D>();
    }
}
