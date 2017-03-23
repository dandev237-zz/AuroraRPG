public class Utilities
{
    public const int LeftMouseButton = 0;
}

//Map the editor layers to in-code layers
public enum Layer
{
    Walkable = 8,
    Enemy = 9,
    RaycastEndStop = -1     //Lowest priority
}