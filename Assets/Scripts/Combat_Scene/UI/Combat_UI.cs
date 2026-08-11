using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combat_UI : MonoBehaviour
{
    [Header("Crosshair")]
    public Enemy_Crosshair crosshairScript;
    public Hover_Crosshair hoverScript;

    [Header("Text")]
    [SerializeField]
    private Text boostText;

    [Header("Players")]
    [SerializeField]
    private List<Player_UI_Controller> playerUI = new List<Player_UI_Controller>();
    public List<Player_UI_Controller> PlayerUI { get => playerUI; }

    private Dictionary<PC, Combat_Move_Button_Controller> moveButtonControllers = new Dictionary<PC, Combat_Move_Button_Controller>();

    // Start is called before the first frame update
    void Awake()
    {
        GameManager.instance.combat.SetCombatUI(this);
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

    public void SetButtonController(PC player, Combat_Move_Button_Controller controller)
    {
        moveButtonControllers.Add(player, controller);
    }

    public void SetupMoveButtons(MoveName[][] teamMoves)
    {
        for (int i = 0; i < teamMoves.Length; i++)
        {
            moveButtonControllers[(PC)i].Init(teamMoves[i]);
        }
    }

    public void HighlightMoveButton(PC pc, int pos)
    {
        moveButtonControllers[pc].SetHighlight(pos);
    }

    public void SetBoostAmount(float val)
    {
        boostText.text = "Boost: x" + Math.Round(val, 2);
    }
}
