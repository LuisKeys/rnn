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
        private double []  _inputValues = null;
        private int _totalNumOfCells = 0;
        private int _numInputCells = 0;
        private int _numOutputCells = 0;
        private int _maxNumOfConnectionsPercell = 0;

        public List<Cell> Cells { get => _cells; set => _cells = value; }
        public List<Cell> InputCells { get => _inputCells; set => _inputCells = value; }
        public int TotalNumOfCells { get => _totalNumOfCells; set => _totalNumOfCells = value; }
        public int NumInputCells { get => _numInputCells; set => _numInputCells = value; }
        public int NumOutputCells { get => _numOutputCells; set => _numOutputCells = value; }
        public double[] InputValues { get => _inputValues; set => _inputValues = value; }
        public int MaxNumOfConnectionsPercell { get => _maxNumOfConnectionsPercell; set => _maxNumOfConnectionsPercell = value; }

        public NNUnit(int totalNumOfCells, 
                      int numInputCells, 
                      int numOutputCells, 
                      int maxNumOfConnectionsPercell)
        {
            TotalNumOfCells = totalNumOfCells;
            NumInputCells = numInputCells;
            NumOutputCells = numOutputCells;
            MaxNumOfConnectionsPercell = maxNumOfConnectionsPercell;

            _inputValues = new double[totalNumOfCells];

            Cells = new List<Cell>();
            InputCells = new List<Cell>();
            _randomManager = new RandomManager();

            createCells();
            assignCells(NumInputCells, true);
            assignCells(NumOutputCells, false);
            connectCells();
        }

        private void createCells()
        {
            for (int i = 0; i < TotalNumOfCells; ++i)
            {
                Cells.Add(new Cell());
            }

            Console.WriteLine(TotalNumOfCells.ToString() + " cells were created...");
        }

        private void connectCells()
        {
            int numOfCells = Cells.Count;
            int numOfConnections = 0;

            for (int j = 0; j < numOfCells; ++j)
                for (int i = 0; i < numOfCells; ++i)
                {
                    int indexSource = _randomManager.getRandom(Cells.Count);
                    int indexDest = _randomManager.getRandom(Cells.Count);

                    if (!Cells[indexSource].IsLastLayer && !Cells[indexDest].IsFirstLayer)
                    {
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
                        Cells[index].IsLastLayer = true;

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

    }
}
