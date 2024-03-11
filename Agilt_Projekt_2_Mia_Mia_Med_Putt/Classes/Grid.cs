using Windows.Foundation;


namespace Agilt_Projekt_2_Mia_Mia_Med_Putt.Pages
{
    /// <summary>
    /// Class for drawing and setting playing grid
    /// </summary>
    class Grid
    {
        private Size[,] grid;

        public Grid(int rows, int columns)
        {
            grid = new Size[rows, columns];
        }

        public Size GetValue(int rowNumber, int columnNumber)
        {
            return grid[rowNumber, columnNumber];
        }

        public void SetValue(int rowNumber, int columnNumber, Size inputItem)
        {
            grid[rowNumber, columnNumber] = inputItem;
        }

        public int RowCount()
        {
            return grid.GetLength(0);
        }

        public int ColumnCount()
        {
            return grid.GetLength(1);
        }
    }
}
