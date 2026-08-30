using UnityEngine;
using TMPro;

public class Move_Details_Box : MonoBehaviour
{
    [SerializeField]
    private MoveName move;

    [SerializeField]
    private TextMeshProUGUI moveName;

    [SerializeField]
    private TextMeshProUGUI description;

    [SerializeField]
    private TextMeshProUGUI potencyText;

    [SerializeField]
    private TextMeshProUGUI manaText;

    public void SetDetails(MoveName move)
    {
        this.move = move;
        UpdateDetails();
    }

    private void UpdateDetails()
    {
        Move_Information moveInfo = GameManager.instance.ll.moveRepository.GetInformation(move);

        moveName.text = moveInfo.MoveName;
        description.text = moveInfo.Description;
        potencyText.text = "Potency: " + moveInfo.Potency;
        manaText.text = "Mana: " + moveInfo.ManaCost;
    }
}
