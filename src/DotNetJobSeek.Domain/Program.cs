using System;
using DotNetJobSeek.Domain.ValueObjects;

namespace DotNetJobSeek.Domain
{
    class Program
    {
        static void Main(string[] args)
        {
            Tag t1 = new Tag { Id = 1, Name = "Jerry", Version = 0};
            Console.WriteLine("Hello World!");
        }
    }
}
