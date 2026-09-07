namespace _260904_Algorithm
{
    public class Program
    {
        static void Main(string[] args)
        {
            OpenPrintPrimeListMenu();
        }

        private static void OpenPrintPrimeListMenu()
        {
            List<short> results = new(10000);

            while (true)
            {
                Console.Write("""
                    소수 목록 구하기 문제 >
                    
                    1부터 입력 값까지의 소수의 개수와 소수의 목록을 출력합니다.
                    2부터 10000까지의 수를 입력해주세요.
                    Q를 입력해 종료할 수 있습니다.
                    
                    구하려는 소수의 최대값: 
                    """);

                string? inputString = Console.ReadLine();

                if (inputString == "q" || inputString == "Q")
                    return;

                if (string.IsNullOrWhiteSpace(inputString))
                {
                    InputKeyToClear("잘못된 입력입니다.");
                    continue;
                }

                bool isSucceedToParse = short.TryParse(inputString, out short input);
                if (!isSucceedToParse)
                {
                    InputKeyToClear("잘못된 입력입니다.");
                    continue;
                }

                bool isInRange = 2 <= input && input <= 10000;
                if (!isInRange)
                {
                    InputKeyToClear("범위 내로 입력해주세요.");
                    continue;
                }

                bool isSucceedToFind = PrimeNumberFinder.TryGetPrimeNumbersUnderNonAlloc(input, results);
                if (!isSucceedToFind)
                {
                    InputKeyToClear("잘못된 입력입니다.");
                    continue;
                }

                Console.WriteLine();

                foreach (short result in results)
                {
                    Console.Write($"{result} ");
                }

                Console.WriteLine("\n");
                Console.WriteLine($"길이 : {results.Count}");

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
