package com.base.engine.physics;

import com.base.engine.core.Vector3f;

public class PhysicsObject {
    private Vector3f position;
    private Vector3f oldPosition;
    private Vector3f velocity;

    private Collider collider;

    public PhysicsObject(Collider collider, Vector3f velocity) {
        position = collider.getCenter();
        oldPosition = position;
        this.velocity = velocity;
        this.collider = collider;
    }

    public static void testData() {
        PhysicsObject test = new PhysicsObject(new BoundingSphere(new Vector3f(0.0f, 1.0f, 0.0f), 1.0f), new Vector3f(1.0f, 2.0f, 3.0f)); // Position, Radius, Velocity
        test.integrate(20.0f);

        Vector3f testPosition = test.getPosition();
        Vector3f testVelocity = test.getVelocity();

        System.out.println(testPosition.toString());
        System.out.println(testVelocity.toString());
    }

    public void integrate(float delta) {
        float position_x = position.GetX();
        float position_y = position.GetY();
        float position_z = position.GetZ();

        position_x += velocity.GetX() * delta;
        position_y += velocity.GetY() * delta;
        position_z += velocity.GetZ() * delta;

        position = new Vector3f(position_x, position_y, position_z);
    }

    public Vector3f getPosition() {
        return position;
    }

    public Vector3f getVelocity() {
        return velocity;
    }

    public Collider getCollider() {
        Vector3f translation = position.Subtract(oldPosition);
        oldPosition = position;
        collider.transform(translation);

        return collider;
    }

    public void setVelocity(Vector3f velocity) {
        this.velocity = velocity;
    }
}
