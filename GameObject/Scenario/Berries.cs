using Godot;
using System;

[GlobalClass]
public partial class Berries : Node2D
{
    [Export]
    public Berry.Type type;


    public override void _Ready()
    {
        foreach (Berry child in GetChildren())
        {
            child.type = type;
        }
    }

}
