using UnityEngine;

public class JellyVertex
{
    public JellyVertex(int id, Vector3 position)
    {
        Id = id;
        Position = position;
    }

    public int Id { get; private set; }
    public Vector3 Position { get; private set; }
    public Vector3 Velocity { get; private set; }
    public Vector3 Force { get; private set; }

    public void Shake(Vector3 target, float mass, float stiffness, float damping)
    {
        Force = (target - Position) * stiffness;
        Velocity = (Velocity + Force / mass) * damping;
        Position += Velocity;

        if ((Velocity + Force + Force / mass).magnitude < 0.001f)
            Position = target;
    }
}

