using ChessBoardModel;
using System.Xml.Linq;

namespace ChessFen
{
    public partial class Form1 : Form
    {
        static Board board = new Board(8);

        public PictureBox[,] pbGrid = new PictureBox[board.Size, board.Size];

        static String[][] puzzles = new string[][] {
            new String[]{"6k1/5ppp/8/r7/8/8/5PPP/4R1K1", "white", "Re8"},
            new String[]{"6k1/5ppp/8/8/2r5/8/5PPP/3R2K1", "white", "Rd8"},
            new String[]{"1k6/1p6/p1p3r1/8/8/3R4/PPP5/1K6", "black", "Rg1 Rd3-d1 Rd1"},
            new String[]{"5rk1/p4pp1/1p4qp/2p5/2P5/1P3RQ1/P4PPP/7K", "black", "Qb1"},
            new String[]{"r1bk1b1r/4qp2/2p1p2p/p2pN1p1/3P4/P3P1B1/1PP2PPP/R2QK2R", "white", "Nc6 Kd8-e8 Ne7"},
            new String[]{"3r2k1/5ppp/3r4/p1p5/Pp6/1P1PR1P1/5K1P/4R3", "white", "Re8 Rd8-e8 Re8"},
            new String[]{"6k1/6p1/1R5p/1P1bP1p1/r7/4R1P1/5P1P/6K1", "black", "Ra1 Re3-e1 Re1"},
            new String[]{"3r2k1/2Q2ppp/8/3pP3/3P2P1/4R1bP/5q2/7K", "white", "Qd8"},
            new String[]{"r3kr2/ppp2p2/2b4Q/3qN3/8/8/PPPP1PPP/R1B1R1K1", "black", "Qg2"},
            new String[]{"1n4k1/2p3pp/3q1r2/2rP1p2/pQ2p3/4R1P1/PPPN1P1P/2KR4", "black", "Rc2 Kc1-c2 Qb4"},
            new String[]{"2nq1bk1/p4pp1/4p3/P1p5/2P1N3/5P2/2Q3PP/R5K1", "black", "Qd4 Kg1-f1 Qa1"},
            new String[]{"b1rq3b/5k1p/p3rnpB/1pNn4/3P4/P6Q/1P3PPP/1B1R2K1", "white", "Qe6"},
            new String[]{"2b5/1Rn2k2/3pnp2/2bPp1p1/2P1P1Pp/r6P/PK1N3R/5B2", "white", "e6 Kf7-e6 Rc7"},
            new String[]{"6r1/pp6/3kbQ2/3p1p2/5P2/4P3/3q1P1P/1RR2K2", "black", "Qd3 Kf1-e1 Rg1"},
            new String[]{"1k1r2nr/ppp2ppp/8/4q3/1b1N4/3PB3/1PP1QPPP/R5K1", "white", "Nc6 Pb7-c6 Ba7 Kb8-c8 Qe5"},
            new String[]{"5r1k/pppbqBp1/2n1pp1p/7Q/3P4/2P1P1R1/PP4PP/R5K1", "white", "Rg7 Kh8-g7 Qg6 Kg7-h8 Qh6"},
            new String[]{"2r1k2r/3pnppp/4p3/p1P5/Pp2n3/1B1QP2q/1PP4P/2KRR3", "white", "Qd7 Ke8-f8 Qd8 Rc8-d8 Rd8"},
            new String[]{"r2q1rk1/p4ppp/1p1p4/2pPb3/2B5/2P5/PP3PPP/1R1Q1RK1", "black", "Qh4 Pf2-f4 Bf4 Rf1-f4 Qf4"},
        };

        private Color lightSquareLegalColor = Color.FromArgb(255, 215, 150, 53);
        private Color lightSquareColor = Color.FromArgb(255, 240, 217, 181);
        private Color darkSquareColor = Color.FromArgb(255, 181, 136, 99);
        private Color darkSquareLegalColor = Color.FromArgb(255, 120, 85, 56);
        private Color lastMoveColor = Color.FromArgb(255, 218, 195, 50);
        private Color userModeButtonColor = Color.Red;
        private Color devModeButtonColor = Color.FromArgb(255, 0, 153, 0);
        private Color radioButtonOffColor = Color.FromArgb(22, 21, 18);

        private PictureBox? sourceSquare = null;
        private PictureBox? targetSquare = null;

        private Boolean takes = false;
        private Boolean isRotated = false;
        private Boolean puzzleMode = false;
        private Boolean devMode = false;

        private String sourceSquareName = "";
        private String targetSquareName = "";
        public String side = "white";
        private String turn = "white";
        private String startingPosition = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR";
        private String moves = "";
        private String puzzleFEN = "";
        private String puzzleColor = "";
        private String[] puzzleSolution = {};

        private int moveCounter = 0;
        private int movesLimit = 0;

        public Form1()
        {
            InitializeComponent();
            generateGrid();
            start();
        }

        private void generateGrid()
        {
            int squareSize = panel1.Width / board.Size;
            panel1.Height = panel1.Width;

            for (int i = 0; i < board.Size; i++)
            {
                for (int j = 0; j < board.Size; j++)
                {
                    pbGrid[i, j] = new PictureBox();

                    pbGrid[i, j].Height = squareSize;
                    pbGrid[i, j].Width = squareSize;
                    pbGrid[i, j].BorderStyle = BorderStyle.FixedSingle;

                    if ((i + j) % 2 == 0)
                    {
                        pbGrid[i, j].BackColor = lightSquareColor;
                    } else
                    {
                        pbGrid[i, j].BackColor = darkSquareColor;
                    }

                    pbGrid[i, j].Click += Grid_Button_Click;
                    pbGrid[i, j].AllowDrop = true;
                    pbGrid[i, j].MouseMove += pictureBox_MouseMove;
                    pbGrid[i, j].MouseDown += pictureBox_MouseDown;
                    pbGrid[i, j].DragEnter += pictureBox_DragEnter;
                    pbGrid[i, j].DragDrop += pictureBox_DragDrop;

                    pbGrid[i, j].Location = new Point(i * squareSize, j * squareSize);

                    pbGrid[i, j].Text = i + "|" + j;
                    pbGrid[i, j].Tag = new Point(i, j);
                    pbGrid[i, j].SizeMode = PictureBoxSizeMode.CenterImage;

                    panel1.Controls.Add(pbGrid[i, j]);
                }
            }
        }

        //Source PictureBox
        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            PictureBox clickedSquare = (PictureBox)sender;
            Point location = (Point)clickedSquare.Tag;
            if (clickedSquare.Image != null)
            {
                if (e.Button == MouseButtons.Left)
                {
                    clickedSquare.DoDragDrop(clickedSquare.Image, DragDropEffects.Move);
                }
                if (e.Button == MouseButtons.Right)
                {
                    Grid_Button_Click(sender, e);
                }
            }
        }

        //Target PictureBox
        //Drag Drop Effects
        private void pictureBox_DragEnter(object sender, DragEventArgs e)
        {
            PictureBox clickedSquare = (PictureBox)sender;
            Point location = (Point)clickedSquare.Tag;
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }

        }

        //Set the image to be the dragged image.
        //move function
        private void pictureBox_DragDrop(object sender, DragEventArgs e)
        {
            PictureBox clickedSquare = (PictureBox)sender;
            Point location = (Point)clickedSquare.Tag;
            targetSquareName = board.theGrid[location.X, location.Y].Name;
            targetSquareLabel.Text = targetSquareName;
            String piece = sourceSquare.Image.Tag.ToString();
            String pieceColor = getColorPiece(piece);
            String turnLetter = getFirstLetterTurn();
            Boolean legalMove = board.SimulateNextMove(sourceSquareName, targetSquareName, pieceColor, isRotated);
            if (puzzleMode)
            {
                if (puzzleSolution.Length > 0)
                {
                    String proposedMove = "";
                    if (piece.Substring(0, piece.Length - 1) == "knight")
                    {

                        piece = piece.Remove(0, 1);
                    }
                    if (piece.Substring(0, piece.Length - 1) != "pawn")
                    {
                        proposedMove = piece.Substring(0, 1).ToUpper();
                    }
                    proposedMove += targetSquareName;
                    puzzleEngine(proposedMove, clickedSquare, location, piece);
                }
            }
            else if (e.Data.GetDataPresent(DataFormats.Bitmap) && board.theGrid[location.X, location.Y].LegalNextMove 
                && turnLetter == pieceColor && !puzzleMode)
            {
                if (legalMove)
                {
                    playerMove(clickedSquare, location, piece);
                }
            }
            colorBoard();
        }

        private Boolean checkMove(String move)
        {
            if (move == puzzleSolution.First())
            {
                return true;
            }
            return false;
        }

        private void puzzleEngine(String proposedMove, PictureBox clickedSquare, Point location, String piece)
        {
            if (checkMove(proposedMove))
            {
                playerMove(clickedSquare, location, piece);
                removeMoveFromPuzzle();
                if (puzzleSolution.Length > 0)
                {
                    puzzleLabel.Text = "Good move!";
                    String fromWhereX = puzzleSolution[0].Substring(1, 1);
                    String fromWhereY = puzzleSolution[0].Substring(2, 1);
                    String toWhereX = puzzleSolution[0].Substring(4, 1);
                    String toWhereY = puzzleSolution[0].Substring(5, 1);
                    PictureBox fromSquare = pbGrid[notationToNumbers(fromWhereX), notationToNumbers(fromWhereY)];
                    PictureBox toSquare = pbGrid[notationToNumbers(toWhereX), notationToNumbers(toWhereY)];
                    move(toSquare, fromSquare);
                    removeMoveFromPuzzle();
                }
                else
                {
                    puzzleLabel.Text = "You finished the puzzle!";
                }
            }
            else
            {
                puzzleLabel.Text = "Incorrect move. Try again.";
            }
        }

        private void removeMoveFromPuzzle()
        {
            var moves = puzzleSolution.ToList();
            moves.RemoveAt(0);
            puzzleSolution = moves.ToArray();
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
                default:         break;
            }
            return x;
        }

        private void playerMove(PictureBox clickedSquare, Point location, String piece)
        {
            String pieceName = getPieceName(piece);
            String pieceColor = getColorPiece(piece);
            if (clickedSquare.Image != null)
            {
                takes = true;
            }
            targetSquare = clickedSquare;
            clickedSquare.Image = (Bitmap)sourceSquare.Image;
            board.theGrid[location.X, location.Y].CurrentlyOccupied = true;
            board.theGrid[location.X, location.Y].FigureName = pieceName;
            board.theGrid[location.X, location.Y].FigureColor = getColorPiece(clickedSquare.Image.Tag.ToString());
            deleteFigure(sourceSquare);
            Point sourceLocation = (Point)sourceSquare.Tag;
            if (pieceName == "pawn") //checking if its a pawn move
            {
                if (location.Y - 2 == sourceLocation.Y || location.Y + 2 == sourceLocation.Y) //checking if its a 2-square-pawn move
                {
                    if (pieceColor == "w")
                    {
                        board.blackEnpassant = true;
                    }
                    if (pieceColor == "b")
                    {
                        board.whiteEnpassant = true;
                    }
                }
                else if (location.X - 1 == sourceLocation.X && location.Y - 1 == sourceLocation.Y
                    || location.X + 1 == sourceLocation.X && location.Y - 1 == sourceLocation.Y
                    || location.X - 1 == sourceLocation.X && location.Y + 1 == sourceLocation.Y
                    || location.X + 1 == sourceLocation.X && location.Y + 1 == sourceLocation.Y) //checking if its an enpassant
                {
                    if (!isRotated)
                    {
                        if (pieceColor == "w" && board.whiteEnpassant)
                        {
                            deleteFigure(pbGrid[location.X, location.Y + 1]);
                        }
                        if (pieceColor == "b" && board.blackEnpassant)
                        {
                            deleteFigure(pbGrid[location.X, location.Y - 1]);
                        }
                    }
                    else
                    {
                        if (pieceColor == "w" && board.whiteEnpassant)
                        {
                            deleteFigure(pbGrid[7 - location.X, 7 - (location.Y - 1)]);
                        }
                        if (pieceColor == "b" && board.blackEnpassant)
                        {
                            deleteFigure(pbGrid[7 - location.X, 7 - (location.Y + 1)]);
                        }
                    }
                    takes = true;
                }
            }
            if (pieceName == "king") //checking if its a castle
            {
                if (pieceColor == "w" && !board.whiteKingMoved) //white king
                {
                    if (!isRotated) //white perspective
                    {
                        if (location.X == 6) //0-0
                        {
                            castle(pbGrid[5, 7], pbGrid[7, 7], board.theGrid[5, 7], "0-0");
                        }
                        else if (location.X == 2) //0-0-0
                        {
                            castle(pbGrid[3, 7], pbGrid[0, 7], board.theGrid[3, 7], "0-0-0");
                        }
                        else
                        {
                            addToNotation(pieceName, targetSquareName, "");
                        }
                    }
                    if (isRotated) //black perspective
                    {
                        if (location.X == 1) //0-0
                        {
                            castle(pbGrid[5, 7], pbGrid[7, 7], board.theGrid[2, 0], "0-0");
                        }
                        else if (location.X == 5) //0-0-0
                        {
                            castle(pbGrid[3, 7], pbGrid[0, 7], board.theGrid[4, 0], "0-0-0");
                        }
                        else
                        {
                            addToNotation(pieceName, targetSquareName, "");
                        }
                    }
                }
                else if (pieceColor == "b" && !board.blackKingMoved) //black king
                {
                    if (!isRotated) //white perspective
                    {
                        if (location.X == 6) //0-0
                        {
                            castle(pbGrid[5, 0], pbGrid[7, 0], board.theGrid[5, 0], "0-0");
                        }
                        else if (location.X == 2) //0-0-0
                        {
                            castle(pbGrid[3, 0], pbGrid[0, 0], board.theGrid[3, 0], "0-0-0");
                        }
                        else
                        {
                            addToNotation(pieceName, targetSquareName, "");
                        }
                    }
                    if (isRotated) //black perspective
                    {
                        if (location.X == 1) //0-0
                        {
                            castle(pbGrid[5, 0], pbGrid[7, 0], board.theGrid[2, 7], "0-0");
                        }
                        else if (location.X == 5) //0-0-0
                        {
                            castle(pbGrid[3, 0], pbGrid[0, 0], board.theGrid[4, 7], "0-0-0");
                        }
                        else
                        {
                            addToNotation(pieceName, targetSquareName, "");
                        }
                    }
                }
                else
                {
                    addToNotation(pieceName, targetSquareName, "");
                }
                if (pieceColor == "w")
                {
                    board.whiteKingMoved = true;
                }
                else if (pieceColor == "b")
                {
                    board.blackKingMoved = true;
                }
            }
            else
            {
                addToNotation(pieceName, targetSquareName, "");
            }
            changeTurn();
        }

        private void castle(PictureBox rookSquare, PictureBox oldSquare, Cell rookSquareGrid, String castle)
        {
            rookSquare.Image = (Bitmap)oldSquare.Image;
            rookSquareGrid.CurrentlyOccupied = true;
            rookSquareGrid.FigureColor = getColorPiece(rookSquare.Image.Tag.ToString());
            deleteFigure(oldSquare);
            addToNotation("castle", "castle", castle);
        }

        private void move(PictureBox toSquare, PictureBox fromSquare)
        {
            toSquare.Image = (Bitmap)fromSquare.Image;
            Point location = (Point)toSquare.Tag;
            Cell toSquareGrid = board.theGrid[location.X, location.Y];
            toSquareGrid.CurrentlyOccupied = true;
            toSquareGrid.FigureColor = getColorPiece(toSquare.Image.Tag.ToString());
            deleteFigure(fromSquare);

            String piece = toSquare.Image.Tag.ToString();
            addToNotation(getPieceName(piece), board.theGrid[location.X, location.Y].Name, "");
            changeTurn();
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            colorBoard();
            PictureBox clickedSquare = (PictureBox)sender;
            Point location = (Point)clickedSquare.Tag;
            sourceSquare = clickedSquare;
            sourceSquareName = board.theGrid[location.X, location.Y].Name;
            sourceSquareLabel.Text = sourceSquareName;
            rowLabel.Text = location.X.ToString();
            columnLabel.Text = location.Y.ToString();
            isOccupiedLabel.Text = board.theGrid[location.X, location.Y].CurrentlyOccupied.ToString();
            attackedLabel.Text = board.theGrid[location.X, location.Y].Attacked.ToString();
            String turnLetter = getFirstLetterTurn();
            String pieceColor = "";
            if (sourceSquare.Image != null)
            {
                pieceColor = getColorPiece(sourceSquare.Image.Tag.ToString());
            }
            if (turnLetter == pieceColor)
            {
                Cell currentCell = board.theGrid[location.X, location.Y];
                String piece = clickedSquare.Image.Tag.ToString();
                board.MarkNextLegalMoves(board.theGrid, currentCell, piece, isRotated, true);
                colorLegalMoves();
            }

        }

        private void changeTurn()
        {
            if (turn == "white")
            {
                turn = "black";
            }
            else
            {
                turn = "white";
            }
            turnLabel.Text = turn;
            board.ScanAttackedSquares(board.theGrid, turn.Substring(0, 1), isRotated);
            if (takes)
            {
                takes = false;
            }
            if (board.whiteEnpassant && turn == "black")
            {
                board.whiteEnpassant = false;
            }
            whiteENLabel.Text = board.whiteEnpassant.ToString();
            if (board.blackEnpassant && turn == "white")
            {
                board.blackEnpassant = false;
            }
            blackENLabel.Text = board.blackEnpassant.ToString();

            checkLabel.Text = board.check.ToString();
        }

        private void clearBoard()
        {
            for (int i = 0; i < board.Size; i++)
            {
                for (int j = 0; j < board.Size; j++)
                {
                    deleteFigure(pbGrid[i, j]);
                }
            }
        }

        private void colorBoard()
        {
            for (int i = 0; i < board.Size; i++)
            {
                for (int j = 0; j < board.Size; j++)
                {
                    if ((i + j) % 2 == 0)
                    {
                        pbGrid[i, j].BackColor = lightSquareColor;
                    }
                    else
                    {
                        pbGrid[i, j].BackColor = darkSquareColor;
                    }
                }
            }
        }

        private void colorLegalMoves()
        {
            for (int i = 0; i < board.Size; i++)
            {
                for (int j = 0; j < board.Size; j++)
                {
                    PictureBox pb = pbGrid[i, j];
                    if (isRotated)
                    {
                        pb = pbGrid[7 - i, 7 - j];
                    }
                    if (board.theGrid[i, j].LegalNextMove)
                    {
                        if ((i + j) % 2 == 0)
                        {
                            pb.BackColor = lightSquareLegalColor;
                        }
                        else
                        {
                            pb.BackColor = darkSquareLegalColor;
                        }
                    }
                    if (board.theGrid[i, j].Attacked && devMode)
                    {
                        pb.BackColor = Color.Red;
                    }
                }
            }
        }

        private void Grid_Button_Click(object? sender, EventArgs e)
        {
            PictureBox clickedSquare = (PictureBox)sender;
            tagLabel.Text = clickedSquare.Tag.ToString();
            colorBoard();
            if (clickedSquare.Image != null)
            {
                Point location = (Point)clickedSquare.Tag;

                Cell currentCell = board.theGrid[location.X, location.Y];
                String piece = clickedSquare.Image.Tag.ToString();
                String pieceName = getPieceName(piece);
                String colorPiece = getColorPiece(piece);
                clickedFigureLabel.Text = pieceName;
                colorPieceLabel.Text = colorPiece;
                board.MarkNextLegalMoves(board.theGrid, currentCell, piece, isRotated, true);

                colorLegalMoves();
            }
            else
            {
                clickedFigureLabel.Text = "";
                colorPieceLabel.Text = "";
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            readFromFen(tbFEN.Text);
        }

        private void readFromFen(String fen)
        {
            int squares = 0;
            int whiteKing = 0, whiteQueen = 0, whiteRook = 0, whiteBishop = 0, whiteKnight = 0, whitePawn = 0,
                blackKing = 0, blackQueen = 0, blackRook = 0, blackBishop = 0, blackKnight = 0, blackPawn = 0;
            bool isCorrect = true;

            char[] characters = fen.ToCharArray();
            int row = 0;
            for (int x = 0; x < characters.Length; x++)
            {
                if (isCorrect)
                {
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
                            generateFenLabel.Text = String.Format("Unknown char: %s", x);
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
                    generateFenLabel.Text = "Incorrect code: Too much black pawns on the board.";
                    return;
                }
                //white pawns > 8
                if (whitePawn > 8)
                {
                    generateFenLabel.Text = "Incorrect code: Too much white pawns on the board.";
                    return;
                }
                //black bishops + pawns > 10
                if (blackBishop + blackPawn > 10)
                {
                    generateFenLabel.Text = "Incorrect code: Too much black bishops on the board.";
                    return;
                }
                //white bishops + pawns > 10
                if (whiteBishop + whitePawn > 10)
                {
                    generateFenLabel.Text = "Incorrect code: Too much white bishops on the board.";
                    return;
                }
                //black knights + pawns > 10
                if (blackKnight + blackPawn > 10)
                {
                    generateFenLabel.Text = "Incorrect code: Too much black knights on the board.";
                    return;
                }
                //white knights + pawns > 10
                if (whiteKnight + whitePawn > 10)
                {
                    generateFenLabel.Text = "Incorrect code: Too much white knights on the board.";
                    return;
                }
                //black rooks + pawns > 10
                if (blackRook + blackPawn > 10)
                {
                    generateFenLabel.Text = "Incorrect code: Too much black rooks on the board.";
                    return;
                }
                //white rooks + pawns > 10
                if (whiteRook + whitePawn > 10)
                {
                    generateFenLabel.Text = "Incorrect code: Too much white rooks on the board.";
                    return;
                }
                //black queens + pawns > 9
                if (blackQueen + blackPawn > 9)
                {
                    generateFenLabel.Text = "Incorrect code: Too much black queens on the board.";
                    return;
                }
                //white queens + pawns > 9
                if (whiteQueen + whitePawn > 9)
                {
                    generateFenLabel.Text = "Incorrect code: Too much white queens on the board.";
                    return;
                }
                //black king == 0
                if (blackKing == 0)
                {
                    generateFenLabel.Text = "Incorrect code: Black king does not exist.";
                    return;
                }
                //black king > 1
                if (blackKing > 1)
                {
                    generateFenLabel.Text = String.Format("Incorrect code: Black king has {0} clones.", blackKing - 1);
                    return;
                }
                //white king == 0
                if (whiteKing == 0)
                {
                    generateFenLabel.Text = "Incorrect code: White king does not exist.";
                    return;
                }
                //white king > 1
                if (whiteKing > 1)
                {
                    generateFenLabel.Text = String.Format("Incorrect code: White king has {0} clones.", whiteKing - 1);
                    return;
                }
                generateFenLabel.Text = "FEN code is correct :)";
                clearBoard();
                int i = 0;
                int j = 0;
                String path = "";
                String name = "";
                String color = "";
                for (int x = 0; x < characters.Length; x++)
                {
                    path = "";
                    name = "";
                    color = "";
                    switch (characters[x].ToString())
                    {
                        case "p":
                            path = "../../../jpg/bPawn.png";
                            name = "pawn";
                            color = "b";
                            break;
                        case "P":
                            path = "../../../jpg/wPawn.png";
                            name = "pawn";
                            color = "w";
                            break;
                        case "b":
                            path = "../../../jpg/bBishop.png";
                            name = "bishop";
                            color = "b";
                            break;
                        case "B":
                            path = "../../../jpg/wBishop.png";
                            name = "bishop";
                            color = "w";
                            break;
                        case "n":
                            path = "../../../jpg/bKnight.png";
                            name = "knight";
                            color = "b";
                            break;
                        case "N":
                            path = "../../../jpg/wKnight.png";
                            name = "knight";
                            color = "w";
                            break;
                        case "r":
                            path = "../../../jpg/bRook.png";
                            name = "rook";
                            color = "b";
                            break;
                        case "R":
                            path = "../../../jpg/wRook.png";
                            name = "rook";
                            color = "w";
                            break;
                        case "q":
                            path = "../../../jpg/bQueen.png";
                            name = "queen";
                            color = "b";
                            break;
                        case "Q":
                            path = "../../../jpg/wQueen.png";
                            name = "queen";
                            color = "w";
                            break;
                        case "k":
                            path = "../../../jpg/bKing.png";
                            name = "king";
                            color = "b";
                            break;
                        case "K":
                            path = "../../../jpg/wKing.png";
                            name = "king";
                            color = "w";
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
                            generateFenLabel.Text = "Something went wrong.";
                            break;
                    }
                    if (path != "" && name != "")
                    {
                        addFigure(pbGrid[j, i], path, name, color);
                        j += 1;
                    }
                }
            }
            else
            {
                generateFenLabel.Text = "Incorrect code. Try again.";
            }
        }

        private void addFigure(PictureBox pb, String path, String name, String color)
        {
            Image image = Image.FromFile(path);
            image.Tag = name + color;
            pb.Image = image;
            Point location = (Point)pb.Tag;
            board.theGrid[location.X, location.Y].CurrentlyOccupied = true;
            board.theGrid[location.X, location.Y].FigureName = name;
            board.theGrid[location.X, location.Y].FigureColor = color;
        }

        private void deleteFigure(PictureBox pb)
        {
            pb.Image = null;
            Point location = (Point)pb.Tag;
            board.theGrid[location.X, location.Y].CurrentlyOccupied = false;
            board.theGrid[location.X, location.Y].FigureName = "";
            board.theGrid[location.X, location.Y].FigureColor = "";
        }

        private String getPieceName(String piece)
        {
            return piece.Substring(0, piece.Length - 1);
        }

        private String getColorPiece(String piece)
        {
            return piece.Substring(piece.Length-1);
        }

        private String getFirstLetterTurn()
        {
            return turn.Substring(0, 1);
        }

        private void btnGenerateFen_Click(object sender, EventArgs e)
        {
            generateFenTB.Text = generateFEN();
        }

        private String generateFEN()
        {
            String fen = "";
            int emptySquares = 0;
            for (int x = 0; x < 8; x++)
            {
                emptySquares = 0;
                for (int y = 0; y < 8; y++)
                {
                    Cell cell = board.theGrid[y, x];
                    PictureBox pb = pbGrid[y, x];
                    if (pb.Image != null)
                    {
                        if (emptySquares > 0)
                        {
                            fen += emptySquares;
                            emptySquares = 0;
                        }
                        String piece = pb.Image.Tag.ToString();
                        String pieceName = getPieceName(piece);
                        String colorPiece = getColorPiece(piece);
                        String letter = "";
                        switch (pieceName)
                        {
                            case "king":
                                letter = "k";
                                break;
                            case "queen":
                                letter = "q";
                                break;
                            case "rook":
                                letter = "r";
                                break;
                            case "knight":
                                letter = "n";
                                break;
                            case "bishop":
                                letter = "b";
                                break;
                            case "pawn":
                                letter = "p";
                                break;
                            default:
                                break;
                        }
                        if (colorPiece == "w")
                        {
                            letter = letter.ToUpper();
                        }
                        fen += letter;
                    }
                    else
                    {
                        emptySquares++;
                        if (y == 7)
                        {
                            fen += emptySquares;
                        }
                    }
                }
                if (x < 7)
                {
                    fen += "/";
                }
            }
            return fen;
        }

        private void clearLabels()
        {
            generateFenLabel.Text = "";
            sourceSquareLabel.Text = "";
            targetSquareLabel.Text = "";
            clickedFigureLabel.Text = "";
            colorPieceLabel.Text = "";
            isOccupiedLabel.Text = "";
            checkLabel.Text = "";
            puzzleLabel.Text = "";
            turn = "white";
            turnLabel.Text = "";
            moves = "";
            moveCounter = 0;
            movesLimit = 0;
            movesLabel.Text = "";
        }

        private void addToNotation(String figure, String square, String castle)
        {
            String move = "";
            //if (movesLimit > 50)
            //{
            //    move += "\n";
            //    movesLimit = 0;
            //}
            if (turn == "white")
            {
                if (moveCounter != 0)
                {
                    move += "\n";
                }
                moveCounter++;
                move += moveCounter + ". ";
            }
            else if (puzzleMode && moveCounter == 0)
            {
                moveCounter++;
                move += moveCounter + ".. ";
            }
            if (castle != "")
            {
                move += castle + " ";
            }
            else
            {
                if (figure == "knight")
                {
                    move += figure.Substring(1, 1).ToUpper();
                }
                else if (figure != "pawn")
                {
                    move += figure.Substring(0, 1).ToUpper();
                }
                if (takes)
                {
                    if (figure == "pawn")
                    {
                        move += sourceSquareName.Substring(0, 1) + square.Substring(0, 1) + " ";
                    }
                    else
                    {
                        move += ":" + square;
                    }
                }
                else
                {
                    move += square;
                }
            }
            if (turn == "white")
            {
                board.ScanAttackedSquares(board.theGrid, "b", isRotated);
            }
            else
            {
                board.ScanAttackedSquares(board.theGrid, "w", isRotated);
            }
            if (board.check)
            {
                move += "+";
            }
            movesLimit += move.Length;
            moves += " " + move.PadRight(10);
            movesLabel.Text = moves;
        }

        private void btnRotateBoard_Click(object sender, EventArgs e)
        {
            rotateBoard();
        }

        private void rotateBoard()
        {
            isRotated = !isRotated;
            if (side == "white")
            {
                side = "black";
            }
            else
            {
                side = "white";
            }
            colorBoard();
            String fen = generateFEN();
            PictureBox[,] rotatedPbGrid = new PictureBox[board.Size, board.Size];
            Cell[,] rotatedBoard = new Cell[board.Size, board.Size];
            for (int x = 0; x < board.Size; x++)
            {
                for (int y = 0; y < board.Size; y++)
                {
                    rotatedPbGrid[x, y] = pbGrid[7 - x, 7 - y];
                    rotatedBoard[x, y] = board.theGrid[7 - x, 7 - y];
                    rotatedBoard[x, y].RowNumber = 7 - rotatedBoard[x, y].RowNumber;
                    rotatedBoard[x, y].ColumnNumber = 7 - rotatedBoard[x, y].ColumnNumber;
                }
            }
            pbGrid = rotatedPbGrid;
            board.theGrid = rotatedBoard;

            readFromFen(fen);
        }

        private void btnPuzzle_Click(object sender, EventArgs e)
        {
            puzzleMode = true; //set puzzle mode
            clearLabels(); //clear all the labels
            Random random = new Random();
            String[] puzzle = puzzles[random.Next(0, puzzles.Length)];
            puzzleFEN = puzzle[0];
            puzzleColor = puzzle[1];
            puzzleSolution = puzzle[2].Split(" ");
            readFromFen(puzzleFEN); //load position
            //rotate board if needed
            if ((puzzleColor == "black" && !isRotated) || (puzzleColor == "white" && isRotated))
            {
                rotateBoard();
            }
            //set turn
            if (puzzleColor == "white")
            {
                turn = "white";
            }
            else
            {
                turn = "black";
            }
            board.ScanAttackedSquares(board.theGrid, turn.Substring(0, 1), isRotated);
            turnLabel.Text = turn;
            puzzleLabel.Text = "Puzzle loaded";
        }

        private void btnStartingPosition_Click(object sender, EventArgs e)
        {
            start();
        }

        private void start()
        {
            puzzleMode = false;
            clearLabels();
            readFromFen(startingPosition);
            board.ScanAttackedSquares(board.theGrid, "w", isRotated);
            turnLabel.Text = turn;
            board.whiteKingMoved = false;
            board.blackKingMoved = false;
        }

        private void rbUserMode_CheckedChanged(object sender, EventArgs e)
        {
            changeMode();
        }

        private void changeMode()
        {
            devMode = !devMode;
            panelDev.Visible = devMode;
            colorBoard();
            if (devMode)
            {
                rbUserMode.BackColor = radioButtonOffColor;
                rbDevMode.BackColor = devModeButtonColor;
            }
            else
            {
                rbUserMode.BackColor = userModeButtonColor;
                rbDevMode.BackColor = radioButtonOffColor;
            }
        }
    }
}