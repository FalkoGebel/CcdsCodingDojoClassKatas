using _04_Hangman;
using FluentAssertions;

namespace Tests
{
    [TestClass]
    public class HangmanTests
    {
        [TestMethod]
        public void Test_example_game()
        {
            Hangman hangman = new Hangman("Developer");
            hangman.Should().NotBeNull();
            hangman.GuessLetter('u').Should().Be("---------");
            hangman.GuessLetter('e').Should().Be("-e-e---e-");
            hangman.GuessLetter('n').Should().Be("-e-e---e-");
            hangman.GuessLetter('o').Should().Be("-e-e-o-e-");
            hangman.GuessLetter('r').Should().Be("-e-e-o-er");
            hangman.GuessLetter('a').Should().Be("-e-e-o-er");
            hangman.GuessLetter('d').Should().Be("De-e-o-er");
            hangman.GuessLetter('l').Should().Be("De-elo-er");
            hangman.GuessLetter('p').Should().Be("De-eloper");
            hangman.GuessLetter('v').Should().Be("Developer");
        }

        [TestMethod]
        public void Test_another_example()
        {
            Hangman hangman = new Hangman("Hangman");
            hangman.Should().NotBeNull();
            hangman.GuessLetter('u').Should().Be("-------");
            hangman.GuessLetter('e').Should().Be("-------");
            hangman.GuessLetter('N').Should().Be("--n---n");
            hangman.GuessLetter('o').Should().Be("--n---n");
            hangman.GuessLetter('r').Should().Be("--n---n");
            hangman.GuessLetter('a').Should().Be("-an--an");
            hangman.GuessLetter('d').Should().Be("-an--an");
            hangman.GuessLetter('l').Should().Be("-an--an");
            hangman.GuessLetter('p').Should().Be("-an--an");
            hangman.GuessLetter('v').Should().Be("-an--an");
            hangman.GuessLetter('h').Should().Be("Han--an");
            hangman.GuessLetter('g').Should().Be("Hang-an");
            hangman.GuessLetter('M').Should().Be("Hangman");
        }
    }
}