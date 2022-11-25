using ChessBoardModel;

Board board = new Board(8);

printBoard(board);

Cell currentCell = setCurrentCell();
currentCell.CurrentlyOccupied = true;

String piece = setPiece();

string setPiece()
{
    String[] pieces = { "King", "Bishop", "Knight", "Rook", "Queen" };
    String piece;
    do
    {
        Console.WriteLine("Enter the piece (King, Bishop, Knight, Rook, Queen): ");
        piece = Console.ReadLine();
    } while (!pieces.Contains(piece));
    return piece;

}

board.MarkNextLegalMoves(currentCell, piece);

printBoard(board);

Cell setCurrentCell()
{
    int currentRow, currentColumn;
    do
    {
        Console.WriteLine("Enter the current row number (0-7): ");
        currentRow = int.Parse(Console.ReadLine());
    } while (currentRow < 0 || currentRow > 7);
    do
    {
        Console.WriteLine("Enter the current column number (0-7): ");
        currentColumn = int.Parse(Console.ReadLine());
    } while (currentColumn < 0 || currentColumn > 7);

    return board.theGrid[currentRow, currentColumn];
}

static void printBoard(Board board)
{
    Console.WriteLine("+---+---+---+---+---+---+---+---+");
    for (int i = 0; i < board.Size; i++)
    {
        for (int j = 0; j < board.Size; j++)
        {
            Cell cell = board.theGrid[i, j];
            if (cell.CurrentlyOccupied)
            {
                Console.Write("| X ");
            } else if (cell.LegalNextMove)
            {
                Console.Write("| + ");
            } else
            {
                Console.Write("|   ");
            }
        }
        Console.Write("|");
        Console.WriteLine();
        Console.WriteLine("+---+---+---+---+---+---+---+---+");

    }
}