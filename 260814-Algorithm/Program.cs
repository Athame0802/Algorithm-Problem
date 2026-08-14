namespace _260814_Algorithm
{
    // 입출력 및 메서드 호출만
    internal class Program
    {
        private static void Main(string[] args)
        {
            MainMenu();
        }

        private static void MainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.Write(
                    """
                    알고리즘 문제 풀이

                    1. 팰린드롬(회문) 검사
                    2. 가장 긴 연속된 수의 연속 횟수 출력
                    3. 특정 금액을 만족시키기 위한 최소 동전 개수 구하기

                    문제 번호 입력 (Q를 입력해 종료) >
                    """);

                string? inputString = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(inputString))
                {
                    InputKeyToClear("잘못된 입력입니다.");
                    continue;
                }

                if (inputString == "Q" || inputString == "q")
                    return;

                bool isUnsignedShort = ushort.TryParse(inputString, out ushort input);
                if (!isUnsignedShort)
                {
                    InputKeyToClear("잘못된 입력입니다.");
                    continue;
                }

                Console.Clear();
                switch (input)
                {
                    case 1:
                        CheckPalindromeMenu();
                        break;

                    case 2:
                        CheckContinuousMenu();
                        break;

                    case 3:
                        CheckMinCoinRequiredMenu();
                        break;

                    default:
                        InputKeyToClear("잘못된 입력입니다.");
                        continue;
                };

            }
        }

        private static void CheckPalindromeMenu()
        {
            while (true)
            {
                Console.Write("팰린드롬을 검사할 숫자를 입력해주세요. (Q를 입력해 나가기) >");
                string? inputString = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(inputString))
                {
                    InputKeyToClear("잘못된 입력입니다.");
                    continue;
                }

                if (inputString == "Q" || inputString == "q")
                    break;

                bool isInputUnsignedLong = ulong.TryParse(inputString, out ulong input);

                if (!isInputUnsignedLong)
                {
                    InputKeyToClear("잘못된 입력입니다.");
                    continue;
                }

                bool isPalindrome = NumberPatternChecker.CheckPalindrome(input, out ulong reversed);

                Console.WriteLine();
                Console.WriteLine($"뒤집은 숫자 : {reversed}");
                Console.Write("팰린드롬 여부 : ");
                Console.WriteLine(isPalindrome ? "YES" : "NO");

                InputKeyToClear();
            }
        }

        private static void CheckContinuousMenu()
        {
            while (true)
            {
                Console.Write("입력할 숫자의 개수를 입력해주세요. (Q를 입력해 나가기) >");
                string? inputLengthString = Console.ReadLine(); 

                if (string.IsNullOrWhiteSpace(inputLengthString))
                {
                    InputKeyToClear("잘못된 입력입니다.");
                    continue;
                }

                if (inputLengthString == "Q" || inputLengthString == "q")
                    break;

                bool isInt = int.TryParse(inputLengthString, out int inputLength);
                if (!isInt || inputLength <= 0)
                {
                    InputKeyToClear("잘못된 입력입니다.");
                    continue;
                }

                Console.Write("검사할 숫자를 입력해주세요. (Q를 입력해 나가기) >");
                string? inputStringNumbers = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(inputStringNumbers))
                {
                    InputKeyToClear("잘못된 입력입니다.");
                    continue;
                }

                if (inputStringNumbers == "Q" || inputStringNumbers == "q")
                    break;

                string[] inputStringNumbersArray = inputStringNumbers.Split([' ', '\r', '\n', '\t'],  StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                
                if (inputStringNumbersArray.Length != inputLength)
                {
                    InputKeyToClear("입력된 숫자의 개수가 다릅니다.");
                    continue;
                }

                long[] inputNumbersArray = new long[inputLength];
                bool isValid = true;

                for (int i = 0; i < inputLength; i++)
                {
                    bool isElementLong = long.TryParse(inputStringNumbersArray[i] , out inputNumbersArray[i]);

                    if (!isElementLong)
                    {
                        InputKeyToClear("잘못된 입력입니다.");
                        isValid = false;
                        break;
                    }
                }

                if (!isValid)
                    continue;

                long maxContinuousLength = NumberPatternChecker.CheckLongestContinuousLength(inputNumbersArray);
                Console.WriteLine($"\n가장 긴 연속 숫자 길이 : {maxContinuousLength}");

                InputKeyToClear();
            }
        }

        private static void CheckMinCoinRequiredMenu()
        {
            while (true)
            {
                Console.Write("필요한 최소 동전 개수를 셀 돈 입력 (10의 배수로 입력, Q를 입력해 나가기) >");
                string? inputString = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(inputString))
                {
                    InputKeyToClear("잘못된 입력입니다.");
                    continue;
                }

                if (inputString == "Q" || inputString == "q")
                    break;

                bool isUnsignedInt = ulong.TryParse(inputString, out ulong input);
                if (!isUnsignedInt)
                {
                    InputKeyToClear("잘못된 입력입니다.");
                    continue;
                }

                bool isSucceed = MinCoinChecker.TryGetMinCoinRequired(input, out (ulong Price, ulong Quantity)[]? coinResult, out ulong requiredCoinTotal);
                if (!isSucceed || coinResult == null)
                {
                    InputKeyToClear("잘못된 입력입니다.");
                    continue;
                }

                Console.WriteLine();
                foreach ((ulong Price, ulong Quantity) coin in coinResult)
                {
                    Console.WriteLine($"{coin.Price}원 : {coin.Quantity}");
                }

                Console.WriteLine($"최소 필요 동전 개수 총합 : {requiredCoinTotal}");

                InputKeyToClear();
            }
        }

        private static void InputKeyToClear(string instruction = "")
        {
            Console.WriteLine(instruction);

            Console.ReadKey();
            Console.Clear();
        }
    }
}
