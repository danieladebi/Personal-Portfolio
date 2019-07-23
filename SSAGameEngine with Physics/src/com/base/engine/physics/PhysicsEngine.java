package com.base.engine.physics;

import com.base.engine.core.Vector3f;

import java.util.ArrayList;

public class PhysicsEngine {
    private ArrayList<PhysicsObject> pObjects = new ArrayList<PhysicsObject>();

    public PhysicsEngine() {

    }

    public void addObject(PhysicsObject object) {
        pObjects.add(object);
    }

    public void simulate(float delta) {
        for (int i = 0; i < pObjects.size(); i++) {
            pObjects.get(i).integrate(delta);
        }
    }

    public void handleCollisions() {
        for (int i = 0; i < pObjects.size(); i++) {
            for (int j = i + 1; j < pObjects.size(); j++) {
                IntersectData intersectData = pObjects.get(i).getCollider().intersect(pObjects.get(j).getCollider());

                if (intersectData.doesIntersect()) {
                    Vector3f direction = intersectData.getDirection().Normalized();
                    Vector3f otherDirection = direction.Reflect(pObjects.get(i).getVelocity().Normalized());
                    pObjects.get(i).setVelocity(pObjects.get(i).getVelocity().Reflect(otherDirection));
                    pObjects.get(j).setVelocity(pObjects.get(j).getVelocity().Reflect(direction));
                }
            }
        }
    }

    // TODO: Temporary Getters
    public PhysicsObject getObject(int index) {
        return pObjects.get(index);
    }

    public int getNumObjects() {
        return pObjects.size();
    }
}
