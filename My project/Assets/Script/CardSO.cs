using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NumberCard
{
    public string name;
    public int attack;
    public Sprite sprite;

    public NumberCard(string n, int a, Sprite s)
    {
        name = n;
        attack = a;
        sprite = s;
    }
}

[System.Serializable]
public class OperatorCard
{
    public enum OPERATOR_TYPE
    {
        PLUS,
        MINUS,
        MULTIPLY,
        DIVIDE,
    };

    public string name;
    public OPERATOR_TYPE type;
    public Sprite sprite;

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
