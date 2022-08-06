using UnityEngine;

public abstract class Particle_Dad : MonoBehaviour
{
    // Marks where the particle spawned
    protected Vector2 spawnPosition;

    // The position the particle will move towards
    protected Vector2 goalPosition;

    // Variables tracking the particle's movement
    [SerializeField]
    protected Vector2 moveDirection;
    protected float moveSpeed = 0;
    protected float moveAccel = 0;

    // Variables to track the particle's turning speed
    protected float turnSpeed = 0;

    // Holds how quickly the particle is approaching its goal
    protected float approachVelocity = 0;
    protected float goalDistance = 0;

    // Allows for the particle's movement and logic to be paused
    protected bool inMotion = false;

    /// <summary>
    /// Called to initialize the particle and start its movement
    /// </summary>
    /// <param name="goal">
    /// The <X,Y> coords of the particle's goal
    /// </param>
    /// <param name="startSpeed">
    /// The speed the particle begins with
    /// </param>
    public virtual void ParticleInitialize(Vector2 goal, float startSpeed)
    {
        spawnPosition = transform.position;
        goalPosition = goal;
        goalDistance = Vector2.Distance(spawnPosition,goalPosition);
        moveSpeed = startSpeed;
        inMotion = true;
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
    public virtual void ParticleInitialize(Vector2 goal, float startSpeed, float startAccel, Vector2 startDirection)
    {
        ParticleInitialize(goal, startSpeed);
        moveAccel = startAccel;
        moveDirection = startDirection;
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
