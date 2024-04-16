using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AnimaresTest.Channels
{
    public class Channel<T> : ScriptableObject
    {
        public UnityAction<T> Action { get; set; }

        public void Trigger(T value)
        {
            Action?.Invoke(value);
        }

    }
}
