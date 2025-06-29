using Godot;
using System;
using System.Collections.Generic;
	
[GlobalClass]
public partial class Character : Node2D
{
    [Export]
    private AnimatedSprite2D animatedSpriteNode;

    private Velocity velocity = new Velocity(
        max: new Vector2(90, 0) * (float)DefaultValues.fpsFactor
    );
    
    private enum State {
        Walk,
        Idle
    }
    private State internalState;
    private CharacterState state
    {
        get
        {
            return states[internalState];
        }
    }
    
    private Dictionary<State, CharacterState> states = new Dictionary<State, CharacterState>()
    {
        {State.Walk, new Walk()},
        {State.Idle, new Idle()}
    };

    public override void _Process(double delta)
    {
        PreProcessInput();
        state.Process(velocity);
        Position += velocity.value * (float)delta;
    }

    public void PreProcessInput()
    {
        if (Input.IsActionPressed("left"))
		{
			velocity.orientation = Direction.Orientation.LEFT;
		} else if (Input.IsActionJustReleased("left"))
		{
			velocity.orientation = Direction.Orientation.STOP;
		}
		else if (Input.IsActionPressed("right"))
		{
			velocity.orientation = Direction.Orientation.RIGHT;
		} else if (Input.IsActionJustReleased("right"))
		{
			velocity.orientation = Direction.Orientation.STOP;
		}
		
        switch (velocity.value.X)
		{
			case not 0:
                if (
                    (velocity.value.X < 0 && velocity.orientation == Direction.Orientation.RIGHT) ||
                    (velocity.value.X > 0 && velocity.orientation == Direction.Orientation.LEFT)
                )
                {
                    animatedSpriteNode.Play("skid");
                    break;
                }
                animatedSpriteNode.Play("walk");
                break;
            case 0:
			    animatedSpriteNode.Play("idle");
                break;
        }
        switch (velocity.orientation)
        {
            case Direction.Orientation.LEFT:
                animatedSpriteNode.FlipH = false;
                break;
            case Direction.Orientation.RIGHT:
                animatedSpriteNode.FlipH = true;
                break;
        }
			// animatedSpriteNode.Play("walk");
			// animatedSpriteNode.Play("walk");
			// animatedSpriteNode.Play("idle");
    }

    class Walk : CharacterState
    {
        public Walk(): base(
            accel: new Acceleration(step: 3.75 * DefaultValues.fpsFactor)
        ){}

        public override void Process(Velocity velocity)
        {
            if (velocity.orientation == Direction.Orientation.STOP)
            {
                velocity.ApplyResistance(accel);
                return;
            }
            
            base.Process(velocity);
        }
    }

    class Idle : CharacterState{}
}

class CharacterState
{
    public Acceleration accel;

    public CharacterState(){
        this.accel = new Acceleration();
    }
    public CharacterState(Acceleration accel)
    {
        this.accel = accel;
    }

    public virtual void Process(Velocity velocity)
    {
        velocity.ApplyAcceleration(accel);
    }
}
