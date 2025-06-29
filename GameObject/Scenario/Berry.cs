using Godot;
using System;
using System.Collections.Generic;

[GlobalClass, Icon("res://Icons/berry.png")]
public partial class Berry : SNESAnimatedSprite
{
    public enum Type{
        Red,
        Pink,
        Green
    }

    private Type _type;

    public Type type{
        set
        {
            _type = value;
            SetSpriteFrames(GD.Load<SpriteFrames>(typeAnimations[value]));
        }

        get
        {
            return _type;
        }
    }


    public Dictionary<Berry.Type, string> typeAnimations = new Dictionary<Berry.Type, string>()
    {
        {Berry.Type.Red, "res://GameObject/Scenario/berries-red.tres"},
        {Berry.Type.Pink, "res://GameObject/Scenario/berries-pink.tres"},
        {Berry.Type.Green, "res://GameObject/Scenario/berries-green.tres"},
    };

    public override void _Ready()
    {
        SetSpriteFrames(GD.Load<SpriteFrames>(typeAnimations[type]));
        base._Ready();
        Play("idle");
    }
}
