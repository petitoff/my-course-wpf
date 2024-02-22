using WhereOperation;

List<Person> people = GetPeople();
var adults = people
    .Where(p => p.Age > 1)
    .OrderBy(p => p.Age * -1)
    .ToList();

adults.ForEach(p =>
{
    Console.WriteLine(new { name = p.Name, age = p.Age });

});

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