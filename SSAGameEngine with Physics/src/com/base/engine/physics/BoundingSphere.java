package com.base.engine.physics;

import com.base.engine.core.Vector3f;

public class BoundingSphere extends Collider {
    private Vector3f center;
    private float radius;

    public BoundingSphere(Vector3f center, float radius) {
        super(ColliderType.SPHERE);
        this.center = center;
        this.radius = radius;
    }

    public IntersectData IntersectBoundingSphere (BoundingSphere other) {
        float radiusDistance = radius + other.getRadius();
        Vector3f direction = other.getCenter().Subtract(center);
        float centerDistance = direction.Length();
        direction.Div(centerDistance);

        float distance = centerDistance - radiusDistance;

        return new IntersectData(distance < 0, direction.Mul(distance));
    }

    @Override
    public void transform(Vector3f translation) {
        float x_ = center.GetX();
        float y_ = center.GetY();
        float z_ = center.GetZ();

        x_ += translation.GetX();
        y_ += translation.GetY();
        z_ += translation.GetZ();

        center.Set(x_, y_, z_);
    }

    public static void testData() {
        BoundingSphere sphere1 = new BoundingSphere(new Vector3f(0.0f, 0.0f, 0.0f), 1.0f);
        BoundingSphere sphere2 = new BoundingSphere(new Vector3f(0.0f, 3.0f, 0.0f), 1.0f);
        BoundingSphere sphere3 = new BoundingSphere(new Vector3f(0.0f, 0.0f, 2.0f), 1.0f);
        BoundingSphere sphere4 = new BoundingSphere(new Vector3f(1.0f, 0.0f, 0.0f), 1.0f);

        IntersectData sphere1IntersectSphere2 = sphere1.IntersectBoundingSphere(sphere2);
        IntersectData sphere1IntersectSphere3 = sphere1.IntersectBoundingSphere(sphere3);
        IntersectData sphere1IntersectSphere4 = sphere1.IntersectBoundingSphere(sphere4);

        System.out.println("Sphere1 intersect Sphere2: " + sphere1IntersectSphere2.doesIntersect()
                + ", Distance: " + sphere1IntersectSphere2.getDistance());
        System.out.println("Sphere1 intersect Sphere3: " + sphere1IntersectSphere3.doesIntersect()
                + ", Distance: " + sphere1IntersectSphere3.getDistance());
        System.out.println("Sphere1 intersect Sphere4: " + sphere1IntersectSphere4.doesIntersect()
                + ", Distance: " + sphere1IntersectSphere4.getDistance());
    }

    public Vector3f getCenter() {
        return center;
    }

    public float getRadius() {
        return radius;
    }
}
