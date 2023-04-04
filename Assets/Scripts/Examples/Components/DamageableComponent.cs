using System;
using Entities;
using Entities.Components;
using Entities.Stats;
using UnityEngine;

namespace Examples.Components
{
    [Serializable]
    public class DamageableComponent : BaseComponent
    {
        public event Action OnDie;
        public event Action<float> OnHpChanged;
        public event Action<float> OnMaxHpChanged;

        [SerializeField]
        private float startVitality;

        private float hp;
        private float maxHp;
        
        public Stat Vitality { get; private set; }

        public float Hp
        {
            get { return hp; }
            set
            {
                if (hp <= 0)
                {
                    Debug.Log("Died");
                    return;
                }

                value = Mathf.Clamp(value, 0, MaxHp);

                if (value != hp)
                {
                    hp = value;
                    OnHpChanged?.Invoke(hp);

                    if (hp == 0)
                    {
                        OnDie?.Invoke();
                    }
                }
            }
        }

        public float MaxHp
        {
            get { return maxHp; }
            set
            {
                if (maxHp == value) return;

                maxHp = value;

                if (value < hp)
                {
                    Hp = value;
                }

                OnMaxHpChanged?.Invoke(value);
            }
        }

        public override void Initialize(Entity myEntity)
        {
            base.Initialize(myEntity);
            Vitality = new Stat(startVitality);
            maxHp = Vitality.Value;
            hp = maxHp;
            Vitality.OnModifiersChanged += OnHpStatValueChanged;
        }

        ~DamageableComponent()
        {
            if (Vitality?.OnModifiersChanged != null)
            {
                Vitality.OnModifiersChanged -= OnHpStatValueChanged;
            }
        }

        private void OnHpStatValueChanged(float newValue)
        {
            MaxHp = newValue;
        }
    }
}