using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour
{
    // ���� ������ ���� ���� �Լ�
    // ���⼭ ������ selectedCardCount�� ���� ���� �÷��̾ ������
    // ī�尡 ȭ�鿡 ǥ�õ� ��ġ�� �����ȴ�.
    #region ���� ���� �Լ�
    public void SelectedCardSecond()
    {
        GameManager.selectedCardCount = SELECTED_CARD_COUNT.SECOND;
    }
    public void SelectedCardThird()
    {
        GameManager.selectedCardCount = SELECTED_CARD_COUNT.THIRD;
    }
    #endregion

    // ������ ī���� ����ī�尡 ���°������ �����ϴ� �Լ�
    #region ������ ī�� ���� ���� �Լ�
    public void SelectFirstNumberCard()
    {
        GameManager.selectNumberCard = PLAYER_CARD.FIRST;
        if(GameManager.selectedCardCount == SELECTED_CARD_COUNT.SECOND)
        { GameManager.selectedCardCount = SELECTED_CARD_COUNT.THIRD; }
    }
    public void SelectSecondNumberCard()
    {
        GameManager.selectNumberCard = PLAYER_CARD.SECOND;
        if (GameManager.selectedCardCount == SELECTED_CARD_COUNT.SECOND)
        { GameManager.selectedCardCount = SELECTED_CARD_COUNT.THIRD; }
    }
    public void SelectThirdNumberCard()
    {
        GameManager.selectNumberCard = PLAYER_CARD.THIRD;
        if (GameManager.selectedCardCount == SELECTED_CARD_COUNT.SECOND)
        { GameManager.selectedCardCount = SELECTED_CARD_COUNT.THIRD; }
    }
    #endregion

    // ������ ī���� ������ ī�尡 ���°������ �����ϴ� �Լ�
    #region ������ ������ ī�� ���� ���� �Լ�
    public void SelectFirstOperatorCard()
    {
        GameManager.selectOperatorCard = PLAYER_OPERATOR.FIRST;
        GameManager.selectedCardCount = SELECTED_CARD_COUNT.SECOND;
    }
    public void SelectSecondOperatorCard()
    {
        GameManager.selectOperatorCard = PLAYER_OPERATOR.SECOND;
        GameManager.selectedCardCount = SELECTED_CARD_COUNT.SECOND;
    }
    #endregion

    
}
