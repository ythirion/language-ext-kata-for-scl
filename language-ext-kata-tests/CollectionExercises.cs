using System;
using language_ext.kata.Persons;
using LanguageExt;
using Xunit;
using static language_ext.kata.Persons.PetType;
using static LanguageExt.Prelude;

namespace language_ext.kata.tests
{
    public class CollectionExercises : PetDomainKata
    {
        [Fact]
        public void GetFirstNamesOfAllPeople()
        {
            // Replace null, with a transformation method on people.
            var firstNames = people.Map(p => p.FirstName);
            var expectedFirstNames = Seq("Mary", "Bob", "Ted", "Jake", "Barry", "Terry", "Harry", "John");

            Assert.Equal(expectedFirstNames, firstNames);
        }

        [Fact]
        public void GetNamesOfMarySmithsPets()
        {
            var person = GetPersonNamed("Mary Smith");

            // Replace null, with a transformation method on people.
            var names = person.Pets.Map(p => p.Name);

            Assert.Equal("Tabby", names.Single());
        }

        [Fact]
        public void GetPeopleWithCats()
        {
            // Replace null, with a positive filtering method on people.
            var peopleWithCats = people.Filter(p => p.HasPetType(CAT));

            Assert.Equal(2, peopleWithCats.Count);
        }

        [Fact]
        public void GetPeopleWithoutCats()
        {
            // Replace null, with a negative filtering method on Seq.
            var peopleWithoutCats = people.Filter(p => !p.HasPetType(CAT));

            Assert.Equal(6, peopleWithoutCats.Count);
        }

        [Fact]
        public void DoAnyPeopleHaveCats()
        {
            //replace null with a Predicate lambda which checks for PetType.CAT
            bool doAnyPeopleHaveCats = people.Find(p => p.HasPetType(CAT)).IsSome;
            Assert.True(doAnyPeopleHaveCats);
        }

        [Fact]
        public void DoAllPeopleHavePets()
        {
            Func<Person, bool> predicate = (p) => p.IsPetPerson();
            // OR use local functions -> static bool predicate(Person p) => p.IsPetPerson();
            // replace with a method call send to this.people that checks if all people have pets
            bool result = people.ForAll(predicate);

            Assert.False(result);
        }

        [Fact]
        public void HowManyPeopleHaveCats()
        {
            // replace 0 with the correct answer
            int count = people.Count(p => p.HasPetType(CAT));
            Assert.Equal(2, count);
        }

        [Fact]
        public void FindMarySmith()
        {
            Person result = GetPersonNamed("Mary Smith");
            Assert.Equal("Mary", result.FirstName);
            Assert.Equal("Smith", result.LastName);
        }

        [Fact]
        public void GetPeopleWithPets()
        {
            // replace with only the pets owners
            var petPeople = people.Filter(p => p.IsPetPerson());
            Assert.Equal(7, petPeople.Count);
        }

        [Fact]
        public void GetAllPetTypesOfAllPeople()
        {
            var petTypes = people.Bind(p => p.GetPetTypes().Keys)
                                 .ToSeq()
                                 .Distinct();

            Assert.Equal(
                    Seq(CAT, DOG, SNAKE, BIRD, TURTLE, HAMSTER),
                    petTypes);
        }

        [Fact]
        public void HowManyPersonHaveCats()
        {
            // use count
            int count = people.Count(p => p.HasPetType(CAT));
            Assert.Equal(2, count);
        }

        [Fact]
        public void TotalPetAge()
        {
            var totalAge = people.Bind(p => p.Pets).Map(pet => pet.Age).Sum();
            Assert.Equal(17L, totalAge);
        }

        [Fact]
        public void PetsNameSorted()
        {
            string sortedPetNames =
                    people.Bind(p => p.Pets)
                          .Map(pet => pet.Name)
                          .OrderBy(s => s)
                          .ToSeq()
                          .ToFullString();

            Assert.Equal("Dolly, Fuzzy, Serpy, Speedy, Spike, Spot, Tabby, Tweety, Wuzzy", sortedPetNames);
        }

        [Fact]
        public void SortByAge()
        {
            // Create a Seq<int> with ascending ordered age values.
            var sortedAgeList =
                    people.Bind(p => p.Pets)
                          .Map(pet => pet.Age)
                          .Distinct()
                          .OrderBy(a => a)
                          .ToSeq();

            Assert.Equal(4, sortedAgeList.Count);
            Assert.Equal(Seq(1, 2, 3, 4), sortedAgeList);
        }

        [Fact]
        public void SortByDescAge()
        {
            // Create a Seq<int> with descending ordered age values.
            var sortedAgeList =
                    people.Bind(p => p.Pets)
                          .Map(pet => pet.Age)
                          .Distinct()
                          .OrderByDescending(a => a)
                          .ToSeq();

            Assert.Equal(4, sortedAgeList.Count);
            Assert.Equal(Seq(4, 3, 2, 1), sortedAgeList);
        }

        [Fact]
        public void Top3OlderPets()
        {
            // Create a Seq<Pet> with the 3 older pets.
            var top3OlderPets =
                    people.Bind(p => p.Pets)
                          .OrderByDescending(pet => pet.Age)
                          .Map(pet => pet.Name)
                          .ToSeq()
                          .Take(3);

            Assert.Equal(3, top3OlderPets.Count);
            Assert.Equal(Seq("Spike", "Dolly", "Tabby"), top3OlderPets);
        }

        [Fact]
        public void GetFirstPersonWithAtLeast2Pets()
        {
            // Find the first person who owns at least 2 pets
            var firstPersonWithAtLeast2Pets = people.Find(person => person.Pets.Count >= 2);

            Assert.True(firstPersonWithAtLeast2Pets.IsSome);
            Assert.Equal("Bob", firstPersonWithAtLeast2Pets.Map(p => p.FirstName));
        }

        [Fact]
        public void IsThereAnyPetOlderThan4()
        {
            // Check whether any exercises older than 4 exists or not
            bool isThereAnyPetOlderThan4 =
                    people.Bind(p => p.Pets)
                            .Find(pet => pet.Age > 4)
                            .IsSome;

            Assert.False(isThereAnyPetOlderThan4);
        }

        [Fact]
        public void IsEveryPetsOlderThan1()
        {
            // Check whether all pets are older than 1 or not
            bool allOlderThan1 =
                    people.Bind(p => p.Pets)
                          .Filter(pet => pet.Age < 1)
                          .IsEmpty;

            Assert.True(allOlderThan1);
        }

        private Seq<string> FilterParksFor(Seq<PetType> petTypes)
        {
            return parks.Filter(park => petTypes.Except(park.AuthorizedPetTypes).ToArr().Count == 0)
                        .Map(park => park.Name);
        }

        [Fact]
        public void GetListOfPossibleParksForAWalkPerPerson()
        {
            // For each person described as "firstName lastName" returns the list of names possible parks to go for a walk
            var possibleParksForAWalkPerPerson =
                people.ToDictionary(
                    p => $"{p.FirstName} {p.LastName}",
                    p => FilterParksFor(p.Pets.Map(pet => pet.Type)));

            Assert.Equal(Seq("Jurassic", "Central", "Hippy"), possibleParksForAWalkPerPerson["John Doe"]);
            Assert.Equal(Seq("Jurassic", "Hippy"), possibleParksForAWalkPerPerson["Jake Snake"]);
        }
    }
}
