using System;
using System.Collections.Generic;
using System.Text;

namespace _260821_Algorithm
{
    public struct MasterMindInputResult
    {
        public byte StrikeCount { get; private set; }
        public byte BallCount { get; private set; }
        public bool IsAnswer { get; private set; }

        public MasterMindInputResult(byte strikeCount, byte ballCount, bool isAnswer)
        {
            StrikeCount = strikeCount;
            BallCount = ballCount;
            IsAnswer = isAnswer;
        }
    }

    /*
     * 숫자야구
     * 
     * 숫자야구 클래스
     * HashSet, 배열로 정답 저장
     * 정답 자릿수
     * 시도 횟수
     * 
     * 입력 >
     * 문자열 연산으로 맨 앞이 0인지 검사
     * TryParse로 변환?
     * 
     * 숫자 각각 다른지 검사 (HashSet)
     * 배열에 담기
     * 
     * 반복문으로 돌면서...
     * 정답 배열과 입력 배열 같은지 체크 > 있으면 StrikeCount++ 하고 continue
     * 아니면 입력 배열[i] 값이 HashSet에 있는지 체크 > 있으면 BallCount++ 하고 continue
     * 
     * 정답 자릿수 == StrikeCount면
     * 정답입니다! 와 시도 횟수 출력
     * 
     * 아니면 스트라이크랑 볼 횟수 출력
     */
    internal class MasterMindController
    {
        private readonly static char[] numbers = {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};

        public int AttemptCount { get; private set; }

        private HashSet<char> answerHashSet;
        private char[] answerArray;
        private const byte MAX_DIGIT = 10;
        private readonly byte digit;

        public MasterMindController(byte digit)
        {
            this.digit = digit;

            if (digit == 0)
                throw new ArgumentException("MasterMindController 생성자에 digit으로 0이 전달되었습니다!");

            if (digit > MAX_DIGIT) 
                throw new ArgumentException($"MasterMindController 생성자에 digit으로 {MAX_DIGIT} 이상의 너무 큰 수가 전달되었습니다!");

            answerHashSet = new(digit);
            answerArray = new char[digit];

            int currentArrayIndex = 0;
            Random rand = new Random();
            while (currentArrayIndex < digit)
            {
                int randomIndex = rand.Next(numbers.Length);
                
                if (currentArrayIndex == 0 && numbers[randomIndex] == '0')
                    continue;

                bool isNotDuplicate = answerHashSet.Add(numbers[randomIndex]);

                if (!isNotDuplicate) continue;
                answerArray[currentArrayIndex++] = numbers[randomIndex];
            }
        }

        public bool TryGuess(char[] inputs, out MasterMindInputResult result)
        {
            result = default;

            if (inputs[0] == '0')
                return false;

            if (inputs.Length != digit)
                return false;

            foreach (char input in inputs)
            {
                if (input < '0' || input > '9')
                    return false;
            }

            // 중복 검사
            bool isInputNotDuplicate = true;
            HashSet<char> inputHashset = new HashSet<char>(digit);
            foreach (char input in inputs)
            {
                bool isNotDuplicate = inputHashset.Add(input);

                if (!isNotDuplicate)
                {
                    isInputNotDuplicate = false;
                    break;
                }
            }

            if (!isInputNotDuplicate)
                return false;

            byte strikeCount = 0;
            byte ballCount = 0;

            // 스트라이크 및 볼 판단
            for (int i = 0; i < inputs.Length; i++)
            {
                if (inputs[i] == answerArray[i])
                {
                    strikeCount++;
                    continue;
                }

                if (answerHashSet.Contains(inputs[i]))
                {
                    ballCount++;
                    continue;
                }
            }

            // 결과 반환
            bool isAnswer = false;
            
            if (strikeCount == digit)
                isAnswer = true;

            AttemptCount++;
            result = new MasterMindInputResult(strikeCount, ballCount, isAnswer);
            return true;
        }
    }
}
