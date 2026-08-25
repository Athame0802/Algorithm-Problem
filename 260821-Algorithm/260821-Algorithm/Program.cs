namespace _260821_Algorithm
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            OpenMainMenu();
        }

        private static void OpenMainMenu()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("""
                    알고리즘 문제 풀이

                    1. 최대공약수 / 최소공배수 문제
                    2. 숫자야구 문제
                    3. 계단을 한 칸 혹은 두 칸 뛰어넘을 때 경우의 수 문제

                    Q를 입력해 종료할 수 있습니다.

                    """);

                Console.Write("메뉴 번호 : ");
                string? inputString = Console.ReadLine();

                if (inputString == "q" || inputString == "Q")
                    return;

                if (string.IsNullOrWhiteSpace(inputString))
                {
                    InputKeyToClear("잘못된 입력입니다.");
                    continue;
                }

                bool isSucceedToParse = byte.TryParse(inputString, out byte input);
                if (!isSucceedToParse)
                {
                    InputKeyToClear("잘못된 입력입니다.");
                    continue;
                }

                switch (input)
                {
                    case 1:
                        Console.Clear();
                        OpenGCDAndLCMMenu();
                        break;
                        
                    case 2:
                        Console.Clear();
                        OpenMasterMindMenu();
                        break;

                    case 3:
                        Console.Clear();
                        OpenStairJumpCaseCountMenu();
                        break;

                    default:
                        InputKeyToClear("잘못된 입력입니다.");
                        continue;
                }
            }
        }

        private static void OpenGCDAndLCMMenu()
        {
            while (true)
            {
                Console.WriteLine("""
                    최대공약수 / 최소공배수 문제

                    양의 정수만 입력해주세요.
                    Q를 입력해 되돌아갈 수 있습니다.

                    """);

                Console.Write("첫 번째 수 : ");
                string? input1String = Console.ReadLine();

                if (input1String == "q" || input1String == "Q")
                    return;

                Console.Write("두 번째 수 : ");
                string? input2String = Console.ReadLine();

                if (input2String == "q" || input2String == "Q")
                    return;

                if (string.IsNullOrWhiteSpace(input1String) && string.IsNullOrWhiteSpace(input2String))
                {
                    InputKeyToClear("잘못된 입력입니다.");
                    continue;
                }

                ulong input1 = 0;
                ulong input2 = 0;

                bool isSucceedToParse = ulong.TryParse(input1String, out input1) && ulong.TryParse(input2String, out input2);
                if (!isSucceedToParse)
                {
                    InputKeyToClear("잘못된 입력입니다.");
                    continue;
                }

                bool isSucceedToGetGCD = MathUtils.TryGetGCD(input1, input2, out ulong gcd);
                if (!isSucceedToGetGCD) 
                {
                    InputKeyToClear("잘못된 입력입니다.");
                    continue;
                }

                bool isSucceedToGetLCM = MathUtils.TryGetLCM(input1, input2, gcd, out ulong lcm);
                if (!isSucceedToGetLCM)
                {
                    InputKeyToClear("잘못된 입력입니다.");
                    continue;
                }

                Console.WriteLine();
                Console.WriteLine($"최대공약수 : {gcd}");
                Console.WriteLine($"최소공배수 : {lcm}");
                InputKeyToClear();
            }
        }

        private static void OpenMasterMindMenu()
        {
            while (true)
            {
                byte digit = 3;
                MasterMindController masterMind = new MasterMindController(digit);

                Console.WriteLine("""
                     숫자 야구 문제

                     0 ~ 9 사이의 숫자 3개를 입력해주세요.
                     세 자리 숫자의 각 숫자는 서로 달라야 합니다.
                     Q를 입력해 되돌아갈 수 있습니다.

                     """);

                while (true)
                {
                    Console.Write("사용자 입력 : ");
                    string? inputString = Console.ReadLine();

                    if (inputString == "q" || inputString == "Q")
                        return;

                    if (string.IsNullOrWhiteSpace(inputString))
                    {
                        Console.WriteLine("잘못된 입력입니다.\n");
                        continue;
                    }

                    bool isSucceedToParse = uint.TryParse(inputString, out uint input);
                    if (!isSucceedToParse)
                    {
                        Console.WriteLine("잘못된 입력입니다.\n");
                        continue;
                    }

                    if (inputString.Length != digit)
                    {
                        Console.WriteLine("잘못된 입력입니다.\n");
                        continue;
                    }

                    bool isSucceedToGuess = masterMind.TryGuess(inputString.ToCharArray(), out MasterMindInputResult result);
                    if (!isSucceedToGuess)
                    {
                        Console.WriteLine("잘못된 입력입니다.\n");
                        continue;
                    }

                    if (result.IsAnswer)
                    {
                        Console.WriteLine($"결과: 정답입니다! (총 시도 횟수 : {masterMind.AttemptCount}회)");
                        InputKeyToClear();
                        masterMind = new MasterMindController(digit);
                        break;
                    }

                    Console.WriteLine($"결과: {result.StrikeCount} Strike, {result.BallCount} Ball\n");
                }
            }
        }

        private static void OpenStairJumpCaseCountMenu()
        {
            while (true)
            {
                Console.WriteLine("""
                    계단을 한 번에 한 칸 혹은 두 칸 오를 때 계단을 모두 오르는 경우의 수 문제
                    
                    계단 수는 1 ~ 50 사이의 정수로 입력해주세요.
                    Q를 입력해 되돌아갈 수 있습니다.

                    """);

                Console.Write("계단의 높이 : ");
                string? inputString = Console.ReadLine();

                if (inputString == "q" || inputString == "Q")
                    return;

                if (string.IsNullOrWhiteSpace(inputString))
                {
                    InputKeyToClear("잘못된 입력입니다.");
                    continue;
                }

                bool isSucceedToParse = byte.TryParse(inputString, out byte input);
                if (!isSucceedToParse)
                {
                    InputKeyToClear("잘못된 입력입니다.");
                    continue;
                }

                if (input > 50)
                {
                    InputKeyToClear("잘못된 입력입니다. - 입력값이 50이 넘습니다.");
                    continue;
                }

                bool isSucceedToCount = StairJumpCaseCounter.TryCountJump1Or2StairCase(input, out ulong result);
                if (!isSucceedToCount)
                {
                    InputKeyToClear("잘못된 입력입니다.");
                    continue;
                }
                
                Console.WriteLine($"올라가는 방법의 수 : {result}");
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
