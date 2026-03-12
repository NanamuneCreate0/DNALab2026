using System.Collections.Generic;
using UnityEngine;

public class CreatureMemory
{
    //public List<(float priority, Vector2 direction)> MoveMotivations = new List<(float, Vector2)>(16);

    public List<(float priority, Vector2 dir)> CurrentMoveMotivations
        = new List<(float, Vector2)>();

    public List<(float priority, Vector2 dir)> NextMoveMotivations
        = new List<(float, Vector2)>();
}
