using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ChessBoardModel
{
    public class Board
    {
        public int Size { get; set; }
        public Cell[,] theGrid { get; set; }
        public Board (int s)
        {
            Size = s;

            theGrid = new Cell[Size, Size];
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    theGrid[i, j] = new Cell(i, j);
                }
            }
        }

        public void MarkNextLegalMoves(Cell currentCell, string chessPiece)
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    theGrid[i, j].LegalNextMove = false;
                    theGrid[i, j].CurrentlyOccupied = false;
                }
            }

            switch(chessPiece)
            {
                case "Pawn":
                    theGrid[currentCell.RowNumber, currentCell.ColumnNumber - 1].LegalNextMove = true;
                    if (currentCell.ColumnNumber == 7)
                    {
                        theGrid[currentCell.RowNumber, currentCell.ColumnNumber - 2].LegalNextMove = true;
                    }
                    break;
                case "Bishop":
                    //up left
                    int y = currentCell.RowNumber - 1;
                    if (currentCell.RowNumber > 0 && currentCell.ColumnNumber > 0)
                    {
                        for (int x = currentCell.ColumnNumber - 1; x >= 0; x--)
                        {
                            theGrid[y, x].LegalNextMove = true;
                            if (y > 0)
                            {
                                y -= 1;
                            } else
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
                            theGrid[y, x].LegalNextMove = true;
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
                            theGrid[y, x].LegalNextMove = true;
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
                            theGrid[y, x].LegalNextMove = true;
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
                    break;
                case "Knight":
                    if (currentCell.RowNumber < 7 && currentCell.ColumnNumber > 1) 
                    {
                        theGrid[currentCell.RowNumber + 1, currentCell.ColumnNumber - 2].LegalNextMove = true;
                    }
                    if (currentCell.RowNumber < 6 && currentCell.ColumnNumber > 0)
                    {
                        theGrid[currentCell.RowNumber + 2, currentCell.ColumnNumber - 1].LegalNextMove = true;
                    }
                    if (currentCell.RowNumber < 6 && currentCell.ColumnNumber < 7)
                    {
                        theGrid[currentCell.RowNumber + 2, currentCell.ColumnNumber + 1].LegalNextMove = true;
                    }
                    if (currentCell.RowNumber < 7 && currentCell.ColumnNumber < 6)
                    {
                        theGrid[currentCell.RowNumber + 1, currentCell.ColumnNumber + 2].LegalNextMove = true;
                    }
                    if (currentCell.RowNumber > 0 && currentCell.ColumnNumber < 6)
                    {
                        theGrid[currentCell.RowNumber - 1, currentCell.ColumnNumber + 2].LegalNextMove = true;
                    }
                    if (currentCell.RowNumber > 1 && currentCell.ColumnNumber < 7)
                    {
                        theGrid[currentCell.RowNumber - 2, currentCell.ColumnNumber + 1].LegalNextMove = true;
                    }
                    if (currentCell.RowNumber > 1 && currentCell.ColumnNumber > 0)
                    {
                        theGrid[currentCell.RowNumber - 2, currentCell.ColumnNumber - 1].LegalNextMove = true;
                    }
                    if (currentCell.RowNumber > 0 && currentCell.ColumnNumber > 1)
                    {
                        theGrid[currentCell.RowNumber - 1, currentCell.ColumnNumber - 2].LegalNextMove = true;
                    }
                    break;
                case "Rook":
                    //up
                    if (currentCell.RowNumber > 0)
                    {
                        for (y = currentCell.RowNumber - 1; y >= 0; y--)
                        {
                            theGrid[y, currentCell.ColumnNumber].LegalNextMove = true;
                        }
                    }
                    //down
                    if (currentCell.RowNumber < 7)
                    {
                        for (y = currentCell.RowNumber + 1; y <= 7; y++)
                        {
                            theGrid[y, currentCell.ColumnNumber].LegalNextMove = true;
                        }
                    }
                    //left
                    if (currentCell.ColumnNumber > 0)
                    {
                        for (int x = currentCell.ColumnNumber - 1; x >= 0; x--)
                        {
                            theGrid[currentCell.RowNumber, x].LegalNextMove = true;
                        }
                    }
                    //right
                    if (currentCell.ColumnNumber < 7)
                    {
                        for (int x = currentCell.ColumnNumber + 1; x <= 7; x++)
                        {
                            theGrid[currentCell.RowNumber, x].LegalNextMove = true;
                        }
                    }
                    break;
                case "Queen":
                    //up
                    if (currentCell.RowNumber > 0)
                    {
                        for (y = currentCell.RowNumber - 1; y >= 0; y--)
                        {
                            theGrid[y, currentCell.ColumnNumber].LegalNextMove = true;
                        }
                    }
                    //down
                    if (currentCell.RowNumber < 7)
                    {
                        for (y = currentCell.RowNumber + 1; y <= 7; y++)
                        {
                            theGrid[y, currentCell.ColumnNumber].LegalNextMove = true;
                        }
                    }
                    //left
                    if (currentCell.ColumnNumber > 0)
                    {
                        for (int x = currentCell.ColumnNumber - 1; x >= 0; x--)
                        {
                            theGrid[currentCell.RowNumber, x].LegalNextMove = true;
                        }
                    }
                    //right
                    if (currentCell.ColumnNumber < 7)
                    {
                        for (int x = currentCell.ColumnNumber + 1; x <= 7; x++)
                        {
                            theGrid[currentCell.RowNumber, x].LegalNextMove = true;
                        }
                    }
                    //up left
                    y = currentCell.RowNumber - 1;
                    if (currentCell.RowNumber > 0 && currentCell.ColumnNumber > 0)
                    {
                        for (int x = currentCell.ColumnNumber - 1; x >= 0; x--)
                        {
                            theGrid[y, x].LegalNextMove = true;
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
                            theGrid[y, x].LegalNextMove = true;
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
                            theGrid[y, x].LegalNextMove = true;
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
                            theGrid[y, x].LegalNextMove = true;
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
                    break;
                case "King":
                    //up
                    if (currentCell.RowNumber > 0)
                    {
                        theGrid[currentCell.RowNumber - 1, currentCell.ColumnNumber].LegalNextMove = true;
                    }
                    //up right
                    if (currentCell.RowNumber > 0 && currentCell.ColumnNumber < 7)
                    {
                        theGrid[currentCell.RowNumber - 1, currentCell.ColumnNumber + 1].LegalNextMove = true;
                    }
                    //right
                    if (currentCell.ColumnNumber < 7)
                    {
                        theGrid[currentCell.RowNumber, currentCell.ColumnNumber + 1].LegalNextMove = true;
                    }
                    //down right
                    if (currentCell.RowNumber < 7 && currentCell.ColumnNumber < 7)
                    {
                        theGrid[currentCell.RowNumber + 1, currentCell.ColumnNumber + 1].LegalNextMove = true;
                    }
                    //down
                    if (currentCell.RowNumber < 7)
                    {
                        theGrid[currentCell.RowNumber + 1, currentCell.ColumnNumber].LegalNextMove = true;
                    }
                    //down left
                    if (currentCell.RowNumber < 7 && currentCell.ColumnNumber > 0)
                    {
                        theGrid[currentCell.RowNumber + 1, currentCell.ColumnNumber - 1].LegalNextMove = true;
                    }
                    //left
                    if (currentCell.ColumnNumber > 0)
                    {
                        theGrid[currentCell.RowNumber, currentCell.ColumnNumber - 1].LegalNextMove = true;
                    }
                    //up left
                    if (currentCell.RowNumber > 0 && currentCell.ColumnNumber > 0)
                    {
                        theGrid[currentCell.RowNumber - 1, currentCell.ColumnNumber - 1].LegalNextMove = true;
                    }
                    break;
            }
            theGrid[currentCell.RowNumber, currentCell.ColumnNumber].CurrentlyOccupied = true;
        }
    }
}
