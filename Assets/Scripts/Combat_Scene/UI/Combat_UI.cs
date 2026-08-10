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

    private List<Combat_Move_Button_Controller> moveButtonControllers = new List<Combat_Move_Button_Controller>();

    // Start is called before the first frame update
    void Awake()
    {
        GameManager.instance.combat.SetCombatUI(this);

        for (int i = 0; i < playerUI.Count; i++)
        {
            moveButtonControllers.Add(playerUI[i].Buttons);
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

    public void SetBoostAmount(float val)
    {
        boostText.text = "Boost: x" + Math.Round(val, 2);
    }
}
