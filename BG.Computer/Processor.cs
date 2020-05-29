using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BG.Log;
using MathNet.Numerics.IntegralTransforms;
//using unity

namespace BG.Computer
{
    public static class Processor
    {
        static MathNet.Numerics.Complex32[] mathNetComplexArrRe = new MathNet.Numerics.Complex32[64];
        static double[] data = new double[] { 0, 3, 2, 5, 3, -7, -6, -9, -5, -13, -12, -15, -13, 17, 6, 19, 10, 13, 22, 22, 3, 27, 36, 19, 25, 13, 52, 45, 33, 22, 6, 19, 0, 3, 2, 5, 3, -7, -6, -9, -5, -13, -12, -15, -13, 17, 6, 19, 10, 13, 22, 22, 3, 27, 36, 19, 25, 13, 52, 45, 33, 22, 6, 19 };
        static public double[] resultArr = new double[64];
        public static void Start()
        {
            double[] filterArr = new double[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            resultArr = Filter(data, filterArr);
        }
        /// <summary>
        /// 滤波数组
        /// </summary>
        /// <param name="inData">输入的数据</param>
        /// <param name="filterArr">滤波数组，可以自定义</param>
        /// <returns></returns>
        public static double[] Filter(double[] inData, double[] filterArr)
        {
            double[] outArr = new double[64];
            outArr = inData;
            MathNet.Numerics.Complex32[] mathNetComplexArr = new MathNet.Numerics.Complex32[64];
            for (int i = 0; i < mathNetComplexArr.Length; i++)
            {
                mathNetComplexArr[i] = new MathNet.Numerics.Complex32((float)outArr[i], 0);
            }
            Fourier.Forward(mathNetComplexArr);//傅里叶变换
            for (int i = 0; i < mathNetComplexArr.Length; i++)
            {
                mathNetComplexArr[i] = new MathNet.Numerics.Complex32((float)(mathNetComplexArr[i].Real * filterArr[i]), (float)(mathNetComplexArr[i].Imaginary * filterArr[i]));
            }
            double[] ArrFreq = new double[64];
            for (int i = 0; i < ArrFreq.Length; i++)
            {
                ArrFreq[i] = Math.Sqrt(mathNetComplexArr[i].Imaginary * mathNetComplexArr[i].Imaginary + mathNetComplexArr[i].Real * mathNetComplexArr[i].Real);//利用LineRenderer显示频域结果
            }
            Fourier.Inverse(mathNetComplexArr);//逆傅里叶变换
            for (int i = 0; i < mathNetComplexArr.Length; i++)
            {
                outArr[i] = mathNetComplexArr[i].Real;
            }
            return outArr;
        }
    }
}
