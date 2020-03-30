using System;
using language_ext.kata.Persons;
using LanguageExt;
using static LanguageExt.Prelude;

namespace language_ext.kata.tests
{
    public abstract class PetDomainKata
    {
        protected Seq<Person> people;
        protected Seq<Park> parks;

        public PetDomainKata()
        {
            people = Seq(
                new Person("Mary", "Smith").AddPet(PetType.CAT, "Tabby", 2),
                new Person("Bob", "Smith")
                        .AddPet(PetType.CAT, "Dolly", 3)
                        .AddPet(PetType.DOG, "Spot", 2),
                new Person("Ted", "Smith").AddPet(PetType.DOG, "Spike", 4),
                new Person("Jake", "Snake").AddPet(PetType.SNAKE, "Serpy", 1),
                new Person("Barry", "Bird").AddPet(PetType.BIRD, "Tweety", 2),
                new Person("Terry", "Turtle").AddPet(PetType.TURTLE, "Speedy", 1),
                new Person("Harry", "Hamster")
                        .AddPet(PetType.HAMSTER, "Fuzzy", 1)
                        .AddPet(PetType.HAMSTER, "Wuzzy", 1),
                new Person("John", "Doe"));

            parks = Seq(
                new Park("Jurassic")
                        .AddAuthorizedPetType(PetType.BIRD)
                        .AddAuthorizedPetType(PetType.SNAKE)
                        .AddAuthorizedPetType(PetType.TURTLE),
                new Park("Central")
                        .AddAuthorizedPetType(PetType.BIRD)
                        .AddAuthorizedPetType(PetType.CAT)
                        .AddAuthorizedPetType(PetType.DOG),
                new Park("Hippy")
                        .AddAuthorizedPetType(PetType.BIRD)
                        .AddAuthorizedPetType(PetType.CAT)
                        .AddAuthorizedPetType(PetType.DOG)
                        .AddAuthorizedPetType(PetType.TURTLE)
                        .AddAuthorizedPetType(PetType.HAMSTER)
                        .AddAuthorizedPetType(PetType.SNAKE));
        }

        public Person GetPersonNamed(string fullName) => people.Find(p => p.Named(fullName))
            .IfNone(() => throw new ArgumentException("Can't find person named: " + fullName));
    }
}