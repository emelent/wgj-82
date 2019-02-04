using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{

    public float maxHealth = 100f;
    private float _health = 100f;
    public float health {
        get {
            return _health;
        }

        protected set{
            _health = Mathf.Clamp(value, 0, maxHealth);
        }
    }

    public void Damage(float dmg){
        health -= dmg;
    }

    public void Heal(float hp){
        health += hp;
    }

    public void Reset(){
        health = maxHealth;
    }
}
