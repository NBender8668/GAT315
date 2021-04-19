using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class World : MonoBehaviour
{
    public BoolData simulate;
    public FloatData gravity;
    public FloatData gravitation;
    public FloatData fixedFps;
    public StringData fpsText;
    public float timeAccumulator;
    public float FixedDeltaTime { get { return 1.0f / fixedFps.value; }  }
    float fps = 0;
    float fpsAverage = 0;
    float smoothing = 0.975f;

    static World instance;
    public static World Instance { get { return instance; } }

    public Vector2 Gravity { get { return new Vector2(0, gravity.value); } }
    
    
    public List<Body> bodies { get; set; } = new List<Body>();
    private void Awake()
    {
        instance = this;
    }
    void Update()
    {
        float dt = Time.deltaTime;
        fps = (1.0f / dt);
        fpsAverage = (fpsAverage * smoothing) + (fps * (1.0f - smoothing));
        fpsText.value = "FPS: " + fpsAverage.ToString("F2");
        if (!simulate) return;

        bodies.ForEach(body => body.Step(dt));
        bodies.ForEach(body => Integrator.SemiImplicitEuler(body,dt));


        timeAccumulator += dt;

        GravitionalForce.ApplyForce(bodies, gravitation.value);

       

        while (timeAccumulator >= FixedDeltaTime) 
        { 
            bodies.ForEach(body => body.Step(FixedDeltaTime)); 
            bodies.ForEach(body => Integrator.SemiImplicitEuler(body, FixedDeltaTime));

            bodies.ForEach(body => body.shape.color = Color.white);

            Collision.CreateContact(bodies, out List<Contact> contacts);
            contacts.ForEach(contact => { contact.bodyA.shape.color = Color.blue; contact.bodyB.shape.color = Color.blue; });

            timeAccumulator = timeAccumulator - FixedDeltaTime;

        }
        
        bodies.ForEach(body => body.force = Vector2.zero);
        bodies.ForEach(body => body.acceleration = Vector2.zero);
       
        
    }
}
