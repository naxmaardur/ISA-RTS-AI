using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UtilityAIControler
{
    public abstract class ControlerAction : ScriptableObject
    {
        public string Name;
        private float _score;
        public float score
        {
            get { return _score; }
            set
            {
                this._score = Mathf.Clamp01(value);
            }
        }

        public ControlerConsideration[] considerations;

        public Transform RequiredDestination { get; protected set; }

        public virtual void Awake()
        {
            score = 0;
        }

        public abstract void Execute(ControlerAI npc);

        public virtual void SetRequiredDestination(ControlerAI npc) { }
    }
}