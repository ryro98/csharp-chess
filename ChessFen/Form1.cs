using ChessBoardModel;
using System;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms.VisualStyles;

namespace ChessFen
{
    public partial class Form1 : Form
    {
        static Board board = new Board(8);

        public PictureBox[,] btnGrid = new PictureBox[board.Size, board.Size];

        public Form1()
        {
            InitializeComponent();
            populateGrid();
        }

        private void populateGrid()
        {
            int buttonSize = panel1.Width / board.Size;
            panel1.Height = panel1.Width;

            for (int i = 0; i < board.Size; i++)
            {
                for (int j = 0; j < board.Size; j++)
                {
                    btnGrid[i, j] = new PictureBox();

                    btnGrid[i, j].Height = buttonSize;
                    btnGrid[i, j].Width = buttonSize;

                    if ((i + j) % 2 == 0)
                    {
                        btnGrid[i, j].BackColor = Color.FromArgb(255, 240, 217, 181);
                    } else
                    {
                        btnGrid[i, j].BackColor = Color.FromArgb(255, 181, 136, 99);
                    }

                    btnGrid[i, j].Click += Grid_Button_Click;


                    btnGrid[i, j].Location = new Point(i * buttonSize, j * buttonSize);

                    btnGrid[i, j].Text = i + "|" + j;
                    btnGrid[i, j].Tag = new Point(i, j);
                    btnGrid[i,j].SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;

                    panel1.Controls.Add(btnGrid[i, j]);
                }
            }
        }

        private void clearBoard()
        {
            for (int i = 0; i < board.Size; i++)
            {
                for (int j = 0; j < board.Size; j++)
                {
                    btnGrid[i,j].Image = null;
                }
            }
        }

        private void Grid_Button_Click(object? sender, EventArgs e)
        {
            if (cbPiece.Text != "")
            {
                PictureBox clickedButton = (PictureBox)sender;
                Point location = (Point)clickedButton.Tag;

                int x = location.X;
                int y = location.Y;

                Cell currentCell = board.theGrid[x, y];
                board.MarkNextLegalMoves(currentCell, cbPiece.Text);

                for (int i = 0; i < board.Size; i++)
                {
                    for (int j = 0; j < board.Size; j++)
                    {
                        //btnGrid[i, j].Text = "";
                        btnGrid[i, j].Image = null;
                        if (board.theGrid[i, j].LegalNextMove)
                        {
                            //btnGrid[i, j].Text = "Legal";
                            String path = "../../../jpg/point.png";
                            btnGrid[i, j].Image = Image.FromFile(path);
                        }
                        else if (board.theGrid[i, j].CurrentlyOccupied)
                        {
                            //btnGrid[i, j].Text = cbPiece.Text;
                            String path = String.Format("../../../jpg/w{0}.png", cbPiece.Text);
                            btnGrid[i, j].Image = Image.FromFile(path);
                        }
                    }
                }
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            int squares = 0;
            int whiteKing = 0, whiteQueen = 0, whiteRook = 0, whiteBishop = 0, whiteKnight = 0, whitePawn = 0,
                blackKing = 0, blackQueen = 0, blackRook = 0, blackBishop = 0, blackKnight = 0, blackPawn = 0;
            bool isCorrect = true;

            String fen = tbFEN.Text;
            char[] characters = fen.ToCharArray();
            int row = 0;
            //foreach (String x in characters)
            for (int x = 0; x < characters.Length; x++)
            {
                if (isCorrect)
                {
                    //String sign = ;
                    switch (characters[x].ToString())
                    {
                        case "p":
                            squares += 1;
                            row += 1;
                            blackPawn += 1;
                            break;
                        case "P":
                            squares += 1;
                            row += 1;
                            whitePawn += 1;
                            break;
                        case "b":
                            squares += 1;
                            row += 1;
                            blackBishop += 1;
                            break;
                        case "B":
                            squares += 1;
                            row += 1;
                            whiteBishop += 1;
                            break;
                        case "n":
                            squares += 1;
                            row += 1;
                            blackKnight += 1;
                            break;
                        case "N":
                            squares += 1;
                            row += 1;
                            whiteKnight += 1;
                            break;
                        case "r":
                            squares += 1;
                            row += 1;
                            blackRook += 1;
                            break;
                        case "R":
                            squares += 1;
                            row += 1;
                            whiteRook += 1;
                            break;
                        case "q":
                            squares += 1;
                            row += 1;
                            blackQueen += 1;
                            break;
                        case "Q":
                            squares += 1;
                            row += 1;
                            whiteQueen += 1;
                            break;
                        case "k":
                            squares += 1;
                            row += 1;
                            blackKing += 1;
                            break;
                        case "K":
                            squares += 1;
                            row += 1;
                            whiteKing += 1;
                            break;
                        case "1":
                            squares += 1;
                            row += 1;
                            break;
                        case "2":
                            squares += 2;
                            row += 2;
                            break;
                        case "3":
                            squares += 3;
                            row += 3;
                            break;
                        case "4":
                            squares += 4;
                            row += 4;
                            break;
                        case "5":
                            squares += 5;
                            row += 5;
                            break;
                        case "6":
                            squares += 6;
                            row += 6;
                            break;
                        case "7":
                            squares += 7;
                            row += 7;
                            break;
                        case "8":
                            squares += 8;
                            row += 8;
                            break;
                        case "/":
                            if (row != 8)
                            {
                                isCorrect = false;
                            }
                            else
                            {
                                row = 0;
                            }
                            break;
                        default:
                            labelConsole.Text = String.Format("Unknown char: %s", x);
                            isCorrect = false;
                            break;
                    }
                }
            }
            if (isCorrect && squares == 64)
            {
                //black pawns > 8
                if (blackPawn > 8)
                {
                    labelConsole.Text = "Incorrect code: Too much black pawns on the board.";
                    return;
                }
                //white pawns > 8
                if (whitePawn > 8)
                {
                    labelConsole.Text = "Incorrect code: Too much white pawns on the board.";
                    return;
                }
                //black bishops + pawns > 10
                if (blackBishop + blackPawn > 10)
                {
                    labelConsole.Text = "Incorrect code: Too much black bishops on the board.";
                    return;
                }
                //white bishops + pawns > 10
                if (whiteBishop + whitePawn > 10)
                {
                    labelConsole.Text = "Incorrect code: Too much white bishops on the board.";
                    return;
                }
                //black knights + pawns > 10
                if (blackKnight + blackPawn > 10)
                {
                    labelConsole.Text = "Incorrect code: Too much black knights on the board.";
                    return;
                }
                //white knights + pawns > 10
                if (whiteKnight + whitePawn > 10)
                {
                    labelConsole.Text = "Incorrect code: Too much white knights on the board.";
                    return;
                }
                //black rooks + pawns > 10
                if (blackRook + blackPawn > 10)
                {
                    labelConsole.Text = "Incorrect code: Too much black rooks on the board.";
                    return;
                }
                //white rooks + pawns > 10
                if (whiteRook + whitePawn > 10)
                {
                    labelConsole.Text = "Incorrect code: Too much white rooks on the board.";
                    return;
                }
                //black queens + pawns > 9
                if (blackQueen + blackPawn > 9)
                {
                    labelConsole.Text = "Incorrect code: Too much black queens on the board.";
                    return;
                }
                //white queens + pawns > 9
                if (whiteQueen + whitePawn > 9)
                {
                    labelConsole.Text = "Incorrect code: Too much white queens on the board.";
                    return;
                }
                //black king == 0
                if (blackKing == 0)
                {
                    labelConsole.Text = "Incorrect code: Black king does not exist.";
                    return;
                }
                //black king > 1
                if (blackKing > 1)
                {
                    labelConsole.Text = String.Format("Incorrect code: Black king has {0} clones.", blackKing - 1);
                    return;
                }
                //white king == 0
                if (whiteKing == 0)
                {
                    labelConsole.Text = "Incorrect code: White king does not exist.";
                    return;
                }
                //white king > 1
                if (whiteKing > 1)
                {
                    labelConsole.Text = String.Format("Incorrect code: White king has {0} clones.", whiteKing - 1);
                    return;
                }
                labelConsole.Text = "FEN code is correct :)";
                clearBoard();
                int i = 0;
                int j = 0;

                for (int x = 0; x < characters.Length; x++)
                {
                    switch (characters[x].ToString())
                    {
                        case "p":
                            btnGrid[j, i].Image = Image.FromFile("../../../jpg/bPawn.png");
                            j += 1;
                            break;
                        case "P":
                            btnGrid[j, i].Image = Image.FromFile("../../../jpg/wPawn.png");
                            j += 1;
                            break;
                        case "b":
                            btnGrid[j, i].Image = Image.FromFile("../../../jpg/bBishop.png");
                            j += 1;
                            break;
                        case "B":
                            btnGrid[j, i].Image = Image.FromFile("../../../jpg/wBishop.png");
                            j += 1;
                            break;
                        case "n":
                            btnGrid[j, i].Image = Image.FromFile("../../../jpg/bKnight.png");
                            j += 1;
                            break;
                        case "N":
                            btnGrid[j, i].Image = Image.FromFile("../../../jpg/wKnight.png");
                            j += 1;
                            break;
                        case "r":
                            btnGrid[j, i].Image = Image.FromFile("../../../jpg/bRook.png");
                            j += 1;
                            break;
                        case "R":
                            btnGrid[j, i].Image = Image.FromFile("../../../jpg/wRook.png");
                            j += 1;
                            break;
                        case "q":
                            btnGrid[j, i].Image = Image.FromFile("../../../jpg/bQueen.png");
                            j += 1;
                            break;
                        case "Q":
                            btnGrid[j, i].Image = Image.FromFile("../../../jpg/wQueen.png");
                            j += 1;
                            break;
                        case "k":
                            btnGrid[j, i].Image = Image.FromFile("../../../jpg/bKing.png");
                            j += 1;
                            break;
                        case "K":
                            btnGrid[j, i].Image = Image.FromFile("../../../jpg/wKing.png");
                            j += 1;
                            break;
                        case "1":
                            j += 1;
                            break;
                        case "2":
                            j += 2;
                            break;
                        case "3":
                            j += 3;
                            break;
                        case "4":
                            j += 4;
                            break;
                        case "5":
                            j += 5;
                            break;
                        case "6":
                            j += 6;
                            break;
                        case "7":
                            j += 7;
                            break;
                        case "8":
                            j += 8;
                            break;
                        case "/":
                            i += 1;
                            j = 0;
                            break;
                        default:
                            labelConsole.Text = "Something went wrong.";
                            break;
                    }
                }

                //System.out.println(String.format("Black pawns: %d", blackPawn));
                //System.out.println(String.format("Black bishops: %d", blackBishop));
                //System.out.println(String.format("Black knights: %d", blackKnight));
                //System.out.println(String.format("Black rooks: %d", blackRook));
                //System.out.println(String.format("Black queens: %d", blackQueen));
                //System.out.println(String.format("Black kings: %d", blackKing));
                //System.out.println(String.format("White pawns: %d", whitePawn));
                //System.out.println(String.format("White bishops: %d", whiteBishop));
                //System.out.println(String.format("White knights: %d", whiteKnight));
                //System.out.println(String.format("White rooks: %d", whiteRook));
                //System.out.println(String.format("White queens: %d", whiteQueen));
                //System.out.println(String.format("White kings: %d", whiteKing));
            }
            else
            {
                labelConsole.Text = "Incorrect code. Try again.";
            }
        }
    }
}