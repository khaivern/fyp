using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Weapon : MonoBehaviour
{
	public int attackDamage = 200;
	public int enragedAttackDamage = 250;

	public float attackRange = 1f;

	public GameObject bossLRAPrefab;
	public GameObject firePoint;

	// Normal Stage
	// Attack Range
	[SerializeField] Transform attackPoint;
	[SerializeField] LayerMask playerLayer;

	public void Attack()
	{
		Collider2D colInfo = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);
		if (colInfo != null)
		{
			colInfo.GetComponent<Player>().TakeDamage(attackDamage);
		}
	}

    private void OnDrawGizmosSelected()
    {
		if(attackPoint == null)
        {
			return;
        }
		Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    public void EnragedAttackSwipe()
    {
		Instantiate(bossLRAPrefab, firePoint.transform.position, transform.rotation);
	}
}
