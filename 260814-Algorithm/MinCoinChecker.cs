using System;
using System.Collections.Generic;
using System.Text;

namespace _260814_Algorithm
{
    // 동전 개수 최소로 금액 맞추기 문제
    public static class MinCoinChecker
    {
        private static readonly ulong[] coinPrices = { 500, 100, 50, 10 };

        /*
         * 입력 받기
         * 입력 / 500, 입력 / 100 ...... 해서 동전 개수 구하고 출력
         * 최소를 구해야하니깐 500 > 100 > 50 > 10 순서
         */
        public static bool TryGetMinCoinRequired(ulong amount, out (ulong Price, ulong Quantity)[]? result, out ulong total)
        {
            result = null;
            total = 0;

            if (amount % 10 != 0)
                return false;

            result = new (ulong Price, ulong Quantity)[coinPrices.Length];

            ulong leftAmount = amount;

            for (int i = 0; i < coinPrices.Length; i++)
            {
                if (coinPrices[i] == 0)
                    return false;

                ulong coinQuantity = leftAmount / coinPrices[i];

                result[i] = (coinPrices[i], coinQuantity);

                total += coinQuantity;
                leftAmount %= coinPrices[i]; // 동전 개수 만큼 빼기
            }

            return true;
        }
    }
}
