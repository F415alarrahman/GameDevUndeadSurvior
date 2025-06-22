using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;    // Start is called before the first frame update
    public Scanner scanner; // Scanner component to find targets
    public Hand[] hands;
    public RuntimeAnimatorController[] animCon; // Animator controllers for different animations

    Rigidbody2D rigid; // Rigidbody component for physics
    SpriteRenderer spriter; // Sprite renderer for visual representation
    Animator anim;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
        hands = GetComponentsInChildren<Hand>(true);
    }

    void OnEnable()
    {
        speed *= Character.Speed; // Adjust speed based on character data
        anim.runtimeAnimatorController = animCon[GameManager.Instance.playerId];
    }

    void Update()
    {
        if (!GameManager.Instance.isLive)
            return;
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        if (!GameManager.Instance.isLive)
            return;
        // Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }

    // void OnMove(InputValue value)
    // {
    //     inputVec = value.Get<Vector2>();
    // }

    void LateUpdate()
    {
        if (!GameManager.Instance.isLive)
            return;
        anim.SetFloat("Speed", inputVec.magnitude);


        if (inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0;
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!GameManager.Instance.isLive)
            return;

        GameManager.Instance.health -= 10;

        if (GameManager.Instance.health < 0)
        {
            for (int index = 2; index < transform.childCount; index++)
            {
                transform.GetChild(index).gameObject.SetActive(false);
            }

            anim.SetTrigger("Dead");
            GameManager.Instance.GameOver();
        }
    }
}
