using System;
using System.Collections.Generic;
using System.Text;

namespace _260821_Algorithm
{
    /// <summary>
    /// 계단이 있고 한 계단 혹은 두 계단을 뛰어넘을 때 경우의 수를 구하는 클래스.
    /// </summary>
    internal static class StairJumpCaseCounter
    {
        /*
         * 만약 2 개수가 0 이하면
         * 결과 = 1
         * 
         * 아니라면
         * 결과 = 2 // 2_개수가 최고 거나 0개면 무조건 결과는 1
         * for (int i = 2_개수 - 1; i > 0; i--)
         * 
         * 분자 = factorial(계단 개수 - 2 개수)
         * 분모 = factorial(2개수) x factorial(계단 개수 - 2 개수 * 2)
         * 
         * 결과 += 분자 / 분모
         * 
         * 
         *    (a - b)! 
         * b! x (a - 2b)!
         * 
         * (a-b)C(b)이고 b는 1씩 줄음
         * > (a-b)C(b) = (a-b)C(a-2b)
         * > (a-b b)
         * 
         * 
         * 
         * 파스칼의 삼각형
         * 
         *     0   1   2   3   4   5
         * 0:  1
         * 1:  1   1
         * 2:  1   2   1
         * 3:  1   3   3   1
         * 4:  1   4   6   4   1
         * 5:  1   5  10  10   5   1
         * 
         * a는 뛰는 횟수, b는 들어갈 수 있는 2의 개수
         * 즉 b는 a / 2까지만 가능
         * 그리고 (a-b b)는 합이 일정한 수를 a를 1씩 줄이고 b를 1씩 늘리는 형태
         * (2 0) + (1 1) = 2
         * (3 0) + (2 1) = 3
         * (4 0) + (3 1) + (2 2) = 5
         * (5 0) + (4 1) + (3 2) = 8 이런 식
         * >> 그 행의 0번 부터 시작해 오른쪽 위 대각선 끝까지의 합
         * >> 피보나치(a + 1)
         * 
         * 피보나치 클래스 따로 구현
         * 메모이제이션해서 Dictionary로 구현
         */

        public static bool TryCountJump1Or2StairCase(byte inputIndex, out ulong result)
        {
            result = default;

            if (inputIndex == byte.MaxValue - 1)
                return false;

            if (inputIndex == 0)
                return false;

            return MathUtils.TryGetFibonacci((byte)(inputIndex + 1), out result);
        }
    }
}
