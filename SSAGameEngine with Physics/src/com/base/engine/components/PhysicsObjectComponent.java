package com.base.engine.components;

import com.base.engine.physics.PhysicsObject;

public class PhysicsObjectComponent extends GameComponent {
    private PhysicsObject physicsObject;

    public PhysicsObjectComponent (PhysicsObject object) {
        physicsObject = object;
    }

    @Override
    public void Update(float delta) {
        GetTransform().SetPos(physicsObject.getPosition());
    }
}
