using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneBehavior : EnemyBehavior
{
    // Override the Initialize method to set the target
    protected override void Initialize()
    {
        // Find the player with the tag "Player"
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        // Assign the player's transform to the target variable
        target = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            MoveToTarget(target);
        }
    }
}