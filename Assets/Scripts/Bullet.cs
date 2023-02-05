using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] public float damage = 100f;
    [SerializeField] string[] damageTheseTags;
    [SerializeField] bool destroyOnHit;

    [Tooltip("Keep this zero and it won't be destroyed. ")]
    [SerializeField] float destroyInTime;

    private void Start()
    {
        if (destroyInTime > 0)
        {
            StartCoroutine(DestroyInSeconds(destroyInTime));
        }
        damage += (PlayerStats.Instance.bulletDamage * 10);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        foreach (string tag in damageTheseTags)
        {
            if (other.tag == tag)
            {
                other.GetComponent<HealthManager>().onLostHealth.Invoke(damage);
                if (destroyOnHit)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    IEnumerator DestroyInSeconds(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
