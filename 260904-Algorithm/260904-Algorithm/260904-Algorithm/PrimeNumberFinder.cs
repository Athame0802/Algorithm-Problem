namespace _260904_Algorithm
{
    public static class PrimeNumberFinder
    {
        private const short MAX_FINDING_RANGE = 10000;
        private static List<short> primes = new(MAX_FINDING_RANGE / 2);

        static PrimeNumberFinder()
        {
            FindAllPrimeNumbers();
        }

        /*
         * 선형 에라토스테네스의 체 
         * - J = 어느 수의 최소 소인수, P = 임의의 소수, A = (J * P).
         * - P가 J를 넘어가면 P는 A의 최소 소인수가 아니다.
         *      - J * P가 A -> J와 P는 모두 A의 인자 -> 결국 J가 A의 최소 소인수
         * 
         * 선형 에라토스테네스의 체 특징 : 시간 복잡도가 O(N)이다. (에라토스테네스의 체에서 중복되어 발생하는 연산을 줄였다.)
         * 
         *  J 배열은 leastPrimeFactors
         *  P는 List로 관리
         */

        private static void FindAllPrimeNumbers()
        {
            short[] leastPrimeFactors = new short[MAX_FINDING_RANGE + 1];

            for (short i = 2; i <= MAX_FINDING_RANGE; i++)
            {
                bool isPrimeNumber = leastPrimeFactors[i] == 0;
                if (isPrimeNumber)
                {
                    leastPrimeFactors[i] = i;
                    primes.Add(i);
                }

                foreach (short prime in primes)
                {
                    // 현재 소수가 최소 소인수를 넘어가거나 | 접근하려는 수가 최대 인덱스를 넘어갈 때
                    bool shouldBreak = prime > leastPrimeFactors[i] || prime * i > MAX_FINDING_RANGE;
                    if (shouldBreak)
                        break;

                    leastPrimeFactors[prime * i] = prime;
                }
            }
        }

        // 잦은 할당을 피하기 위해(재사용을 위해) results를 받아 clear한 뒤 다시 Add
        public static bool TryGetPrimeNumbersUnderNonAlloc(short n, List<short> results)
        {
            bool isNInRange = 2 <= n && n <= MAX_FINDING_RANGE;

            if (!isNInRange)
                return false;

            results.Clear();

            foreach (short prime in primes)
            {
                if (prime > n)
                    break;

                results.Add(prime);
            }

            return true;
        }
    }
}
