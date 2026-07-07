using UnityEngine;

public class Combat_Move_Button_Controller : MonoBehaviour
{
    private const float HORIZONTALPADDING = 2.25f;
    private const float VERTICALPADDING = 0.75f;

    [SerializeField]
    private Direction facing;

    [SerializeField]
    private GameObject baseMoveButton;

    private Combat_Move_Button[] activeButtons;

    // Convenience var
    private Player_UI_Info playerInfo { get => GetComponent<Player_UI_Info>(); }

    public void Init(MoveName[] moves)
    {
        activeButtons = new Combat_Move_Button[moves.Length];

        for (int i = 0; i < moves.Length; i++)
        {
            CreateButton(i);
        }

        SetMoves(moves);
        SetHighlight(0);
    }

    public void SetMoves(MoveName[] moves)
    {
        for (int i = 0; i < moves.Length; i++)
        {
            activeButtons[i].SetDetails(playerInfo.Player, moves[i], i);
        }
    }

    public void SetHighlight(int pos)
    {
        for (int i = 0; i < activeButtons.Length; i++)
        {
            activeButtons[i].SetHighlight(i == pos);
        }
    }

    private void CreateButton(int pos)
    {
        Vector3 spawnPos = transform.position;
        spawnPos.x += HORIZONTALPADDING * facing.NumericRepresentation();
        spawnPos.y += VERTICALPADDING * (1 - pos);

        GameObject tempButton = Instantiate(baseMoveButton, transform);
        tempButton.transform.position = spawnPos;
        activeButtons[pos] = tempButton.GetComponent<Combat_Move_Button>();
    }
}
