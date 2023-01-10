namespace ChessBoardModel
{
    public class Cell
    {
        public int RowNumber { get; set; }
        public int ColumnNumber { get; set; }
        public string Name { get; set; }
        public bool CurrentlyOccupied { get; set; }
        public String FigureName { get; set; }
        public String FigureColor { get; set; }
        public bool LegalNextMove { get; set; }
        public bool Attacked { get; set; }
        public Cell(int x, int y)
        {
            RowNumber = x;
            ColumnNumber = y;
        }

    }
}
