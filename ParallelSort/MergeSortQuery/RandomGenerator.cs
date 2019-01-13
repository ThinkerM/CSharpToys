using System;
using System.Text;
using LibraryModel;

namespace MergeSortQuery
{
    internal class RandomGenerator
    {
        public int BookCount { get; set; }
        public int MaxCopyCount { get; set; }
        public int LoanCount { get; set; }
        public int ClientCount { get; set; }
        public int RandomSeed { get; set; }

        private Random randomInstance = null;

        private Random R => randomInstance ?? (randomInstance = new Random(RandomSeed));

        private string GetRandomNumberString(int numberCount)
        {
            var sb = new StringBuilder(numberCount);

            for (int i = 0; i < numberCount; i++)
            {
                sb.Append((char) ('0' + R.Next(10)));
            }

            return sb.ToString();
        }

        private string GetRandomEnglishUpperString(int letterCount)
        {
            var sb = new StringBuilder(letterCount);

            for (int i = 0; i < letterCount; i++)
            {
                sb.Append((char) ('A' + R.Next('Z' - 'A' + 1)));
            }

            return sb.ToString();
        }

        private string GetRandomName(int length)
        {
            var sb = new StringBuilder(length);

            int type0LastIdx = -1;
            int type1LastIdx = -1;

            sb.Append((char) ('A' + R.Next('Z' - 'A' + 1)));
            for (int i = 1; i < length; i++)
            {
                var type = R.Next(6);
                switch (type)
                {
                    case 0 when i != type0LastIdx:
                        sb.Append((char) (0x300 + R.Next(0x0A)));
                        type0LastIdx = i;
                        break;
                    case 1 when i != type1LastIdx:
                        sb.Append((char) (0x326 + R.Next(0x32B - 0x326 + 1)));
                        type1LastIdx = i;
                        break;
                    default: sb.Append((char) ('a' + R.Next('z' - 'a' + 1)));
                        break;
                }
            }

            return sb.ToString();
        }

        public void FillLibrary(Library library)
        {
            for (int i = 0; i < BookCount; i++)
            {
                var b = new Book
                {
                    Author = GetRandomName(5) + " " + GetRandomName(10),
                    Title = GetRandomName(30)
                };
                int l1 = R.Next(7) + 2;
                int l2 = R.Next(l1 - 1) + 1;
                b.Isbn = GetRandomNumberString(l2) + "-" + GetRandomNumberString(9 - l1) + "-" + GetRandomNumberString(l1 - l2) + "-" + GetRandomNumberString(1);
                b.DatePublished = new DateTime(1950 + R.Next(50), 1, 1);
                b.Shelf = GetRandomNumberString(2) + GetRandomEnglishUpperString(1);

                int copies = R.Next(MaxCopyCount) + 1;
                for (int j = 0; j < copies; j++)
                {
                    var c = new Copy
                    {
                        Book = b,
                        Id = GetRandomNumberString(2) + GetRandomEnglishUpperString(2) + GetRandomNumberString(4),
                        State = CopyState.InShelf
                    };

                    b.Copies.Add(c);
                    library.Copies.Add(c);
                }

                library.Books.Add(b);
            }

            for (int i = 0; i < ClientCount; i++)
            {
                var c = new Client
                {
                    FirstName = GetRandomName(7),
                    LastName = GetRandomName(12)
                };
                library.Clients.Add(c);
            }

            var today = DateTime.Today;
            for (int i = 0; i < LoanCount; i++)
            {
                var copy = library.Copies[R.Next(library.Copies.Count)];
                if (copy.State == CopyState.OnLoan) continue;

                var l = new Loan
                {
                    Client = library.Clients[R.Next(ClientCount)],
                    Copy = copy,
                    DueDate = today.AddDays(R.Next(31))
                };

                l.Copy.OnLoan = l;
                l.Copy.State = CopyState.OnLoan;

                library.Loans.Add(l);
            }
        }
    }
}