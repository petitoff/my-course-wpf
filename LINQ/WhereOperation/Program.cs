﻿using WhereOperation;


var people = GetPeople();
var adults = people.Where(p => p.Age > 18).ToList();

adults.ForEach(p => Console.WriteLine(new { name = p.Name, age = p.Age }));


Console.ReadKey();
return;

List<Person> GetPeople()
{
    return new List<Person>
    {
        new()
        {
            Name = "Tomek",
            Age = 10
        },
        new()
        {
            Name = "Adrian",
            Age = 20
        }
    };
}