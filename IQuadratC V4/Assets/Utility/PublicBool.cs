﻿using System;
using UnityEngine;

namespace Utility
{
    [CreateAssetMenu(fileName = "PublicBool", menuName = "Utility/PublicBool")]
    public class PublicBool : ScriptableObject, ISerializationCallbackReceiver
    {
        [NonSerialized] public bool value;
        [SerializeField] private bool initalValue;
        
        public void OnBeforeSerialize() { }
        public void OnAfterDeserialize()
        {
            value = initalValue;
        }
    }
}