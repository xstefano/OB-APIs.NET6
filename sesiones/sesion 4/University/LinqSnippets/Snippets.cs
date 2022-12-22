using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace LinqSnippets
{
    public class Snippets
    {
        static public void BasicLinq()
        {

            string[] cars =
            {
                "VW Golf",
                "VW California",
                "Audi A3",
                "Audi A5",
                "Fiat Punto",
                "Seat Ibiza",
                "Seat León"
            };


            // 1. SELECT * FROM cars:

            var carList = from car in cars select car;
            carList.ToList().ForEach(car => { });


            // 2. SELECT WHERE car IS Audi:

            var audiList = from car in cars where car.Contains("Audi") select car;
            audiList.ToList().ForEach(audi => { });
        }

        // Numbers Examples:
        static public void LinqNumbers()
        {
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };


            var processedNumberList = numbers
                                            .Select(num => num * 3)
                                            .Where(num => num != 9)
                                            .OrderBy(num => num);
        }


        static public void SearchExamples()
        {
            List<string> textList = new List<string>
            {
                "a",
                "bx",
                "c",
                "d",
                "e",
                "cj",
                "f",
                "c"
            };

            // 1. First of all elements:
            var first = textList.First();

            // 2. First element that is "c":
            var cText = textList.First(text => text.Equals("c"));

            // 3. First element that contains "j":
            var jText = textList.First(text => text.Contains("j"));

            // 4. First element that contains "z" or default:
            var firstOrDefaulText = textList.FirstOrDefault(text => text.Contains("z"));

            // 5. First element that contains "z" or default:
            var lastOrDefaulText = textList.LastOrDefault(text => text.Contains("z"));

            // 6. Single Values:
            var uniqueTexts = textList.Single();
            var uniqueDefaultTexts = textList.SingleOrDefault();

            // 7. Obtain {4, 8}:
            int[] evenNumbers = { 0, 2, 4, 6, 8 };
            int[] otherEvenNumbers = { 0, 2, 6 };

            var myEvenNumbers = evenNumbers.Except(otherEvenNumbers);
        }

        static public void MultipleSelects()
        {
            // SELECT MANY:
            string[] myOpinions =
            {
                "Opinión 1, text 1",
                "Opinión 2, text 2",
                "Opinión 3, text 3"
            };

            var myOpinonSelection = myOpinions.SelectMany(opinion => opinion.Split(","));
            var enterprises = new[]
            {
                new Enterprise()
                {
                    Id = 1,
                    Name = "Enterprise 1",
                    Employees = new[]
                    {
                        new Employee()
                        {
                            Id = 1,
                            Name = "Andres",
                            Email = "xcloudsgls@gmail.com",
                            Salary = 3000
                        },
                        new Employee()
                        {
                            Id = 2,
                            Name = "Pepe",
                            Email = "xpepe@gmail.com",
                            Salary = 2000
                        },
                        new Employee()
                        {
                            Id = 3,
                            Name = "Juanjo",
                            Email = "xjuanjo@gmail.com",
                            Salary = 1000
                        }
                    }
                },
                new Enterprise()
                {
                    Id = 2,
                    Name = "Enterprise 2",
                    Employees = new[]
                    {
                        new Employee()
                        {
                            Id = 4,
                            Name = "Ana",
                            Email = "xana@gmail.com",
                            Salary = 3000
                        },
                        new Employee()
                        {
                            Id = 5,
                            Name = "Maria",
                            Email = "xmaria@gmail.com",
                            Salary = 1500
                        },
                        new Employee()
                        {
                            Id = 6,
                            Name = "Samuel",
                            Email = "xsamuel@gmail.com",
                            Salary = 4000
                        }
                    }
                }
            };

            // Obtain all employees of all Enterprises:
            var employeeList = enterprises.SelectMany(enterprise => enterprise.Employees);

            // Knows if ana list is empty:
            bool hasEnterprises = enterprises.Any();

            bool hasEmployees = enterprises.Any(enterprise => enterprise.Employees.Any());

            // All enterprises at least has an employee with more than 1000€ of salary:
            bool hasEmployeeWithSalaryMorethan1000 =
                enterprises.Any(enterprise =>
                    enterprise.Employees.Any(employee => employee.Salary >= 1000));
        }

        static public void linqCollections()
        {
            var firstList = new List<string>() { "a", "b", "c" };
            var secondList = new List<string>() { "a", "c", "d" };

            // Inner Join:
            var commonResult = from element in firstList
                               join secondElement in secondList
                               on element equals secondElement
                               select new { element, secondElement };

            var commonResult2 = firstList.Join(
                secondList,
                element => element,
                secondElement => secondElement,
                (element, secondElement) => new { element, secondElement }
              );

            // Outer Join - Left:
            var leftOuterJoin = from element in firstList
                                join secondElement in secondList
                                on element equals secondElement
                                into temporalList
                                from temporalElement in temporalList.DefaultIfEmpty()
                                where element != temporalElement
                                select new { Element = element };

            var leftOuterJoin2 = from element in firstList
                                 from secondElement in secondList.Where(s => s == element).DefaultIfEmpty()
                                 select new { Element = element, SecondElement = secondElement };



            // Outer Join - Right:
            var rightOuterJoin = from secondElement in secondList
                                 join element in firstList
                                 on secondElement equals element
                                 into temporalList
                                 from temporalElement in temporalList.DefaultIfEmpty()
                                 where secondElement != temporalElement
                                 select new { Element = secondElement };

            // Union:
            var unionList = leftOuterJoin.Union(rightOuterJoin);
        }

        static public void SkipTakeLinq()
        {
            var myList = new[]
            {
                1,2,3,4,5,6,7,8,9,10
            };

            // Skip:

            var skipTwoFirstValues = myList.Skip(2);            // { 3, 4, 5, 6, 7, 8, 9, 10}
            var skipLastTwoValues = myList.SkipLast(2);         // { 1, 2, 3, 4, 5, 6, 7, 8 }
            var skipWhileSmallerThan4 = myList.SkipWhile(num => num < 4);   // { 4, 5, 6, 7, 8}

            // Take:
            var takeFirstTwoValues = myList.Take(2);            // { 1, 2 }
            var takeLastTwoValues = myList.TakeLast(2);         // { 9, 10}
            var takeWhileSmallerThan4 = myList.TakeWhile(num => num < 4);   // { 1, 2, 3}
        }
    }
}