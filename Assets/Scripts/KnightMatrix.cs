using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KnightMatrix : MonoBehaviour {

    public static KnightMatrix instance;
    public List<Knight> l_Knights;

	public Knight[,] _kKnights = new Knight[7, 7];

    private bool _movingKnight;

    void Awake()
    {
        _movingKnight = false;

        if (instance == null)
            instance = this;

        int i_KnightCount = 0;

        for (int row = 0; row < _kKnights.GetLength(0); row++)
        {
            for (int col = 0; col < _kKnights.GetLength(1); col++)
            {
                _kKnights[row, col] = l_Knights[i_KnightCount].GetComponent<Knight>();
                i_KnightCount++;
            }
        }

    }

    public void AttemptMoveKnight (int content, int sequence)
    {
        if (_movingKnight) return;
        if (content != 1) return;

        _movingKnight = true;

        for (int row = 0; row < _kKnights.GetLength(0); row++)
        {
            for (int col = 0; col < _kKnights.GetLength(1); col++)
            {
                if (_kKnights[row, col].i_Sequence == sequence)
                {
                    CheckPossiblePositions(row, col);
                    return;
                }
            }
        }
    }

    private void CheckPossiblePositions (int row, int col)
    {
        int[] i_PossibleRows = new int[8] { 2, 2, 1, -1, -2, -2, -1, 1 };
        int[] i_PossibleCols = new int[8] { 1, -1, -2, -2, -1, 1, 2, 2 };

        for (int tryPos = 0; tryPos < i_PossibleRows.Length; tryPos++)
        {
            int i_NewRow = row + i_PossibleRows[tryPos];
            int i_NewCol = col + i_PossibleCols[tryPos];

            if ( ( (i_NewRow >= 0) && (i_NewRow < _kKnights.GetLength(0)) ) &&
                 ( (i_NewCol >= 0) && (i_NewCol < _kKnights.GetLength(1)) )  )
            {
                if (_kKnights[i_NewRow, i_NewCol].i_SquareContent == 0)
                    HideKnightAnimation(row, col, i_NewRow, i_NewCol);
            }
        }
    }


    private void HideAnimationCallback(int rowFrom, int colFrom, int rowTo, int colTo)
    {
        MoveKnight(rowFrom, colFrom, rowTo, colTo);
    }

    private void HideKnightAnimation (int rowFrom, int colFrom, int rowTo, int colTo)
    {
        _kKnights[rowFrom, colFrom].GetComponent<Knight>().HideAnimation(HideAnimationCallback, rowFrom, colFrom, rowTo, colTo);
    }
    

    private void MoveKnight (int rowFrom, int colFrom, int rowTo, int colTo)
    {
        Score.instance.AddMovement();

        Knight b_KnightAuxiliar;
        float f_KnightXPos = 0f;
        float f_KnightYPos = 0f;
        float f_BlankXPos = 0f;
        float f_BlankYPos = 0f;
        float f_KnightAuxXpos = 0f;
        float f_KnightAuxYpos = 0f;
        int row1 = -1;
        int col1 = -1;
        int row2 = -1;
        int col2 = -1;

        f_KnightXPos = _kKnights[rowFrom, colFrom].GetComponent<RectTransform>().anchoredPosition.x;
        f_KnightYPos = _kKnights[rowFrom, colFrom].GetComponent<RectTransform>().anchoredPosition.y;
        row1 = rowFrom;
        col1 = colFrom;

        f_BlankXPos = _kKnights[rowTo, colTo].GetComponent<RectTransform>().anchoredPosition.x;
        f_BlankYPos = _kKnights[rowTo, colTo].GetComponent<RectTransform>().anchoredPosition.y;
        row2 = rowTo;
        col2 = colTo;

        f_KnightAuxXpos = f_KnightXPos;
        f_KnightAuxYpos = f_KnightYPos;
        Vector2 vT;
        vT = _kKnights[row1, col1].GetComponent<RectTransform>().anchoredPosition;
        vT.x = f_BlankXPos;
        vT.y = f_BlankYPos;
        _kKnights[row1,col1].GetComponent<RectTransform>().anchoredPosition = vT;
        vT = _kKnights[row2,col2].GetComponent<RectTransform>().anchoredPosition;
        vT.x = f_KnightAuxXpos;
        vT.y = f_KnightAuxYpos;
        _kKnights[row2,col2].GetComponent<RectTransform>().anchoredPosition = vT;

        b_KnightAuxiliar = _kKnights[row1,col1];
        _kKnights[row1,col1] = _kKnights[row2,col2];
        _kKnights[row2,col2] = b_KnightAuxiliar;

        ShowKnightAnimation(rowTo, colTo);
    }

    private void ShowKnightAnimation(int rowTo, int colTo)
    {
        _kKnights[rowTo, colTo].GetComponent<Knight>().ShowAnimation(CheckSequence);
    }

    private void CheckSequence ()
    {
        int i_TrueSequence = 1;

        for (int row = 0; row < _kKnights.GetLength(0); row++)
        {
            for (int col = 0; col < _kKnights.GetLength(1); col++)
            {
                if (_kKnights[row, col].i_Sequence == -1) continue;

                if (_kKnights[row, col].i_Sequence == i_TrueSequence)
                {
                    i_TrueSequence++;
                }
                else
                {
                    _movingKnight = false;
                    return;
                }
            }
        }

        GameFinished();
    }

    private void GameFinished () 
    {
        Menu.instance.ShowModalWindow(4);
        Score.instance.InsertFinalResults();
    }
}
