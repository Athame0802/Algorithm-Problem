using System;
using System.Collections.Generic;
using System.Text;

namespace _260814_Algorithm
{
    // 동전 개수 최소로 금액 맞추기 문제
    public static class MinCoinChecker
    {
        private static readonly ulong[] coinPrices = { 500, 100, 50, 10 }; // 나중에 필요 시 내림차순 정렬 추가하기

        /*
         * 입력 받기
         * 입력 / 500, 입력 / 100 ...... 해서 동전 개수 구하고 출력
         * 최소를 구해야하니깐 500 > 100 > 50 > 10 순서
         */
        public static bool TryGetMinCoinRequired(ulong amount, out (ulong Price, ulong Quantity)[]? result, out ulong total)
        {
            result = null;
            total = 0;

            if (amount % 10 != 0) // 10의 배수 아니면 제대로 구할 수 없음
                return false;

            result = new (ulong Price, ulong Quantity)[coinPrices.Length]; // 가격이랑 동전 개수 같이 전달용

            ulong leftAmount = amount;

            for (int i = 0; i < coinPrices.Length; i++)
            {
                if (coinPrices[i] == 0) // 0으로 나누기 예외 처리
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
