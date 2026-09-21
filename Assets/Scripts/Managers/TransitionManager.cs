using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    private FXManager _fx;
    private FXManager fx { 
        get
        {
            if (_fx == null)
            {
                _fx = GameManager.instance.fx;
            }
            return _fx;
        }
    }

    public void TransitionToEncounter(Encounter encounter)
    {
        StartCoroutine(TransitionToEncounterCoroutine(encounter));
    }

    private IEnumerator TransitionToEncounterCoroutine(Encounter encounter)
    {
        yield return StartCoroutine(fx.Overlay.PlayScreenCrack());
        yield return SceneManager.LoadSceneAsync("TestBoard");
        GameManager.instance.combat.CombatSetup(encounter);
        yield return StartCoroutine(fx.Overlay.FadeInScreen());
        GameManager.instance.combat.board.MouseLock = false;
    }

    public void TransitionToNewRoom()
    {
        StartCoroutine(TransitionToNewRoomCoroutine());
    }

    private IEnumerator TransitionToNewRoomCoroutine()
    {
        yield return StartCoroutine(fx.Overlay.FadeOutScreen());
        yield return SceneManager.LoadSceneAsync("DungeonBoardBasic");
        yield return StartCoroutine(fx.Overlay.FadeInScreen());
    }
}
