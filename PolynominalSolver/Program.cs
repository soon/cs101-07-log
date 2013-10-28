using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolynominalSolver
{
    class PolynominalSolver
    {
        public PolynominalSolver(List<double> factors)
        {
            Factors = factors;
        }

        public List<double> Factors
        {
            get;
            set;
        }

        public double Calculate(double x)
        {
            double res = 0;

            for(int i = 0; i < Factors.Count; ++i)
            {
                res += Factors[i] * Math.Pow(x, i);
            }

            return res;
        }

        /// <summary>
        /// NOTE: This is NOT a pure fucntion
        /// </summary>
        public void RecursiveSolveAndPrint(double left, double right, double eps)
        {
            var l_calc = Calculate(left);
            var r_calc = Calculate(right);

            if(Math.Sign(l_calc) == Math.Sign(r_calc))
            {
#if DEBUG
                Console.WriteLine("[ II ]: f(left) = {0}, f(right) = {1}", l_calc, r_calc);
#endif
                Console.WriteLine("[ EE ]: f(left) and f(right) has the same sign.");
                Console.WriteLine("[ EE ]: I will not solve it");
                return;
            }
            else if(left > right)
            {
                Console.WriteLine("[ WW ]: left bound must not be greater than right");
                Console.WriteLine("[ II ]: I'll swap it");

                var tmp = left;
                left = right;
                right = tmp;

                tmp = l_calc;
                l_calc = r_calc;
                r_calc = tmp;
            }

            _RecursiveSolveAndPrint(left, right, eps, l_calc, r_calc);
        }

        /// <summary>
        /// NOTE: This is NOT a pure function
        /// Precondition: left <= right and sign f(left) != sign f(right)
        /// </summary>
        private void _RecursiveSolveAndPrint(double left, double right, double eps, 
                                             double l_calc, double r_calc, uint num = 0)
        {
            Console.WriteLine("Current iteration: {0}", num);
            Console.WriteLine("Current bounds: [{0}, {1}]", left, right);

#if DEBUG
            Console.WriteLine("[ II ]: l_calc = {0}, r_calc = {1}", l_calc, r_calc);
            Console.WriteLine("Press Enter to continue");
            Console.ReadLine();
#endif
            if(Math.Abs(right - left) < double.Epsilon)
            {
                Console.WriteLine("Cannot find root :(");
                return;
            }

            var curr = (right - left) / 2 + left;
            var curr_calc = Calculate(curr);

#if DEBUG
            Console.WriteLine("[ II ]: curr_calc = {0}", curr_calc);
#endif

            if(Math.Abs(curr_calc) < eps)
            {
                Console.WriteLine("Root: {0}", curr);
            }
            else if(Math.Sign(curr_calc) == Math.Sign(l_calc))
            {
                _RecursiveSolveAndPrint(curr, right, eps, curr_calc, r_calc, num + 1);
            }
            else
            {
                _RecursiveSolveAndPrint(left, curr, eps, l_calc, curr_calc, num + 1);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            double left;
            double right;
            List<double> factors;

            var parser = new Soon.ArgumentsParser(args);

            if(!parser.TryParseNext(out left))
            {
                throw new ArgumentException("Cannot parse left bound");
            }
            if(!parser.TryParseNext(out right))
            {
                throw new ArgumentException("Cannot parse right bound");
            }

            factors = parser.ParseAllPossible<double>();

            if(factors.Count != args.Length - 2)
            {
                Console.WriteLine("[ WW ]: Not all factors were succesfully parsed");
                Console.WriteLine("[ WW ]: Number of parsed factors: {0}", factors.Count);
            }

            var solver = new PolynominalSolver(factors);
            var eps = 1e-6;

            solver.RecursiveSolveAndPrint(left, right, eps);
        }
    }
}
