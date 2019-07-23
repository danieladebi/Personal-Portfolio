package com.base.engine.components;

import com.base.engine.physics.PhysicsEngine;

public class PhysicsEngineComponent extends GameComponent {
    private PhysicsEngine physicsEngine;

    public PhysicsEngineComponent (PhysicsEngine engine) {
        physicsEngine = engine;
    }

    @Override
    public void Update(float delta) {
        physicsEngine.simulate(delta);
        physicsEngine.handleCollisions();
    }

    public PhysicsEngine getPhysicsEngine() {
        return physicsEngine;
    }
}
