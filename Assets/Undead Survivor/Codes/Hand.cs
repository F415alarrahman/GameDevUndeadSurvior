using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public bool isLeft;
    public SpriteRenderer spriter;

    SpriteRenderer player;

    Vector3 leftPos = new Vector3(-0.17f, -0.38f, 0);
    Vector3 leftPosReverse = new Vector3(0.17f, -0.38f, 0);
    Quaternion leftRot = Quaternion.Euler(0, 0, -35);
    Quaternion leftRotReverse = Quaternion.Euler(0, 0, -135);

    Vector3 rightPos = new Vector3(0.35f, -0.15f, 0);
    Vector3 rightPosReverse = new Vector3(-0.35f, -0.15f, 0);

    void Awake()
    {
        player = GetComponentsInParent<SpriteRenderer>()[1];
    }

    void LateUpdate()
    {
        bool isReverse = player.flipX;

        if (isLeft)
        {
            transform.localPosition = isReverse ? leftPosReverse : leftPos;
            transform.localRotation = isReverse ? leftRotReverse : leftRot;
            spriter.flipY = isReverse;
            spriter.sortingOrder = isReverse ? 4 : 6;
        }
        else
        {
            // Tangan kanan tidak di-rotate! Hanya flip posisi dan flipY
            transform.localPosition = isReverse ? rightPosReverse : rightPos;
            transform.localRotation = Quaternion.identity;
            spriter.flipY = isReverse;
            spriter.sortingOrder = isReverse ? 6 : 4;
        }
    }

}
