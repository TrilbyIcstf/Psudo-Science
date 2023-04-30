using UnityEngine;

public abstract class Particle_Dad : MonoBehaviour
{
    // The script of the particle controller that spawned this
    protected Particle_Controller_Dad father;

    // Marks where the particle spawned
    protected Vector2 spawnPosition;

    // The position the particle will move towards
    protected Vector2 goalPosition;

    // The distance from the target the particle will be destroyed at
    protected float targetDistance = 1;

    // Variables tracking the particle's movement
    [SerializeField]
    protected Vector2 moveDirection;
    protected float moveSpeed = 0;
    protected float moveAccel = 0;

    // Variables to track the particle's turning speed
    protected float turnSpeed = 0;

    // Allows for the particle's movement and logic to be paused
    protected bool inMotion = false;

    // Marks the time in seconds the particle has been active
    protected double age = 0;

    // Marks how long (in seconds) the particle can live for before despawning
    protected float lifeSpan = 0;

    /// <summary>
    /// Called to initialize the particle and start its movement
    /// </summary>
    /// <param name="goal">
    /// The <X,Y> coords of the particle's goal
    /// </param>
    /// <param name="startSpeed">
    /// The speed the particle begins with
    /// </param>
    public virtual void ParticleInitialize(Vector2 goal, float startSpeed, Particle_Controller_Dad papa)
    {
        father = papa;
        spawnPosition = transform.position;
        goalPosition = goal;
        moveSpeed = startSpeed;
        inMotion = true;
        father.AddParticle(gameObject);
    }

    /// <summary>
    /// Called to initialize the particle and start its movement
    /// </summary>
    /// <param name="goal">
    /// The <X,Y> coords of the particle's goal
    /// </param>
    /// <param name="startSpeed">
    /// The speed the particle begins with
    /// </param>
    /// <param name="startAccel">
    /// The acceleration the particle begins with
    /// </param>
    /// <param name="startDirection">
    /// The direction the particle should begin moving in
    /// </param>
    /// <param name="startTurnSpeed">
    /// The strength of turning the particle begins with
    /// </param>
    /// <param name="targetDist">
    /// The distance from the target the particle reaches EOL at
    /// </param>
    public virtual void ParticleInitialize(Vector2 goal, float startSpeed, float startAccel, Vector2 startDirection, float startTurnSpeed, float targetDist, Particle_Controller_Dad papa)
    {
        ParticleInitialize(goal, startSpeed, papa);
        moveAccel = startAccel;
        moveDirection = startDirection;
        turnSpeed = startTurnSpeed;
        targetDistance = targetDist;
    }

    // Update is called once per frame
    protected virtual void FixedUpdate()
    {
        if (inMotion)
        {
            if (ParticleEOL())
            {
                ParticleDestroy();
            }

            ParticleUpdate();
        }
    }

    // Abstract method to be called every frame. Should handle movement logic.
    protected abstract void ParticleUpdate();
    // Abstract method to determine if the particle has hit its End Of Life and should expire.
    protected abstract bool ParticleEOL();
    // Abstract method to be called once a particle expires.
    protected abstract void ParticleDestroy();

    public bool InMotion { get => inMotion; set => inMotion = value; }
    public Vector2 GoalPosition { get => goalPosition; set => goalPosition = value; }
}
