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
    public CombatManager combat;

    [Header("SceneManagement")]
    [SerializeField] private GameState currentState = GameState.DUNGEON;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Get/Set
    public GameState CurrentState { get => currentState; set => currentState = value; }
}
