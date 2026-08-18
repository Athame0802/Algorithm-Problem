using System;
using System.Collections.Generic;
using System.Text;

namespace _260814_Algorithm
{
    // 팰린드롬, 연속 횟수 구하는 문제
    public static class NumberPatternChecker
    {
        /* 
         * 팰린드롬 문제
         * 
         * 입력 받기 - 12345
         * 변수에 저장 - int input = int leftNumber = 12345
         * 
         * 무한 반복
         * {
         *     곱하기 10해서 자리 넘김 - answer *= 10
         *     10으로 나눈 나머지 저장 - answer = leftNumber % 10    // == 5
         *     입력된 값 / 10해서 거기 저장 - leftNumber /= 10
         *     0인지 체크해서 break - if (leftNumber == 0) break
         * }
         * 
         * answer와 input이 같다면 팰린드롬 여부 O
         * 아니면 X 
         */
        public static bool TryCheckPalindrome(ulong input, out bool isPalindrome, out ulong reversedInput)
        {
            ulong leftNumber = input;
            reversedInput = 0;

            try
            {
                checked // 곱하기 10 하다가 오버플로우 나는 거 방지용 - 예시 : ulong 범위 내인 10000000000000000009는 뒤집었을 때 오버플로우가 남
                {
                    while (leftNumber != 0)
                    {
                        reversedInput *= 10;
                        reversedInput += leftNumber % 10;

                        leftNumber /= 10;
                    }
                }
            }
            catch
            {
                isPalindrome = false;
                return false;
            }

            isPalindrome = reversedInput == input;
            return true;
        }

        /* 
         * 가장 긴 연속된 수 길이 문제
         * 
         * 배열 길이 받기
         * 배열 길이로 할당
         * int input[] = new int[length];
         * 
         * int maxContinuousLength = 0;
         * int currentContinuousLength = 0;
         * 
         * for문으로 돌면서 체크 (i = 1부터)
         * {
         *      bool isContinuous = input[i - 1] + 1 == input[i]
         *      if (isContinuous)
         *      {
         *      	currentContinuousLength++;
         *      	continue;
         *      }
         *      
         *      if (maxContinuousLength < currentContinuousLength) 
         *      	maxContinuousLength = currentContinuousLength;
         *      
         *      currentContinuousLength = 0;
         * }
         * 
         * 한 번 더 Continuous 길이 검사 (마지막에 안끊긴 거 방지용)
         * 
         * 가장 긴 길이 출력
         */
        public static int CheckLongestContinuousLength(long[] inputs)
        {
            if (inputs == null || inputs.Length == 0)
                return 0;

            // 숫자가 하나만 있어도 연속은 1회
            int maxContinuousLength = 1;
            int currentContinuousLength = 1;

            for (int i = 1; i < inputs.Length; i++)
            {
                bool isStraight = inputs[i - 1] + 1 == inputs[i];
                if (isStraight)
                {
                    currentContinuousLength++; // i번째 반영
                    continue;
                }

                UpdateMaxLength();

                currentContinuousLength = 1;
            }

            UpdateMaxLength();

            return maxContinuousLength;


            void UpdateMaxLength()
            {
                if (maxContinuousLength < currentContinuousLength)
                    maxContinuousLength = currentContinuousLength;
            }
        }
    }
}
