using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wielomiany;

namespace Wielomiany
{
    static class Program
    {
        static void Main(string[] args)
        {
            Calculate(args);
            Console.ReadKey(true);

        }
        #region Helpers
        public static Int32 SetStringToInt(this String arg)
        {
             return Convert.ToInt32(arg);
        }
        public static List<Polynomial> GetPolynomialList(string[] args)
        {
            List<Polynomial> polynomialList = new List<Polynomial>();
            for (int i = 0; i < args.Length; i++)
            {
                string[] argSplit = args[i].Split(' ');
                if (argSplit[1].SetStringToInt() != -1)
                {
                    polynomialList.Last().AddComponent(argSplit[0].SetStringToInt(), argSplit[1].SetStringToInt());
                }
                else
                {
                    polynomialList.Add(new Polynomial());
                }
            }
            return polynomialList;
        }
        public static void PrintPolynomials(List<Polynomial> list)
        {
            foreach(var item in list)
            {
                Console.WriteLine("Wielomian " + item);
            }
            Console.WriteLine("\n");
        }

        public static void Calculate(string[] args)
        {
            List<Polynomial> polynomialList = GetPolynomialList(args);
            PrintPolynomials(polynomialList);

            Polynomial result1 = polynomialList.First();
            Polynomial result2 = polynomialList.First();
            Polynomial result3 = polynomialList.First();

            for (int i = 1; i < polynomialList.Count; i++)
            {
                result1 += polynomialList[i];
                result2 -= polynomialList[i];
                result3 *= polynomialList[i];
            }

            Console.WriteLine("(+) " + result1);
            Console.WriteLine("(-) " + result2);
            Console.WriteLine("(*) " + result3);
        }
        #endregion
    }
}
