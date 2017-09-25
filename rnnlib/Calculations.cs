using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rnnlib
{
    public class Calculations
    {
        public void forwardProp(NNUnit rnn)
        {
            foreach (double value in rnn.InputValues)
            {
                foreach (Cell inputcell in rnn.InputCells)
                {
                    inputcell.InputValue += value;
                }
            }

            //foreach (Cell inputcell in rnn.InputCells)
            //{
            //    propagate(inputcell);
            //}
        }

        public void propagate(Cell inputCell)
        {
            foreach (Cell output in inputCell.Outputs)
            {
                output.InputValue += relu(inputCell.InputValue);
                propagate(output);
            }
        }

        public double relu(double input)
        {
            if (input <= 0)
                return 0;

            return input;
        }
    }
}
