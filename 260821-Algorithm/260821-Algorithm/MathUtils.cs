using System;
using System.Collections.Generic;
using System.Text;

namespace _260821_Algorithm
{
    /// <summary>
    /// 수학 관련된 메서드를 구현하는 클래스.
    /// 
    /// 최대공약수, 최소공배수를 구하는 메서드,
    /// 계단 오르기 경우의 수 문제 주요 로직인 피보나치 메서드
    /// </summary>
    internal static class MathUtils
    {
        private static readonly ulong[] fibonacciResults = new ulong[MAX_MEMOIZED_INDEX + 1];
        private const byte MAX_MEMOIZED_INDEX = 93; // ulong 범위에서 수용할 수 있는 최대 피보나치 인덱스 (93까지만 가능)

        private static byte lastMemoizedIndex = 2;

        static MathUtils()
        {
            fibonacciResults[0] = 0;
            fibonacciResults[1] = 1;
            fibonacciResults[2] = 1;
        }

        /*
         * 최대 공약수 메서드 - 유클리드 호제법으로 구현
         * 
         * 유클리드 호제법 :
         * a > 0, b > 0이고 a > b이면
         * a / b = q ... r 일 때 a와 b 사이의 최대공약수는 b와 r 사이의 최대공약수와 같다.
         * 
         * 매개변수 > uint input1, uint input2
         * input 2개는 모두 양의 정수여야 함
         * 
         * a > b여야 하니깐 비교하기
         * int inputDividend = input1;
         * int inputDivisor = input2;
         * if (inputDividend < inputDivisor) inputDividend = inputDivisor;
         * 
         * int dividend = inputDividend;
         * int divisor = inputDivisor;
         * 
         * int remainder = dividend % divisor;
         * 
         * while (remainder == 0)
         * {
         * 	dividend = divisor;
         * 	divisor = remainder;
         * 	
         * 	remainder = dividend % divisor;
         * } 
         * 
         * return divisor;
         */
        public static bool TryGetGCD(ulong input1, ulong input2, out ulong result)
        {
            result = default;

            if (input1 == 0 || input2 == 0) 
                return false;

            if (input1 == input2)
            {
                result = input1;
                return true;
            }

            ulong dividend = input1 > input2 ? input1 : input2;
            ulong divisor = input1 > input2 ? input2 : input1;

            ulong remainder = dividend % divisor;

            while (remainder != 0)
            {
                dividend = divisor;
                divisor = remainder;

                remainder = dividend % divisor;
            }

            result = divisor;
            return true;
        }

        public static bool TryGetLCM(ulong input1, ulong input2, out ulong result)
        {
            result = default;

            if (input1 == 0 || input2 == 0)
                return false;

            bool isSucceedToGetGCD = TryGetGCD(input1, input2, out ulong gcd);
            if (!isSucceedToGetGCD) return false;

            ulong dividedInput1 = input1 / gcd;

            if (dividedInput1 > ulong.MaxValue / input2)
                return false;

            result = dividedInput1 * input2;
            return true;
        }

        public static bool TryGetLCM(ulong input1, ulong input2, ulong gcd, out ulong result)
        {
            result = default;

            if (input1 == 0 || input2 == 0 || gcd == 0)
                return false;

            ulong dividedInput1 = input1 / gcd;

            if (dividedInput1 > ulong.MaxValue / input2)
                return false;

            result = dividedInput1 * input2;
            return true;
        }

        /*
         * 피보나치는 재귀 사용 시 스택 오버플로우 위험(+ 느림)이 있으므로 대신 반복문으로 구하기
         * 
         * 변수 이름은 임시
         * int[] fibonacciResults;
         * int maxFibonacciMemoizationed = 2;
         * 
         * input 받는 피보나치 메서드
         * {
         *      if (input <= maxFibonacciMemoizationed) return fibonacciResults[input];
         *      
         *      for (int i = maxFibonacciMemoizationed + 1; i <= input; i++)
         *      {
         *      	fibonacciResults[i] = fibonacciResults[i - 1] + fibonacciResults[i - 2]);
         *      }
         *      
         *      maxFibonacciMemoizationed = input;
         *      return fibonacciResults[input];
         * }
         * 
         * fibonacciResults는 처음에 초기화 필요
         * 1, 2번 피보나치 결과 1로 저장, 맥스 2로 저장
         * 이니셜라이저로
         */
        public static bool TryGetFibonacci(byte inputIndex, out ulong result)
        {
            result = default;

            if (inputIndex == 0)
                return false;

            if (inputIndex > MAX_MEMOIZED_INDEX) 
                return false;

            if (inputIndex <= lastMemoizedIndex)
            {
                result = fibonacciResults[inputIndex];
                return true;
            }

            for (byte i = (byte)(lastMemoizedIndex + 1); i <= inputIndex; i++)
            {
                fibonacciResults[i] = fibonacciResults[i - 1] + fibonacciResults[i - 2];
            }

            lastMemoizedIndex = inputIndex;

            result = fibonacciResults[inputIndex];
            return true;
        }
    }
}
