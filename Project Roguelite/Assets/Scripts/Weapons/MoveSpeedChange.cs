using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpeedChange : MonoBehaviour
{
    public int speedDif;
    private MonoBehaviour playerSpeed;

    void Start()
    {
        ChangeSpeed();
    }

    void ChangeSpeed()
    {
        if (GetComponentInParent<KnightMovement>() != null)
        {
            playerSpeed = GetComponentInParent<KnightMovement>();
        }
        else if (GetComponentInParent<RogueMovement>() != null)
        {
            playerSpeed = GetComponentInParent<RogueMovement>();
        }
        else if (GetComponentInParent<SorcererMovement>() != null)
        {
            playerSpeed = GetComponentInParent<SorcererMovement>();
        }

        if (playerSpeed != null)
        {
            if (playerSpeed is KnightMovement knightMovement)
            {
                knightMovement.moveSpeed += speedDif;
            }
            else if (playerSpeed is RogueMovement rogueMovement)
            {
                rogueMovement.moveSpeed += speedDif;
            }
            else if (playerSpeed is SorcererMovement sorcererMovement)
            {
                sorcererMovement.moveSpeed += speedDif;
            }
        }
    }
}
