using System.Collections.Generic;
using UnityEngine;

public class Share_Energy_Move : Player_Move
{
    List<(Vector2Int pos, TColor color)> resultList = new List<(Vector2Int pos, TColor color)>();

    public override float? DelayOverride { 
        get { return resultList.Count == 0 ? 0.0f : null; } 
    }

    public override bool ApplyMove(Player_Information pi, List<MoveResult> results, Move_Information mi)
    {
        foreach((Vector2Int pos, TColor color) result in resultList)
        {
            GameManager.instance.combat.board.GetTileScript(result.pos).SetColor(result.color);
        }

        if (resultList.Count > 0)
        {
            Combat_Commands.BoardChanged();
        }

        return true;
    }

    public override void EndMove(int user) { }

    public override bool IsMoveFinished()
    {
        return moveStarted && particleControllerList.Count <= 0;
    }

    public override MoveResult TargetCalc(Player_Information pi, int target, Move_Information mi)
    {
        return new MoveResult(0, Target.NULL, pi.position);
    }

    public override List<MoveResult> ResultsCalc(Player_Information pi, int target, Move_Information mi)
    {
        TColor pColor = TColorExtensions.FromPos(pi.position);
        List<Vector2Int> tilePos = GameManager.instance.combat.board.GetAllTilePosOfColor(pColor);

        List<TColor> colorList = new List<TColor> { TColor.BLUE, TColor.ORANGE, TColor.PINK, TColor.PURPLE };
        colorList.Remove(pColor);

        foreach (Vector2Int pos in tilePos)
        {
            int randInt = Random.Range(0, colorList.Count);
            TColor newColor = colorList[randInt];

            resultList.Add((pos, newColor));
        }

        return new List<MoveResult> { TargetCalc(pi, target, mi) };
    }

    public override void StartMove(int user, List<MoveResult> results)
    {
        Transform playerPos = Combat_UI_Commands.GetPlayerPosition(user);
        GameObject tempParticleController = Instantiate(mainParticleController, playerPos.position, Quaternion.identity);
        tempParticleController.GetComponent<Board_Effect_Particle_Controller>().Setup(resultList, this, results);
        GameManager.instance.fx.AddParticleManager(tempParticleController);
        moveStarted = true;
    }
}
