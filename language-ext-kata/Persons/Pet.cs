namespace language_ext.kata.Persons
{
    public class Pet
    {
        public PetType Type { get; }
        public string Name { get; }
        public int Age { get; }

        public Pet(PetType type, string name, int age)
        {
            Type = type;
            Name = name;
            Age = age;
        }
    }
}