using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class RecipeSO : ScriptableObject
{
    public string recipleName;

    public List<KitchenObjectSO> kitchenObjectSOList;

    [Tooltip("上菜得分：简单菜3分，复杂菜5分")]
    public int scoreReward = 3;
}
