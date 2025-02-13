using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float health = 100f;
        bool isDead = false;

        public bool IsDead()
        {
            return isDead;
        }


        public void TakeDamage(float damage)
        {
            //print(Mathf.Max(10, 20));
            health = Mathf.Max(health - damage , 0);
            //print(health);
            if(health == 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if(isDead)
            {
                return;
            }
            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }
    }
}
