using iCopyKarta.Contracts;
using iCopyKarta.Logic;
using Moq;

namespace TestiCopyKarta
{
    public class Tests
    {
        private Mock<ISource> _source;
        private Mock<IDestination> _destination;
        private char _returnCharValue;
        private readonly List<char> _returnCharArray = new();

        [OneTimeSetUp]
        public void Setup()
        {
            _source = new Mock<ISource>(MockBehavior.Strict);
            _destination = new Mock<IDestination>(MockBehavior.Strict);
        }

        private static char[] RandomString(int size = 1)
        {
            var returnRandomString = new char[size];
            for (var i = 0; i < size; i++)
            {

                returnRandomString[i] = (char)('a' + (new Random().Next(0, 26)));
            }
            return returnRandomString;
        }

        private void SetupSingleCharacterMoc(char randomCharacter)
        {
            _returnCharValue = char.MaxValue;
            _source.Setup(x => x.ReadChar()).Returns(randomCharacter);

            _destination.Setup(mr => mr.WriteChar(It.IsAny<char>()))
                .Callback((char character) => { _returnCharValue = character; }).Verifiable();
        }

        private void SetupArrayMoc(char[] randomCharacters)
        {
            _returnCharArray.Clear();
            _source.Setup(x => x.ReadChars(It.IsAny<int>())).Returns(randomCharacters);

            _destination.Setup(mr => mr.WriteChars(It.IsAny<char[]>()))
                .Callback((char[] character) =>
                {
                    _returnCharArray.AddRange(character);
                }).Verifiable();
        }

        private char[] GenerateAndCopyARandomCharacterSequence(int size)
        {
            //Setup
            var randomCharacter = RandomString(size);
            SetupArrayMoc(randomCharacter);

            //Act
            var copy = new Copier(_source.Object, _destination.Object);
            copy.CopyCharacters(randomCharacter.Length);
            return randomCharacter;
        }

        [Test]
        public void Given_Random_Character_Copy_Source_To_Destination_Return_Copy_Of_Source()
        {
            //Setup
            var randomCharacter = RandomString()[0];

            SetupSingleCharacterMoc(randomCharacter);

            //Act
            var copy = new Copier(_source.Object, _destination.Object);
            copy.Copy();

            // Assert
            Assert.That(_returnCharValue, Is.EqualTo(randomCharacter));
        }

        [Test]
        public void Given_Source_With_New_Line_Destination_Should_Not_Return_A_Value()
        {
            //Setup
            const char testCaseCharacter = '\n';

            SetupSingleCharacterMoc(testCaseCharacter);

            //Act
            var copy = new Copier(_source.Object, _destination.Object);
            copy.Copy();

            //Assert
            Assert.That(_returnCharValue, Is.EqualTo(char.MaxValue));
        }

        [Test]
        public void Given_Random_Character_Array_Of_Length_5_Should_Return_Same_Array_of_Length_5()
        {
            //Setup
            //Act
            var randomCharacter = GenerateAndCopyARandomCharacterSequence(5);

            //Assert
            Assert.That(_returnCharArray, Is.EqualTo(randomCharacter));
        }

        [Test]
        public void Given_Random_Character_Array_Of_Length_10_Should_Return_Array_Of_Length_10()
        {
            //Setup
            //Act
            var randomCharacter = GenerateAndCopyARandomCharacterSequence(10);

            //Assert
            Assert.That(_returnCharArray, Is.EqualTo(randomCharacter));
        }

        [Test] 
        public void Given_An_Empty_Character_Array_Return_Empty_Character_Array()
        {
            //Setup
            var randomCharacter = Array.Empty<char>();
            var testCase = Array.Empty<char>(); 

            SetupArrayMoc(randomCharacter);

            //Act
            var copy = new Copier(_source.Object, _destination.Object);
            copy.CopyCharacters(randomCharacter.Length);

            //Assert
            Assert.That(testCase, Is.EqualTo(_returnCharArray.ToArray()));
        }

        [Test]
        public void Given_Character_Array_Of_Length_5_With_New_Line_Should_Return_Array_Of_Length_3_Without_New_Line()
        {
            //Setup
            var randomCharacter = new[] { 'a', 'e', 'i', '\n', 'u' };
            var testCase = new[] { 'a', 'e', 'i' };

            SetupArrayMoc(randomCharacter);

            //Act
            var copy = new Copier(_source.Object, _destination.Object);
            copy.CopyCharacters(randomCharacter.Length);

            //Assert
            Assert.That(testCase, Is.EqualTo(_returnCharArray.ToArray()));
        }

        [Test]
        public void Given_Character_Array_Of_Length_5_With_New_Line_At_Position_1_Should_Return_An_Empty_Array()
        {
            //Setup
            var randomCharacter = new[] { '\n', 'a', 'e', 'i', 'u' };
            var testCase = Array.Empty<char>();

            SetupArrayMoc(randomCharacter);

            //Act
            var copy = new Copier(_source.Object, _destination.Object);
            copy.CopyCharacters(randomCharacter.Length);

            //Assert
            Assert.That(testCase, Is.EqualTo(_returnCharArray.ToArray()));
        }

        [Test]
        public void Given_Character_Array_Of_Length_6_With_New_Line_At_Position_3_Should_Return_Array_Of_Length_2_Without_New_Line()
        {
            //Setup
            var randomCharacter = new[] { 'a', 'e', '\n', 'i', 'o', 'u' };
            var testCase = new[] { 'a', 'e' };

            SetupArrayMoc(randomCharacter);

            //Act
            var copy = new Copier(_source.Object, _destination.Object);
            copy.CopyCharacters(randomCharacter.Length);

            //Assert
            Assert.That(testCase, Is.EqualTo(_returnCharArray.ToArray()));
        }

        [Test]
        public void Given_Character_Array_Of_Length_6_With_New_Line_At_Position_6_Should_Return_A_Copy_With_Length_5()
        {
            //Setup
            var randomCharacter = new[] { 'a', 'e', 'i', 'o', 'u', '\n' };
            var testCase = new[] { 'a', 'e', 'i', 'o', 'u' };

            SetupArrayMoc(randomCharacter);

            //Act
            var copy = new Copier(_source.Object, _destination.Object);
            copy.CopyCharacters(randomCharacter.Length);

            //Assert
            Assert.That(testCase, Is.EqualTo(_returnCharArray.ToArray()));
        }
    }
}