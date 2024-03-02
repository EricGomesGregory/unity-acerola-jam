using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    PlayerController player;

    public static GameManager Instance { get; private set; }
    public PlayerController Player { get => player; }

    private void Awake() {
        Instance = this;
    }
}
