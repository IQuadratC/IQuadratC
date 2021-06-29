﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    [CreateAssetMenu(fileName = "PublicEventfloat2", menuName = "Utility/PublicEventfloat2")]
    public class PublicEventWithVar : ScriptableObject
    {
        private Action<float2>[] funcs = new Action<float2>[1];
        private int maxId = 0;
        private List<int> freeIds = new List<int>();
        
        public void Raise(float2 variable)
        {
            foreach (Action<float2> func in funcs)
            {
                func?.Invoke(variable);
            }
        }
    
        public int Register(Action<float2> func)
        {
            int id;
            if (freeIds.Count == 0)
            {
                id = maxId;
                maxId++;
            }
            else
            {
                id = freeIds[0];
                freeIds.RemoveAt(0);
            }
            
            if (funcs.Length >= id)
            {
                raiseArray();
            }
            
            funcs[id] = func;
            return id;
        }
    
        public void Unregister(int id)
        {
            freeIds.Add(id);
            funcs[id] = null;
        }
    
        private void raiseArray()
        {
            int length = funcs.Length;
            Action<float2>[] newFunc = new Action<PublicInt>[length + 1];
            
            for (int i = 0; i < length; i++)
            {
                newFunc[i] = funcs[i];
            }

            funcs = newFunc;
        }
    }
}