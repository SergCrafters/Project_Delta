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
        public static readonly int IsAttack = Animator.StringToHash(nameof(IsAttack));
        public static readonly int IsHit = Animator.StringToHash(nameof(IsHit));
        public static readonly int IsDeath = Animator.StringToHash(nameof(IsDeath));
        public static readonly int Action = Animator.StringToHash(nameof(Action));
        public static readonly int LastDirection = Animator.StringToHash(nameof(LastDirection));
    }

    public static class ActionParameters
    {
        public const int IDLE = 0;
        public const int WALK = 1;
        public const int RUN = 2;
        public const int DASH = 3;
    }

    public static class DirectionData
    {
        public const int DIRECTION_LEFT = 0;
        public const int DIRECTION_UP = 1;
        public const int DIRECTION_RIGHT = 2;
        public const int DIRECTION_DOWN = 3;
    }

    public static class SaveData
    {
        public const string MUSIC_KEY = "Music";
        public const string MUSIC_MUTE_KEY = "MusicIsOn";
        public const string SOUND_KEY = "Sound";
        public const string SOUND_MUTE_KEY = "SoundIsOn";

        public const int IS_ON_VALUE = 1;
        public const int IS_OFF_VALUE = 0;

        public const float DEFAULT_VALUME = 1;
        public const float FOOTSTEP_VOLUME_SCALE = 0.3f;
    }
}
