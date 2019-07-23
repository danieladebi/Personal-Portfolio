package com.base.engine.physics;

import com.base.engine.core.Vector3f;

public class AABB {
    private Vector3f minExtents;
    private Vector3f maxExtents;

    public AABB(Vector3f minExtents, Vector3f maxExtents) {
        this.minExtents = minExtents;
        this.maxExtents = maxExtents;
    }

    public IntersectData IntersectAABB(AABB other) {
        Vector3f distances1 = other.getMinExtents().Subtract(maxExtents);
        Vector3f distances2 = minExtents.Subtract(other.getMaxExtents());
        Vector3f distances = distances1.Max(distances2);

        float maxDistance = distances.Max(); // Max Value

        return new IntersectData(maxDistance < 0, distances);
    }

    public static void testData() {
        AABB aabb1 = new AABB(new Vector3f(0.0f, 0.0f, 0.0f), new Vector3f(1.0f, 1.0f, 1.0f));
        AABB aabb2 = new AABB(new Vector3f(1.0f, 1.0f, 1.0f), new Vector3f(2.0f, 2.0f, 2.0f));
        AABB aabb3 = new AABB(new Vector3f(1.0f, 0.0f, 0.0f), new Vector3f(2.0f, 1.0f, 1.0f));
        AABB aabb4 = new AABB(new Vector3f(0.0f, 0.0f, -2.0f), new Vector3f(1.0f, 1.0f, -1.0f));
        AABB aabb5 = new AABB(new Vector3f(0.0f, 0.5f, 0.0f), new Vector3f(1.0f, 1.5f, 1.0f));

        IntersectData aabb1Intersectaabb2 = aabb1.IntersectAABB(aabb2);
        IntersectData aabb1Intersectaabb3 = aabb1.IntersectAABB(aabb3);
        IntersectData aabb1Intersectaabb4 = aabb1.IntersectAABB(aabb4);
        IntersectData aabb1Intersectaabb5 = aabb1.IntersectAABB(aabb5);

        System.out.println("AABB1 intersect AABB2: " + aabb1Intersectaabb2.doesIntersect() + ", Distance: " + aabb1Intersectaabb2.getDistance());
        System.out.println("AABB1 intersect AABB3: " + aabb1Intersectaabb3.doesIntersect() + ", Distance: " + aabb1Intersectaabb3.getDistance());
        System.out.println("AABB1 intersect AABB4: " + aabb1Intersectaabb4.doesIntersect() + ", Distance: " + aabb1Intersectaabb4.getDistance());
        System.out.println("AABB1 intersect AABB5: " + aabb1Intersectaabb5.doesIntersect() + ", Distance: " + aabb1Intersectaabb5.getDistance());
    }

    public Vector3f getMinExtents() {
        return minExtents;
    }

    public Vector3f getMaxExtents() {
        return maxExtents;
    }
}
