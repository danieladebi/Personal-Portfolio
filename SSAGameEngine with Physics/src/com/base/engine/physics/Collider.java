package com.base.engine.physics;

import com.base.engine.core.Vector3f;

import static com.base.engine.physics.Collider.ColliderType.*;

public class Collider {
    public enum ColliderType {SPHERE, AABB, SIZE};

    private ColliderType type; // type of collider

    public Collider(ColliderType type) {
        this.type = type;
    }

    public ColliderType getType() {
        return type;
    }

    public IntersectData intersect(Collider other) {
        try {
            if (type == SPHERE && other.getType() == SPHERE) {
                BoundingSphere self = (BoundingSphere)this;
                return self.IntersectBoundingSphere((BoundingSphere)other);
            }
        } catch (Exception e) {
            System.out.println("Error: Collisions not implemented between specified colliders.");
            e.printStackTrace();
            System.exit(1);
        }

        return new IntersectData(false, new Vector3f(0,0,0));
    }

    public void transform(Vector3f translation) { }
    public Vector3f getCenter() {
        return new Vector3f(0,0,0);
    }
}
