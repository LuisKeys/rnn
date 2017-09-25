using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rnnlib
{
    public class NNUnit
    {
        private RandomManager _randomManager = null;
        private List<Cell> _cells = null;
        private List<Cell> _inputCells = null;
        private List<Cell> _outputCells = null;
        private List<List<Cell>> _levels = null;
        private double[] _inputValues = null;
        private int _totalNumOfCells = 0;
        private int _numInputCells = 0;
        private int _numOutputCells = 0;
        private int _maxNumOfConnectionsPercell = 0;
        private int _numOfLevels = 0;


        public List<Cell> Cells { get => _cells; set => _cells = value; }
        public List<Cell> InputCells { get => _inputCells; set => _inputCells = value; }
        public List<Cell> OutputCells { get => _outputCells; set => _outputCells = value; }
        public int TotalNumOfCells { get => _totalNumOfCells; set => _totalNumOfCells = value; }
        public int NumInputCells { get => _numInputCells; set => _numInputCells = value; }
        public int NumOutputCells { get => _numOutputCells; set => _numOutputCells = value; }
        public double[] InputValues { get => _inputValues; set => _inputValues = value; }
        public int MaxNumOfConnectionsPercell { get => _maxNumOfConnectionsPercell; set => _maxNumOfConnectionsPercell = value; }
        public int NumOfLevels { get => _numOfLevels; set => _numOfLevels = value; }
        public List<List<Cell>> Levels { get => _levels; set => _levels = value; }

        public NNUnit(int totalNumOfCells,
                      int numInputCells,
                      int numOutputCells,
                      int maxNumOfConnectionsPercell,
                      int numOfLevels)
        {
            TotalNumOfCells = totalNumOfCells;
            NumInputCells = numInputCells;
            NumOutputCells = numOutputCells;
            MaxNumOfConnectionsPercell = maxNumOfConnectionsPercell;
            NumOfLevels = numOfLevels;

            _inputValues = new double[totalNumOfCells];

            Cells = new List<Cell>();
            InputCells = new List<Cell>();
            OutputCells = new List<Cell>();
            Levels = new List<List<Cell>>();
            _randomManager = new RandomManager();

            createCells();
            assignCells(NumInputCells, true);
            assignCells(NumOutputCells, false);
            connectCells();
        }

        private void createCells()
        {
            for (int level = 0; level < NumOfLevels; ++level)
            {
                List<Cell> cellsPerLevel = new List<Cell>();

                for (int i = 0; i < getCellsPerLevel(); ++i)
                {
                    Cell cell = new Cell();
                    cell.Level = level;
                    Cells.Add(new Cell());
                }

                Levels.Add(cellsPerLevel);
            }

            Console.WriteLine(TotalNumOfCells.ToString() + " cells were created...");
            Console.WriteLine(NumOfLevels.ToString() + " levels...");
            Console.WriteLine(getCellsPerLevel().ToString() + " cells per level...");
        }

        private void connectCells()
        {
            int numOfCells = Cells.Count;
            int numOfConnections = 0;
            int cellsPerlevel = getCellsPerLevel(); ;

            for (int level = 0; level < NumOfLevels - 1; ++level)
                for (int j = 0; j < cellsPerlevel; ++j)
                    for (int i = 0; i < cellsPerlevel; ++i)
                    {
                        int indexSource = _randomManager.getRandom(cellsPerlevel);
                        int indexDest = _randomManager.getRandom(cellsPerlevel + 1);

                        Cell sourceCell = getCellByLevel(indexSource, level);
                        Cell destCell = getCellByLevel(indexDest, level);

                        if (!Cells[indexSource].IsLastLayer && !Cells[indexDest].IsFirstLayer)
                        {
                            if (!Cells[indexDest].Inputs.Contains(Cells[indexSource]))
                                if (Cells[indexSource].Outputs.Count < _maxNumOfConnectionsPercell)
                                {
                                    Cells[indexSource].Outputs.Add(Cells[indexDest]);
                                    Cells[indexDest].Inputs.Add(Cells[indexSource]);
                                    numOfConnections++;
                                }
                        }
                    }

            Console.WriteLine(numOfConnections.ToString() + " connections were created...");
        }

        private void assignCells(int numberOfCells, bool markInput)
        {
            int numberOfMarkedCells = 0;
            while (numberOfMarkedCells < numberOfCells)
            {
                int index = _randomManager.getRandom(Cells.Count);
                if (!Cells[index].IsFirstLayer && !Cells[index].IsLastLayer)
                {
                    if (markInput)
                    {
                        Cells[index].IsFirstLayer = true;
                        InputCells.Add(Cells[index]);
                    }
                    else
                    {
                        Cells[index].IsLastLayer = true;
                        OutputCells.Add(Cells[index]);
                    }

                    numberOfMarkedCells++;
                }
            }

            if (markInput)
                Console.WriteLine(numberOfMarkedCells.ToString() + " cells were marked as input...");
            else
                Console.WriteLine(numberOfMarkedCells.ToString() + " cells were marked as output...");
        }

        public void resetCells()
        {
            foreach (Cell cell in _cells)
            {
                cell.InputValue = 0;
            }
        }

        public int getCellsPerLevel()
        {
            return (int)((double)TotalNumOfCells / (double)NumOfLevels);
        }

        public Cell getCellByLevel(int indexInLevel, int level)
        {
            List<Cell> cellsOfLevel = Levels[level];
            return (cellsOfLevel[indexInLevel]);
        }
    }
}
