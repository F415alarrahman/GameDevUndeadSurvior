using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;    // Start is called before the first frame update

    Rigidbody2D rigid; // Rigidbody component for physics
    SpriteRenderer spriter; // Sprite renderer for visual representation
    Animator anim;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
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
        anim.SetFloat("Speed", inputVec.magnitude);
        

        if (inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0;
        }
    }
}
