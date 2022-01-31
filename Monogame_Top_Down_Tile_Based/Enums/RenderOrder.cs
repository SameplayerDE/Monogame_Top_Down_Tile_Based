namespace Monogame_Top_Down_Tile_Based.Enums
{
    public enum RenderOrder
    {
        RightDown,
        RightUp,
        LeftDown,
        LeftUp
    }

    public static class RenderOrderHelper
    {
        public static RenderOrder FromString(string value)
        {
            return value switch
            {
                "right-down" => RenderOrder.RightDown,
                "right-up" => RenderOrder.RightUp,
                "left-down" => RenderOrder.LeftDown,
                "left-up" => RenderOrder.LeftUp,
                _ => RenderOrder.RightDown
            };
        }
    }
}