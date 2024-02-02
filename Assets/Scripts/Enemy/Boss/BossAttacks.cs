using System.Collections;
using UnityEngine;

public class BossAttacks : MonoBehaviour
{
    public enum AttackType
    {
        None,
        Attack1,
        Attack2,
        Attack3
    }

    [SerializeField] private BossData bossData;

    private AttackType currentAttack;

    private void Start()
    {
        currentAttack = AttackType.None;
        StartCoroutine(InitialDelayRoutine());
    }

    private IEnumerator InitialDelayRoutine()
    {
        yield return new WaitForSeconds(bossData.bossPresentationDuration);

        StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        while (!bossData.isDead)
        {
            yield return new WaitForSeconds(Random.Range(2f, 5f));

            //Open to debate, we do it by prob or by phase, what do you prefer Facu?
            float rand = Random.value;
            if (rand < 0.4f)
                currentAttack = AttackType.Attack1;
            else if (rand < 0.7f)
                currentAttack = AttackType.Attack2;
            else
                currentAttack = AttackType.Attack3;

            PerformAttack();
        }
    }

    public void PerformAttack()
    {
        switch (currentAttack)
        {
            case AttackType.Attack1:

                break;
            case AttackType.Attack2:

                break;
            case AttackType.Attack3:

                break;
            default:
                break;
        }
    }
}