using Math;

namespace Drawing
{
    public static class Colors
    {
        public static Tuple Black => Tuple.Color(0f, 0f, 0f);
        public static Tuple White => Tuple.Color(1f, 1f, 1f);
        
        public static Tuple Red => Tuple.Color(1f, 0f, 0f);
        public static Tuple Green => Tuple.Color(0f, 1f, 0f);
        public static Tuple Blue => Tuple.Color(0f, 0f, 1f);

        public static Tuple Orange => Tuple.Color(1f, 0.5f, 0f);
        
        public static Tuple Yellow => Tuple.Color(1f, 1f, 0f);
        public static Tuple Cyan => Tuple.Color(0f, 1f, 1f);
        public static Tuple Magenta => Tuple.Color(1f, 0f, 1f);
        
        public static Tuple Grey75 => Grey(0.75f);
        public static Tuple Grey50 => Grey(0.5f);
        public static Tuple Grey25 => Grey(0.25f);
        public static Tuple Grey10 => Grey(0.1f);
        public static Tuple Grey(float percent) => Tuple.Color(percent, percent, percent);
    }
}