using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Status_Icon_Holder : MonoBehaviour
{
    private const float ICONSPACING = 0.65f;

    [SerializeField] private Target type;
    [SerializeField] private GameObject statusIcon;
    [SerializeField] private Direction facing;

    private Dictionary<StatusEffect, GameObject> iconList = new Dictionary<StatusEffect, GameObject>();

    public void SetStatusList(Dictionary<StatusEffect, int> statusEffects)
    {
        ClearList();

        (Dictionary<StatusEffect, int> buffs, Dictionary<StatusEffect, int> debuffs) = statusEffects.SplitStatusEffects();
        if (buffs.Count > 3 || debuffs.Count > 3)
        {
            CreateCondensedDisplay(buffs, debuffs);
        } else
        {
            if (type == Target.ENEMY)
            {
                CreateEnemyDisplay(buffs, debuffs);
            } else
            {
                CreatePlayerDisplay(buffs, debuffs);
            }
        }
    }

    private void CreateCondensedDisplay(Dictionary<StatusEffect, int> buffs, Dictionary<StatusEffect, int> debuffs)
    {

    }

    private void CreatePlayerDisplay(Dictionary<StatusEffect, int> buffs, Dictionary<StatusEffect, int> debuffs)
    {
        for (int i = 0; i < buffs.Count; i++)
        {
            StatusEffect se = buffs.ElementAt(i).Key;
            int duration = buffs.ElementAt(i).Value;

            GameObject tempIcon = Instantiate(statusIcon, transform);
            Vector3 pos = tempIcon.transform.position;
            pos.y += ICONSPACING * i;
            tempIcon.transform.position = pos;

            tempIcon.GetComponent<Status_Effect_Indicator>().Setup(se, duration);

            iconList.Add(se, tempIcon);
        }

        for (int i = 0; i < debuffs.Count; i++)
        {
            StatusEffect se = debuffs.ElementAt(i).Key;
            int duration = debuffs.ElementAt(i).Value;

            GameObject tempIcon = Instantiate(statusIcon, transform);
            Vector3 pos = tempIcon.transform.position;
            pos.y += ICONSPACING * i;
            pos.x += ICONSPACING * facing.NumericRepresentation();
            tempIcon.transform.position = pos;

            tempIcon.GetComponent<Status_Effect_Indicator>().Setup(se, duration);

            iconList.Add(se, tempIcon);
        }
    }

    private void CreateEnemyDisplay(Dictionary<StatusEffect, int> buffs, Dictionary<StatusEffect, int> debuffs)
    {
        for (int i = 0; i < buffs.Count; i++)
        {
            StatusEffect se = buffs.ElementAt(i).Key;
            int duration = buffs.ElementAt(i).Value;

            GameObject tempIcon = Instantiate(statusIcon, transform);
            Vector3 pos = tempIcon.transform.position;
            pos.x += ICONSPACING * i;
            tempIcon.transform.position = pos;

            tempIcon.GetComponent<Status_Effect_Indicator>().Setup(se, duration);

            iconList.Add(se, tempIcon);
        }

        for (int i = 0; i < debuffs.Count; i++)
        {
            StatusEffect se = debuffs.ElementAt(i).Key;
            int duration = debuffs.ElementAt(i).Value;

            GameObject tempIcon = Instantiate(statusIcon, transform);
            Vector3 pos = tempIcon.transform.position;
            pos.x += ICONSPACING * i;
            pos.y += ICONSPACING * facing.NumericRepresentation();
            tempIcon.transform.position = pos;

            tempIcon.GetComponent<Status_Effect_Indicator>().Setup(se, duration);

            iconList.Add(se, tempIcon);
        }
    }

    private void ClearList()
    {
        foreach (var se in iconList)
        {
            Destroy(se.Value);
        }

        iconList = new Dictionary<StatusEffect, GameObject>();
    }
}
