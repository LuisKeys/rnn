using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rnnlib
{
    public class Cell
    {
        private List<Cell> _inputs = null;
        private List<Cell> _outpus = null;
        private bool _isFirstLayer = false;
        private bool _isLastLayer = false;
        private double _inputValue = 0.0;
        private double _weight = 0.0;
        private int _level = 0;

        public List<Cell> Inputs { get => _inputs; set => _inputs = value; }
        public List<Cell> Outputs { get => _outpus; set => _outpus = value; }
        public bool IsFirstLayer { get => _isFirstLayer; set => _isFirstLayer = value; }
        public bool IsLastLayer { get => _isLastLayer; set => _isLastLayer = value; }
        public double InputValue { get => _inputValue; set => _inputValue = value; }
        public double Weight { get => _weight; set => _weight = value; }
        public int Level { get => _level; set => _level = value; }

        public Cell() {
            _inputs = new List<Cell>();
            _outpus = new List<Cell>();
        }
    }
}
