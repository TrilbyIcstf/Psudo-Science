using UnityEngine;

public class Combat_Move_Button_Controller : MonoBehaviour
{
    private const float HORIZONTALPADDING = 2.25f;
    private const float VERTICALPADDING = 0.75f;

    [SerializeField]
    private Direction facing;

    [SerializeField]
    private GameObject baseMoveButton;

    private Combat_Move_Button[] activeButtons = new Combat_Move_Button[3];

    public void Init(MoveName[] moves, int startPos)
    {
        startPos = Mathf.Min(startPos, moves.Length - 1);

        for (int i = 0; i < moves.Length; i++)
        {
            CreateButton(i);
            activeButtons[i].SetHighlight(i == startPos);
        }

        SetMoves(moves);
    }

    public void SetMoves(MoveName[] moves)
    {
        for (int i = 0; i < moves.Length; i++)
        {

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
