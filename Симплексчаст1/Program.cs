
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Симплексчаст1
{
    public class Program
    {

        public static bool IsDone(double[][] matrix)
        {
            double[][] temp = DeepCopyMatrix(matrix);

            int SolutionVextorNumber = temp.Length - 1;

            bool answer = true;

            for (int i = 0; i < temp[SolutionVextorNumber].Length; i++)
            {
                if (temp[SolutionVextorNumber][i] > 0) answer = false;
            }
            return answer;
        }
        public static void Main(string[] args)
        {
            double[][] matrix = new double[][]
            {
            new double[] { 1, 1, 0, 3, 1, 0, 8 },
            new double[] { 2, 3, 1, 0, 0, -1, 14 },
            new double[] { 0, 1, 2, 4, 0, 0, 9 },
            new double[] { 1, 2, 1, 2, 0, 0, 0 }
            };

            /*Console.WriteLine("Введите матрицу");

            for(int i = 0; i < matrix.Length; i++)
            {
                for(int j = 0; j < matrix[i].Length; j++)
                {
                    Console.Write($"{i}:{j} = ");
                    matrix[i][j] = Double.Parse(Console.ReadLine());
                }
            }*/

            Console.Clear();


            while (!IsDone(matrix))
            {
                
                Console.WriteLine("Ваша матрица:");
                foreach (double[] row in matrix)
                {
                    Console.WriteLine(string.Join(" ", row));
                }

                int I, J;

                Console.WriteLine("Введите разрешающий элемент: ");
                Console.Write("i: ");
                I = Int32.Parse(Console.ReadLine());
                Console.Write("j: ");
                J = Int32.Parse(Console.ReadLine());

                Element enablingElement = new Element(I, J);

                double[][] newMatrix = MatrixTransformation(enablingElement, matrix);

                
                
                Console.Clear();
                matrix = DeepCopyMatrix(newMatrix);

            }
            Console.WriteLine("Конечная матрица:");
            foreach (double[] row in matrix)
            {
                Console.WriteLine(string.Join(" ", row));
            }


        }

        public static double[][] MatrixTransformation(Element enablingElement, double[][] matrix)
        {
            double[][] newMatrix = DeepCopyMatrix(matrix);

            double enEl = newMatrix[enablingElement.i][enablingElement.j];
            for(int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[0].Length; j++)
                {
                    if (i == enablingElement.i || j == enablingElement.j) continue;

                    double aij = matrix[i][j];
                    double apq = matrix[enablingElement.i][enablingElement.j];
                    double apj = matrix[enablingElement.i][j];
                    double aiq = matrix[i][enablingElement.j];

                    double newElement = aij - (apj * aiq) / apq;

                    newMatrix[i][j] = newElement;
                }
            }

            for (int j = 0; j < newMatrix[enablingElement.i].Length; j++)
            {
                newMatrix[enablingElement.i][j] = newMatrix[enablingElement.i][j] / matrix[enablingElement.i][enablingElement.j];
            }

            for(int i = 0; i < newMatrix.Length; i++)
            {
                if (i == enablingElement.i) continue;

                newMatrix[i][enablingElement.j] = 0;
            }


            for (int i = 0; i < newMatrix.Length; i++)
            {
                for (int j = 0; j < newMatrix[i].Length; j++)
                {
                    newMatrix[i][j] = Math.Round(newMatrix[i][j], 2);
                }
            }


            return newMatrix;
        }

        public static double[][] DeepCopyMatrix(double[][] matrix)
        {
            double[][] newMatrix = new double[matrix.Length][];

            for (int i = 0; i < matrix.Length; i++)
            {
                newMatrix[i] = new double[matrix[i].Length];
                Array.Copy(matrix[i], newMatrix[i], matrix[i].Length);
            }

            return newMatrix;
        }
        
    }

    

    public class Element
    {
        public int i { get; set; }
        public int j { get; set; }

        public Element(int i, int j)
        {
           this.i = i;
           this.j = j;
        }
    }



}
