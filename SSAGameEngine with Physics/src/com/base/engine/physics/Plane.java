package com.base.engine.physics;

import com.base.engine.core.Vector3f;

public class Plane {
    private Vector3f normal;
    private float distance;

    public Plane(Vector3f normal, float distance) {
        this.normal = normal;
        this.distance = distance;
    }

    public static void testData() {
        Plane plane1 = new Plane(new Vector3f(0.0f, 1.0f, 0.0f), 0.0f);

        BoundingSphere sphere1 = new BoundingSphere(new Vector3f(0.0f, 0.0f, 0.0f), 1.0f);
        BoundingSphere sphere2 = new BoundingSphere(new Vector3f(0.0f, 3.0f, 0.0f), 1.0f);
        BoundingSphere sphere3 = new BoundingSphere(new Vector3f(0.0f, 0.0f, 2.0f), 1.0f);
        BoundingSphere sphere4 = new BoundingSphere(new Vector3f(1.0f, 0.0f, 0.0f), 1.0f);

        IntersectData plane1IntersectSphere1 = plane1.IntersectSphere(sphere1);
        IntersectData plane1IntersectSphere2 = plane1.IntersectSphere(sphere2);
        IntersectData plane1IntersectSphere3 = plane1.IntersectSphere(sphere3);
        IntersectData plane1IntersectSphere4 = plane1.IntersectSphere(sphere4);

        System.out.println("Plane1 intersect Sphere1: " + plane1IntersectSphere1.doesIntersect() + ", Distance: " + plane1IntersectSphere1.getDistance());
        System.out.println("Plane1 intersect Sphere2: " + plane1IntersectSphere2.doesIntersect() + ", Distance: " + plane1IntersectSphere2.getDistance());
        System.out.println("Plane1 intersect Sphere3: " + plane1IntersectSphere3.doesIntersect() + ", Distance: " + plane1IntersectSphere3.getDistance());
        System.out.println("Plane1 intersect Sphere4: " + plane1IntersectSphere4.doesIntersect() + ", Distance: " + plane1IntersectSphere4.getDistance());
    }

    public Plane normalized() {
        float magnitude = normal.Length();

        return new Plane(normal.Div(magnitude), distance/magnitude);
    }

    public IntersectData IntersectSphere(BoundingSphere other) {
        float distanceFromSphereCenter = normal.Dot(other.getCenter()) + distance; // how far sphere center is along the plane's normal vector
        float distanceFromSphere = distanceFromSphereCenter - other.getRadius(); // how far sphere is from plane

        return new IntersectData(distanceFromSphere < 0, normal.Mul(distanceFromSphere));
    }

    public Vector3f getNormal() {
        return normal;
    }

    public float getDistance() {
        return distance;
    }
}
