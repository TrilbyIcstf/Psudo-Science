using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combat_UI : MonoBehaviour
{
    [Header("Player UI")]
    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;
    [Header("")]
    public GameObject player1Health;
    public GameObject player2Health;
    public GameObject player3Health;
    public GameObject player4Health;
    [Header("")]
    public GameObject player1Energy;
    public GameObject player2Energy;
    public GameObject player3Energy;
    public GameObject player4Energy;
    [Header("Crosshair")]
    public Enemy_Crosshair crosshairScript;
    public Hover_Crosshair hoverScript;

    public List<GameObject> Players => new List<GameObject> { player1, player2, player3, player4 };
    public List<GameObject> PlayerHealths => new List<GameObject> { player1Health, player2Health, player3Health, player4Health };
    public List<GameObject> PlayerEnergies => new List<GameObject> { player1Energy, player2Energy, player3Energy, player4Energy };


    [SerializeField]
    private List<Player_UI_Controller> playerUI = new List<Player_UI_Controller>();
    public List<Player_UI_Controller> PlayerUI { get => playerUI; }

    private List<Combat_Move_Button_Controller> moveButtonControllers = new List<Combat_Move_Button_Controller>();

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.combat.SetCombatUI(this);
        List<GameObject> tempPlayers = Players;

        for (int i = 0; i < tempPlayers.Count; i++)
        {
            moveButtonControllers.Add(tempPlayers[i].GetComponent<Combat_Move_Button_Controller>());
        }
    }

    public void TargetCrosshair(Vector2 target)
    {
        crosshairScript.TargetCrosshair(target);
    }

    public void SetCrosshairEnabled(bool val)
    {
        crosshairScript.SetCrosshairEnabled(val);
    }

    public void HoverCrosshair(Vector2 target)
    {
        hoverScript.TargetCrosshair(target);
    }

    public void SetHoverEnabled(bool val)
    {
        hoverScript.SetCrosshairEnabled(val);
    }

    public void SetupMoveButtons(MoveName[][] teamMoves)
    {
        for (int i = 0; i < teamMoves.Length; i++)
        {
            moveButtonControllers[i].Init(teamMoves[i]);
        }
    }

    public void HighlightMoveButton(PC pc, int pos)
    {
        moveButtonControllers[(int)pc].SetHighlight(pos);
    }
}
