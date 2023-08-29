using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DynamicNumber
{
    public static class EventID
    {
        public const string EVENT_DATA_KEY = "data";

        public const string ON_CARD_FLIPPED = "ON_CARD_FLIPPED";
        public const string ON_CARD_ENQUEUED = "ON_CARD_ENQUEUED";
        public const string ON_CARD_QUEUE_FULL = "ON_CARD_QUEUE_FULL";

        public const string ON_CARD_EFFECT_STARTED = "ON_CARD_EFFECT_STARTED";
        public const string ON_CARD_EFFECT_FINISHED = "ON_CARD_EFFECT_FINISHED";

        public const string ON_PLAYER_POINT_CHANGE = "ON_PLAYER_POINT_CHANGE";
    }
}

