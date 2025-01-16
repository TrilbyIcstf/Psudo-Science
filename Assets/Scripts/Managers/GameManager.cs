using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance
    {
        get { return GameManager._instance != null ? GameManager._instance : (GameManager)FindObjectOfType(typeof(GameManager)); }
        set { GameManager._instance = value; }
    }
    private static GameManager _instance;

    [Header("Managers")]
    public CombatManager combat; // Handles the combat board, enemies, attack queue, etc.
    public PartyManager party; // Handles party members, equipment, inventory, etc.
    public FXManager fx; // Handles particles, sounds, music, etc.
    public LoreLibrarian ll; // I AM THE LORE LIBRARIAN, I HOLD REFERENCES TO ALL NEEDED PIECES OF EQUIPMENT, ATTACKS, ENEMIES OR ANYTHING ELSE YOU NEED

    [Header("SceneManagement")]
    [SerializeField] private GameState currentState = GameState.DUNGEON;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        GameManager.instance = this;
    }

    public delegate void CallbackFunction();

    // Get/Set
    public GameState CurrentState { get => currentState; set => currentState = value; }
}
