﻿using Bridge.QUnit;
using ClientTestLibrary.Utilities;
using System.Linq;

namespace ClientTestLibrary.Linq
{
    class TestLinqProjectionOperators
    {
        //public class A
        //{
        //    public string Name { get; set; }
        //}

        public static void Test(Assert assert)
        {
            assert.Expect(8);

            // TEST
            var numbers = new[] { 1, 3, 5, 7 };
            var numberPlusOne = (from n in numbers select n + 1).ToArray();
            assert.DeepEqual(numberPlusOne, new[] { 2, 4, 6, 8 }, "A sequence of ints one higher than the numbers[]");

            // TEST
            var persons = Person.GetPersons();
            var names = (from p in persons select p.Name).ToArray();
            assert.DeepEqual(names, new[] { "Frank", "Zeppa", "John", "Billy", "Dora", "Ian", "Mary", "Nemo" }, "Selects names as instance field");

            // TEST
            string[] strings = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            
            var textNumbers = (from n in numbers select strings[n]).ToArray();
            assert.DeepEqual(textNumbers, new[] { "one", "three", "five", "seven" }, "Selects names as items of another array");

            // TEST
            var anonimNames = (from p in persons select new { Name = p.Name }).ToArray();
            
            object[] anonimNamesToCompare = {
                                                new { Name = "Frank" }, new { Name = "Zeppa" }, new { Name = "John" },
                                                new { Name = "Billy" }, new { Name = "Dora" }, new { Name = "Ian" },
                                                new { Name = "Mary" }, new { Name = "Nemo" } };
            
            assert.DeepEqual(anonimNames, anonimNamesToCompare, "Selects names as an anonymous type");

            // TEST
            numbers = new[] { 0, 1, 3, 3 };
            
            var numberssInPlace = numbers
                                    .Select((n, index) => new
                                        {
                                            Number = n,
                                            IsIndex = n == index
                                        })
                                    .ToArray();

            object[] anonimNumbersToCompare = { new { Number = 0, IsIndex = true },
                                                  new { Number = 1, IsIndex = true },
                                                  new { Number = 3, IsIndex = false },
                                                  new { Number = 3, IsIndex = true }
                                              };
            
            assert.DeepEqual(numberssInPlace, anonimNumbersToCompare, "Selects numbers as an anonymous type");

            // TEST
            var numbersA = new[] { 1, 5, 2 };
            var numbersB = new[] { 3, 4, 2 };
            var simplePairs =
                (from a in numbersA
                 from b in numbersB
                 where a < b
                 select new { A = a, B = b }
                ).ToArray();
            
            object[] expectedSimplePairs = { new { A = 1, B = 3 }, new { A = 1, B = 4 }, new { A = 1, B = 2 }, new { A = 2, B = 3 }, new { A = 2, B = 4 } };
            
            assert.DeepEqual(simplePairs, expectedSimplePairs, "Join two numeric arrays with one where clause");

            // TEST
            numbersA = new[] { 1, 5, 2, 4, 3 };
            numbersB = new[] { 3, 4, 2, 5, 1 };

            var pairs =
                (from a in numbersA
                where a > 1
                from b in numbersB
                where b < 4 && a > b
                select new { Sum = a + b }
                ).ToArray();

            object[] expectedPairs = { new { Sum = 8}, new { Sum = 7}, new { Sum = 6}, new { Sum = 3},
                                     new { Sum = 7}, new { Sum = 6}, new { Sum = 5},
                                     new { Sum = 5}, new { Sum = 4},};
            
            assert.DeepEqual(pairs, expectedPairs, "Join two numeric arrays with two where clauses");

            // TEST
            numbersA = new[] { 1, 5, 2, 4, 3 };
            numbersB = new[] { 3, 4, 2, 5, 1 };

            var manyNumbers = numbersA
                .SelectMany((a, aIndex) => numbersB.Where(b => a == b && b > aIndex).Select(b => new { A = a, B = b, I = aIndex }))
                .ToArray();

            object[] expectedManyNumbers = { new { A = 1, B = 1, I = 0 },
                                           new { A = 5, B = 5, I = 1 },
                                           new { A = 4, B = 4, I = 3 }};

            assert.DeepEqual(manyNumbers, expectedManyNumbers, "SelectMany() two number arrays");
        }
    }
}
