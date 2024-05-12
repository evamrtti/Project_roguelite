using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpeedChange : MonoBehaviour
{
    public int speedDif;
    private KnightMovement playerSpeed;

    void Start()
    {
        ChangeSpeed();
    }

    void ChangeSpeed()
    {
        playerSpeed = GetComponentInParent<KnightMovement>();
        playerSpeed.moveSpeed += speedDif;
    }
}
