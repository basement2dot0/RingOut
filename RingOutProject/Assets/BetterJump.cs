using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterJump : MonoBehaviour {
    [SerializeField]
    [Range(0, 100)]
    private float jumpVelocity;

    private float fallMultiplyer;
    private float lowJumpMultiplyer;

    private void Awake()
    {
        fallMultiplyer = 25.0f - 1.0f;
        lowJumpMultiplyer = 2.0f;
    }

    private void Update()
    {

    }
}
