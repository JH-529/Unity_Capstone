using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardScript : MonoBehaviour
{
    // 최종 계산식의 순서 결정 함수
    // 여기서 결정된 selectedCardCount에 따라 현재 플레이어가 선택한
    // 카드가 화면에 표시될 위치가 결정된다.
    #region 계산식 순서 함수
    public void SelectedCardSecond()
    {
        GameManager.selectedCardCount = SELECTED_CARD_COUNT.SECOND;
    }
    public void SelectedCardThird()
    {
        GameManager.selectedCardCount = SELECTED_CARD_COUNT.THIRD;
    }
    #endregion

    // 선택한 카드의 숫자카드가 몇번째인지를 지정하는 함수
    #region 선택한 카드 순서 인지 함수
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

    // 선택한 카드의 연산자 카드가 몇번째인지를 지정하는 함수
    #region 선택한 연산자 카드 순서 인지 함수
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
