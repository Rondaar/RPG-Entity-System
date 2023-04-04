using Entities.Components;
using Entities.Effects;
using Examples.Components;

namespace Entities.Abilities
{
    public class AttackAbility : Ability<DefaultTargetAbilityArgs>
    {
        public AttackAbility() : base("attack")
        {
        }

        protected override void OnPerform()
        {
            base.OnPerform();
            if (!args.AbilityTarget.TryGetComponent(out DamageableComponent otherDamageableComponent)) return;
            otherDamageableComponent.Hp -= 1; //args.AbilityOwner.EntityStatsModel.Strength.Value;
        }
    }
}