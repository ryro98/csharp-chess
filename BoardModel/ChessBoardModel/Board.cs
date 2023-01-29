using System;
using System.ComponentModel;
using System.Diagnostics.SymbolStore;
using System.Runtime.Serialization.Formatters;
using System.Xml.Serialization;

namespace ChessBoardModel
{
    public class Board
    {
        public Cell[,] TheGrid { get; set; }
        public int Size { get; set; }
        public int LegalMovesCounter { get; set; }
        public bool WhiteKingMoved { get; set; }
        public bool BlackKingMoved { get; set; }
        public bool WhiteEnpassant { get; set; }
        public bool BlackEnpassant { get; set; }
        public bool Check { get; set; }
        private bool CheckDetected { get; set; }
        public bool Draw { get; set; }
        public bool Stalemate { get; set; }
        public string ActualPlayer { get; set; }
        public IDictionary<string, int> Positions { get; set; }
        public List<string[]> ComputerLegalMoves { get; set; }
        public List<string[]> EvalMoves { get; set; }
        public List<int> EvalMovesInt { get; set; }
        public List<string[]> LastMove { get; set; }
        public FigureMap figureMap = new FigureMap();
        public Board()
        {
            Size = 8;
            TheGrid = new Cell[Size, Size];
            WhiteKingMoved = false;
            BlackKingMoved = false;
            WhiteEnpassant = false;
            BlackEnpassant = false;
            Check = false;
            Stalemate = false;
            Draw = false;
            CheckDetected = false;
            ActualPlayer = "player";
            Positions = new Dictionary<string, int>();
            ComputerLegalMoves = new List<string[]>();
            EvalMoves = new List<string[]>();
            EvalMovesInt = new List<int>();
            LastMove = new List<string[]>();
            for (int i = 0; i < Size; i++)
            {
                string x = "";
                switch (i)
                {
                    case 0:
                        x = "a";
                        break;
                    case 1:
                        x = "b";
                        break;
                    case 2:
                        x = "c";
                        break;
                    case 3:
                        x = "d";
                        break;
                    case 4:
                        x = "e";
                        break;
                    case 5:
                        x = "f";
                        break;
                    case 6:
                        x = "g";
                        break;
                    case 7:
                        x = "h";
                        break;
                    default:
                        break;
                }
                for (int j = 0; j < Size; j++)
                {
                    TheGrid[i, j] = new Cell(i, j);
                    TheGrid[i, j].Name = x + (8 - j);
                }
            }
        }

        public void MarkNextLegalMoves(Cell[,] theGrid, Cell currentCell, string chessPiece, bool isRotated, bool moveMode, bool setProtectedSquares)
        {
            string pieceName = chessPiece.Substring(0, chessPiece.Length - 1);
            string pieceColor = chessPiece.Substring(chessPiece.Length - 1);
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    theGrid[i, j].LegalNextMove = false;
                }
            }
            switch (pieceName)
            {
                case "pawn":
                    PawnMoves(theGrid, currentCell, pieceColor, isRotated, moveMode, setProtectedSquares);
                    break;
                case "bishop":
                    BishopMoves(theGrid, currentCell, pieceColor, isRotated, moveMode, setProtectedSquares);
                    break;
                case "knight":
                    KnightMoves(theGrid, currentCell, pieceColor, isRotated, moveMode, setProtectedSquares);
                    break;
                case "rook":
                    RookMoves(theGrid, currentCell, pieceColor, isRotated, moveMode, setProtectedSquares);
                    break;
                case "queen":
                    QueenMoves(theGrid, currentCell, pieceColor, isRotated, moveMode, setProtectedSquares);
                    break;
                case "king":
                    KingMoves(theGrid, currentCell, pieceColor, isRotated, moveMode, setProtectedSquares);
                    break;
            }
        }

        private void PawnMoves(Cell[,] theGrid, Cell currentCell, string pieceColor, bool isRotated, bool moveMode, bool setProtectedSquares)
        {
            //up
            if (((currentCell.FigureColor == "w" && !isRotated) || (currentCell.FigureColor == "b" && isRotated)) && currentCell.RowNumber > 0)
            {
                //movement
                if (!theGrid[currentCell.ColumnNumber, currentCell.RowNumber - 1].CurrentlyOccupied
                    && moveMode && SimulateNextMove(currentCell.Name, theGrid[currentCell.ColumnNumber, currentCell.RowNumber - 1].Name, pieceColor, isRotated, true))
                {
                    SetLegalMove(currentCell, theGrid[currentCell.ColumnNumber, currentCell.RowNumber - 1]);
                    if (currentCell.RowNumber == 6 && !theGrid[currentCell.ColumnNumber, currentCell.RowNumber - 2].CurrentlyOccupied
                    && SimulateNextMove(currentCell.Name, theGrid[currentCell.ColumnNumber, currentCell.RowNumber - 2].Name,
                            pieceColor, isRotated, true))
                    {
                        SetLegalMove(currentCell, theGrid[currentCell.ColumnNumber, currentCell.RowNumber - 2]);
                    }
                }
                //movement by 2 squares if its a check
                if (currentCell.RowNumber == 6)
                {
                    if (!theGrid[currentCell.ColumnNumber, currentCell.RowNumber - 1].CurrentlyOccupied
                        && !theGrid[currentCell.ColumnNumber, currentCell.RowNumber - 2].CurrentlyOccupied
                        && moveMode && Check && SimulateNextMove(currentCell.Name, theGrid[currentCell.ColumnNumber, currentCell.RowNumber - 2].Name, pieceColor, isRotated, true))
                    {
                        SetLegalMove(currentCell, theGrid[currentCell.ColumnNumber, currentCell.RowNumber - 2]);
                    }
                }
                //takes left
                if (currentCell.ColumnNumber > 0)
                {
                    if (theGrid[currentCell.ColumnNumber - 1, currentCell.RowNumber - 1].CurrentlyOccupied)
                    {
                        CheckForCheck(theGrid[currentCell.ColumnNumber - 1, currentCell.RowNumber - 1], pieceColor);
                        MoveModeCheck(currentCell, theGrid[currentCell.ColumnNumber - 1, currentCell.RowNumber - 1], moveMode, pieceColor, isRotated, setProtectedSquares);
                    }
                    else if (!moveMode)
                    {
                        theGrid[currentCell.ColumnNumber - 1, currentCell.RowNumber - 1].Attacked = true;
                        SetAttackedProtected(currentCell, theGrid[currentCell.ColumnNumber - 1, currentCell.RowNumber - 1]);
                    }
                }
                //takes right
                if (currentCell.ColumnNumber < 7)
                {
                    if (theGrid[currentCell.ColumnNumber + 1, currentCell.RowNumber - 1].CurrentlyOccupied)
                    {
                        CheckForCheck(theGrid[currentCell.ColumnNumber + 1, currentCell.RowNumber - 1], pieceColor);
                        MoveModeCheck(currentCell, theGrid[currentCell.ColumnNumber + 1, currentCell.RowNumber - 1], moveMode, pieceColor, isRotated, setProtectedSquares);
                    }
                    else if (!moveMode)
                    {
                        theGrid[currentCell.ColumnNumber + 1, currentCell.RowNumber - 1].Attacked = true;
                        SetAttackedProtected(currentCell, theGrid[currentCell.ColumnNumber + 1, currentCell.RowNumber - 1]);
                    }
                }
                //enpassant
                if ((WhiteEnpassant || BlackEnpassant) && currentCell.RowNumber == 3)
                {
                    //left
                    if (currentCell.ColumnNumber > 0 && theGrid[currentCell.ColumnNumber - 1, currentCell.RowNumber - 1].EnPassant
                        && SimulateNextMove(currentCell.Name, theGrid[currentCell.ColumnNumber - 1, currentCell.RowNumber - 1].Name, pieceColor, isRotated, true))
                    {
                        SetLegalMove(currentCell, theGrid[currentCell.ColumnNumber - 1, currentCell.RowNumber - 1]);
                    }

                    //right
                    if (currentCell.ColumnNumber < 7 && theGrid[currentCell.ColumnNumber + 1, currentCell.RowNumber - 1].EnPassant
                        && SimulateNextMove(currentCell.Name, theGrid[currentCell.ColumnNumber + 1, currentCell.RowNumber - 1].Name, pieceColor, isRotated, true))
                    {
                        SetLegalMove(currentCell, theGrid[currentCell.ColumnNumber + 1, currentCell.RowNumber - 1]);
                    }
                }
            }
            //down
            else if (((currentCell.FigureColor == "b" && !isRotated) || (currentCell.FigureColor == "w" && isRotated)) && currentCell.RowNumber < 7)
            {
                //movement
                if (!theGrid[currentCell.ColumnNumber, currentCell.RowNumber + 1].CurrentlyOccupied
                    && moveMode && SimulateNextMove(currentCell.Name, theGrid[currentCell.ColumnNumber, currentCell.RowNumber + 1].Name, pieceColor, isRotated, true))
                {
                    SetLegalMove(currentCell, theGrid[currentCell.ColumnNumber, currentCell.RowNumber + 1]);
                    if (currentCell.RowNumber == 1 && !theGrid[currentCell.ColumnNumber, currentCell.RowNumber + 2].CurrentlyOccupied
                        && SimulateNextMove(currentCell.Name, theGrid[currentCell.ColumnNumber, currentCell.RowNumber + 2].Name, pieceColor, isRotated, true))
                    {
                        SetLegalMove(currentCell, theGrid[currentCell.ColumnNumber, currentCell.RowNumber + 2]);
                    }
                }
                //movement by 2 squares if its a check
                if (currentCell.RowNumber < 6)
                {
                    if (!theGrid[currentCell.ColumnNumber, currentCell.RowNumber + 1].CurrentlyOccupied
                        && !theGrid[currentCell.ColumnNumber, currentCell.RowNumber + 2].CurrentlyOccupied
                        && moveMode && Check && SimulateNextMove(currentCell.Name, theGrid[currentCell.ColumnNumber, currentCell.RowNumber + 2].Name, pieceColor, isRotated, true))
                    {
                        SetLegalMove(currentCell, theGrid[currentCell.ColumnNumber, currentCell.RowNumber + 2]);
                    }
                }
                //takes left
                if (currentCell.ColumnNumber > 0)
                {
                    if (theGrid[currentCell.ColumnNumber - 1, currentCell.RowNumber + 1].CurrentlyOccupied)
                    {
                        CheckForCheck(theGrid[currentCell.ColumnNumber - 1, currentCell.RowNumber + 1], pieceColor);
                        MoveModeCheck(currentCell, theGrid[currentCell.ColumnNumber - 1, currentCell.RowNumber + 1], moveMode, pieceColor, isRotated, setProtectedSquares);
                    }
                    else if (!moveMode)
                    {
                        theGrid[currentCell.ColumnNumber - 1, currentCell.RowNumber + 1].Attacked = true;
                        SetAttackedProtected(currentCell, theGrid[currentCell.ColumnNumber - 1, currentCell.RowNumber + 1]);
                    }
                }
                //takes right
                if (currentCell.ColumnNumber < 7)
                {
                    if (theGrid[currentCell.ColumnNumber + 1, currentCell.RowNumber + 1].CurrentlyOccupied)
                    {
                        CheckForCheck(theGrid[currentCell.ColumnNumber + 1, currentCell.RowNumber + 1], pieceColor);
                        MoveModeCheck(currentCell, theGrid[currentCell.ColumnNumber + 1, currentCell.RowNumber + 1], moveMode, pieceColor, isRotated, setProtectedSquares);
                    }
                    else if (!moveMode)
                    {
                        theGrid[currentCell.ColumnNumber + 1, currentCell.RowNumber + 1].Attacked = true;
                        SetAttackedProtected(currentCell, theGrid[currentCell.ColumnNumber + 1, currentCell.RowNumber + 1]);
                    }
                }
                //enpassant
                if ((WhiteEnpassant || BlackEnpassant) && currentCell.RowNumber == 4)
                {
                    //left
                    if (currentCell.ColumnNumber > 0 && theGrid[currentCell.ColumnNumber - 1, currentCell.RowNumber + 1].EnPassant
                        && SimulateNextMove(currentCell.Name, theGrid[currentCell.ColumnNumber - 1, currentCell.RowNumber + 1].Name, pieceColor, isRotated, true))
                    {
                        SetLegalMove(currentCell, theGrid[currentCell.ColumnNumber - 1, currentCell.RowNumber + 1]);
                    }
                    //right
                    if (currentCell.ColumnNumber < 7 && theGrid[currentCell.ColumnNumber + 1, currentCell.RowNumber + 1].EnPassant
                        && SimulateNextMove(currentCell.Name, theGrid[currentCell.ColumnNumber + 1, currentCell.RowNumber + 1].Name, pieceColor, isRotated, true))
                    {
                        SetLegalMove(currentCell, theGrid[currentCell.ColumnNumber + 1, currentCell.RowNumber + 1]);
                    }
                }
            }
        }

        private void BishopMoves(Cell[,] theGrid, Cell currentCell, string pieceColor, bool isRotated, bool moveMode, bool setProtectedSquares)
        {
            int y;
            //up left
            if (currentCell.ColumnNumber > 0 && currentCell.RowNumber > 0)
            {
                y = currentCell.ColumnNumber - 1;
                for (int x = currentCell.RowNumber - 1; x >= 0; x--)
                {
                    MoveModeCheck(currentCell, theGrid[y, x], moveMode, pieceColor, isRotated, setProtectedSquares);
                    if (theGrid[y, x].CurrentlyOccupied)
                    {
                        CheckForCheck(theGrid[y, x], pieceColor);
                        break;
                    }
                    if (y > 0)
                    {
                        y -= 1;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            //up right
            if (currentCell.ColumnNumber > 0 && currentCell.RowNumber < 7)
            {
                y = currentCell.ColumnNumber - 1;
                for (int x = currentCell.RowNumber + 1; x <= 7; x++)
                {
                    MoveModeCheck(currentCell, theGrid[y, x], moveMode, pieceColor, isRotated, setProtectedSquares);
                    if (theGrid[y, x].CurrentlyOccupied)
                    {
                        CheckForCheck(theGrid[y, x], pieceColor);
                        break;
                    }
                    if (y > 0)
                    {
                        y -= 1;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            //down left
            if (currentCell.ColumnNumber < 7 && currentCell.RowNumber > 0)
            {
                y = currentCell.ColumnNumber + 1;
                for (int x = currentCell.RowNumber - 1; x >= 0; x--)
                {
                    MoveModeCheck(currentCell, theGrid[y, x], moveMode, pieceColor, isRotated, setProtectedSquares);
                    if (theGrid[y, x].CurrentlyOccupied)
                    {
                        CheckForCheck(theGrid[y, x], pieceColor);
                        break;
                    }
                    if (y < 7)
                    {
                        y += 1;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            //down right
            if (currentCell.ColumnNumber < 7 && currentCell.RowNumber < 7)
            {
                y = currentCell.ColumnNumber + 1;
                for (int x = currentCell.RowNumber + 1; x <= 7; x++)
                {
                    MoveModeCheck(currentCell, theGrid[y, x], moveMode, pieceColor, isRotated, setProtectedSquares);
                    if (theGrid[y, x].CurrentlyOccupied)
                    {
                        CheckForCheck(theGrid[y, x], pieceColor);
                        break;
                    }
                    if (y < 7)
                    {
                        y += 1;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        private void KnightMoves(Cell[,] theGrid, Cell currentCell, string pieceColor, bool isRotated, bool moveMode, bool setProtectedSquares)
        {
            if (currentCell.ColumnNumber < 7 && currentCell.RowNumber > 1)
            {
                CheckForCheck(theGrid[currentCell.ColumnNumber + 1, currentCell.RowNumber - 2], pieceColor);
                MoveModeCheck(currentCell, theGrid[currentCell.ColumnNumber + 1, currentCell.RowNumber - 2], moveMode, pieceColor, isRotated, setProtectedSquares);
            }
            if (currentCell.ColumnNumber < 6 && currentCell.RowNumber > 0)
            {
                CheckForCheck(theGrid[currentCell.ColumnNumber + 2, currentCell.RowNumber - 1], pieceColor);
                MoveModeCheck(currentCell, theGrid[currentCell.ColumnNumber + 2, currentCell.RowNumber - 1], moveMode, pieceColor, isRotated, setProtectedSquares);
            }
            if (currentCell.ColumnNumber < 6 && currentCell.RowNumber < 7)
            {
                CheckForCheck(theGrid[currentCell.ColumnNumber + 2, currentCell.RowNumber + 1], pieceColor);
                MoveModeCheck(currentCell, theGrid[currentCell.ColumnNumber + 2, currentCell.RowNumber + 1], moveMode, pieceColor, isRotated, setProtectedSquares);
            }
            if (currentCell.ColumnNumber < 7 && currentCell.RowNumber < 6)
            {
                CheckForCheck(theGrid[currentCell.ColumnNumber + 1, currentCell.RowNumber + 2], pieceColor);
                MoveModeCheck(currentCell, theGrid[currentCell.ColumnNumber + 1, currentCell.RowNumber + 2], moveMode, pieceColor, isRotated, setProtectedSquares);
            }
            if (currentCell.ColumnNumber > 0 && currentCell.RowNumber < 6)
            {
                CheckForCheck(theGrid[currentCell.ColumnNumber - 1, currentCell.RowNumber + 2], pieceColor);
                MoveModeCheck(currentCell, theGrid[currentCell.ColumnNumber - 1, currentCell.RowNumber + 2], moveMode, pieceColor, isRotated, setProtectedSquares);
            }
            if (currentCell.ColumnNumber > 1 && currentCell.RowNumber < 7)
            {
                CheckForCheck(theGrid[currentCell.ColumnNumber - 2, currentCell.RowNumber + 1], pieceColor);
                MoveModeCheck(currentCell, theGrid[currentCell.ColumnNumber - 2, currentCell.RowNumber + 1], moveMode, pieceColor, isRotated, setProtectedSquares);
            }
            if (currentCell.ColumnNumber > 1 && currentCell.RowNumber > 0)
            {
                CheckForCheck(theGrid[currentCell.ColumnNumber - 2, currentCell.RowNumber - 1], pieceColor);
                MoveModeCheck(currentCell, theGrid[currentCell.ColumnNumber - 2, currentCell.RowNumber - 1], moveMode, pieceColor, isRotated, setProtectedSquares);
            }
            if (currentCell.ColumnNumber > 0 && currentCell.RowNumber > 1)
            {
                CheckForCheck(theGrid[currentCell.ColumnNumber - 1, currentCell.RowNumber - 2], pieceColor);
                MoveModeCheck(currentCell, theGrid[currentCell.ColumnNumber - 1, currentCell.RowNumber - 2], moveMode, pieceColor, isRotated, setProtectedSquares);
            }
        }

        private void RookMoves(Cell[,] theGrid, Cell currentCell, string pieceColor, bool isRotated, bool moveMode, bool setProtectedSquares)
        {
            //up
            if (currentCell.ColumnNumber > 0)
            {
                for (int y = currentCell.ColumnNumber - 1; y >= 0; y--)
                {
                    MoveModeCheck(currentCell, theGrid[y, currentCell.RowNumber], moveMode, pieceColor, isRotated, setProtectedSquares);
                    if (theGrid[y, currentCell.RowNumber].CurrentlyOccupied)
                    {
                        CheckForCheck(theGrid[y, currentCell.RowNumber], pieceColor);
                        break;
                    }
                }
            }
            //down
            if (currentCell.ColumnNumber < 7)
            {
                for (int y = currentCell.ColumnNumber + 1; y <= 7; y++)
                {
                    MoveModeCheck(currentCell, theGrid[y, currentCell.RowNumber], moveMode, pieceColor, isRotated, setProtectedSquares);
                    if (theGrid[y, currentCell.RowNumber].CurrentlyOccupied)
                    {
                        CheckForCheck(theGrid[y, currentCell.RowNumber], pieceColor);
                        break;
                    }
                }
            }
            //left
            if (currentCell.RowNumber > 0)
            {
                for (int x = currentCell.RowNumber - 1; x >= 0; x--)
                {
                    MoveModeCheck(currentCell, theGrid[currentCell.ColumnNumber, x], moveMode, pieceColor, isRotated, setProtectedSquares);
                    if (theGrid[currentCell.ColumnNumber, x].CurrentlyOccupied)
                    {
                        CheckForCheck(theGrid[currentCell.ColumnNumber, x], pieceColor);
                        break;
                    }
                }
            }
            //right
            if (currentCell.RowNumber < 7)
            {
                for (int x = currentCell.RowNumber + 1; x <= 7; x++)
                {
                    MoveModeCheck(currentCell, theGrid[currentCell.ColumnNumber, x], moveMode, pieceColor, isRotated, setProtectedSquares);
                    if (theGrid[currentCell.ColumnNumber, x].CurrentlyOccupied)
                    {
                        CheckForCheck(theGrid[currentCell.ColumnNumber, x], pieceColor);
                        break;
                    }
                }
            }
        }

        private void QueenMoves(Cell[,] theGrid, Cell currentCell, string pieceColor, bool isRotated, bool moveMode, bool setProtectedSquares)
        {
            //up
            if (currentCell.ColumnNumber > 0)
            {
                for (int i = currentCell.ColumnNumber - 1; i >= 0; i--)
                {
                    MoveModeCheck(currentCell, theGrid[i, currentCell.RowNumber], moveMode, pieceColor, isRotated, setProtectedSquares);
                    if (theGrid[i, currentCell.RowNumber].CurrentlyOccupied)
                    {
                        CheckForCheck(theGrid[i, currentCell.RowNumber], pieceColor);
                        break;
                    }
                }
            }
            //down
            if (currentCell.ColumnNumber < 7)
            {
                for (int i = currentCell.ColumnNumber + 1; i <= 7; i++)
                {
                    MoveModeCheck(currentCell, theGrid[i, currentCell.RowNumber], moveMode, pieceColor, isRotated, setProtectedSquares);
                    if (theGrid[i, currentCell.RowNumber].CurrentlyOccupied)
                    {
                        CheckForCheck(theGrid[i, currentCell.RowNumber], pieceColor);
                        break;
                    }
                }
            }
            //left
            if (currentCell.RowNumber > 0)
            {
                for (int x = currentCell.RowNumber - 1; x >= 0; x--)
                {
                    MoveModeCheck(currentCell, theGrid[currentCell.ColumnNumber, x], moveMode, pieceColor, isRotated, setProtectedSquares);
                    if (theGrid[currentCell.ColumnNumber, x].CurrentlyOccupied)
                    {
                        CheckForCheck(theGrid[currentCell.ColumnNumber, x], pieceColor);
                        break;
                    }
                }
            }
            //right
            if (currentCell.RowNumber < 7)
            {
                for (int x = currentCell.RowNumber + 1; x <= 7; x++)
                {
                    MoveModeCheck(currentCell, theGrid[currentCell.ColumnNumber, x], moveMode, pieceColor, isRotated, setProtectedSquares);
                    if (theGrid[currentCell.ColumnNumber, x].CurrentlyOccupied)
                    {
                        CheckForCheck(theGrid[currentCell.ColumnNumber, x], pieceColor);
                        break;
                    }
                }
            }
            int y = currentCell.ColumnNumber - 1;
            //up left
            if (currentCell.ColumnNumber > 0 && currentCell.RowNumber > 0)
            {
                for (int x = currentCell.RowNumber - 1; x >= 0; x--)
                {
                    MoveModeCheck(currentCell, theGrid[y, x], moveMode, pieceColor, isRotated, setProtectedSquares);
                    if (theGrid[y, x].CurrentlyOccupied)
                    {
                        CheckForCheck(theGrid[y, x], pieceColor);
                        break;
                    }
                    if (y > 0)
                    {
                        y -= 1;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            //up right
            if (currentCell.ColumnNumber > 0 && currentCell.RowNumber < 7)
            {
                y = currentCell.ColumnNumber - 1;
                for (int x = currentCell.RowNumber + 1; x <= 7; x++)
                {
                    MoveModeCheck(currentCell, theGrid[y, x], moveMode, pieceColor, isRotated, setProtectedSquares);
                    if (theGrid[y, x].CurrentlyOccupied)
                    {
                        CheckForCheck(theGrid[y, x], pieceColor);
                        break;
                    }
                    if (y > 0)
                    {
                        y -= 1;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            //down left
            if (currentCell.ColumnNumber < 7 && currentCell.RowNumber > 0)
            {
                y = currentCell.ColumnNumber + 1;
                for (int x = currentCell.RowNumber - 1; x >= 0; x--)
                {
                    MoveModeCheck(currentCell, theGrid[y, x], moveMode, pieceColor, isRotated, setProtectedSquares);
                    if (theGrid[y, x].CurrentlyOccupied)
                    {
                        CheckForCheck(theGrid[y, x], pieceColor);
                        break;
                    }
                    if (y < 7)
                    {
                        y += 1;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            //down right
            if (currentCell.ColumnNumber < 7 && currentCell.RowNumber < 7)
            {
                y = currentCell.ColumnNumber + 1;
                for (int x = currentCell.RowNumber + 1; x <= 7; x++)
                {
                    CheckForCheck(theGrid[y, x], pieceColor);
                    MoveModeCheck(currentCell, theGrid[y, x], moveMode, pieceColor, isRotated, setProtectedSquares);
                    if (theGrid[y, x].CurrentlyOccupied)
                    {
                        break;
                    }
                    if (y < 7)
                    {
                        y += 1;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        private void KingMoves(Cell[,] theGrid, Cell currentCell, string pieceColor, bool isRotated, bool moveMode, bool setProtectedSquares)
        {
            //up
            if (currentCell.ColumnNumber > 0)
            {
                CheckSquareForKing(currentCell, theGrid[currentCell.ColumnNumber - 1, currentCell.RowNumber], moveMode, pieceColor, isRotated, setProtectedSquares);
            }
            //up right
            if (currentCell.ColumnNumber > 0 && currentCell.RowNumber < 7)
            {
                CheckSquareForKing(currentCell, theGrid[currentCell.ColumnNumber - 1, currentCell.RowNumber + 1], moveMode, pieceColor, isRotated, setProtectedSquares);
            }
            //right
            if (currentCell.RowNumber < 7)
            {
                CheckSquareForKing(currentCell, theGrid[currentCell.ColumnNumber, currentCell.RowNumber+1], moveMode, pieceColor, isRotated, setProtectedSquares);
            }
            //down right
            if (currentCell.ColumnNumber < 7 && currentCell.RowNumber < 7)
            {
                CheckSquareForKing(currentCell, theGrid[currentCell.ColumnNumber + 1, currentCell.RowNumber + 1], moveMode, pieceColor, isRotated, setProtectedSquares);
            }
            //down
            if (currentCell.ColumnNumber < 7)
            {
                CheckSquareForKing(currentCell, theGrid[currentCell.ColumnNumber + 1, currentCell.RowNumber], moveMode, pieceColor, isRotated, setProtectedSquares);
            }
            //down left
            if (currentCell.ColumnNumber < 7 && currentCell.RowNumber > 0)
            {
                CheckSquareForKing(currentCell, theGrid[currentCell.ColumnNumber + 1, currentCell.RowNumber - 1], moveMode, pieceColor, isRotated, setProtectedSquares);
            }
            //left
            if (currentCell.RowNumber > 0)
            {
                CheckSquareForKing(currentCell, theGrid[currentCell.ColumnNumber, currentCell.RowNumber - 1], moveMode, pieceColor, isRotated, setProtectedSquares);
            }
            //up left
            if (currentCell.ColumnNumber > 0 && currentCell.RowNumber > 0)
            {
                CheckSquareForKing(currentCell, theGrid[currentCell.ColumnNumber - 1, currentCell.RowNumber - 1], moveMode, pieceColor, isRotated, setProtectedSquares);
            }
            //castle white king
            if (pieceColor == "w" && !WhiteKingMoved && !Check)
            {
                //white perspective
                //0-0
                if (!theGrid[5, 7].CurrentlyOccupied && !theGrid[6, 7].CurrentlyOccupied
                   && theGrid[7, 7].FigureName == "rook" && theGrid[7, 7].FigureColor == "w"
                   && theGrid[5, 7].Protecting.Count == 0 && theGrid[6, 7].Protecting.Count == 0)
                {
                    MoveModeCheck(currentCell, theGrid[6, 7], moveMode, pieceColor, isRotated, setProtectedSquares);
                }
                //0-0-0
                if (!theGrid[1, 7].CurrentlyOccupied && !theGrid[2, 7].CurrentlyOccupied && !theGrid[3, 7].CurrentlyOccupied
                    && theGrid[0, 7].FigureName == "rook" && theGrid[0, 7].FigureColor == "w"
                    && theGrid[2, 7].Protecting.Count == 0 && theGrid[3, 7].Protecting.Count == 0)
                {
                    MoveModeCheck(currentCell, theGrid[2, 7], moveMode, pieceColor, isRotated, setProtectedSquares);
                }
                //black perspective
                //0-0
                if (!theGrid[1, 0].CurrentlyOccupied && !theGrid[2, 0].CurrentlyOccupied
                    && theGrid[0, 0].FigureName == "rook" && theGrid[0, 0].FigureColor == "w"
                    && theGrid[1, 0].Protecting.Count == 0 && theGrid[2, 0].Protecting.Count == 0)
                {
                    MoveModeCheck(currentCell, theGrid[1, 0], moveMode, pieceColor, isRotated, setProtectedSquares);
                }
                //0-0-0
                if (!theGrid[4, 0].CurrentlyOccupied && !theGrid[5, 0].CurrentlyOccupied && !theGrid[6, 0].CurrentlyOccupied
                    && theGrid[7, 0].FigureName == "rook" && theGrid[7, 0].FigureColor == "w"
                    && theGrid[4, 0].Protecting.Count == 0 && theGrid[5, 0].Protecting.Count == 0)
                {
                    MoveModeCheck(currentCell, theGrid[5, 0], moveMode, pieceColor, isRotated, setProtectedSquares);
                }
            }
            //castle black king
            if (pieceColor == "b" && currentCell.FigureName == "king" && !BlackKingMoved && !Check)
            {
                //white perspective
                //0-0
                if (!theGrid[5, 0].CurrentlyOccupied && !theGrid[6, 0].CurrentlyOccupied
                    && theGrid[7, 0].FigureName == "rook" && theGrid[7, 0].FigureColor == "b"
                    && theGrid[5, 0].Attacking.Count == 0 && theGrid[6, 0].Attacking.Count == 0)
                {
                    MoveModeCheck(currentCell, theGrid[6, 0], moveMode, pieceColor, isRotated, setProtectedSquares);
                }
                //0-0-0
                if (!theGrid[1, 0].CurrentlyOccupied && !theGrid[2, 0].CurrentlyOccupied && !theGrid[3, 0].CurrentlyOccupied
                    && theGrid[0, 0].FigureName == "rook" && theGrid[0, 0].FigureColor == "b"
                    && theGrid[2, 0].Attacking.Count == 0 && theGrid[3, 0].Attacking.Count == 0)
                {
                    MoveModeCheck(currentCell, theGrid[2, 0], moveMode, pieceColor, isRotated, setProtectedSquares);
                }
                //black perspective
                //0-0
                if (!theGrid[1, 7].CurrentlyOccupied && !theGrid[2, 7].CurrentlyOccupied
                    && theGrid[0, 7].FigureName == "rook" && theGrid[0, 7].FigureColor == "b"
                    && theGrid[1, 7].Attacking.Count == 0 && theGrid[2, 7].Attacking.Count == 0)
                {
                    MoveModeCheck(currentCell, theGrid[1, 7], moveMode, pieceColor, isRotated, setProtectedSquares);
                }
                //0-0-0
                if (!theGrid[4, 7].CurrentlyOccupied && !theGrid[5, 7].CurrentlyOccupied && !theGrid[6, 7].CurrentlyOccupied
                    && theGrid[7, 7].FigureName == "rook" && theGrid[7, 7].FigureColor == "b"
                    && theGrid[4, 7].Attacking.Count == 0 && theGrid[5, 7].Attacking.Count == 0)
                {
                    MoveModeCheck(currentCell, theGrid[5, 7], moveMode, pieceColor, isRotated, setProtectedSquares);
                }
            }
        }
        private void CheckSquareForKing(Cell fromCell, Cell toCell, bool moveMode, string pieceColor, bool isRotated, bool setProtectedSquares)
        {
            if (toCell.FigureColor != pieceColor
                && ((pieceColor == "w" && toCell.Protecting.Count == 0) || pieceColor == "b" && toCell.Attacking.Count == 0))
            {
                MoveModeCheck(fromCell, toCell, moveMode, pieceColor, isRotated, setProtectedSquares);
            }
            SetAttackedProtected(fromCell, toCell);
        }

        public void ScanAttackedSquares(Cell[,] theGrid, string pieceColor, bool isRotated)
        {
            CheckDetected = false;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    theGrid[i, j].Attacked = false;
                    if (theGrid[i, j].Attacking?.Count > 0)
                    {
                        foreach (Cell cell in theGrid[i, j].Attacking.ToList())
                        {
                            if (cell.FigureName != pieceColor)
                            {
                                theGrid[i, j].Attacking.Remove(cell);
                            }
                        }
                    }
                    if (theGrid[i, j].Protecting?.Count > 0)
                    {
                        foreach (Cell cell in theGrid[i, j].Protecting.ToList())
                        {
                            if (cell.FigureName != pieceColor)
                            {
                                theGrid[i, j].Protecting.Remove(cell);
                            }
                        }
                    }

                }
            }
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    Cell cell = theGrid[x, y];
                    if (cell.FigureName != "" && cell.FigureColor != pieceColor)
                    {
                        MarkNextLegalMoves(theGrid, cell, cell.FigureName + cell.FigureColor.Substring(0, 1), isRotated, false, true);
                    }
                }
            }
            Check = CheckDetected;
        }
        private void MoveModeCheck(Cell fromCell, Cell toCell, bool moveMode, string figureColor, bool isRotated, bool setProtectedSquare)
        {
            if (moveMode)
            {
                if (toCell.FigureColor != figureColor && SimulateNextMove(fromCell.Name, toCell.Name, figureColor, isRotated, true))
                {
                    SetLegalMove(fromCell, toCell);
                }
            }
            else
            {
                toCell.Attacked = true;
            }
            SetAttackedProtected(fromCell, toCell);
        }
        private void SetAttackedProtected(Cell fromCell, Cell toCell)
        {
            if (fromCell.FigureColor == "w")
            {
                if (!toCell.Attacking.Contains(fromCell))
                {
                    toCell.Attacking.Add(fromCell);
                }
            }
            else
            {
                if (!toCell.Protecting.Contains(fromCell))
                {
                    toCell.Protecting.Add(fromCell);
                }
            }
        }
        public void SetLegalMove(Cell fromCell, Cell toCell)
        {
            toCell.LegalNextMove = true;
            LegalMovesCounter++;
            string[] legalMove = { fromCell.Name, toCell.Name };
            if (!ComputerLegalMoves.Contains(legalMove))
            {
                ComputerLegalMoves.Add(legalMove);
            }
        }
        public void CheckForCheck(Cell cell, string figureColor)
        {
            if (cell.FigureName == "king" && cell.FigureColor != figureColor)
            {
                CheckDetected = true;
            }
        }
        public Board GenerateCloneBoard()
        {
            Board board = new Board();
            board.ActualPlayer = ActualPlayer;
            board.Check = Check;
            board.BlackEnpassant = BlackEnpassant;
            board.WhiteEnpassant = WhiteEnpassant;
            board.ComputerLegalMoves = ComputerLegalMoves;
            board.Draw = Draw;
            board.LegalMovesCounter = LegalMovesCounter;
            board.TheGrid = board.GenerateCloneGrid();
            board.WhiteKingMoved = WhiteKingMoved;
            board.BlackKingMoved = BlackKingMoved;
            return board;
        }
        public Cell[,] GenerateCloneGrid()
        {
            Cell[,] simulatedGrid = new Cell[Size, Size];
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    Cell cell = new Cell(x, y);
                    cell.Name = TheGrid[x, y].Name;
                    cell.FigureName = TheGrid[x, y].FigureName;
                    cell.FigureColor = TheGrid[x, y].FigureColor;
                    cell.CurrentlyOccupied = TheGrid[x, y].CurrentlyOccupied;
                    cell.ColumnNumber = TheGrid[x, y].ColumnNumber;
                    cell.RowNumber = TheGrid[x, y].RowNumber;
                    cell.Attacked = TheGrid[x, y].Attacked;
                    cell.Attacking = TheGrid[x, y].Attacking;
                    cell.Protecting = TheGrid[x, y].Protecting;
                    simulatedGrid[x, y] = cell;
                }
            }
            return simulatedGrid;
        }
        public bool SimulateNextMove(string fromSquareName, string toSquareName, string pieceColor, bool isRotated, bool legalMoveMode)
        {
            Cell[,] simulatedGrid = GenerateCloneGrid();
            int fromX = NotationToNumbers(fromSquareName.Substring(0, 1));
            int fromY = NotationToNumbers(fromSquareName.Substring(1, 1));
            int toX = NotationToNumbers(toSquareName.Substring(0, 1));
            int toY = NotationToNumbers(toSquareName.Substring(1, 1));
            if (isRotated)
            {
                fromX = 7 - fromX;
                fromY = 7 - fromY;
                toX = 7 - toX;
                toY = 7 - toY;
            }

            Cell fromSquare = simulatedGrid[fromX, fromY];
            Cell toSquare = simulatedGrid[toX, toY];

            toSquare.FigureName = fromSquare.FigureName;
            toSquare.FigureColor = fromSquare.FigureColor;
            toSquare.CurrentlyOccupied = true;
            fromSquare.FigureName = "";
            fromSquare.FigureColor = "";
            fromSquare.CurrentlyOccupied = false;
            bool saveCheckStatus = Check;
            ScanAttackedSquares(simulatedGrid, pieceColor, isRotated);
            Check = saveCheckStatus;
            return !CheckDetected;
            //if (legalMoveMode)
            //{
            //    if (!Check)
            //    {
            //        return true;
            //    }
            //    else
            //    {
            //        return !CheckDetected;
            //    }
            //}
            //else
            //{
            //    if (toSquare.Protecting.Count > 1)
            //    {
            //        return true;
            //    }
            //    else
            //    {
            //        return false;
            //    }
            //}
        }
        public Boolean MateIsItMate(Cell[,] theGrid, string pieceColor, bool isRotated)
        {
            bool mate = false;
            LegalMovesCounter = 0;
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (theGrid[x, y].FigureName != "" && theGrid[x, y].FigureColor == pieceColor)
                    {
                        MarkNextLegalMoves(theGrid, theGrid[x, y], theGrid[x, y].FigureName + pieceColor, isRotated, true, false);
                    }
                }
            }
            if (LegalMovesCounter == 0)
            {
                if (Check)
                {
                    mate = true;
                }
                else
                {
                    Stalemate = true;
                }
            }
            return mate;
        }
        private int NotationToNumbers(string move)
        {
            int x = 0;
            switch (move)
            {
                case "8":
                case "a": break;
                case "7":
                case "b": x = 1; break;
                case "6":
                case "c": x = 2; break;
                case "5":
                case "d": x = 3; break;
                case "4":
                case "e": x = 4; break;
                case "3":
                case "f": x = 5; break;
                case "2":
                case "g": x = 6; break;
                case "1":
                case "h": x = 7; break;
                default: break;
            }
            return x;
        }
        public string[] ComputerMove(string player, bool isRotated)
        {
            Random random = new Random();
            string[] move = ComputerLegalMoves[random.Next(0, ComputerLegalMoves.Count)];
            Cell fromSquare = FindCell(TheGrid, move[0]);
            if (player == "random")
            {

            }
            else if (player == "pawns")
            {
                int x = 0;
                while (fromSquare.FigureName != "pawn" && !Check && x < 50 && ComputerLegalMoves.Count > 1)
                {
                    ComputerLegalMoves.Remove(move);
                    move = ComputerLegalMoves[random.Next(0, ComputerLegalMoves.Count)];
                    fromSquare = FindCell(TheGrid, move[0]);
                    x++;
                }
            }
            else if (player == "safe")
            {
                move = FindTakesAndDanger(ComputerLegalMoves);
            }
            return move;
        }
        private string[] FindTakesAndDanger(List<string[]> moves)
        {
            List<string[]> possibleTake = new List<string[]>();
            List<string> attackedPieces = new List<string>();

            //part 0 - search attacked pieces which need more defence
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (TheGrid[x, y].FigureColor == "b" && TheGrid[x, y].Attacking.Count > 0)
                    {
                        int weakestAttacker = 1000, weakestDefender = 1000;
                        foreach (Cell cell in TheGrid[x, y].Attacking)
                        {
                            if (PieceValue(cell) < weakestAttacker)
                            {
                                weakestAttacker = PieceValue(cell);
                            }
                        }
                        foreach (Cell cell in TheGrid[x, y].Protecting)
                        {
                            if (PieceValue(cell) < weakestDefender)
                            {
                                weakestDefender = PieceValue(cell);
                            }
                        }
                        if (TheGrid[x, y].Attacking.Count > TheGrid[x, y].Protecting.Count || PieceValue(TheGrid[x, y]) > weakestAttacker)
                        {
                            attackedPieces.Add(TheGrid[x, y].Name);
                        }
                    }
                }
            }
            //search pieces to take down
            foreach (string[] move in moves)
            {
                if (FindCell(TheGrid, move[1]).FigureName != "")
                {
                    possibleTake.Add(move);
                }
            }
            //part 1 - if both lists contains moves
            if (attackedPieces.Count > 0 && possibleTake.Count > 0)
            {
                //possible take
                Tuple<List<string[]>, List<int>> attTuple = GenerateTakesMove(possibleTake);
                List<string[]> selectedTakes = attTuple.Item1 as List<string[]>;
                List<int> selectedTakesEval = attTuple.Item2 as List<int>;
                //possible defence
                Tuple<List<string[]>, List<int>> defTuple = GenerateDefMove(moves, attackedPieces);
                List<string[]> selectedDefence = defTuple.Item1 as List<string[]>;
                List<int> selectedDefenceEval = defTuple.Item2 as List<int>;

                //here code

                if (selectedTakes.Count > 0 && selectedDefence.Count > 0)
                {
                    List<string[]> takesNDefence= new List<string[]>();
                    foreach (string[] move in selectedTakes)
                    {
                        if (selectedDefence.Contains(move))
                        {
                            takesNDefence.Add(move);
                        }
                    }
                    if (takesNDefence.Count > 0)
                    {
                        return takesNDefence[0];
                    }
                    if (selectedTakesEval.ToList().Max() >= selectedDefenceEval.ToList().Max())
                    {
                        return selectedTakes[selectedTakesEval.IndexOf(selectedTakesEval.Max())];
                    }
                    else
                    {
                        //return selectedDefence[selectedDefenceEval.IndexOf(selectedDefenceEval.Max())];
                        return ChooseDefMove(selectedDefence, selectedDefenceEval);
                    }
                }
                else if (selectedTakes.Count > 0)
                {
                    return selectedTakes[selectedTakesEval.IndexOf(selectedTakesEval.Max())];
                }
                else if (selectedDefence.Count > 0)
                {
                    //return selectedDefence[selectedDefenceEval.IndexOf(selectedDefenceEval.Max())];
                    return ChooseDefMove(selectedDefence, selectedDefenceEval);
                }
            }
            //part 2
            if (possibleTake.Count > 0)
            {
                Tuple<List<string[]>, List<int>> attTuple = GenerateTakesMove(possibleTake);
                List<string[]> selectedTakes = attTuple.Item1 as List<string[]>;
                List<int> selectedTakesEval = attTuple.Item2 as List<int>;
                if (selectedTakes.Count > 0)
                {
                    return selectedTakes[selectedTakesEval.IndexOf(selectedTakesEval.Max())];
                }
            }
            //part 3
            if (attackedPieces.Count > 0)
            {
                Tuple<List<string[]>, List<int>> tuple = GenerateDefMove(moves, attackedPieces);
                List<string[]> selectedDefence = tuple.Item1 as List<string[]>;
                List<int> selectedDefenceEval = tuple.Item2 as List<int>;
                if (selectedDefence.Count > 0)
                {
                    foreach (string[] move in selectedDefence)
                    {
                        Cell fromSquare = FindCell(TheGrid, move[0]);
                        int fromSquareValue = SquareValue(fromSquare.FigureName, move[0]);
                        int toSquareValue = SquareValue(fromSquare.FigureName, move[1]);
                        selectedDefenceEval[selectedDefence.IndexOf(move)] *= (toSquareValue - fromSquareValue);
                    }
                    return selectedDefence[selectedDefenceEval.IndexOf(selectedDefenceEval.Max())];
                }
            }
            //part 4
            List<string[]> selectedMoves = new List<string[]>();
            List<int> selectedMovesEval = new List<int>();
            foreach (string[] move in moves)
            {
                Cell[,] simulatedGrid = GenerateCloneGrid();
                Cell fromSquare = FindCell(simulatedGrid, move[0]);
                Cell toSquare = FindCell(simulatedGrid, move[1]);
                int fromSquareValue = SquareValue(fromSquare.FigureName, move[0]);
                int toSquareValue = SquareValue(fromSquare.FigureName, move[1]);
                int weakest = 1000;

                MakeMove(fromSquare, toSquare);
                ScanAttackedSquares(simulatedGrid, "b", false);
                //count defenders of square
                foreach (Cell cell in toSquare.Attacking.ToList())
                {
                    int pieceValue = PieceValue(cell);
                    //checking the weakest piece defending
                    if (pieceValue < weakest)
                    {
                        weakest = pieceValue;
                    }
                }
                //checking if the move is safe
                if (toSquare.Attacking.Count == 0
                 || (PieceValue(fromSquare) <= weakest && toSquare.Protecting.Count >= toSquare.Attacking.Count))
                {
                    selectedMoves.Add(move);
                    selectedMovesEval.Add(toSquareValue - fromSquareValue);
                }
            }
            List<string[]> bestMoves = new List<string[]>();
            int bestEval = selectedMovesEval.Max();
            foreach (string[] move in selectedMoves)
            {
                int moveEval = selectedMovesEval[selectedMoves.IndexOf(move)];
                if (moveEval == bestEval)
                {
                    bestMoves.Add(move);
                }
            }
            //return selectedMoves[selectedMovesEval.IndexOf(selectedMovesEval.Max())];
            return bestMoves[new Random().Next(0, bestMoves.Count)];
        }
        private int SquareValue(String pieceName, String move)
        {
            int squareValue = 0;
            switch (pieceName)
            {
                case "pawn":
                    squareValue = figureMap.PawnMap[NotationToNumbers(move.Substring(1, 1)), NotationToNumbers(move.Substring(0, 1))];
                    break;
                case "knight":
                    squareValue = figureMap.KnightMap[NotationToNumbers(move.Substring(1, 1)), NotationToNumbers(move.Substring(0, 1))];
                    break;
                case "bishop":
                    squareValue = figureMap.BishopMap[NotationToNumbers(move.Substring(1, 1)), NotationToNumbers(move.Substring(0, 1))];
                    break;
                case "rook":
                    squareValue = figureMap.RookMap[NotationToNumbers(move.Substring(1, 1)), NotationToNumbers(move.Substring(0, 1))];
                    break;
                case "queen":
                    squareValue = figureMap.QueenMap[NotationToNumbers(move.Substring(1, 1)), NotationToNumbers(move.Substring(0, 1))];
                    break;
                case "king":
                    squareValue = figureMap.KingMap[NotationToNumbers(move.Substring(1, 1)), NotationToNumbers(move.Substring(0, 1))];
                    break;
            }
            return squareValue;
        }
        private string[] ChooseDefMove(List<string[]> selectedDefence, List<int> selectedDefenceEval)
        {
            foreach (string[] move in selectedDefence)
            {
                Cell fromSquare = FindCell(TheGrid, move[0]);
                int fromSquareValue = SquareValue(fromSquare.FigureName, move[0]);
                int toSquareValue = SquareValue(fromSquare.FigureName, move[1]);
                selectedDefenceEval[selectedDefence.IndexOf(move)] *= (toSquareValue - fromSquareValue);
            }
            return selectedDefence[selectedDefenceEval.IndexOf(selectedDefenceEval.Max())];
        }
        private Tuple<List<string[]>, List<int>> GenerateDefMove(List<string[]> moves, List<string> attackedPieces)
        {
            List<string[]> selectedDefence = new List<string[]>();
            List<int> selectedDefenceEval = new List<int>();
            //possible defence
            foreach (string attackedPiece in attackedPieces)
            {
                Cell attackedCell = FindCell(TheGrid, attackedPiece);
                int howManyAttacked = attackedCell.Attacking.Count;
                int howManyProtected = attackedCell.Protecting.Count;
                //find weakest attacker
                int weakestAttacker = 1000;
                foreach (Cell cell in attackedCell.Attacking.ToList())
                {
                    int pieceValue = PieceValue(cell);
                    //checking the weakest piece defending
                    if (pieceValue < weakestAttacker)
                    {
                        weakestAttacker = pieceValue;
                    }
                }
                foreach (string[] move in moves)
                {
                    Cell[,] simulatedGrid = GenerateCloneGrid();
                    Cell fromSquare = FindCell(simulatedGrid, move[0]);
                    Cell toSquare = FindCell(simulatedGrid, move[1]);
                    MakeMove(fromSquare, toSquare);
                    ScanAttackedSquares(simulatedGrid, "b", false);
                    //comparing names the attacked square and the starting square
                    if (attackedCell.Name == fromSquare.Name)
                    {
                        int weakest = 1000;
                        //count defenders of square
                        foreach (Cell cell in toSquare.Attacking.ToList())
                        {
                            int pieceValue = PieceValue(cell);
                            //checking the weakest piece defending
                            if (pieceValue < weakest)
                            {
                                weakest = pieceValue;
                            }
                        }
                        //checking move
                        if (toSquare.Attacking.Count == 0
                            || (PieceValue(fromSquare) <= weakest && toSquare.Protecting.Count >= toSquare.Attacking.Count))
                        {
                            selectedDefence.Add(move);
                            selectedDefenceEval.Add(PieceValue(toSquare));
                        }
                    }
                    else if (PieceValue(attackedCell) <= weakestAttacker)
                    {
                        //checking move
                        if (attackedCell.Protecting.Count > howManyProtected || attackedCell.Attacking.Count < howManyAttacked)
                        {
                            int weakest = 1000;
                            foreach (Cell cell in toSquare.Attacking.ToList())
                            {
                                int pieceValue = PieceValue(cell);
                                //checking the weakest piece defending
                                if (pieceValue < weakest)
                                {
                                    weakest = pieceValue;
                                }
                            }
                            //checking if the move is safe
                            if (toSquare.Attacking.Count == 0
                             || (PieceValue(fromSquare) <= weakest && toSquare.Protecting.Count >= toSquare.Attacking.Count))
                            {
                                selectedDefence.Add(move);
                                selectedDefenceEval.Add(PieceValue(attackedCell));
                            }
                        }
                    }
                }
            }
            return new Tuple<List<string[]>, List<int>>(selectedDefence, selectedDefenceEval);
        }
        private Tuple<List<string[]>, List<int>> GenerateTakesMove(List<string[]> possibleTake)
        {
            List<string[]> selectedTakes = new List<string[]>();
            List<int> selectedTakesEval = new List<int>();
            foreach (string[] move in possibleTake)
            {
                Cell[,] simulatedGrid = GenerateCloneGrid();
                Cell fromSquare = FindCell(simulatedGrid, move[0]);
                Cell toSquare = FindCell(simulatedGrid, move[1]);
                //find weakest attacker
                int weakestAttacker = 1000;
                foreach (Cell cell in toSquare.Attacking.ToList())
                {
                    int pieceValue = PieceValue(cell);
                    //checking the weakest piece defending
                    if (pieceValue < weakestAttacker)
                    {
                        weakestAttacker = pieceValue;
                    }
                }
                //checking move
                if (toSquare.Attacking.Count == 0 || PieceValue(fromSquare) <= PieceValue(toSquare) || (PieceValue(fromSquare) <= weakestAttacker && toSquare.Protecting.Count > toSquare.Attacking.Count))
                {
                    selectedTakes.Add(move);
                    selectedTakesEval.Add(PieceValue(toSquare));
                }
            }
            return new Tuple<List<string[]>, List<int>>(selectedTakes, selectedTakesEval);
        }
        private bool CheckForFigure(string figure)
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (TheGrid[x, y].FigureName == "figure" && TheGrid[x, y].FigureColor == "b")
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public Cell FindCell(Cell[,] theGrid, string move) 
        {
            string whereX = move.Substring(0, 1);
            string whereY = move.Substring(1, 1);
            return theGrid[NotationToNumbers(whereX), NotationToNumbers(whereY)];
        }

        public int MaxValue(int firstDouble, int secondDouble)
        {
            if (firstDouble >= secondDouble)
            {
                return firstDouble;
            }
            return secondDouble;
        }
        public int Evaluate()
        {
            int whiteEval = CountMaterial("w");
            int blackEval = CountMaterial("b");

            return whiteEval - blackEval;
        }
        public int CountMaterial (string pieceColor)
        {
            int material = 0;

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (TheGrid[x, y].FigureColor == pieceColor)
                    {
                        material += PieceValue(TheGrid[x, y]);
                    }
                }
            }
            return material;
        }
        public int PieceValue (Cell cell)
        {
            //const int pawnValue = 100;
            //const int knightValue = 300;
            //const int bishopValue = 300;
            //const int rookValue = 500;
            //const int queenValue = 900;
            switch (cell.FigureName)
            {
                case "pawn":
                    return 100;
                case "bishop":
                    return 300;
                case "knight":
                    return 300;
                case "rook":
                    return 500;
                case "queen":
                    return 900;
                case "king":
                    return 1000;
            }
            return 0;
        }
        public void MakeMove(Cell fromSquare, Cell toSquare)
        {
            toSquare.CurrentlyOccupied = true;
            toSquare.FigureName = fromSquare.FigureName;
            toSquare.FigureColor = fromSquare.FigureColor;
            fromSquare.CurrentlyOccupied = false;
            fromSquare.FigureName = "";
            fromSquare.FigureColor = "";
        }
    }
}