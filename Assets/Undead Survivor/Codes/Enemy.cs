using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float speed;
    public float health;
    public float maxHealth;

    public RuntimeAnimatorController[] animCon;
    public Rigidbody2D target;

    bool isLive;
    Rigidbody2D rigit;
    Animator anim;
    SpriteRenderer spriter;
    // Start is called before the first frame update
    void Start()
    {
        rigit = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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
        isLive = true;
        health = maxHealth;
    }

    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet"))
            return;
        health -= collision.GetComponent<Bullet>().damage;
        if (health > 0)
        {
        }
        else
        {
            Dead();
        }
    }

    void Dead()
    {
        gameObject.SetActive(false);
    }
}
