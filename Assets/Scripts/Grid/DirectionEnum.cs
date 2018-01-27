
using System.Text;
using UnityEngine;

public enum DirectionEnum
{
    EMPTY = 0,
    NORTH = 1,
    EAST = 2,
    SOUTH = 4,
    WEST = 8
}

public class Direction {
    public static DirectionEnum Clockwise(DirectionEnum dir)
    {
        switch (dir)
        {
            case DirectionEnum.WEST: return DirectionEnum.NORTH;
            default: return (DirectionEnum)((int)dir << 1);
        }
    }

    public static DirectionEnum CounterClockwise(DirectionEnum dir)
    {
        switch (dir)
        {
            case DirectionEnum.NORTH: return DirectionEnum.WEST;
            default: return (DirectionEnum)((int)dir >> 1);
        }
    }

    public static float ToAngle(DirectionEnum dir)
    {
        switch (dir)
        {
            case DirectionEnum.NORTH: return 0f;
            case DirectionEnum.EAST: return 90f;
            case DirectionEnum.SOUTH: return 180f;
            case DirectionEnum.WEST: return -90f;
        }
        Debug.LogError("cannot convert dir to angle");
        return 0f;
    }

    public static DirectionEnum FromAngle(float angle)
    {
        if (Mathf.Abs(Mathf.DeltaAngle(0, angle)) < 10f) return DirectionEnum.NORTH;
        else if (Mathf.Abs(Mathf.DeltaAngle(90, angle)) < 10f) return DirectionEnum.EAST;
        else if (Mathf.Abs(Mathf.DeltaAngle(180, angle)) < 10f) return DirectionEnum.SOUTH;
        else if (Mathf.Abs(Mathf.DeltaAngle(-90, angle)) < 10f) return DirectionEnum.WEST;

        Debug.LogError("Cannot convert angle " + angle + " to dir");
        return DirectionEnum.NORTH;
    }

    public static DirectionEnum RotateClockWise(DirectionEnum input)
    {
        DirectionEnum output = DirectionEnum.EMPTY;


        if (Contains(input, DirectionEnum.WEST))
            output = Add(output, DirectionEnum.NORTH);

        if (Contains(input, DirectionEnum.NORTH))
            output = Add(output, DirectionEnum.EAST);

        if (Contains(input, DirectionEnum.EAST))
            output = Add(output, DirectionEnum.SOUTH);

        if (Contains(input, DirectionEnum.SOUTH))
            output = Add(output, DirectionEnum.WEST);
        

        return output;
    }

    public static DirectionEnum RotateCounterClockWise(DirectionEnum input)
    {
        DirectionEnum output = DirectionEnum.EMPTY;

        if (Contains(input, DirectionEnum.EAST))
            output = Add(output, DirectionEnum.NORTH);

        if (Contains(input, DirectionEnum.SOUTH))
            output = Add(output, DirectionEnum.EAST);

        if (Contains(input, DirectionEnum.WEST))
            output = Add(output, DirectionEnum.SOUTH);

        if (Contains(input, DirectionEnum.NORTH))
            output = Add(output, DirectionEnum.WEST);

        return output;
    }

    public static bool Contains(DirectionEnum input, DirectionEnum toCheck)
    {
        return (input & toCheck) == toCheck;
    }

    public static DirectionEnum Add(DirectionEnum input, DirectionEnum addit)
    {
        return (input | addit);
    }

    public static string ToString(DirectionEnum dir)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(Contains(dir, DirectionEnum.NORTH)? 'N': 'n');
        sb.Append(Contains(dir, DirectionEnum.EAST) ? 'E' : 'e');
        sb.Append(Contains(dir, DirectionEnum.SOUTH) ? 'S' : 's');
        sb.Append(Contains(dir, DirectionEnum.WEST) ? 'W' : 'w');
        return sb.ToString();
    }
}