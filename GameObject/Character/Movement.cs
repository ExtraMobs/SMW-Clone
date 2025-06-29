using Godot;
using System;

public class Velocity: Direction
{
    public Vector2 max;
    public Vector2 min;
    public Vector2 value = new Vector2();

    public Velocity(Vector2 max)
    {
        this.min = max *-1;
        this.max = max;
    }

    public Velocity(Vector2 min, Vector2 max)
    {
        this.min = min;
        this.max = max;
    }

    public void ApplyAcceleration(Acceleration accel)
    {
        value += (float)accel.step * direction;
        if (value.X > max.X)
        {
            value.X = max.X;
        } else if (value.X < min.X)
        {
            value.X = min.X;
        }

        if (value.Y > max.Y)
        {
            value.Y = max.Y;
        }
        else if (value.Y < min.Y)
        {
            value.Y = min.Y;
        }
    }

    public void ApplyResistance(Acceleration accel)
    {
        if (value.X > 0)
        {
            value.X = Math.Max(value.X - (float)accel.resistance, 0);
        } else if (value.X < 0)
        {
            value.X = Math.Min(0, value.X + (float)accel.resistance);
        }

        if (value.Y > 0)
        {
            value.Y -= (float)accel.resistance;
        } else if (value.Y < 0)
        {
            value.Y += (float)accel.resistance;
        }
    }
}

public class Acceleration: Direction
{
    public double min = 0;
    public double max = 0;
    public double step;
    public double resistance;


    public Acceleration(){}
    
    public Acceleration(double min, double max, double step)
    {
        this.min = min;
        this.max = max;
        this.step = step;
        this.resistance = step;
    }
    public Acceleration(double max, double step)
    {
        this.max = max;
        this.step = step;
        this.resistance = step;
    }
    public Acceleration(double step)
    {
        this.max = step;
        this.step = step;
        this.resistance = step;
    }
}

public class Direction
{
    public Vector2 direction;

    public Direction(Vector2 direction)
    {
        this.direction = direction;
    }

    public Direction()
    {
        this.direction = new Vector2();
    }
    public Direction(float x, float y)
    {
        this.direction.X = x;
        this.direction.Y = y;
    }

    public enum Orientation {
        UP,
        DOWN,
        LEFT,
        RIGHT,
        UPLEFT,
        UPRIGHT,
        DOWNLEFT,
        DOWNRIGHT,
        STOP
    }

    public Orientation orientation {        
        get
        {
            switch(this.direction.X)
            {
                case < 0:
                    switch(this.direction.Y)
                    {
                        case < 0:
                            return Orientation.UPLEFT;
                        case > 0:
                            return Orientation.DOWNLEFT;
                    }
                return Orientation.LEFT;
                case > 0:
                    switch(this.direction.Y)
                    {
                        case < -1:
                            return Orientation.UPRIGHT;
                        case > 1:
                            return Orientation.DOWNRIGHT;
                    }
                return Orientation.RIGHT;
                default:
                    return Orientation.STOP;
            }
        }
        set
        {
            switch(value)
            {
                case Orientation.UP:
                    this.direction.Y = -1;
                    break;
                case Orientation.DOWN:
                    this.direction.Y = 1;
                    break;
                case Orientation.LEFT:
                    this.direction.X = -1;
                    break;
                case Orientation.RIGHT:
                    this.direction.X = 1;
                    break;
                case Orientation.UPLEFT:
                    this.direction.X = -1;
                    this.direction.Y = -1;
                    break;
                case Orientation.UPRIGHT:
                    this.direction.X = 1;
                    this.direction.Y = -1;
                    break;
                case Orientation.DOWNLEFT:
                    this.direction.X = -1;
                    this.direction.Y = 1;
                    break;
                case Orientation.DOWNRIGHT:
                    this.direction.X = 1;
                    this.direction.Y = 1;
                    break;
                case Orientation.STOP:
                    this.direction.X = 0;
                    this.direction.Y = 0;
                    break;
            }
        }
    }
}
