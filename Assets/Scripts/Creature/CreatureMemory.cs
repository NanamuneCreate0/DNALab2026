using System.Collections.Generic;
using UnityEngine;

public class CreatureMemory
{
    public List<(float priority, Vector2 direction)> MoveMotivations = new List<(float, Vector2)>(16);
}
