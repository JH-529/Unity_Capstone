using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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
        //GameManager.button = EventSystem.current.currentSelectedGameObject;
        //GameManager.button.GetComponent<Button>().interactable = false;
        GameManager.selectNumberCard = PLAYER_CARD.FIRST;
        if(GameManager.selectedCardCount == SELECTED_CARD_COUNT.SECOND)
        { GameManager.selectedCardCount = SELECTED_CARD_COUNT.THIRD; }
    }
    public void SelectSecondNumberCard()
    {
        //GameManager.button = EventSystem.current.currentSelectedGameObject;
        //GameManager.button.GetComponent<Button>().interactable = false;
        GameManager.selectNumberCard = PLAYER_CARD.SECOND;
        if (GameManager.selectedCardCount == SELECTED_CARD_COUNT.SECOND)
        { GameManager.selectedCardCount = SELECTED_CARD_COUNT.THIRD; }
    }
    public void SelectThirdNumberCard()
    {
        //GameManager.button = EventSystem.current.currentSelectedGameObject;
        //GameManager.button.GetComponent<Button>().interactable = false;
        GameManager.selectNumberCard = PLAYER_CARD.THIRD;
        if (GameManager.selectedCardCount == SELECTED_CARD_COUNT.SECOND)
        { GameManager.selectedCardCount = SELECTED_CARD_COUNT.THIRD; }
    }
    #endregion

    // ������ ī���� ������ ī�尡 ���°������ �����ϴ� �Լ�
    #region ������ ������ ī�� ���� ���� �Լ�
    public void SelectFirstOperatorCard()
    {
        if(GameManager.selectedCardCount == SELECTED_CARD_COUNT.FIRST)
        { ButtonLock(); }      
        GameManager.selectOperatorCard = PLAYER_OPERATOR.FIRST;
        GameManager.selectedCardCount = SELECTED_CARD_COUNT.SECOND;
    }
    public void SelectSecondOperatorCard()
    {
        if (GameManager.selectedCardCount == SELECTED_CARD_COUNT.FIRST)
        { ButtonLock(); }
        GameManager.selectOperatorCard = PLAYER_OPERATOR.SECOND;
        GameManager.selectedCardCount = SELECTED_CARD_COUNT.SECOND;        
    }
    #endregion

    void LockButton(GameObject buttonObject)
    {
        if (buttonObject.GetComponent<Button>())
        {
            buttonObject.GetComponent<Button>().interactable = false;
        }        
    }

    void ButtonLock()
    {
        if (GameManager.selectNumberCard == PLAYER_CARD.FIRST)
        {
            GameObject button = GameObject.Find("Card1");
            LockButton(button);
        }
        if (GameManager.selectNumberCard == PLAYER_CARD.SECOND)
        {
            GameObject button = GameObject.Find("Card2");
            LockButton(button);
        }
        if (GameManager.selectNumberCard == PLAYER_CARD.THIRD)
        {
            GameObject button = GameObject.Find("Card3");
            LockButton(button);
        }
    }
}
