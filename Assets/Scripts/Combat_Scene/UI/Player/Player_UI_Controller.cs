using UnityEngine;

public class Player_UI_Controller : MonoBehaviour
{
    [SerializeField]
    private Player_Animation_Controller anim;
    public Player_Animation_Controller Anim { get => anim; }
}
