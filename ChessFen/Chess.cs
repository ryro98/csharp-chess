using ChessBoardModel;
using System.Linq;
using System.Windows.Forms;

namespace ChessGame
{
    public partial class Chess : Form
    {
        static Board board = new Board();

        private PictureBox[,] pbGrid = new PictureBox[board.Size, board.Size];

        static string[][] puzzles = new string[][] {
            new string[]{"6k1/5ppp/8/r7/8/8/5PPP/4R1K1", "white", "Re8"},
            new string[]{"6k1/5ppp/8/8/2r5/8/5PPP/3R2K1", "white", "Rd8"},
            new string[]{"1k6/1p6/p1p3r1/8/8/3R4/PPP5/1K6", "black", "Rg1 Rd3-d1 Rd1"},
            new string[]{"5rk1/p4pp1/1p4qp/2p5/2P5/1P3RQ1/P4PPP/7K", "black", "Qb1"},
            new string[]{"r1bk1b1r/4qp2/2p1p2p/p2pN1p1/3P4/P3P1B1/1PP2PPP/R2QK2R", "white", "Nc6 Kd8-e8 Ne7"},
            new string[]{"3r2k1/5ppp/3r4/p1p5/Pp6/1P1PR1P1/5K1P/4R3", "white", "Re8 Rd8-e8 Re8"},
            new string[]{"6k1/6p1/1R5p/1P1bP1p1/r7/4R1P1/5P1P/6K1", "black", "Ra1 Re3-e1 Re1"},
            new string[]{"3r2k1/2Q2ppp/8/3pP3/3P2P1/4R1bP/5q2/7K", "white", "Qd8"},
            new string[]{"r3kr2/ppp2p2/2b4Q/3qN3/8/8/PPPP1PPP/R1B1R1K1", "black", "Qg2"},
            new string[]{"1n4k1/2p3pp/3q1r2/2rP1p2/pQ2p3/4R1P1/PPPN1P1P/2KR4", "black", "Rc2 Kc1-c2 Qb4"},
            new string[]{"2nq1bk1/p4pp1/4p3/P1p5/2P1N3/5P2/2Q3PP/R5K1", "black", "Qd4 Kg1-f1 Qa1"},
            new string[]{"b1rq3b/5k1p/p3rnpB/1pNn4/3P4/P6Q/1P3PPP/1B1R2K1", "white", "Qe6"},
            new string[]{"2b5/1Rn2k2/3pnp2/2bPp1p1/2P1P1Pp/r6P/PK1N3R/5B2", "white", "e6 Kf7-e6 Rc7"},
            new string[]{"6r1/pp6/3kbQ2/3p1p2/5P2/4P3/3q1P1P/1RR2K2", "black", "Qd3 Kf1-e1 Rg1"},
            new string[]{"1k1r2nr/ppp2ppp/8/4q3/1b1N4/3PB3/1PP1QPPP/R5K1", "white", "Nc6 Pb7-c6 Ba7 Kb8-c8 Qe5"},
            new string[]{"5r1k/pppbqBp1/2n1pp1p/7Q/3P4/2P1P1R1/PP4PP/R5K1", "white", "Rg7 Kh8-g7 Qg6 Kg7-h8 Qh6"},
            new string[]{"2r1k2r/3pnppp/4p3/p1P5/Pp2n3/1B1QP2q/1PP4P/2KRR3", "white", "Qd7 Ke8-f8 Qd8 Rc8-d8 Rd8"},
            new string[]{"r2q1rk1/p4ppp/1p1p4/2pPb3/2B5/2P5/PP3PPP/1R1Q1RK1", "black", "Qh4 Pf2-f4 Bf4 Rf1-f4 Qf4"},
        };

        private readonly Color lightSquareColor = Color.FromArgb(255, 240, 217, 181);
        private readonly Color darkSquareColor = Color.FromArgb(255, 181, 136, 99);
        private readonly Color lastMoveColor = Color.FromArgb(255, 218, 195, 50);
        private readonly Color userModeButtonColor = Color.Red;
        private readonly Color devModeButtonColor = Color.FromArgb(255, 0, 153, 0);
        private readonly Color radioButtonOffColor = Color.FromArgb(22, 21, 18);

        private readonly Image lightSquareLegalColorImage = Image.FromFile("../../../jpg/lightSquareLegalMove.png");
        private readonly Image darkSquareLegalColorImage = Image.FromFile("../../../jpg/darkSquareLegalMove.png");

        private PictureBox? sourceSquare = null;

        private bool devMode = false;
        private bool isRotated = false;
        private bool puzzleMode = false;
        private bool takes = false;

        private string moves = "";
        private string sourceSquareName = "";
        private string targetSquareName = "";
        private string turn = "white";
        private readonly string startingPosition = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR";
        private string[] puzzleSolution = Array.Empty<string>();
        private string[] computerMoves = Array.Empty<string>();

        private int moveCounter = 0;
        private int movesLimit = 0;

        public Chess()
        {
            InitializeComponent();
            GenerateGrid();
            Start();
        }
        private void GenerateGrid()
        {
            int squareSize = panelBoard.Width / board.Size;

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
                    }
                    else
                    {
                        pbGrid[i, j].BackColor = darkSquareColor;
                    }

                    pbGrid[i, j].AllowDrop = true;
                    pbGrid[i, j].MouseMove += PictureBox_MouseMove;
                    pbGrid[i, j].MouseDown += PictureBox_MouseDown;
                    pbGrid[i, j].DragEnter += PictureBox_DragEnter;
                    pbGrid[i, j].DragDrop += PictureBox_DragDrop;

                    pbGrid[i, j].Location = new Point(i * squareSize, j * squareSize);

                    pbGrid[i, j].Tag = new Point(i, j);
                    pbGrid[i, j].SizeMode = PictureBoxSizeMode.CenterImage;

                    panelBoard.Controls.Add(pbGrid[i, j]);
                }
            }
        }
        //source PictureBox
        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            PictureBox clickedSquare = (PictureBox)sender;
            if (clickedSquare.Image != null)
            {
                if (e.Button == MouseButtons.Left)
                {
                    clickedSquare.DoDragDrop(clickedSquare.Image, DragDropEffects.Move);
                }
                if (e.Button == MouseButtons.Right)
                {
                    PictureBox_MouseDown(sender, e);
                }
            }
        }
        //target PictureBox - Drag Drop Effects
        private void PictureBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
        //function activated after clicking the square
        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            ColorBoard();
            PictureBox clickedSquare = (PictureBox)sender;
            Point location = (Point)clickedSquare.Tag;
            sourceSquare = clickedSquare;
            sourceSquareName = board.TheGrid[location.X, location.Y].Name;
            string piece = "", pieceName = "", pieceColor = "";
            if (sourceSquare.Image != null)
            {
                piece = clickedSquare.Image.Tag.ToString();
                pieceName = GetPieceName(piece);
                pieceColor = GetColorPiece(sourceSquare.Image.Tag.ToString());
                if (GetFirstLetterTurn() == pieceColor)
                {
                    Cell currentCell = board.TheGrid[location.X, location.Y];
                    board.MarkNextLegalMoves(board.TheGrid, currentCell, piece, isRotated, true, true);
                    ColorLegalMoves();
                }
            }
            //SetLabel(protCountLabel, board.TheGrid[location.X, location.Y].Protecting.Count.ToString());
            if (devMode)
            {
                SetLabel(sourceSquareLabel, sourceSquareName);
                SetLabel(targetSquareLabel, "");
                SetLabel(rowLabel, location.Y.ToString());
                SetLabel(columnLabel, location.X.ToString());
                SetLabel(isOccupiedLabel, board.TheGrid[location.X, location.Y].CurrentlyOccupied.ToString());
                SetLabel(attackedLabel, board.TheGrid[location.X, location.Y].Attacked.ToString());
                SetLabel(enPassantLabel, board.TheGrid[location.X, location.Y].EnPassant.ToString());
                SetLabel(tagLabel, location.ToString());
                SetLabel(lastMoveLabel, board.TheGrid[location.X, location.Y].LastMove.ToString());
                SetLabel(clickedFigureLabel, pieceName);
                SetLabel(colorPieceLabel, pieceColor);
            }
            //if (board.TheGrid[location.X, location.Y].Attacking.Count > 0)
            //{
            //    string message = "Attacking:\n";
            //    foreach (Cell cell in board.TheGrid[location.X, location.Y].Attacking)
            //    {
            //        message += cell.Name.ToString() + " " + cell.FigureName.ToString() + "\n";
            //    }
            //    SetLabel(attackLabel, message);
            //}
            //else
            //{
            //    SetLabel(attackLabel, "Attacking:");
            //}
            //if (board.TheGrid[location.X, location.Y].Protecting.Count > 0)
            //{
            //    string message = "Protecting:\n";
            //    foreach (Cell cell in board.TheGrid[location.X, location.Y].Protecting)
            //    {
            //        message += cell.Name.ToString() + " " + cell.FigureName.ToString() + "\n";
            //    }
            //    SetLabel(protLabel, message);
            //}
            //else
            //{
            //    SetLabel(protLabel, "Protecting:");
            //}
        }
        //set the image to be the dragged image - move function
        private void PictureBox_DragDrop(object sender, DragEventArgs e)
        {
            board.ComputerLegalMoves.Clear();
            PictureBox clickedSquare = (PictureBox)sender;
            Point location = (Point)clickedSquare.Tag;
            targetSquareName = board.TheGrid[location.X, location.Y].Name;
            try
            {
                string piece = sourceSquare.Image.Tag.ToString();
                string pieceColor = GetColorPiece(piece);
                string turnLetter = GetFirstLetterTurn();
                if (puzzleMode)
                {
                    if (puzzleSolution.Length > 0)
                    {
                        string proposedMove = "";
                        if (piece.Substring(0, piece.Length - 1) == "knight")
                        {
                            proposedMove = piece.Substring(1, 1).ToUpper();
                        }
                        else if (piece.Substring(0, piece.Length - 1) != "pawn")
                        {
                            proposedMove = piece.Substring(0, 1).ToUpper();
                        }
                        proposedMove += targetSquareName;
                        PuzzleEngine(proposedMove, clickedSquare, location, piece);
                    }
                }
                else if (e.Data.GetDataPresent(DataFormats.Bitmap) && board.TheGrid[location.X, location.Y].LegalNextMove
                    && turnLetter == pieceColor)
                {
                    SetComputerName();
                    PlayerMove(clickedSquare, location, piece);
                }
                ColorBoard();
                SetLabel(targetSquareLabel, targetSquareName);
            }
            catch (NullReferenceException exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }
        //puzzle main function
        private void PuzzleEngine(string proposedMove, PictureBox clickedSquare, Point location, string piece)
        {
            if (CheckMove(proposedMove))
            {
                PlayerMove(clickedSquare, location, piece);
                RemoveMoveFromPuzzle();
                if (puzzleSolution.Length > 0)
                {
                    SetLabel(puzzleLabel, "Good move!");
                    string fromWhereX = puzzleSolution[0].Substring(1, 1);
                    string fromWhereY = puzzleSolution[0].Substring(2, 1);
                    string toWhereX = puzzleSolution[0].Substring(4, 1);
                    string toWhereY = puzzleSolution[0].Substring(5, 1);
                    PictureBox fromSquare = pbGrid[NotationToNumbers(fromWhereX), NotationToNumbers(fromWhereY)];
                    PictureBox toSquare = pbGrid[NotationToNumbers(toWhereX), NotationToNumbers(toWhereY)];
                    ComputerMove(fromSquare, toSquare);
                    RemoveMoveFromPuzzle();
                }
                else
                {
                    SetLabel(puzzleLabel, "You finished the puzzle!");
                }
            }
            else
            {
                SetLabel(puzzleLabel, "Incorrect move. Try again.");
            }
        }
        //puzzle function - checking proposed move
        private Boolean CheckMove(string move)
        {
            if (move == puzzleSolution.First())
            {
                return true;
            }
            return false;
        }
        //puzzle function - makes the opponent's move after the successful player's move
        private void ComputerMove(PictureBox fromSquare, PictureBox toSquare)
        {
            board.ComputerLegalMoves.Clear();
            if (toSquare.Image != null)
            {
                takes = true;
            }
            toSquare.Image = (Bitmap)fromSquare.Image;
            Point sourceLocation = (Point)fromSquare.Tag;
            Point location = (Point)toSquare.Tag;
            string piece = toSquare.Image.Tag.ToString();
            Cell fromSquareGrid = board.TheGrid[sourceLocation.X, sourceLocation.Y];
            Cell toSquareGrid = board.TheGrid[location.X, location.Y];
            ClearLastMove();
            fromSquareGrid.LastMove = true;
            toSquareGrid.LastMove = true;
            ColorBoard();
            toSquareGrid.CurrentlyOccupied = true;
            toSquareGrid.FigureName = GetPieceName(piece);
            toSquareGrid.FigureColor = GetColorPiece(piece);
            if (GetPieceName(piece) == "pawn")
            {
                PawnMove(sourceLocation, location, GetColorPiece(piece), toSquare);
            }
            if (GetPieceName(piece) == "king")
            {
                KingMove(location, GetPieceName(piece), GetColorPiece(piece));
            }
            DeleteFigure(fromSquare);
            AddToNotation(GetPieceName(piece), board.TheGrid[location.X, location.Y].Name, "");
            board.ActualPlayer = "player";
            ChangeTurn();
        }
        //puzzle function - remove done move from the solution string
        private void RemoveMoveFromPuzzle()
        {
            var moves = puzzleSolution.ToList();
            moves.RemoveAt(0);
            puzzleSolution = moves.ToArray();
        }
        //converting letters' coords to numbers' coords
        private int NotationToNumbers(string move)
        {
            int x = 0;
            switch (move)
            {
                case "8":
                case "a":        break;
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
        //function that makes a player move after checking it
        private void PlayerMove(PictureBox clickedSquare, Point location, string piece)
        {
            string pieceName = GetPieceName(piece);
            string pieceColor = GetColorPiece(piece);
            if (clickedSquare.Image != null)
            {
                takes = true;
            }
            Point sourceLocation = (Point)sourceSquare.Tag;
            clickedSquare.Image = (Bitmap)sourceSquare.Image;
            ClearLastMove();
            board.TheGrid[location.X, location.Y].CurrentlyOccupied = true;
            board.TheGrid[location.X, location.Y].FigureName = pieceName;
            board.TheGrid[location.X, location.Y].FigureColor = pieceColor;
            board.TheGrid[location.X, location.Y].LastMove = true;
            board.TheGrid[sourceLocation.X, sourceLocation.Y].LastMove = true;
            DeleteFigure(sourceSquare);

            if (pieceName == "pawn") //checking if its a pawn move
            {
                PawnMove(sourceLocation, location, pieceColor, clickedSquare);
            }
            if (pieceName == "king") //checking if its a castle
            {
                KingMove(location, pieceName, pieceColor);
            }
            else
            {
                AddToNotation(pieceName, targetSquareName, "");
            }
            ChangeTurn();
        }
        //function that moves the rook in the castle and evoke the notation function
        private void Castle(PictureBox rookSquare, PictureBox oldSquare, Cell rookSquareGrid, string castle)
        {
            rookSquare.Image = (Bitmap)oldSquare.Image;
            rookSquareGrid.CurrentlyOccupied = true;
            rookSquareGrid.FigureName = GetPieceName(rookSquare.Image.Tag.ToString());
            rookSquareGrid.FigureColor = GetColorPiece(rookSquare.Image.Tag.ToString());
            DeleteFigure(oldSquare);
            AddToNotation("castle", "castle", castle);
        }
        //function that changes the turn
        private void ChangeTurn()
        {
            SavePosition(GenerateFEN());
            if (turn == "white")
            {
                turn = "black";
            }
            else
            {
                turn = "white";
            }
            SetLabel(turnLabel, turn);
            if (board.MateIsItMate(board.TheGrid, turn.Substring(0, 1), isRotated))
            {
                MessageBox.Show("MATE");
            }
            else if (board.Stalemate)
            {
                DrawOccurred("Stalemate");
            }
            else if (CheckThreeMoveRepetition())
            {
                DrawOccurred("3-moves-repetition rule");
            }
            board.ScanAttackedSquares(board.TheGrid, turn.Substring(0, 1), isRotated);
            //string allMoves = "";
            //foreach (string[] move in board.ComputerLegalMoves)
            //{
            //    allMoves += move[0] + "-" + move[1] + "\n";
            //}
            //MessageBox.Show(allMoves);
            if (takes)
            {
                takes = false;
            }
            if (board.WhiteEnpassant && turn == "black")
            {
                board.WhiteEnpassant = false;
            }
            if (board.BlackEnpassant && turn == "white")
            {
                board.BlackEnpassant = false;
            }
            SetLabel(whiteENLabel, board.WhiteEnpassant.ToString());
            SetLabel(blackENLabel, board.WhiteEnpassant.ToString());
            SetLabel(checkLabel, board.Check.ToString());
            SetLabel(legalMovesLabel, board.LegalMovesCounter.ToString());
            //SetLabel(evalLabel, board.Evaluate().ToString());
            if (turn == "black" && board.ComputerLegalMoves.Count > 0 && !puzzleMode && board.ActualPlayer != "player")
            {
                board.ScanAttackedSquares(board.TheGrid, "black", isRotated);
                GenerateComputerMove();

                //btnCompMove.Enabled = true;
            }
        }
        //function that clears the board, used when setting the position from FEN
        private void ClearBoard()
        {
            for (int i = 0; i < board.Size; i++)
            {
                for (int j = 0; j < board.Size; j++)
                {
                    DeleteFigure(pbGrid[i, j]);
                }
            }
        }
        //function that colors the table's squares
        private void ColorBoard()
        {
            for (int i = 0; i < board.Size; i++)
            {
                for (int j = 0; j < board.Size; j++)
                {
                    pbGrid[i, j].BackgroundImage = null;
                    if (!board.TheGrid[i, j].LastMove)
                    {
                        if ((i + j) % 2 == 0)
                        {
                            if (!isRotated)
                            {
                                pbGrid[i, j].BackColor = lightSquareColor;
                            }
                            else
                            {
                                pbGrid[7 - i, 7 - j].BackColor = lightSquareColor;
                            }
                        }
                        else
                        {
                            if (!isRotated)
                            {
                                pbGrid[i, j].BackColor = darkSquareColor;
                            }
                            else
                            {
                                pbGrid[7 - i, 7 - j].BackColor = darkSquareColor;
                            }
                        }
                    }
                    else
                    {
                        if (!isRotated)
                        {
                            pbGrid[i, j].BackColor = lastMoveColor;
                        }
                        else
                        {
                            pbGrid[7 - i, 7 - j].BackColor = lastMoveColor;
                        }
                    }
                }
            }
        }
        private void ClearLastMove()
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    board.TheGrid[x, y].LastMove = false;
                }
            }
        }
        private void ClearEnPassant()
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    board.TheGrid[x, y].EnPassant = false;
                }
            }
        }
        private void ColorLegalMoves()
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
                    if (board.TheGrid[i, j].LegalNextMove)
                    {
                        if ((i + j) % 2 == 0)
                        {
                            pb.BackgroundImage = lightSquareLegalColorImage;
                        }
                        else
                        {
                            pb.BackgroundImage = darkSquareLegalColorImage;
                        }
                    }
                    if (board.TheGrid[i, j].Attacked && devMode)
                    {
                        pb.BackColor = Color.Red;
                    }
                }
            }
        }

        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            ClearLastMove();
            ColorBoard();
            ClearLabels();
            ReadFromFen(tbFEN.Text);
        }
        private void ReadFromFen(string fen)
        {
            int whiteKing = 0, whiteQueen = 0, whiteRook = 0, whiteBishop = 0, whiteKnight = 0, whitePawn = 0,
                blackKing = 0, blackQueen = 0, blackRook = 0, blackBishop = 0, blackKnight = 0, blackPawn = 0,
                squares = 0, row = 0;
            bool isCorrect = true;

            char[] characters = fen.ToCharArray();
            for (int x = 0; x < characters.Length; x++)
            {
                if (isCorrect)
                {
                    switch (characters[x].ToString())
                    {
                        case "p": squares += 1; row += 1; blackPawn += 1; break;
                        case "P": squares += 1; row += 1; whitePawn += 1; break;
                        case "b": squares += 1; row += 1; blackBishop += 1; break;
                        case "B": squares += 1; row += 1; whiteBishop += 1; break;
                        case "n": squares += 1; row += 1; blackKnight += 1; break;
                        case "N": squares += 1; row += 1; whiteKnight += 1; break;
                        case "r": squares += 1; row += 1; blackRook += 1; break;
                        case "R": squares += 1; row += 1; whiteRook += 1; break;
                        case "q": squares += 1; row += 1; blackQueen += 1; break;
                        case "Q": squares += 1; row += 1; whiteQueen += 1; break;
                        case "k": squares += 1; row += 1; blackKing += 1; break;
                        case "K": squares += 1; row += 1; whiteKing += 1; break;
                        case "1": squares += 1; row += 1; break;
                        case "2": squares += 2; row += 2; break;
                        case "3": squares += 3; row += 3; break;
                        case "4": squares += 4; row += 4; break;
                        case "5": squares += 5; row += 5; break;
                        case "6": squares += 6; row += 6; break;
                        case "7": squares += 7; row += 7; break;
                        case "8": squares += 8; row += 8; break;
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
                            SetLabel(generateFenLabel, String.Format("Unknown char: %s", x));
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
                    SetLabel(generateFenLabel, "Incorrect code: Too much black pawns on the board.");
                    return;
                }
                //white pawns > 8
                if (whitePawn > 8)
                {
                    SetLabel(generateFenLabel, "Incorrect code: Too much white pawns on the board.");
                    return;
                }
                //black bishops + pawns > 10
                if (blackBishop + blackPawn > 10)
                {
                    SetLabel(generateFenLabel, "Incorrect code: Too much black bishops on the board.");
                    return;
                }
                //white bishops + pawns > 10
                if (whiteBishop + whitePawn > 10)
                {
                    SetLabel(generateFenLabel, "Incorrect code: Too much white bishops on the board.");
                    return;
                }
                //black knights + pawns > 10
                if (blackKnight + blackPawn > 10)
                {
                    SetLabel(generateFenLabel, "Incorrect code: Too much black knights on the board.");
                    return;
                }
                //white knights + pawns > 10
                if (whiteKnight + whitePawn > 10)
                {
                    SetLabel(generateFenLabel, "Incorrect code: Too much white knights on the board."); 
                    return;
                }
                //black rooks + pawns > 10
                if (blackRook + blackPawn > 10)
                {
                    SetLabel(generateFenLabel, "Incorrect code: Too much black rooks on the board.");
                    return;
                }
                //white rooks + pawns > 10
                if (whiteRook + whitePawn > 10)
                {
                    SetLabel(generateFenLabel, "Incorrect code: Too much white rooks on the board.");
                    return;
                }
                //black queens + pawns > 9
                if (blackQueen + blackPawn > 9)
                {
                    SetLabel(generateFenLabel, "Incorrect code: Too much black queens on the board.");
                    return;
                }
                //white queens + pawns > 9
                if (whiteQueen + whitePawn > 9)
                {
                    SetLabel(generateFenLabel, "Incorrect code: Too much white queens on the board.");
                    return;
                }
                //black king == 0
                if (blackKing == 0)
                {
                    SetLabel(generateFenLabel, "Incorrect code: Black king does not exist.");
                    return;
                }
                //black king > 1
                if (blackKing > 1)
                {
                    SetLabel(generateFenLabel, String.Format("Incorrect code: Black king has {0} clones.", blackKing - 1));
                    return;
                }
                //white king == 0
                if (whiteKing == 0)
                {
                    SetLabel(generateFenLabel, "Incorrect code: White king does not exist.");
                    return;
                }
                //white king > 1
                if (whiteKing > 1)
                {
                    SetLabel(generateFenLabel, String.Format("Incorrect code: White king has {0} clones.", whiteKing - 1));
                    return;
                }
                SetLabel(generateFenLabel, "FEN code is correct :)");
                ClearBoard();
                int i = 0, j = 0;
                string path, name, color;
                for (int x = 0; x < characters.Length; x++)
                {
                    path = "";
                    name = "";
                    color = "";
                    switch (characters[x].ToString())
                    {
                        case "p": path = "../../../jpg/bPawn.png"; name = "pawn"; color = "b"; break;
                        case "P": path = "../../../jpg/wPawn.png"; name = "pawn"; color = "w"; break;
                        case "b": path = "../../../jpg/bBishop.png"; name = "bishop"; color = "b"; break;
                        case "B": path = "../../../jpg/wBishop.png"; name = "bishop"; color = "w"; break;
                        case "n": path = "../../../jpg/bKnight.png"; name = "knight"; color = "b"; break;
                        case "N": path = "../../../jpg/wKnight.png"; name = "knight"; color = "w"; break;
                        case "r": path = "../../../jpg/bRook.png"; name = "rook"; color = "b"; break;
                        case "R": path = "../../../jpg/wRook.png"; name = "rook"; color = "w"; break;
                        case "q": path = "../../../jpg/bQueen.png"; name = "queen"; color = "b"; break;
                        case "Q": path = "../../../jpg/wQueen.png"; name = "queen"; color = "w"; break;
                        case "k": path = "../../../jpg/bKing.png"; name = "king"; color = "b"; break;
                        case "K": path = "../../../jpg/wKing.png"; name = "king"; color = "w"; break;
                        case "1": j += 1; break;
                        case "2": j += 2; break;
                        case "3": j += 3; break;
                        case "4": j += 4; break;
                        case "5": j += 5; break;
                        case "6": j += 6; break;
                        case "7": j += 7; break;
                        case "8": j += 8; break;
                        case "/": i += 1; j = 0; break;
                        default: SetLabel(generateFenLabel, "Something went wrong."); break;
                    }
                    if (path != "" && name != "")
                    {
                        AddFigure(pbGrid[j, i], path, name, color);
                        j += 1;
                    }
                }
            }
            else
            {
                SetLabel(generateFenLabel, "Incorrect code. Try again.");
            }
        }
        private void AddFigure(PictureBox pb, string path, string name, string color)
        {
            Image image = Image.FromFile(path);
            image.Tag = name + color;
            pb.Image = image;
            Point location = (Point)pb.Tag;
            board.TheGrid[location.X, location.Y].CurrentlyOccupied = true;
            board.TheGrid[location.X, location.Y].FigureName = name;
            board.TheGrid[location.X, location.Y].FigureColor = color;
        }
        private void DeleteFigure(PictureBox pb)
        {
            pb.Image = null;
            Point location = (Point)pb.Tag;
            board.TheGrid[location.X, location.Y].CurrentlyOccupied = false;
            board.TheGrid[location.X, location.Y].FigureName = "";
            board.TheGrid[location.X, location.Y].FigureColor = "";
        }
        private String GetPieceName(string piece)
        {
            return piece.Substring(0, piece.Length - 1);
        }
        private String GetColorPiece(string piece)
        {
            return piece.Substring(piece.Length - 1);
        }
        private String GetFirstLetterTurn()
        {
            return turn.Substring(0, 1);
        }
        private void BtnGenerateFen_Click(object sender, EventArgs e)
        {
            generateFenTB.Text = GenerateFEN();
        }
        private String GenerateFEN()
        {
            string fen = "";
            int emptySquares;
            for (int x = 0; x < 8; x++)
            {
                emptySquares = 0;
                for (int y = 0; y < 8; y++)
                {
                    PictureBox pb = pbGrid[y, x];
                    if (pb.Image != null)
                    {
                        if (emptySquares > 0)
                        {
                            fen += emptySquares;
                            emptySquares = 0;
                        }
                        string piece = pb.Image.Tag.ToString();
                        string pieceName = GetPieceName(piece);
                        string colorPiece = GetColorPiece(piece);
                        string letter = "";
                        switch (pieceName)
                        {
                            case "king": letter = "k"; break;
                            case "queen": letter = "q"; break;
                            case "rook": letter = "r"; break;
                            case "knight": letter = "n"; break;
                            case "bishop": letter = "b"; break;
                            case "pawn": letter = "p"; break;
                            default: break;
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
        private void ClearLabels()
        {
            Label[] labels = {attackedLabel, blackENLabel, checkLabel, clickedFigureLabel, colorPieceLabel, columnLabel,
                enPassantLabel, generateFenLabel, isOccupiedLabel, lastMoveLabel, movesLabel, puzzleLabel, rowLabel,
                sourceSquareLabel, tagLabel, targetSquareLabel, turnLabel, whiteENLabel};
            turn = "white";
            moves = "";
            movesLimit = 0;
            moveCounter = 0;
            board.Stalemate = false;
            board.Draw = false;
            board.Positions.Clear();
            board.Positions.Add(startingPosition, 1);
            foreach (Label label in labels)
            {
                label.Text = "";
            }
        }
        private void AddToNotation(string figure, string square, string castle)
        {
            string move = "";
            if (turn == "white")
            {
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
                board.ScanAttackedSquares(board.TheGrid, "b", isRotated);
            }
            else
            {
                board.ScanAttackedSquares(board.TheGrid, "w", isRotated);
            }
            if (board.Check)
            {
                bool mate;
                if (turn == "white")
                {
                    mate = board.MateIsItMate(board.TheGrid, "b", isRotated);
                }
                else
                {
                    mate = board.MateIsItMate(board.TheGrid, "w", isRotated);
                }
                if (mate)
                {
                    move += "#";
                    if (turn == "white")
                    {
                        move += "\n1-0";
                    }
                    else
                    {
                        move += "\n0-1";
                    }
                }
                else
                {
                    move += "+";
                }
            }
            if (movesLimit + move.Length > 34)
            {
                moves += "\n";
                movesLimit = 0;
            }
            movesLimit += move.Length;
            moves += move + " ";
            SetLabel(movesLabel, moves);
        }
        private void BtnRotateBoard_Click(object sender, EventArgs e)
        {
            RotateBoard();
        }
        private void RotateBoard()
        {
            isRotated = !isRotated;
            ColorBoard();
            string fen = GenerateFEN();
            PictureBox[,] rotatedPbGrid = new PictureBox[board.Size, board.Size];
            Cell[,] rotatedBoard = new Cell[board.Size, board.Size];
            for (int x = 0; x < board.Size; x++)
            {
                for (int y = 0; y < board.Size; y++)
                {
                    rotatedPbGrid[x, y] = pbGrid[7 - x, 7 - y];
                    rotatedBoard[x, y] = board.TheGrid[7 - x, 7 - y];
                    rotatedBoard[x, y].ColumnNumber = 7 - rotatedBoard[x, y].ColumnNumber;
                    rotatedBoard[x, y].RowNumber = 7 - rotatedBoard[x, y].RowNumber;
                }
            }
            pbGrid = rotatedPbGrid;
            board.TheGrid = rotatedBoard;

            ReadFromFen(fen);
        }
        private void BtnPuzzle_Click(object sender, EventArgs e)
        {
            puzzleMode = true; //set puzzle mode
            ClearLabels(); //clear all the labels
            ClearLastMove();
            ColorBoard();
            Random random = new Random();
            string[] puzzle = puzzles[random.Next(0, puzzles.Length)];
            string puzzleFEN = puzzle[0];
            string puzzleColor = puzzle[1];
            puzzleSolution = puzzle[2].Split(" ");
            ReadFromFen(puzzleFEN); //load position
            //rotate board if needed
            if ((puzzleColor == "black" && !isRotated) || (puzzleColor == "white" && isRotated))
            {
                RotateBoard();
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
            board.ScanAttackedSquares(board.TheGrid, turn.Substring(0, 1), isRotated);
            SetLabel(turnLabel, turn);
            SetLabel(puzzleLabel, "Puzzle loaded");
        }

        private void BtnStartingPosition_Click(object sender, EventArgs e)
        {
            Start();
        }
        private void Start()
        {
            puzzleMode = false;
            ClearLabels();
            ClearLastMove();
            ColorBoard();
            ReadFromFen(startingPosition);
            board.ScanAttackedSquares(board.TheGrid, "w", isRotated);
            SetLabel(turnLabel, turn);
            SetLabel(legalMovesLabel, board.LegalMovesCounter.ToString());
            board.WhiteKingMoved = false;
            board.BlackKingMoved = false;
        }
        private void RbUserMode_CheckedChanged(object sender, EventArgs e)
        {
            ChangeMode();
        }
        private void ChangeMode()
        {
            devMode = !devMode;
            panelDev.Visible = devMode;
            ColorBoard();
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
        private void Promoting(PictureBox pb, Cell cell, string pieceColor)
        {
            if (MessageBox.Show("Do you want to promote to queen?", "Promotion", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                AddFigure(pb, String.Format("../../../jpg/{0}Queen.png", pieceColor), "queen", pieceColor);
                cell.FigureName = "queen";
            }
            else if (MessageBox.Show("Do you want to promote to rook?", "Promotion", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                AddFigure(pb, String.Format("../../../jpg/{0}Rook.png", pieceColor), "rook", pieceColor);
                cell.FigureName = "rook";
            }
            else if (MessageBox.Show("Do you want to promote to knight?\n(If no, promoting to bishop)", "Promotion", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                AddFigure(pb, String.Format("../../../jpg/{0}Knight.png", pieceColor), "knight", pieceColor);
                cell.FigureName = "knight";
            }
            else
            {
                AddFigure(pb, String.Format("../../../jpg/{0}Bishop.png", pieceColor), "bishop", pieceColor);
                cell.FigureName = "bishop";
            }
        }
        private void InfoButton_Click(object sender, EventArgs e)
        {
            string patchNotes = "";
            string[] lines = File.ReadAllLines("../../../patchnotes.txt");
            foreach (string line in lines)
            {
                patchNotes += line + "\n";
            }
            MessageBox.Show(patchNotes, "Patch notes");
        }
        private void SetLabel(Label label, string text)
        {
            label.Text = text;
        }
        private void SavePosition(string fen)
        {
            if (board.Positions.ContainsKey(fen))
            {
                board.Positions[fen] += 1;
            }
            else
            {
                board.Positions.Add(fen, 1);
            }
        }
        private Boolean CheckThreeMoveRepetition()
        {
            foreach (KeyValuePair<string, int> position in board.Positions)
            {
                if (position.Value == 3)
                {
                    return true;
                }
            }
            return false;
        }
        private void DrawOccurred(string reason)
        {
            MessageBox.Show("Draw: " + reason);
            turn = "draw";
            moves += "\n1/2-1/2";
            SetLabel(movesLabel, moves);
        }
        private void GenerateComputerMove()
        {
            //board.ScanAttackedSquares(board.TheGrid, "black", isRotated);
            string[] computerMove = board.ComputerMove(board.ActualPlayer, isRotated);
            //string evalMoves = "";
            //foreach (int x in Enumerable.Range(0, board.EvalMoves.Count))
            //{
            //    string[] move = board.EvalMoves.ElementAt(x);
            //    int eval = board.EvalMovesInt.ElementAt(x);
            //    evalMoves += move[0] + "-" + move[1] + " " + eval + "\n";
            //}
            //MessageBox.Show(evalMoves);
            string fromWhereX = computerMove[0].Substring(0, 1);
            string fromWhereY = computerMove[0].Substring(1, 1);
            string toWhereX = computerMove[1].Substring(0, 1);
            string toWhereY = computerMove[1].Substring(1, 1);
            PictureBox fromSquare = pbGrid[NotationToNumbers(fromWhereX), NotationToNumbers(fromWhereY)];
            PictureBox toSquare = pbGrid[NotationToNumbers(toWhereX), NotationToNumbers(toWhereY)];
            ComputerMove(fromSquare, toSquare);
        }
        private void PawnMove(Point sourceLocation, Point location, string pieceColor, PictureBox toSquare)
        {
            bool checkEnPassant = board.TheGrid[location.X, location.Y].EnPassant;
            if (location.Y - 2 == sourceLocation.Y || location.Y + 2 == sourceLocation.Y) //checking if its a 2-square-pawn move
            {
                if (pieceColor == "w")
                {
                    board.BlackEnpassant = true;
                    if (!isRotated)
                    {
                        board.TheGrid[location.X, location.Y + 1].EnPassant = true;
                    }
                    else
                    {
                        board.TheGrid[location.X, location.Y - 1].EnPassant = true;
                    }
                }
                else // pieceColor == "b"
                {
                    board.WhiteEnpassant = true;
                    if (!isRotated)
                    {
                        board.TheGrid[location.X, location.Y - 1].EnPassant = true;
                    }
                    else
                    {
                        board.TheGrid[location.X, location.Y + 1].EnPassant = true;
                    }
                }
            }
            else if (checkEnPassant)
            {
                if (!isRotated)
                {
                    if (pieceColor == "w" && board.WhiteEnpassant)
                    {
                        DeleteFigure(pbGrid[location.X, location.Y + 1]);
                    }
                    else if (pieceColor == "b" && board.BlackEnpassant)
                    {
                        DeleteFigure(pbGrid[location.X, location.Y - 1]);
                    }
                }
                else
                {
                    if (pieceColor == "w" && board.WhiteEnpassant)
                    {
                        DeleteFigure(pbGrid[7 - location.X, 7 - (location.Y - 1)]);
                    }
                    else if (pieceColor == "b" && board.BlackEnpassant)
                    {
                        DeleteFigure(pbGrid[7 - location.X, 7 - (location.Y + 1)]);
                    }
                }
                takes = true;
            }
            else if (location.Y == 0 || location.Y == 7)
            {
                Promoting(toSquare, board.TheGrid[location.X, location.Y], pieceColor);
            }
        }
        private void KingMove(Point location, string pieceName, string pieceColor)
        {
            if (pieceColor == "w" && !board.WhiteKingMoved) //white king
            {
                if (!isRotated) //white perspective
                {
                    if (location.X == 6) //0-0
                    {
                        Castle(pbGrid[5, 7], pbGrid[7, 7], board.TheGrid[5, 7], "0-0");
                    }
                    else if (location.X == 2) //0-0-0
                    {
                        Castle(pbGrid[3, 7], pbGrid[0, 7], board.TheGrid[3, 7], "0-0-0");
                    }
                    else
                    {
                        AddToNotation(pieceName, targetSquareName, "");
                    }
                }
                if (isRotated) //black perspective
                {
                    if (location.X == 1) //0-0
                    {
                        Castle(pbGrid[5, 7], pbGrid[7, 7], board.TheGrid[2, 0], "0-0");
                    }
                    else if (location.X == 5) //0-0-0
                    {
                        Castle(pbGrid[3, 7], pbGrid[0, 7], board.TheGrid[4, 0], "0-0-0");
                    }
                    else
                    {
                        AddToNotation(pieceName, targetSquareName, "");
                    }
                }
            }
            else if (pieceColor == "b" && !board.BlackKingMoved) //black king
            {
                if (!isRotated) //white perspective
                {
                    if (location.X == 6) //0-0
                    {
                        Castle(pbGrid[5, 0], pbGrid[7, 0], board.TheGrid[5, 0], "0-0");
                    }
                    else if (location.X == 2) //0-0-0
                    {
                        Castle(pbGrid[3, 0], pbGrid[0, 0], board.TheGrid[3, 0], "0-0-0");
                    }
                    else
                    {
                        AddToNotation(pieceName, targetSquareName, "");
                    }
                }
                if (isRotated) //black perspective
                {
                    if (location.X == 1) //0-0
                    {
                        Castle(pbGrid[5, 0], pbGrid[7, 0], board.TheGrid[2, 7], "0-0");
                    }
                    else if (location.X == 5) //0-0-0
                    {
                        Castle(pbGrid[3, 0], pbGrid[0, 0], board.TheGrid[4, 7], "0-0-0");
                    }
                    else
                    {
                        AddToNotation(pieceName, targetSquareName, "");
                    }
                }
            }
            else
            {
                AddToNotation(pieceName, targetSquareName, "");
            }
            if (pieceColor == "w" && !board.WhiteKingMoved)
            {
                board.WhiteKingMoved = true;
            }
            else if (!board.BlackKingMoved) //pieceColor == "b"
            {
                board.BlackKingMoved = true;
            }
        }
        private void SetComputerName()
        {
            if (rbRandomMoves.Checked)
            {
                board.ActualPlayer = "random";
            }
            else if (rbPawnMoves.Checked)
            {
                board.ActualPlayer = "pawns";
            }
            else if (rbSafeMove.Checked)
            {
                board.ActualPlayer = "safe";
            }
            else if (rbPlayerMoves.Checked)
            {
                board.ActualPlayer = "player";
            }
        }
    }
}