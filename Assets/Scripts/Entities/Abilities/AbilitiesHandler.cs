using System;
using System.Collections.Generic;
using Entities.Components;
using Entities.Effects;
using UnityEngine;

namespace Entities.Abilities
{
// The AbilitiesHandler class manages a collection of Ability classes and allows
// for performing abilities that affect Entities.
    [Serializable]
    public class AbilitiesHandler : BaseComponent
    {
        [SerializeReference, ReferencePicker(TypeGrouping = TypeGrouping.ByFlatName), ReorderableList]
        private IAmAbility[] startingAbilities;

        // A dictionary mapping Ability types to the Ability objects
        private Dictionary<Type, IAmAbility> abilities;

        public override void Initialize(Entity myEntity)
        {
            base.Initialize(myEntity);
            abilities = new Dictionary<Type, IAmAbility>();

            foreach (IAmAbility ability in startingAbilities)
            {
                AddAbility((IAmAbility)ability.Clone(), myEntity);
            }
        }

        public bool TryGetAbility<T>(out T ability) where T : IAmAbility
        {
            if (abilities.TryGetValue(typeof(T), out IAmAbility searchedAbilityInterface))
            {
                ability = (T)searchedAbilityInterface;
                return true;
            }

            ability = default(T);
            return false;
        }

        // Adds an Ability to the abilities dictionary
        public void AddAbility(IAmAbility ability, Entity abilityOwner)
        {
            abilities.Add(ability.GetType(), ability);
            ability.Initialize(abilityOwner);
        }

        // Removes an Ability from the abilities dictionary
        public void RemoveAbility<T>() where T : IAmAbility
        {
            abilities.Remove(typeof(T));
        }

        // Performs the ability with the given type, affecting the given Entity or
        // another separate Entity.
        public void PerformAbility<T>(DefaultAbilityArgs args) where T : IAmAbility
        {
            if (abilities.ContainsKey(typeof(T)))
            {
                abilities[typeof(T)].Perform(args);
            }
        }

        public override void UpdateComponent()
        {
            base.UpdateComponent();

            foreach (IAmAbility ability in abilities.Values)
            {
                ability.UpdateAbility();
            }
        }

        public override void FixedUpdateComponent()
        {
            foreach (IAmAbility ability in abilities.Values)
            {
                ability.FixedUpdateAbility();
            }
        }
    }
}