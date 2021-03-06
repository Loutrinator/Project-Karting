﻿using UnityEngine;

/// <summary>
/// Contains a series tunable parameters to tweak various karts for unique driving mechanics.
/// </summary>
[System.Serializable]
public struct Stats
{
    [Header("Movement Settings")]
    [Tooltip("The maximum speed forwards")]
    [Range(0f,1f)]
    public float topSpeed;

    [Tooltip("How quickly the Kart reaches top speed.")]
    [Range(0f,1f)]
    public float acceleration;

    [Tooltip("The maximum speed backward.")]
    [Range(0f,1f)]
    public float reverseSpeed;

    [Tooltip("The rate at which the kart increases its backward speed.")]
    [Range(0f,1f)]
    public float reverseAcceleration;

    [Tooltip("How quickly the Kart slows down when going in the opposite direction.")]
    [Range(0f,1f)]
    public float braking;

    [Tooltip("How quickly the Kart can turn left and right.")]
    [Range(0f,1f)]
    public float steer;

    [Tooltip("Additional gravity for when the Kart is in the air.")]
    [Range(0f,1f)]
    public float addedGravity;

    [Tooltip("How much the Kart tries to keep going forward when on bumpy terrain.")]
    [Range(0f,1f)]
    public float suspension;

    // allow for stat adding for powerups.
    public static Stats operator +(Stats a, Stats b)
    {
        return new Stats
        {
            acceleration        = a.acceleration + b.acceleration,
            braking             = a.braking + b.braking,
            addedGravity        = a.addedGravity + b.addedGravity,
            reverseAcceleration = a.reverseAcceleration + b.reverseAcceleration,
            reverseSpeed        = a.reverseSpeed + b.reverseSpeed,
            topSpeed            = a.topSpeed + b.topSpeed,
            steer               = a.steer + b.steer,
            suspension          = a.suspension + b.suspension
        };
    }
    public static Stats operator *(Stats a, float c)
    {
        return new Stats
        {
            acceleration        = a.acceleration * c,
            braking             = a.braking * c,
            addedGravity        = a.addedGravity * c,
            reverseAcceleration = a.reverseAcceleration * c,
            reverseSpeed        = a.reverseSpeed * c,
            topSpeed            = a.topSpeed * c,
            steer               = a.steer * c,
            suspension          = a.suspension * c
        };
    }
}
