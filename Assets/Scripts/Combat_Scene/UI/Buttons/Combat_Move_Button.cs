using TMPro;
using UnityEngine;

public class Combat_Move_Button : MonoBehaviour
{
    private PC player;
    private MoveName move;
    private int pos;

    [SerializeField]
    private GameObject highlight;
    [SerializeField]
    private TextMeshProUGUI moveName;

    public void OnClick()
    {
        GameManager.instance.combat.SelectMove(player, move, pos);
    }

    public void SetDetails(PC player, MoveName move, int pos)
    {
        this.player = player;
        this.move = move;
        this.pos = pos;

        Move_Information moveInfo = GameManager.instance.ll.moveRepository.GetInformation(move);
        moveName.text = moveInfo.MoveName;
    }

    public void SetHighlight(bool val)
    {
        highlight.SetActive(val);
    }
}
