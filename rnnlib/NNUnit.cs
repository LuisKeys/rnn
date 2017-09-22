using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rnnlib
{
    public class NNUnit
    {
        private List<Cell> _cells = null;
        private RandomManager _randomManager = null;

        public NNUnit()
        {
            _cells = new List<Cell>();
            _randomManager = new RandomManager();

            createCells();
            assignCells(1024, true);
            assignCells(2, false);
            connectCells();
        }

        private void createCells()
        {
            int numberOfCells = 10000;
            for (int i = 0; i < numberOfCells; ++i)
            {
                _cells.Add(new Cell());
            }

            Console.WriteLine(numberOfCells.ToString() + " cells were created...");
        }

        private void connectCells()
        {
            int numOfCells = _cells.Count;
            int numOfConnections = 0;

            for (int i = 0; i < numOfCells; ++i)
            {
                int indexSource = _randomManager.getRandom(_cells.Count);
                int indexDest = _randomManager.getRandom(_cells.Count);
                if (!_cells[indexSource].IsFirstLayer)
                {
                    _cells[indexSource].Outputs.Add(_cells[indexDest]);
                    _cells[indexDest].Inputs.Add(_cells[indexSource]);
                    numOfConnections++;
                }
            }

            Console.WriteLine(numOfConnections.ToString() + " connections were created...");
        }

        private void assignCells(int numberOfCells, bool markInput)
        {
            int numberOfMarkedCells = 0;
            while (numberOfMarkedCells < numberOfCells)
            {
                int index = _randomManager.getRandom(_cells.Count);
                if (!_cells[index].IsFirstLayer && !_cells[index].IsLastLayer)
                {
                    if (markInput)
                        _cells[index].IsFirstLayer = true;
                    else
                        _cells[index].IsLastLayer = true;

                    numberOfMarkedCells++;
                }
            }

            if (markInput)
                Console.WriteLine(numberOfMarkedCells.ToString() + " cells were marked as input...");
            else
                Console.WriteLine(numberOfMarkedCells.ToString() + " cells were marked as output...");
        }

    }
}
