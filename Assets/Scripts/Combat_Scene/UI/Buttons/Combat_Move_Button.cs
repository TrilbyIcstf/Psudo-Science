using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Combat_Move_Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        GameManager.instance.combat.combatUI.DisplayMoveDetails(move);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GameManager.instance.combat.combatUI.HideMoveDetails();
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
