using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AIBunnies
{
    public class BunniesHelper
    {
        public class Constants
        {
            public const string PLAYER_TAG = "Player";
            public const string CARROT_TAG = "Carrot";
            public const string FARMER_TAG = "Farmer";
            public const float FIELD_DISTANCE = 30;
            public const float FIELD_ANGLE_PATROL = 140;
            public const float FIELD_ANGLE_PERSUE = 360;
            public const int AI_PATROL_SPEED = 3;
            public const int AI_PERSUE_SPEED = 6;
            public const int PLAYER_SPEED = 4;
            public const int NEEDED_POINTS = 6;
        }
    }
}