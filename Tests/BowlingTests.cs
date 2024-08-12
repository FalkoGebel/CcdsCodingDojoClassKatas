using _02_Bowling;
using FluentAssertions;

namespace Tests
{
    [TestClass]
    public class BowlingTests
    {
        [TestMethod]
        public void Game_initialized_correct()
        {
            Game _game = new();
            _game.Should().NotBeNull();
            _game.TotalScore.Should().Be(0);
            _game.Over().Should().BeFalse();
        }

        [TestMethod]
        public void Frame_initialized_correct()
        {
            Frame _frame = new();
            _frame.Should().NotBeNull();
            _frame.Score.Should().Be(0);
            _frame.PinsRolled.Length.Should().Be(0);
        }

        [DataTestMethod]
        [DataRow(-5)]
        [DataRow(-1)]
        [DataRow(11)]
        [DataRow(55)]
        public void Add_roll_with_with_invalid_pins_get_argument_exception(int pins)
        {
            Game _game = new();
            Action act = () => _game.AddRoll(pins);
            act.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void Add_first_roll_with_0_pins_and_get_correct_vales_for_scores_and_so_on()
        {
            Game _game = new();
            _game.AddRoll(0);
            _game.Over().Should().BeFalse();
            _game.TotalScore.Should().Be(0);
        }

        [TestMethod]
        public void Add_first_roll_with_2_pins_and_get_correct_vales_for_scores_and_so_on()
        {
            Game _game = new();
            _game.AddRoll(2);
            _game.Over().Should().BeFalse();
            _game.TotalScore.Should().Be(2);
        }

        [TestMethod]
        public void Add_two_rolls_with_2_and_7_pins_and_get_correct_vales_for_scores_and_so_on()
        {
            Game _game = new();
            _game.AddRoll(2);
            _game.AddRoll(7);
            _game.Over().Should().BeFalse();
            _game.TotalScore.Should().Be(9);
        }

        [TestMethod]
        public void Add_three_rolls_with_2_and_7_and_5_pins_and_get_correct_vales_for_scores_and_so_on()
        {
            Game _game = new();
            _game.AddRoll(2);
            _game.AddRoll(7);
            _game.AddRoll(5);
            _game.Over().Should().BeFalse();
            _game.TotalScore.Should().Be(14);
        }

        [TestMethod]
        public void Do_the_example_game_from_Robert_C_Martin()
        {
            Game _game = new();
            _game.AddRoll(1);
            _game.Over().Should().BeFalse();
            _game.TotalScore.Should().Be(1);

            _game.AddRoll(4);
            _game.Over().Should().BeFalse();
            _game.TotalScore.Should().Be(5);

            _game.AddRoll(4);
            _game.Over().Should().BeFalse();
            _game.TotalScore.Should().Be(9);

            _game.AddRoll(5);
            _game.Over().Should().BeFalse();
            _game.TotalScore.Should().Be(14);

            _game.AddRoll(6);
            _game.Over().Should().BeFalse();
            _game.TotalScore.Should().Be(20);

            _game.AddRoll(4);
            _game.Over().Should().BeFalse();
            _game.TotalScore.Should().Be(24);

            _game.AddRoll(5);
            _game.Over().Should().BeFalse();
            _game.TotalScore.Should().Be(34);

            _game.AddRoll(5);
            _game.Over().Should().BeFalse();
            _game.TotalScore.Should().Be(39);

            _game.AddRoll(10);
            _game.Over().Should().BeFalse();
            _game.TotalScore.Should().Be(59);

            _game.AddRoll(0);
            _game.Over().Should().BeFalse();
            _game.TotalScore.Should().Be(59);

            _game.AddRoll(1);
            _game.Over().Should().BeFalse();
            _game.TotalScore.Should().Be(61);

            _game.AddRoll(7);
            _game.Over().Should().BeFalse();
            _game.TotalScore.Should().Be(68);

            _game.AddRoll(3);
            _game.Over().Should().BeFalse();
            _game.TotalScore.Should().Be(71);

            _game.AddRoll(6);
            _game.Over().Should().BeFalse();
            _game.TotalScore.Should().Be(83);

            _game.AddRoll(4);
            _game.Over().Should().BeFalse();
            _game.TotalScore.Should().Be(87);

            _game.AddRoll(10);
            _game.Over().Should().BeFalse();
            _game.TotalScore.Should().Be(107);

            _game.AddRoll(2);
            _game.Over().Should().BeFalse();
            _game.TotalScore.Should().Be(111);

            _game.AddRoll(8);
            _game.Over().Should().BeFalse();
            _game.TotalScore.Should().Be(127);

            _game.AddRoll(6);
            _game.Over().Should().BeTrue();
            _game.TotalScore.Should().Be(133);
        }

        [TestMethod]
        public void Do_perfect_game()
        {
            Game _game = new();
            _game.AddRoll(10);
            _game.Over().Should().BeFalse();
            _game.TotalScore.Should().Be(10);

            _game.AddRoll(10);
            _game.Over().Should().BeFalse();
            _game.TotalScore.Should().Be(30);

            _game.AddRoll(10);
            _game.Over().Should().BeFalse();
            _game.TotalScore.Should().Be(60);

            _game.AddRoll(10);
            _game.Over().Should().BeFalse();
            _game.TotalScore.Should().Be(90);

            _game.AddRoll(10);
            _game.Over().Should().BeFalse();
            _game.TotalScore.Should().Be(120);

            _game.AddRoll(10);
            _game.Over().Should().BeFalse();
            _game.TotalScore.Should().Be(150);

            _game.AddRoll(10);
            _game.Over().Should().BeFalse();
            _game.TotalScore.Should().Be(180);

            _game.AddRoll(10);
            _game.Over().Should().BeFalse();
            _game.TotalScore.Should().Be(210);

            _game.AddRoll(10);
            _game.Over().Should().BeFalse();
            _game.TotalScore.Should().Be(240);

            _game.AddRoll(10);
            _game.Over().Should().BeFalse();
            _game.TotalScore.Should().Be(270);

            _game.AddRoll(10);
            _game.Over().Should().BeFalse();
            _game.TotalScore.Should().Be(290);

            _game.AddRoll(10);
            _game.Over().Should().BeTrue();
            _game.TotalScore.Should().Be(300);
        }

        [TestMethod]
        public void Do_perfect_game_and_next_roll_throws_exception()
        {
            Game _game = new();
            _game.AddRoll(10);
            _game.AddRoll(10);
            _game.AddRoll(10);
            _game.AddRoll(10);
            _game.AddRoll(10);
            _game.AddRoll(10);
            _game.AddRoll(10);
            _game.AddRoll(10);
            _game.AddRoll(10);
            _game.AddRoll(10);
            _game.AddRoll(10);
            _game.AddRoll(10);
            _game.Over().Should().BeTrue();

            Action act = () => _game.AddRoll(1);
            act.Should().Throw<InvalidOperationException>();
        }


        [TestMethod]
        public void Do_almost_perfect_game_and_next_roll_throws_exception()
        {
            Game _game = new();
            _game.AddRoll(10);
            _game.AddRoll(10);
            _game.AddRoll(10);
            _game.AddRoll(10);
            _game.AddRoll(10);
            _game.AddRoll(10);
            _game.AddRoll(10);
            _game.AddRoll(10);
            _game.AddRoll(10);
            _game.AddRoll(5);
            _game.AddRoll(3);
            _game.Over().Should().BeTrue();

            Action act = () => _game.AddRoll(1);
            act.Should().Throw<InvalidOperationException>();
        }
    }
}