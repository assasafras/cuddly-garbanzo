using UnityEngine;
using Item;

namespace Unit
{
    public class UnitModel : MonoBehaviour
    {
        public int HitpointsCurrent {
            get { return hitpointsCurrent; }
            set
            {
                hitpointsCurrent = value;
                // Raise a HP changed event!

                // Quick and dirty for now...
                if (hitpointsCurrent <= 0)
                {
                    // Kill the Unit.
                    Destroy(gameObject);
                }
            }
        }
        public int hitpointsCurrent;
        public int hitpointsMax;
        public int armourRating;
        public int resistance;
        public int damageMagical;
        public int damagePhysical;
        public int initiative;

        public double criticalChance;
        public double criticalBonusDamage;

        public Weapon primaryWeapon;
        public Weapon secondaryWeapon;
        public Armour armour;
        public Trinket trinket1;
        public Trinket trinket2;


        public void Attack(UnitModel other)
        {
            double totalDamagePhysical = damagePhysical;
            double totalDamageMagical = damageMagical;

            // Roll to see if crit.
            if (HasCrit(this))
            {
                totalDamagePhysical *= criticalBonusDamage;
                totalDamageMagical *= criticalBonusDamage;
            }

            other.TakeDamage((int) totalDamagePhysical, (int) totalDamageMagical);
        }

        private void TakeDamage(int physical, int magical)
        {
            // Calculate damage reductions.
            physical -= armourRating;
            magical -= resistance;

            // Take physical damage.
            if (physical > 0)
                this.HitpointsCurrent -= physical;

            // Take magical damage.
            if (magical > 0)
                this.HitpointsCurrent -= magical;
        }

        static bool HasCrit(UnitModel unit)
        {
            return (Random.value >= 1 - unit.criticalChance);
        }
    }
}