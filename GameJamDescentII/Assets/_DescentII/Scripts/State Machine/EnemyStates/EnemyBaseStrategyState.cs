using System;
using System.Collections.Generic;
using UnityEngine;

namespace DescentII
{
    public abstract class EnemyBaseStrategyState<T> : EnemyBaseState<T> where T : EnemyBaseStrategy
    {
        protected readonly List<T> strategies = new();
        protected readonly Dictionary<Type, IPredicate> conditions = new();

        protected EnemyBaseStrategyState(Creature enemy, Animator animator) 
            : base(enemy, animator) { }

        public void SetStrategy(T strategy, IPredicate condition)
        {
            conditions.Add(strategy.GetType(), condition);
            base.SetStrategy(strategy);
        }

        public void AddStrategy(T strategy, IPredicate condition)
        {
            conditions.Add(strategy.GetType(), condition);
            strategies.Add(strategy);
        }

        public override void Update()
        {
            for (int i = 0; i < strategies.Count; i++)
            {
                if (conditions[strategies[i].GetType()].Evaluate())
                {
                    (strategy, strategies[i]) = (strategies[i], strategy);
                    OnEnter();
                    continue;
                }
            }

            base.Update();
        }
    }
}
