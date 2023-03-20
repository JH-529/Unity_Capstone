using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NumberCard
{
    public int count;
    public int number;
    public Sprite sprite;

    public NumberCard()
    { count = 0; number = 0; sprite = null; }
    public NumberCard(int c, int n, Sprite s)
    {
        count = c;
        number = n;
        sprite = s;
    }
}

[System.Serializable]
public class OperatorCard
{
    public enum OPERATOR_TYPE
    {
        NONE,
        PLUS,
        MINUS,
        MULTIPLY,
        DIVIDE,
    };

    public string name;
    public OPERATOR_TYPE type;
    public Sprite sprite;

    public OperatorCard()
    { name = "empty operator"; type = OPERATOR_TYPE.NONE; sprite = null; }
    public OperatorCard(string n, OPERATOR_TYPE t, Sprite s)
    {
        name = n;
        type = t;
        sprite = s;
    }
}

[CreateAssetMenu(fileName = "CardSO", menuName = "Scriptable Object/CardSO")]
public class CardSO : ScriptableObject
{
    public NumberCard[] numbers;
    public OperatorCard[] operators;
}
