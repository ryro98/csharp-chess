namespace ChessBoardModel
{
    public class Cell
    {
        public int ColumnNumber { get; set; }
        public int RowNumber { get; set; }
        public int Value { get; set; }
        public string Name { get; set; }
        public string FigureName { get; set; }
        public string FigureColor { get; set; }
        public bool Attacked { get; set; }
        public bool CurrentlyOccupied { get; set; }
        public bool EnPassant { get; set; }
        public bool LastMove { get; set; }
        public bool LegalNextMove { get; set; }
        public List<Cell> Attacking { get; set; }
        public List<Cell> Protecting { get; set; }
        public Cell(int x, int y)
        {
            ColumnNumber = x;
            RowNumber = y;
            Attacking = new List<Cell>();
            Protecting = new List<Cell>();
        }
    }
}