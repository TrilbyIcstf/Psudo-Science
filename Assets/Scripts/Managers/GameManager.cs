using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance
    {
        get { return (GameManager)FindObjectOfType(typeof(GameManager)); }
    }

    [Header("Managers")]
    public CombatManager combat; // Handles the combat board, enemies, attack queue, etc.
    public PartyManager party; // Handles party members, equipment, inventory, etc.
    public FXManager fx; // Handles particles, sounds, music, etc.

    [Header("SceneManagement")]
    [SerializeField] private GameState currentState = GameState.DUNGEON;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Get/Set
    public GameState CurrentState { get => currentState; set => currentState = value; }
}
