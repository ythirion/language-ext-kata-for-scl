using System;
using System.Collections.Generic;
using language_ext.kata.Persons;
using LanguageExt;
using LanguageExt.Common;
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
            // Replace it, with a transformation method on people.
            Seq<string> firstNames = Seq<string>();
            Seq<string> expectedFirstNames = Seq("Mary", "Bob", "Ted", "Jake", "Barry", "Terry", "Harry", "John");

            Assert.Equal(expectedFirstNames, firstNames);
        }

        [Fact]
        public void GetNamesOfMarySmithsPets()
        {
            var person = GetPersonNamed("Mary Smith");

            // Replace it, with a transformation method on people.
            Seq<string> names = Seq<string>();

            Assert.Equal("Tabby", names.Single());
        }

        [Fact]
        public void GetPeopleWithCats()
        {
            // Replace it, with a positive filtering method on people.
            Seq<string> peopleWithCats = Seq<string>();

            Assert.Equal(2, peopleWithCats.Count);
        }

        [Fact]
        public void GetPeopleWithoutCats()
        {
            // Replace it, with a negative filtering method on Seq.
            Seq<string> peopleWithoutCats = Seq<string>();

            Assert.Equal(6, peopleWithoutCats.Count);
        }

        [Fact]
        public void DoAnyPeopleHaveCats()
        {
            //replace null with a Predicate lambda which checks for PetType.CAT
            var doAnyPeopleHaveCats = false;
            Assert.True(doAnyPeopleHaveCats);
        }

        [Fact]
        public void DoAllPeopleHavePets()
        {
            Func<Person, bool> predicate = p => true;
            // OR use local functions -> static bool predicate(Person p) => p.IsPetPerson();
            // replace with a method call send to this.people that checks if all people have pets
            var result = people.ForAll(predicate);

            Assert.False(result);
        }

        [Fact]
        public void HowManyPeopleHaveCats()
        {
            // replace 0 with the correct answer
            var count = 0;
            Assert.Equal(2, count);
        }

        [Fact]
        public void FindMarySmith()
        {
            Person result = null;
            Assert.Equal("Mary", result.FirstName);
            Assert.Equal("Smith", result.LastName);
        }

        [Fact]
        public void GetPeopleWithPets()
        {
            // replace with only the pets owners
            Seq<Person> petPeople = Seq<Person>();
            Assert.Equal(7, petPeople.Count);
        }

        [Fact]
        public void GetAllPetTypesOfAllPeople()
        {
            Seq<PetType> petTypes = Seq<PetType>();

            Assert.Equal(Seq(Cat, Dog, Snake, Bird, Turtle, Hamster), petTypes);
        }

        [Fact]
        public void HowManyPersonHaveCats()
        {
            // use count
            var count = 0;
            Assert.Equal(2, count);
        }

        [Fact]
        public void TotalPetAge()
        {
            var totalAge = 0L;
            Assert.Equal(17L, totalAge);
        }

        [Fact]
        public void PetsNameSorted()
        {
            string sortedPetNames = null;

            Assert.Equal("Dolly, Fuzzy, Serpy, Speedy, Spike, Spot, Tabby, Tweety, Wuzzy", sortedPetNames);
        }

        [Fact]
        public void SortByAge()
        {
            // Create a Seq<int> with ascending ordered age values.
            Seq<int> sortedAgeList = Seq<int>();

            Assert.Equal(4, sortedAgeList.Count);
            Assert.Equal(Seq(1, 2, 3, 4), sortedAgeList);
        }

        [Fact]
        public void SortByDescAge()
        {
            // Create a Seq<int> with descending ordered age values.
            Seq<int> sortedAgeList = Seq<int>();

            Assert.Equal(4, sortedAgeList.Count);
            Assert.Equal(Seq(4, 3, 2, 1), sortedAgeList);
        }

        [Fact]
        public void Top3OlderPets()
        {
            // Create a Seq<string> with the 3 older pets.
            Seq<string> top3OlderPets = Seq<string>();

            Assert.Equal(3, top3OlderPets.Count);
            Assert.Equal(Seq("Spike", "Dolly", "Tabby"), top3OlderPets);
        }

        [Fact]
        public void GetFirstPersonWithAtLeast2Pets()
        {
            // Find the first person who owns at least 2 pets
            Option<Person> firstPersonWithAtLeast2Pets = Option<Person>.None;

            Assert.True(firstPersonWithAtLeast2Pets.IsSome);
            Assert.Equal("Bob", firstPersonWithAtLeast2Pets.Map(p => p.FirstName));
        }

        [Fact]
        public void IsThereAnyPetOlderThan4()
        {
            // Check whether any exercises older than 4 exists or not
            var isThereAnyPetOlderThan4 = true;

            Assert.False(isThereAnyPetOlderThan4);
        }

        [Fact]
        public void IsEveryPetsOlderThan1()
        {
            // Check whether all pets are older than 1 or not
            var allOlderThan1 = false;

            Assert.True(allOlderThan1);
        }

        [Fact]
        public void GetListOfPossibleParksForAWalkPerPerson()
        {
            // For each person described as "firstName lastName" returns the list of names possible parks to go for a walk
            Dictionary<string, Seq<string>> possibleParksForAWalkPerPerson = null;

            Assert.Equal(Seq("Jurassic", "Central", "Hippy"), possibleParksForAWalkPerPerson["John Doe"]);
            Assert.Equal(Seq("Jurassic", "Hippy"), possibleParksForAWalkPerPerson["Jake Snake"]);
        }
    }
}
