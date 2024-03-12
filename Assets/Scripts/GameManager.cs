using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    PlayerController player;

    [Header("World Settings")]
    [SerializeField, Min(0f)]
    private float gravity = 20f;

    public static GameManager Instance { get; private set; }
    public PlayerController Player { get => player; }


    public float Gravity { get => -gravity; }

    private void Awake() {
        Instance = this;
    }
}
