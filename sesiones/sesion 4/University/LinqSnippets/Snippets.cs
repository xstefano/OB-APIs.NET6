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

        // Paging with Skip & Take: 

        static public IEnumerable<T> GetPage<T>(IEnumerable<T> collection, int pageNumber, int resultPerPage)
        {
            int startIndex = (pageNumber - 1) * resultPerPage;
            return collection.Skip(startIndex).Take(resultPerPage);
        }

        // Variables:

        static public void LinqVariables()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 19 };

            var aboveAverage = from number in numbers
                               let average = numbers.Average()
                               let nSquared = Math.Pow(number, 2)
                               where nSquared > average
                               select number;

            Console.WriteLine("Average: {0}", numbers.Average());

            foreach (int number in aboveAverage)
            {
                Console.WriteLine("Query: Number: {0} Square: {1} ", number, Math.Pow(number, 2));
            }
        }

        // Zip: 
        static public void ZipLinq()
        {
            int[] numbers = { 1, 2, 3, 4, 5 };

            string[] stringNumbers = { "one", "two", "three", "four", "five" };

            IEnumerable<string> zipNumbers = numbers.Zip(stringNumbers, (number, word) => number + "= " + word);

            // {"1=one", "2=two", ...}:
        }

        // Repeat & Range:
        static public void repeatRangeLinq()
        {
            // Generate collection from 1 - 1000:
            IEnumerable<int> first1000 = Enumerable.Range(0, 1000);

            // Repeat a value N times:
            IEnumerable<string> fiveXs = Enumerable.Repeat("X", 5); // {"X", "X", "X", "X", "X"}
        }

        static public void studentsLinq()
        {
            var classRoom = new[]
            {
                new Student
                {
                    Id = 1,
                    Name = "Andres",
                    Grade = 90,
                    Certified = true,
                },
                new Student
                {
                    Id = 2,
                    Name = "Juan",
                    Grade = 50,
                    Certified = false,
                },
                new Student
                {
                    Id = 3,
                    Name = "Ana",
                    Grade = 96,
                    Certified = true,
                },
                new Student
                {
                    Id = 4,
                    Name = "Alvaro",
                    Grade = 10,
                    Certified = false,
                },
                new Student
                {
                    Id = 5,
                    Name = "Pedro",
                    Grade = 50,
                    Certified = true,
                },

            };

            var certifiedStudents = from student in classRoom
                                    where student.Certified
                                    select student;

            var notCertifiedStudents = from student in classRoom
                                       where student.Certified == true
                                       select student;

            var appovedStudentsNames = from student in classRoom
                                       where student.Grade >= 50 && student.Certified == true
                                       select student.Name;
        }

        // All:
        static public void AllLinq()
        {
            var numbers = new List<int>() { 1, 2, 3, 4, 5 };

            bool allAreSmallerThan10 = numbers.All(x => x < 10); // true

            bool allAreBiggerOrEqualThan2 = numbers.All(x => x >= 2); // false

            var emptyList = new List<int>();

            bool allNumbersAreGreaterThan0 = numbers.All(x => x >= 0); // true
        }

        // Agregate:
        static public void aggregateQueries()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            // Sum all numbers:
            int sum = numbers.Aggregate((prevSum, current) => prevSum + current);

            // 0, 1 => 1
            // 0, 2 => 3
            // 3, 4 => 7
            // etc.

            string[] words = { "hello", "my", "name", "is", "Andres" }; // hello, my name is Martin
            string greeting = words.Aggregate((prevGreeting, current) => prevGreeting + current);


            // "", "hello, " => hello
            // "hello, ", "my" => hello, my
            // "hello, my", "name" => hello, my name
            // etc.
        }

        // Distinct:
        static public void distinctValues()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 5, 4, 3, 2, 1 };
            IEnumerable<int> distintctValues = numbers.Distinct();
        }

        // Group by:
        static public void groupByExamples()
        {
            List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            // Obtain only even numbers and generate two groups:
            var grouped = numbers.GroupBy(x => x % 2 == 0);

            // We will have two groups:
            // 1. The group that doesnt fit the condition:
            // 2. The group that fits the condition:

            foreach (var group in grouped)
            {
                foreach(var value in group)
                {
                    Console.WriteLine(value); // 1, 3, 5, 7, 9 ... 2, 4, 6, 8 (first the odds and then the even)
                }
            }


            // Another Example:
            var classRoom = new[]
{
                new Student
                {
                    Id = 1,
                    Name = "Andres",
                    Grade = 90,
                    Certified = true,
                },
                new Student
                {
                    Id = 2,
                    Name = "Juan",
                    Grade = 50,
                    Certified = false,
                },
                new Student
                {
                    Id = 3,
                    Name = "Ana",
                    Grade = 96,
                    Certified = true,
                },
                new Student
                {
                    Id = 4,
                    Name = "Alvaro",
                    Grade = 10,
                    Certified = false,
                },
                new Student
                {
                    Id = 5,
                    Name = "Pedro",
                    Grade = 50,
                    Certified = true,
                },
            };

            var certifiedQuery = classRoom.GroupBy(student => student.Certified);

            // We obtain two groups:
            // 1. Not certified Students:
            // 2. Certified Students:

            foreach(var group in certifiedQuery)
            {
                Console.WriteLine("------ {0} -----", group.Key);
                foreach(var student in group)
                {
                    Console.WriteLine(student.Name);
                }
            }
        }

        static public void relantionsLin()
        {
            List<Post> posts = new List<Post>()
            {
                new Post()
                {
                    Id = 1,
                    Title = "My first post",
                    Content = "My first content",
                    Created = DateTime.Now,
                    Comments = new List<Comment>()
                    {
                        new Comment()
                        {
                            Id = 1,
                            Created = DateTime.Now,
                            Title = "My frist comment",
                            Content = "My content"
                        },
                        new Comment()
                        {
                            Id = 2,
                            Created = DateTime.Now,
                            Title = "My second comment",
                            Content = "My other content"
                        }
                    }
                },
                new Post()
                {
                    Id = 2,
                    Title = "My second post",
                    Content = "My second content",
                    Created = DateTime.Now,
                    Comments = new List<Comment>()
                    {
                        new Comment()
                        {
                            Id = 3,
                            Created = DateTime.Now,
                            Title = "My other comment",
                            Content = "My new content"
                        },
                        new Comment()
                        {
                            Id = 4,
                            Created = DateTime.Now,
                            Title = "My other new comment",
                            Content = "My new content"
                        }
                    }
                }
            };


            var commentsWithContent = posts.SelectMany(
                    post => post.Comments, 
                        (post, comment) => new { PostId = post.Id, CommentContent = comment.Content });
        }
    }
}