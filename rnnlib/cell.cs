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

        public List<Cell> Inputs { get => _inputs; set => _inputs = value; }
        public List<Cell> Outputs { get => _outpus; set => _outpus = value; }
        public bool IsFirstLayer { get => _isFirstLayer; set => _isFirstLayer = value; }
        public bool IsLastLayer { get => _isLastLayer; set => _isLastLayer = value; }

        public Cell() {
            _inputs = new List<Cell>();
            _outpus = new List<Cell>();
        }
    }
}
