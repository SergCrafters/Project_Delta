using UnityEngine;

public static class ConstantData
{
    public static class InpudData
    {
        public const string HORIZONTAL_AXIS = "Horizontal";
        public const string VERTICAL_AXIS = "Vertical";
    }

    public static class AnimatorParameters
    {
        public static readonly int IsOn = Animator.StringToHash(nameof(IsOn));
        public static readonly int IsOff = Animator.StringToHash(nameof(IsOff));
    }
    //public static class DirectionData
    //{
    //public const int DIRECTION_LEFT = 0;
    //public const int DIRECTION_UP = 1;
    //public const int DIRECTION_RIGHT = 2;
    //public const int DIRECTION_DOWN = 3;
    //}
}
