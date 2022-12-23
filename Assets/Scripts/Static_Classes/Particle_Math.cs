using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Particle_Math
{
    /// <summary>
    /// Checks if a particle's approach velocity will allow it to reach its goal this step, and if not calculates its new approach velocity
    /// and goal distance.
    /// </summary>
    /// <param name="goal">
    /// The target position of the particle.
    /// </param>
    /// <param name="pos">
    /// The current position of the particle.
    /// <param name="targetDistance">
    /// The distance to the target which will count as "close enough."
    /// </param>
    /// </param>
    /// <param name="approachVel">
    /// The distance towards the goal the particle moved in the previous update.
    /// </param>
    /// <param name="goalDist">
    /// The distance between goal and pos at the previous update.
    /// </param>
    /// <returns>
    /// Returns true if the old approach velocity will put the particle at or behind the goal.
    /// </returns>
    public static bool CheckApproach(Vector2 goal, Vector2 pos, float targetDistance, float moveSpeed, Vector2 direction)
    {
        /* Holding on to this makeshift solution in case my maths turn out wrong
        if (approachVel >= goalDist || goalDist <= targetDistance) { return true; }

        float newDist = (goal - pos).magnitude;
        approachVel = goalDist - newDist;
        goalDist = newDist;
        
        return false;
        */

        Vector2 moveVector = direction * moveSpeed;
        Vector2 endPoint = pos + moveVector;
        if (Vector2.Distance(pos, goal) <= targetDistance || Vector2.Distance(endPoint, goal) <= targetDistance) { return true; }

        float posSlope = direction.y / direction.x;
        float goalSlope = -direction.x / direction.y;

        float closestX = ((posSlope * pos.x) - pos.y + ((-goalSlope) * goal.x) + goal.y) / (posSlope - goalSlope);
        float closestY = posSlope * closestX + pos.y - (posSlope * pos.x);
        Vector2 closestPoint = new Vector2(closestX, closestY);

        /* Alternate calculation, more compact but slightly less accurate
        float timeVar = -((moveVector.x * endPoint.x) - (goal.x * moveVector.x) + (moveVector.y * endPoint.y) - (goal.y * moveVector.y)) / (Mathf.Pow(moveVector.x, 2) + Mathf.Pow(moveVector.y, 2));
        Vector2 closestPoint = pos + (moveVector * timeVar);
        */

        if (Vector2.Distance(closestPoint, goal) > targetDistance) { return false; }

        Vector2 posToClose = closestPoint - pos;
        Vector2 endToClose = closestPoint - endPoint;
        if (posToClose.x != 0 && Mathf.Sign(posToClose.x) != Mathf.Sign(endToClose.x)) { return true; }
        if (posToClose.y != 0 && Mathf.Sign(posToClose.y) != Mathf.Sign(endToClose.y)) { return true; }
        return false;
    }

    /// <summary>
    /// Takes the current direction of a particle and the position of its goal and turns it towards that point a small amount.
    /// </summary>
    /// <param name="goal">
    /// The position the particle is moving towards.
    /// </param>
    /// <param name="pos">
    /// The particle's current position.
    /// </param>
    /// <param name="direction">
    /// The direction vector of the particle.
    /// </param>
    /// <param name="strength">
    /// The strength with which the particle turns.
    /// </param>
    /// <returns>
    /// A new direction vector for the particle.
    /// </returns>
    public static Vector2 LerpTowardsPoint(Vector2 goal, Vector2 pos, Vector2 direction, float strength)
    {
        Vector2 toPoint = (goal - pos).normalized;
        direction = direction.normalized;
        float toAngle = Vector2.SignedAngle(direction, toPoint);
        float dirAngle = Mathf.Atan(direction.y / direction.x);
        dirAngle += (Mathf.Deg2Rad * toAngle) * strength;
        toAngle = Mathf.Deg2Rad * toAngle * strength;
        float dirX = direction.x * Mathf.Cos(toAngle) - direction.y * Mathf.Sin(toAngle);
        float dirY = direction.x * Mathf.Sin(toAngle) + direction.y * Mathf.Cos(toAngle);
        direction = new Vector2(dirX, dirY);
        toAngle = Vector2.Angle(direction, toPoint);
        if (Mathf.Abs(toAngle) <= 8)
        {
            return toPoint;
        }
        return direction;
    }

    /// <summary>
    /// The same as LerpTowardsPoint but will instantly snap the particle towards the point if it is close enough.
    /// </summary>
    /// <param name="goal">
    /// The position the particle is moving towards.
    /// </param>
    /// <param name="pos">
    /// The particle's current position.
    /// </param>
    /// <param name="direction">
    /// The direction vector of the particle.
    /// </param>
    /// <param name="strength">
    /// The strength with which the particle turns.
    /// </param>
    /// <param name="targetDistance">
    /// The distance from the goal for the particle to instantly snap towards it.
    /// </param>
    /// <returns>
    /// A new direction vector for the particle.
    /// </returns>
    public static Vector2 LerpTowardsPointWithSnap(Vector2 goal, Vector2 pos, Vector2 direction, float strength, float targetDistance)
    {
        float distToGoal = Vector2.Distance(goal, pos);
        if (distToGoal <= targetDistance)
        {
            return (goal - pos).normalized;
        }
        else
        {
            return LerpTowardsPoint(goal, pos, direction, strength);
        }
    }
}
