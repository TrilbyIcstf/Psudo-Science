using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    [SerializeField]
    private Dungeon_Layout dungeonLayout;
    private Dungeon_Board_Controller board;
    private Vector2Int pos = Vector2Int.zero;
    private DoorConnection previousTransition;

    private SavedBoardState? savedBoard;

    public Dungeon_Layout DungeonLayout { get => dungeonLayout; set => dungeonLayout = value; }
    public Dungeon_Board_Controller Board { get => board; }
    public Vector2Int Pos { get => pos; set => pos = value; }
    public SavedBoardState SavedBoard { set => savedBoard = value; }

    private bool TESTMAPSETUP = false;

    public void BeginDungeon()
    {
        pos = dungeonLayout.StartingBoard;
        previousTransition = new DoorConnection(pos, dungeonLayout.StartingPos);
    }

    public void SetupDungeonBoard(Dungeon_Board_Controller board)
    {
        if (!TESTMAPSETUP)
        {
            BeginDungeon();
            TESTMAPSETUP = true;
        }
        this.board = board;
        Dungeon_Board_Layout layout = dungeonLayout.Layout.GetValue(Pos);

        if (savedBoard is SavedBoardState boardState)
        {
            board.ResumeSetup(layout, boardState);
            savedBoard = null;
        } else
        {
            board.FreshSetup(layout, previousTransition.ExitPos);
        }
    }

    public void MoveThroughDoor(DoorConnection dc)
    {
        previousTransition = dc;
        pos = dc.MapPos;
        GameManager.instance.transition.TransitionToNewRoom();
    }
}
