using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class World : MonoBehaviour
{
    public BoolData simulate;
    public FloatData gravity;
    public FloatData fixedFps;
    public TMP_Text valueText = null;
    public float timeAccumulator;
    public float FixedDeltaTime { get { return 1.0f / fixedFps.value; }  }

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
        if (!simulate.value) return;
        float dt = Time.deltaTime;


        bodies.ForEach(body => body.Step(dt));
        bodies.ForEach(body => Integrator.SemiImplicitEuler(body,dt));

        bodies.ForEach(body => body.force = Vector2.zero);
        bodies.ForEach(body => body.acceleration = Vector2.zero);

        timeAccumulator += dt;

        while (timeAccumulator > FixedDeltaTime) 
        { 
            bodies.ForEach(body => body.Step(FixedDeltaTime)); 
            bodies.ForEach(body => Integrator.ExplicitEuler(body, FixedDeltaTime)); 
            timeAccumulator = timeAccumulator - FixedDeltaTime; 
        }

        valueText.text = fixedFps.value.ToString("F2");
        Debug.Log(1.0f / Time.deltaTime);
    }
}
