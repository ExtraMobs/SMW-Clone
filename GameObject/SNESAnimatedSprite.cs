using Godot;
using System;

[GlobalClass]
public partial class SNESAnimatedSprite : AnimatedSprite2D
{

    public override void _Ready()
    {
        SetSpeedScale((float)DefaultValues.fpsFactor);
    }
}
