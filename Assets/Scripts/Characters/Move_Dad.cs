using System.Collections.Generic;
using UnityEngine;

public abstract class Move_Dad : MonoBehaviour
{
    protected List<Particle_Controller_Dad> particleControllerList = new List<Particle_Controller_Dad>();

    [SerializeField]
    protected GameObject mainParticleController;

    protected bool moveStarted = false;

    public virtual float? DelayOverride { get; } = null;

    // Section for handling animations and particles
    public abstract void StartMove(int user, List<MoveResult> results);
    public abstract void EndMove(int user);
    public abstract bool IsMoveFinished();

    public void AddController(Particle_Controller_Dad newController)
    {
        particleControllerList.Add(newController);
    }

    public bool RemoveController(Particle_Controller_Dad deadController)
    {
        particleControllerList.Remove(deadController);
        return particleControllerList.Count <= 0;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    protected int BasicDamageCalc(float potency, int offense, int defense, Effectiveness effectiveness, Target userType)
    {
        return DamageCalc(potency, offense, 2, defense, 0.5f, effectiveness, userType);
    }

    protected int DamageCalc(float potency, int offense, float offenseRatio, int defense, float defenseRatio, Effectiveness effectiveness, Target userType)
    {
        float result = offense * offenseRatio;
        result = result - (defense * defenseRatio);
        result = result * potency;
        result = ApplyEffectiveness(result, effectiveness);

        if (userType == Target.PC)
        {
            result = result * Combat_Commands.GetBoost();
        }

        result = Mathf.Max(result, 1);

        return Mathf.CeilToInt(result);
    }

    protected float ApplyEffectiveness(float result, Effectiveness effectiveness)
    {
        switch(effectiveness)
        {
            case Effectiveness.WEAKNESS:
                return result * 1.5f;
            case Effectiveness.STRENGTH:
                return result * 0.75f;
            case Effectiveness.NEUTRAL:
            default:
                return result;
        }
    }
}
