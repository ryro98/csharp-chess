namespace ChessBoardModel
{
    public class Board
    {
        public int Size { get; set; }
        public Cell[,] theGrid { get; set; }

        public Boolean whiteKingMoved { get; set; }
        public Boolean blackKingMoved { get; set; }
        public Boolean whiteEnpassant { get; set; }
        public Boolean blackEnpassant { get; set; }
        public Boolean check { get; set; }
        private Boolean checkDetected = false;
        public Board (int s)
        {
            Size = s;

            theGrid = new Cell[Size, Size];
            whiteKingMoved = false;
            blackKingMoved = false;
            whiteEnpassant = false;
            blackEnpassant = false;
            check = false;
            for (int i = 0; i < Size; i++)
            {
                String x = "";
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
                    theGrid[i, j] = new Cell(i, j);
                    theGrid[i, j].Name = x + (8 - j);
                }
            }
        }

        public void MarkNextLegalMoves(Cell[,] theGrid, Cell currentCell, string chessPiece, Boolean isRotated, Boolean moveMode)
        {
            String pieceName = chessPiece.Substring(0, chessPiece.Length - 1);
            String pieceColor = chessPiece.Substring(chessPiece.Length - 1);
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    theGrid[i, j].LegalNextMove = false;
                }
            }
            switch(pieceName)
            {
                case "pawn":
                    pawnMoves(theGrid, currentCell, pieceColor, isRotated, moveMode);
                    break;
                case "bishop":
                    bishopMoves(theGrid, currentCell, pieceColor, isRotated, moveMode);
                    break;
                case "knight":
                    knightMoves(theGrid, currentCell, pieceColor, isRotated, moveMode);
                    break;
                case "rook":
                    rookMoves(theGrid, currentCell, pieceColor, isRotated, moveMode);
                    break;
                case "queen":
                    queenMoves(theGrid, currentCell, pieceColor, isRotated, moveMode);
                    break;
                case "king":
                    kingMoves(theGrid, currentCell, pieceColor, isRotated, moveMode);
                    break;
            }
        }

        private void pawnMoves(Cell[,] theGrid, Cell currentCell, String pieceColor, Boolean isRotated, Boolean moveMode)
        {
            //up
            if ((currentCell.FigureColor == "w" && !isRotated) || (currentCell.FigureColor == "b" && isRotated))
            {
                if (!theGrid[currentCell.RowNumber, currentCell.ColumnNumber - 1].CurrentlyOccupied)
                {
                    if (moveMode && SimulateNextMove(currentCell.Name, theGrid[currentCell.RowNumber, currentCell.ColumnNumber - 1].Name, pieceColor, isRotated))
                    {
                        theGrid[currentCell.RowNumber, currentCell.ColumnNumber - 1].LegalNextMove = true;
                    }
                    if (currentCell.ColumnNumber == 6 && !theGrid[currentCell.RowNumber, currentCell.ColumnNumber - 2].CurrentlyOccupied
                        && moveMode && SimulateNextMove(currentCell.Name, theGrid[currentCell.RowNumber, currentCell.ColumnNumber - 2].Name,
                                pieceColor, isRotated))
                    {
                        theGrid[currentCell.RowNumber, currentCell.ColumnNumber - 2].LegalNextMove = true;
                    }
                }
                if (currentCell.RowNumber > 0)
                {
                    if (theGrid[currentCell.RowNumber - 1, currentCell.ColumnNumber - 1].CurrentlyOccupied)
                    {
                        CheckForCheck(theGrid[currentCell.RowNumber - 1, currentCell.ColumnNumber - 1], pieceColor);
                        MoveModeCheck(currentCell, theGrid[currentCell.RowNumber - 1, currentCell.ColumnNumber - 1], moveMode, pieceColor, isRotated);
                    }
                    else if (!moveMode)
                    {
                        theGrid[currentCell.RowNumber - 1, currentCell.ColumnNumber - 1].Attacked = true;
                    }
                }
                if (currentCell.RowNumber < 7)
                {
                    if (theGrid[currentCell.RowNumber + 1, currentCell.ColumnNumber - 1].CurrentlyOccupied)
                    {   
                        CheckForCheck(theGrid[currentCell.RowNumber + 1, currentCell.ColumnNumber - 1], pieceColor);
                        MoveModeCheck(currentCell, theGrid[currentCell.RowNumber + 1, currentCell.ColumnNumber - 1], moveMode, pieceColor, isRotated);
                    }
                    else if (!moveMode)
                    {
                        theGrid[currentCell.RowNumber + 1, currentCell.ColumnNumber - 1].Attacked = true;
                    }
                }
                //white enpassant
                if (whiteEnpassant && currentCell.FigureColor == "w" && currentCell.ColumnNumber == 3)
                {
                    //left
                    if (currentCell.RowNumber > 0 && theGrid[currentCell.RowNumber - 1, currentCell.ColumnNumber].FigureName == "pawn"
                        && moveMode && SimulateNextMove(currentCell.Name, theGrid[currentCell.RowNumber - 1, currentCell.ColumnNumber - 1].Name, pieceColor, isRotated))
                    {
                        theGrid[currentCell.RowNumber - 1, currentCell.ColumnNumber - 1].LegalNextMove = true;
                    }
                    //right
                    if (currentCell.RowNumber < 7 && theGrid[currentCell.RowNumber + 1, currentCell.ColumnNumber].FigureName == "pawn" 
                        && moveMode && SimulateNextMove(currentCell.Name, theGrid[currentCell.RowNumber + 1, currentCell.ColumnNumber - 1].Name, pieceColor, isRotated))
                    {
                        theGrid[currentCell.RowNumber + 1, currentCell.ColumnNumber - 1].LegalNextMove = true;
                    }
                    
                }
                //black enpassant
                if (blackEnpassant && currentCell.FigureColor == "b" && currentCell.ColumnNumber == 3)
                {
                    //left
                    if (currentCell.RowNumber > 0 && theGrid[currentCell.RowNumber - 1, currentCell.ColumnNumber].FigureName == "pawn"
                        && moveMode && SimulateNextMove(currentCell.Name, theGrid[currentCell.RowNumber - 1, currentCell.ColumnNumber - 1].Name, pieceColor, isRotated))
                    {
                        theGrid[currentCell.RowNumber - 1, currentCell.ColumnNumber - 1].LegalNextMove = true;
                    }
                    //right
                    if (currentCell.RowNumber < 7 && theGrid[currentCell.RowNumber + 1, currentCell.ColumnNumber].FigureName == "pawn" 
                        && moveMode && SimulateNextMove(currentCell.Name, theGrid[currentCell.RowNumber + 1, currentCell.ColumnNumber - 1].Name, pieceColor, isRotated))
                    {
                        theGrid[currentCell.RowNumber + 1, currentCell.ColumnNumber - 1].LegalNextMove = true;
                    }


                }
            }
            //down
            if ((currentCell.FigureColor == "b" && !isRotated) || (currentCell.FigureColor == "w" && isRotated))
            {
                if (!theGrid[currentCell.RowNumber, currentCell.ColumnNumber + 1].CurrentlyOccupied)
                {
                    if (moveMode && SimulateNextMove(currentCell.Name, theGrid[currentCell.RowNumber, currentCell.ColumnNumber + 1].Name, pieceColor, isRotated))
                    {
                        theGrid[currentCell.RowNumber, currentCell.ColumnNumber + 1].LegalNextMove = true;
                    }
                    if (currentCell.ColumnNumber == 1 && !theGrid[currentCell.RowNumber, currentCell.ColumnNumber + 2].CurrentlyOccupied
                        && moveMode && SimulateNextMove(currentCell.Name, theGrid[currentCell.RowNumber, currentCell.ColumnNumber + 2].Name, pieceColor, isRotated))
                    {
                        theGrid[currentCell.RowNumber, currentCell.ColumnNumber + 2].LegalNextMove = true;
                    }
                }
                if (currentCell.RowNumber > 0)
                {
                    if (theGrid[currentCell.RowNumber - 1, currentCell.ColumnNumber + 1].CurrentlyOccupied)
                    {
                        CheckForCheck(theGrid[currentCell.RowNumber - 1, currentCell.ColumnNumber + 1], pieceColor);
                        MoveModeCheck(currentCell, theGrid[currentCell.RowNumber - 1, currentCell.ColumnNumber + 1], moveMode, pieceColor, isRotated);
                    }
                    else if (!moveMode)
                    {
                        theGrid[currentCell.RowNumber - 1, currentCell.ColumnNumber + 1].Attacked = true;
                    }
                }
                if (currentCell.RowNumber < 7)
                {
                    if (theGrid[currentCell.RowNumber + 1, currentCell.ColumnNumber + 1].CurrentlyOccupied)
                    {
                        CheckForCheck(theGrid[currentCell.RowNumber + 1, currentCell.ColumnNumber + 1], pieceColor);
                        MoveModeCheck(currentCell, theGrid[currentCell.RowNumber + 1, currentCell.ColumnNumber + 1], moveMode, pieceColor, isRotated);
                    }
                    else if (!moveMode)
                    {
                        theGrid[currentCell.RowNumber + 1, currentCell.ColumnNumber + 1].Attacked = true;
                    }
                }
                //white enpassant
                if (whiteEnpassant && currentCell.FigureColor == "w" && currentCell.ColumnNumber == 4)
                {
                    //left
                    if (currentCell.RowNumber > 0 && theGrid[currentCell.RowNumber - 1, currentCell.ColumnNumber].FigureName == "pawn"
                        && moveMode && SimulateNextMove(currentCell.Name, theGrid[currentCell.RowNumber - 1, currentCell.ColumnNumber + 1].Name, pieceColor, isRotated))
                    {
                        theGrid[currentCell.RowNumber - 1, currentCell.ColumnNumber + 1].LegalNextMove = true;
                    }
                    //right
                    if (currentCell.RowNumber < 7 && theGrid[currentCell.RowNumber + 1, currentCell.ColumnNumber].FigureName == "pawn"
                        && moveMode && SimulateNextMove(currentCell.Name, theGrid[currentCell.RowNumber + 1, currentCell.ColumnNumber + 1].Name, pieceColor, isRotated))
                    {
                        theGrid[currentCell.RowNumber + 1, currentCell.ColumnNumber + 1].LegalNextMove = true;
                    }

                }
                //black enpassant
                if (blackEnpassant && currentCell.FigureColor == "b" && currentCell.ColumnNumber == 4)
                {
                    //left
                    if (currentCell.RowNumber > 0 && theGrid[currentCell.RowNumber - 1, currentCell.ColumnNumber].FigureName == "pawn"
                        && moveMode && SimulateNextMove(currentCell.Name, theGrid[currentCell.RowNumber - 1, currentCell.ColumnNumber + 1].Name, pieceColor, isRotated))
                    {
                        theGrid[currentCell.RowNumber - 1, currentCell.ColumnNumber + 1].LegalNextMove = true;
                    }
                    //right
                    if (currentCell.RowNumber < 7 && theGrid[currentCell.RowNumber + 1, currentCell.ColumnNumber].FigureName == "pawn"
                        && moveMode && SimulateNextMove(currentCell.Name, theGrid[currentCell.RowNumber + 1, currentCell.ColumnNumber + 1].Name, pieceColor, isRotated))
                    {
                        theGrid[currentCell.RowNumber + 1, currentCell.ColumnNumber + 1].LegalNextMove = true;
                    }

                }
            }
        }

        private void bishopMoves(Cell[,] theGrid, Cell currentCell, String pieceColor, Boolean isRotated, Boolean moveMode)
        {
            int y = currentCell.RowNumber - 1;
            //up left
            if (currentCell.RowNumber > 0 && currentCell.ColumnNumber > 0)
            {
                for (int x = currentCell.ColumnNumber - 1; x >= 0; x--)
                {
                    MoveModeCheck(currentCell, theGrid[y, x], moveMode, pieceColor, isRotated);
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
            if (currentCell.RowNumber > 0 && currentCell.ColumnNumber < 7)
            {
                y = currentCell.RowNumber - 1;
                for (int x = currentCell.ColumnNumber + 1; x <= 7; x++)
                {
                    MoveModeCheck(currentCell, theGrid[y, x], moveMode, pieceColor, isRotated);
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
            if (currentCell.RowNumber < 7 && currentCell.ColumnNumber > 0)
            {
                y = currentCell.RowNumber + 1;
                for (int x = currentCell.ColumnNumber - 1; x >= 0; x--)
                {
                    MoveModeCheck(currentCell, theGrid[y, x], moveMode, pieceColor, isRotated);
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
            if (currentCell.RowNumber < 7 && currentCell.ColumnNumber < 7)
            {
                y = currentCell.RowNumber + 1;
                for (int x = currentCell.ColumnNumber + 1; x <= 7; x++)
                {
                    MoveModeCheck(currentCell, theGrid[y, x], moveMode, pieceColor, isRotated);
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

        private void knightMoves(Cell[,] theGrid, Cell currentCell, String pieceColor, Boolean isRotated, Boolean moveMode) 
        {
            if (currentCell.RowNumber < 7 && currentCell.ColumnNumber > 1)
            {
                CheckForCheck(theGrid[currentCell.RowNumber + 1, currentCell.ColumnNumber - 2], pieceColor);
                MoveModeCheck(currentCell, theGrid[currentCell.RowNumber + 1, currentCell.ColumnNumber - 2], moveMode, pieceColor, isRotated);
            }
            if (currentCell.RowNumber < 6 && currentCell.ColumnNumber > 0)
            {
                CheckForCheck(theGrid[currentCell.RowNumber + 2, currentCell.ColumnNumber - 1], pieceColor);
                MoveModeCheck(currentCell, theGrid[currentCell.RowNumber + 2, currentCell.ColumnNumber - 1], moveMode, pieceColor, isRotated);
            }
            if (currentCell.RowNumber < 6 && currentCell.ColumnNumber < 7)
            {
                CheckForCheck(theGrid[currentCell.RowNumber + 2, currentCell.ColumnNumber + 1], pieceColor);
                MoveModeCheck(currentCell, theGrid[currentCell.RowNumber + 2, currentCell.ColumnNumber + 1], moveMode, pieceColor, isRotated);
            }
            if (currentCell.RowNumber < 7 && currentCell.ColumnNumber < 6)
            {
                CheckForCheck(theGrid[currentCell.RowNumber + 1, currentCell.ColumnNumber + 2], pieceColor);
                MoveModeCheck(currentCell, theGrid[currentCell.RowNumber + 1, currentCell.ColumnNumber + 2], moveMode, pieceColor, isRotated);
            }
            if (currentCell.RowNumber > 0 && currentCell.ColumnNumber < 6)
            {
                CheckForCheck(theGrid[currentCell.RowNumber - 1, currentCell.ColumnNumber + 2], pieceColor);
                MoveModeCheck(currentCell, theGrid[currentCell.RowNumber - 1, currentCell.ColumnNumber + 2], moveMode, pieceColor, isRotated);
            }
            if (currentCell.RowNumber > 1 && currentCell.ColumnNumber < 7)
            {
                CheckForCheck(theGrid[currentCell.RowNumber - 2, currentCell.ColumnNumber + 1], pieceColor);
                MoveModeCheck(currentCell, theGrid[currentCell.RowNumber - 2, currentCell.ColumnNumber + 1], moveMode, pieceColor, isRotated);
            }
            if (currentCell.RowNumber > 1 && currentCell.ColumnNumber > 0)
            {
                CheckForCheck(theGrid[currentCell.RowNumber - 2, currentCell.ColumnNumber - 1], pieceColor);
                MoveModeCheck(currentCell, theGrid[currentCell.RowNumber - 2, currentCell.ColumnNumber - 1], moveMode, pieceColor, isRotated);
            }
            if (currentCell.RowNumber > 0 && currentCell.ColumnNumber > 1)
            {
                CheckForCheck(theGrid[currentCell.RowNumber - 1, currentCell.ColumnNumber - 2], pieceColor);
                MoveModeCheck(currentCell, theGrid[currentCell.RowNumber - 1, currentCell.ColumnNumber - 2], moveMode, pieceColor, isRotated);
            }
        }

        private void rookMoves(Cell[,] theGrid, Cell currentCell, String pieceColor, Boolean isRotated, Boolean moveMode)
        {
            //up
            if (currentCell.RowNumber > 0)
            {
                for (int y = currentCell.RowNumber - 1; y >= 0; y--)
                {
                    MoveModeCheck(currentCell, theGrid[y, currentCell.ColumnNumber], moveMode, pieceColor, isRotated);
                    if (theGrid[y, currentCell.ColumnNumber].CurrentlyOccupied)
                    {
                        CheckForCheck(theGrid[y, currentCell.ColumnNumber], pieceColor);
                        break;
                    }
                }
            }
            //down
            if (currentCell.RowNumber < 7)
            {
                for (int y = currentCell.RowNumber + 1; y <= 7; y++)
                {
                    MoveModeCheck(currentCell, theGrid[y, currentCell.ColumnNumber], moveMode, pieceColor, isRotated);
                    if (theGrid[y, currentCell.ColumnNumber].CurrentlyOccupied)
                    {
                        CheckForCheck(theGrid[y, currentCell.ColumnNumber], pieceColor);
                        break;
                    }
                }
            }
            //left
            if (currentCell.ColumnNumber > 0)
            {
                for (int x = currentCell.ColumnNumber - 1; x >= 0; x--)
                {
                    MoveModeCheck(currentCell, theGrid[currentCell.RowNumber, x], moveMode, pieceColor, isRotated);
                    if (theGrid[currentCell.RowNumber, x].CurrentlyOccupied)
                    {
                        CheckForCheck(theGrid[currentCell.RowNumber, x], pieceColor);
                        break;
                    }
                }
            }
            //right
            if (currentCell.ColumnNumber < 7)
            {
                for (int x = currentCell.ColumnNumber + 1; x <= 7; x++)
                {
                    MoveModeCheck(currentCell, theGrid[currentCell.RowNumber, x], moveMode, pieceColor, isRotated);
                    if (theGrid[currentCell.RowNumber, x].CurrentlyOccupied)
                    {
                        CheckForCheck(theGrid[currentCell.RowNumber, x], pieceColor);
                        break;
                    }
                }
            }
        }

        private void queenMoves(Cell[,] theGrid, Cell currentCell, String pieceColor, Boolean isRotated, Boolean moveMode)
        {
            //up
            if (currentCell.RowNumber > 0)
            {
                for (int i = currentCell.RowNumber - 1; i >= 0; i--)
                {
                    MoveModeCheck(currentCell, theGrid[i, currentCell.ColumnNumber], moveMode, pieceColor, isRotated);
                    if (theGrid[i, currentCell.ColumnNumber].CurrentlyOccupied)
                    {
                        CheckForCheck(theGrid[i, currentCell.ColumnNumber], pieceColor);
                        break;
                    }
                }
            }
            //down
            if (currentCell.RowNumber < 7)
            {
                for (int i = currentCell.RowNumber + 1; i <= 7; i++)
                {
                    MoveModeCheck(currentCell, theGrid[i, currentCell.ColumnNumber], moveMode, pieceColor, isRotated);
                    if (theGrid[i, currentCell.ColumnNumber].CurrentlyOccupied)
                    {
                        CheckForCheck(theGrid[i, currentCell.ColumnNumber], pieceColor);
                        break;
                    }
                }
            }
            //left
            if (currentCell.ColumnNumber > 0)
            {
                for (int x = currentCell.ColumnNumber - 1; x >= 0; x--)
                {
                    MoveModeCheck(currentCell, theGrid[currentCell.RowNumber, x], moveMode, pieceColor, isRotated);
                    if (theGrid[currentCell.RowNumber, x].CurrentlyOccupied)
                    {
                        CheckForCheck(theGrid[currentCell.RowNumber, x], pieceColor);
                        break;
                    }
                }
            }
            //right
            if (currentCell.ColumnNumber < 7)
            {
                for (int x = currentCell.ColumnNumber + 1; x <= 7; x++)
                {
                    MoveModeCheck(currentCell, theGrid[currentCell.RowNumber, x], moveMode, pieceColor, isRotated);
                    if (theGrid[currentCell.RowNumber, x].CurrentlyOccupied)
                    {
                        CheckForCheck(theGrid[currentCell.RowNumber, x], pieceColor);
                        break;
                    }
                }
            }
            int y = currentCell.RowNumber - 1;
            //up left
            if (currentCell.RowNumber > 0 && currentCell.ColumnNumber > 0)
            {
                for (int x = currentCell.ColumnNumber - 1; x >= 0; x--)
                {
                    MoveModeCheck(currentCell, theGrid[y, x], moveMode, pieceColor, isRotated);
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
            if (currentCell.RowNumber > 0 && currentCell.ColumnNumber < 7)
            {
                y = currentCell.RowNumber - 1;
                for (int x = currentCell.ColumnNumber + 1; x <= 7; x++)
                {
                    MoveModeCheck(currentCell, theGrid[y, x], moveMode, pieceColor, isRotated);
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
            if (currentCell.RowNumber < 7 && currentCell.ColumnNumber > 0)
            {
                y = currentCell.RowNumber + 1;
                for (int x = currentCell.ColumnNumber - 1; x >= 0; x--)
                {
                    MoveModeCheck(currentCell, theGrid[y, x], moveMode, pieceColor, isRotated);
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
            if (currentCell.RowNumber < 7 && currentCell.ColumnNumber < 7)
            {
                y = currentCell.RowNumber + 1;
                for (int x = currentCell.ColumnNumber + 1; x <= 7; x++)
                {
                    CheckForCheck(theGrid[y, x], pieceColor);
                    MoveModeCheck(currentCell, theGrid[y, x], moveMode, pieceColor, isRotated);
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

        private void kingMoves(Cell[,] theGrid, Cell currentCell, String pieceColor, Boolean isRotated, Boolean moveMode)
        {
            //up - left
            if (currentCell.RowNumber > 0)
            {
                if (theGrid[currentCell.RowNumber - 1, currentCell.ColumnNumber].FigureColor != pieceColor
                    && !theGrid[currentCell.RowNumber - 1, currentCell.ColumnNumber].Attacked)
                {
                    MoveModeCheck(currentCell, theGrid[currentCell.RowNumber - 1, currentCell.ColumnNumber], moveMode, pieceColor, isRotated);
                }
            }
            //up right - up left
            if (currentCell.RowNumber > 0 && currentCell.ColumnNumber < 7)
            {
                if (theGrid[currentCell.RowNumber - 1, currentCell.ColumnNumber + 1].FigureColor != pieceColor
                    && !theGrid[currentCell.RowNumber - 1, currentCell.ColumnNumber + 1].Attacked)
                {
                    MoveModeCheck(currentCell, theGrid[currentCell.RowNumber - 1, currentCell.ColumnNumber + 1], moveMode, pieceColor, isRotated);
                }
            }
            //right - up
            if (currentCell.ColumnNumber < 7)
            {
                if (theGrid[currentCell.RowNumber, currentCell.ColumnNumber + 1].FigureColor != pieceColor
                    && !theGrid[currentCell.RowNumber, currentCell.ColumnNumber + 1].Attacked)
                {
                    MoveModeCheck(currentCell, theGrid[currentCell.RowNumber, currentCell.ColumnNumber + 1], moveMode, pieceColor, isRotated);
                }
            }
            //down right
            if (currentCell.RowNumber < 7 && currentCell.ColumnNumber < 7)
            {
                if (theGrid[currentCell.RowNumber + 1, currentCell.ColumnNumber + 1].FigureColor != pieceColor
                    && !theGrid[currentCell.RowNumber + 1, currentCell.ColumnNumber + 1].Attacked)
                {
                    MoveModeCheck(currentCell, theGrid[currentCell.RowNumber + 1, currentCell.ColumnNumber + 1], moveMode, pieceColor, isRotated);
                }
            }
            //down - right
            if (currentCell.RowNumber < 7)
            {
                if (theGrid[currentCell.RowNumber + 1, currentCell.ColumnNumber].FigureColor != pieceColor
                    && !theGrid[currentCell.RowNumber + 1, currentCell.ColumnNumber].Attacked)
                {
                    MoveModeCheck(currentCell, theGrid[currentCell.RowNumber + 1, currentCell.ColumnNumber], moveMode, pieceColor, isRotated);
                }
            }
            //down left
            if (currentCell.RowNumber < 7 && currentCell.ColumnNumber > 0)
            {
                if (theGrid[currentCell.RowNumber + 1, currentCell.ColumnNumber - 1].FigureColor != pieceColor
                    && !theGrid[currentCell.RowNumber + 1, currentCell.ColumnNumber - 1].Attacked)
                {
                    MoveModeCheck(currentCell, theGrid[currentCell.RowNumber + 1, currentCell.ColumnNumber - 1], moveMode, pieceColor, isRotated);
                }
            }
            //left
            if (currentCell.ColumnNumber > 0)
            {
                if (theGrid[currentCell.RowNumber, currentCell.ColumnNumber - 1].FigureColor != pieceColor
                    && !theGrid[currentCell.RowNumber, currentCell.ColumnNumber - 1].Attacked)
                {
                    MoveModeCheck(currentCell, theGrid[currentCell.RowNumber, currentCell.ColumnNumber - 1], moveMode, pieceColor, isRotated);
                }
            }
            //up left
            if (currentCell.RowNumber > 0 && currentCell.ColumnNumber > 0)
            {
                if (theGrid[currentCell.RowNumber - 1, currentCell.ColumnNumber - 1].FigureColor != pieceColor
                    && !theGrid[currentCell.RowNumber - 1, currentCell.ColumnNumber - 1].Attacked)
                {
                    MoveModeCheck(currentCell, theGrid[currentCell.RowNumber - 1, currentCell.ColumnNumber - 1], moveMode, pieceColor, isRotated);
                }
            }
            //castle white king
            if (pieceColor == "w" && !whiteKingMoved && !check)
            {
                //white perspective
                if (currentCell.RowNumber == 4 && currentCell.ColumnNumber == 7)
                {
                    //0-0
                     if (!theGrid[5, 7].CurrentlyOccupied && !theGrid[6, 7].CurrentlyOccupied
                        && theGrid[7, 7].FigureName == "rook" && theGrid[7, 7].FigureColor == "w" 
                        && !theGrid[5, 7].Attacked && !theGrid[6, 7].Attacked)
                    {
                        theGrid[6, 7].LegalNextMove = true;
                    }
                    //0-0-0
                    if (!theGrid[1, 7].CurrentlyOccupied && !theGrid[2, 7].CurrentlyOccupied && !theGrid[3, 7].CurrentlyOccupied
                        && theGrid[0, 7].FigureName == "rook" && theGrid[0, 7].FigureColor == "w"
                        && !theGrid[2, 7].Attacked && !theGrid[3, 7].Attacked)
                    {
                        theGrid[2, 7].LegalNextMove = true;
                    }
                }
                //black perspective
                if (currentCell.RowNumber == 3 && currentCell.ColumnNumber == 0)
                {
                    //0-0
                    if (!theGrid[1, 0].CurrentlyOccupied && !theGrid[2, 0].CurrentlyOccupied
                        && theGrid[0, 0].FigureName == "rook" && theGrid[0, 0].FigureColor == "w"
                        && !theGrid[1, 0].Attacked && !theGrid[2, 0].Attacked)
                    {
                        theGrid[1, 0].LegalNextMove = true;
                    }
                    //0-0-0
                    if (!theGrid[4, 0].CurrentlyOccupied && !theGrid[5, 0].CurrentlyOccupied && !theGrid[6, 0].CurrentlyOccupied
                        && theGrid[7, 0].FigureName == "rook" && theGrid[7, 0].FigureColor == "w"
                        && !theGrid[4, 0].Attacked && !theGrid[5, 0].Attacked)
                    {
                        theGrid[5, 0].LegalNextMove = true;
                    }
                }
            }
            //castle black king
            if (pieceColor == "b" && !blackKingMoved && !check)
            {
                //white perspective
                if (currentCell.RowNumber == 4 && currentCell.ColumnNumber == 0)
                {
                    //0-0
                    if (!theGrid[5, 0].CurrentlyOccupied && !theGrid[6, 0].CurrentlyOccupied 
                        && theGrid[7, 0].FigureName == "rook" && theGrid[7, 0].FigureColor == "b"
                        && !theGrid[5, 0].Attacked && !theGrid[6, 0].Attacked)
                    {
                        theGrid[6, 0].LegalNextMove = true;
                    }
                    //0-0-0
                    if (!theGrid[1, 0].CurrentlyOccupied && !theGrid[2, 0].CurrentlyOccupied && !theGrid[3, 0].CurrentlyOccupied 
                        && theGrid[0, 0].FigureName == "rook" && theGrid[0, 0].FigureColor == "b"
                        && !theGrid[2, 0].Attacked && !theGrid[3, 0].Attacked)
                    {
                        theGrid[2, 0].LegalNextMove = true;
                    }
                }
                //black perspective
                if (currentCell.RowNumber == 3 && currentCell.ColumnNumber == 7)
                {
                    //0-0
                    if (!theGrid[1, 7].CurrentlyOccupied && !theGrid[2, 7].CurrentlyOccupied
                        && theGrid[0, 7].FigureName == "rook" && theGrid[0, 7].FigureColor == "b"
                        && !theGrid[1, 7].Attacked && !theGrid[2, 7].Attacked)
                    {
                        theGrid[1, 7].LegalNextMove = true;
                    }
                    //0-0-0
                    if (!theGrid[4, 7].CurrentlyOccupied && !theGrid[5, 7].CurrentlyOccupied && !theGrid[6, 7].CurrentlyOccupied
                        && theGrid[7, 7].FigureName == "rook" && theGrid[7, 7].FigureColor == "b"
                        && !theGrid[4, 7].Attacked && !theGrid[5, 7].Attacked)
                    {
                        theGrid[5, 7].LegalNextMove = true;
                    }
                }
            }

        }

        public void ScanAttackedSquares(Cell[,] theGrid, String pieceColor, Boolean isRotated)
        {
            checkDetected = false;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    theGrid[i, j].Attacked = false;
                }
            }
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    Cell cell = theGrid[x, y];
                    if (cell.FigureName != "" && cell.FigureColor != pieceColor)
                    {
                        MarkNextLegalMoves(theGrid, cell, cell.FigureName + cell.FigureColor.Substring(0, 1), isRotated, false);
                    }
                }
            }
            if (checkDetected)
            {
                check = true;
            }
            else
            {
                check = false;
            }
        }

        public void MoveModeCheck(Cell fromCell, Cell toCell, Boolean moveMode, String figureColor, Boolean isRotated)
        {
            if (moveMode)
            {
                if (toCell.FigureColor != figureColor && SimulateNextMove(fromCell.Name, toCell.Name, figureColor, isRotated))
                {
                    toCell.LegalNextMove = true;
                }
            }
            else
            {
                toCell.Attacked = true;
            }
        }
        public void CheckForCheck(Cell cell, String figureColor)
        {
            if (cell.FigureName == "king" && cell.FigureColor != figureColor)
            {
                checkDetected = true;
            }
        }
        public Boolean SimulateNextMove(String fromSquareName, String toSquareName, String pieceColor, Boolean isRotated)
        {
            Cell[,] simulatedGrid = new Cell[Size, Size];

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    Cell cell = new Cell(x,y);

                    cell.Name = theGrid[x, y].Name;
                    cell.FigureName = theGrid[x, y].FigureName;
                    cell.FigureColor = theGrid[x, y].FigureColor;
                    cell.CurrentlyOccupied = theGrid[x, y].CurrentlyOccupied;
                    cell.RowNumber = theGrid[x, y].RowNumber;
                    cell.ColumnNumber = theGrid[x, y].ColumnNumber;

                    simulatedGrid[x, y] = cell;
                }
            }
            int fromX = notationToNumbers(fromSquareName.Substring(0, 1));
            int fromY = notationToNumbers(fromSquareName.Substring(1, 1));
            int toX = notationToNumbers(toSquareName.Substring(0, 1));
            int toY = notationToNumbers(toSquareName.Substring(1, 1));
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

            Boolean saveCheckStatus = check;
            ScanAttackedSquares(simulatedGrid, pieceColor, isRotated);
            check = saveCheckStatus;
            if (!checkDetected)
            {
                return true;
            }
            return false;
        }
        private int notationToNumbers(String move)
        {
            int x = 0;
            switch (move)
            {
                case "8":
                case "a": x = 0; break;
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
    }
}