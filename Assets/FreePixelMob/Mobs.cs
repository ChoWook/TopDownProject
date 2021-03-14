using System.Collections;
using UnityEngine;
[RequireComponent(typeof(CanvasGroup))]
public class Mobs : MonoBehaviour
{
	static int AnimatorWalk = Animator.StringToHash("Walk");
	static int AnimatorAttack = Animator.StringToHash("Attack");
	public int Hp_max = 10;
	int Hp;
	Animator _animator;

	void Awake()
	{
		_animator = GetComponentInChildren<Animator>();
		Hp = Hp_max;
	}

	void Start()
	{
		StartCoroutine(Animate());
	}

	IEnumerator Animate()
	{
		yield return new WaitForSeconds(5f);
		while (true)
		{
			_animator.SetBool(AnimatorWalk, true);
			yield return new WaitForSeconds(1f);

			_animator.transform.localScale = new Vector3(-1, 1, 1);
			yield return new WaitForSeconds(1f);

			_animator.SetBool(AnimatorWalk, false);
			yield return new WaitForSeconds(1f);

			_animator.SetTrigger(AnimatorAttack);
			yield return new WaitForSeconds(1f);

			_animator.SetTrigger(AnimatorAttack);
			yield return new WaitForSeconds(1f);

			_animator.SetTrigger(AnimatorAttack);
			yield return new WaitForSeconds(5f);
		}
	}

	public int takeDamage(int dmg)
    {
		Hp -= dmg;
		if(Hp <= 0)
        {
			Hp = 0;
			Debug.Log("Destroyed");
			Destroy(gameObject);
        }
		return Hp;
    }
}
