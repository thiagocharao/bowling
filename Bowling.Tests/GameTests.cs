using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;

namespace Bowling.Tests
{
    public class GameTests
    {
        [Fact]
        public void CanProcessGame()
        {
            // Arrange
            var scores = new Dictionary<int, List<int>>
            {
                { 1, new List<int>() { 8, 0 } },
                { 2, new List<int>() { 1, 9 } },
                { 3, new List<int>() { 4, 0 } },
                { 4, new List<int>() { 9, 0 } },
                { 5, new List<int>() { 10 } },
                { 6, new List<int>() { 9, 1 } },
                { 7, new List<int>() { 4, 2 } },
                { 8, new List<int>() { 0, 3 } },
                { 9, new List<int>() { 5, 5 } },
                { 10, new List<int>() { 9, 1, 8 } },
            };
            
            // Act
            var game = new Game();
            var finalScore = game.ProcessGame(scores);

            // Assert
            finalScore.Should().Be(115);
        }
        
        [Fact]
        public void ShouldNotProcessGameWithMoreThanTenFrames()
        {
            // Arrange
            var scores = new Dictionary<int, List<int>>
            {
                { 1, new List<int>() { 8, 0 } },
                { 2, new List<int>() { 1, 9 } },
                { 3, new List<int>() { 4, 0 } },
                { 4, new List<int>() { 9, 0 } },
                { 5, new List<int>() { 10 } },
                { 6, new List<int>() { 9, 1 } },
                { 7, new List<int>() { 4, 2 } },
                { 8, new List<int>() { 0, 3 } },
                { 9, new List<int>() { 5, 5 } },
                { 10, new List<int>() { 5, 5 } },
                { 11, new List<int>() { 9, 1, 8 } },
            };
            
            // Act
            var game = new Game();
            Action act = () => game.ProcessGame(scores);
            
            // Assert
            act.Should().Throw<Exception>()
                .WithMessage("You are cheating...");
        }
        
        [Fact]
        public void AllFramesMustHaveAtLeastTwoThrows()
        {
            // Arrange
            var scores = new Dictionary<int, List<int>>
            {
                { 1, new List<int>() { 8, 0 } },
                { 2, new List<int>() { 1, 9 } },
                { 3, new List<int>() { 4 } },
                { 4, new List<int>() { 9, 0 } },
                { 5, new List<int>() { 10 } },
                { 6, new List<int>() { 9, 1 } },
                { 7, new List<int>() { 4, 2 } },
                { 8, new List<int>() { 0, 3 } },
                { 9, new List<int>() { 5, 5 } },
                { 10, new List<int>() { 9, 1, 8 } },
            };
            
            // Act
            var game = new Game();
            Action act = () => game.ProcessGame(scores);
            
            // Assert
            act.Should().Throw<Exception>()
                .WithMessage("Frame 3 is invalid due to number of throws");
        }

        [Theory]
        [ClassData(typeof(CalculatorTestData))]
        public void TenthFrameRulesAreValidated(Dictionary<int, List<int>> scores, string expectedErrorMessage)
        {
            // Act
            var game = new Game();
            Action act = () => game.ProcessGame(scores);
            
            // Assert
            act.Should().Throw<Exception>().WithMessage(expectedErrorMessage);
        }
        
        class CalculatorTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new Dictionary<int, List<int>>
                {
                    { 1, new List<int>() { 8, 0 } },
                    { 2, new List<int>() { 1, 9 } },
                    { 3, new List<int>() { 4, 1 } },
                    { 4, new List<int>() { 9, 0 } },
                    { 5, new List<int>() { 10 } },
                    { 6, new List<int>() { 9, 1 } },
                    { 7, new List<int>() { 4, 2 } },
                    { 8, new List<int>() { 0, 3 } },
                    { 9, new List<int>() { 5, 5 } },
                    { 10, new List<int>() { 3, 1, 8 } },
                }, "You've thrown an illegal extra ball during 10th frame" };
                
                yield return new object[] { new Dictionary<int, List<int>>
                {
                    { 1, new List<int>() { 8, 0 } },
                    { 2, new List<int>() { 1, 9 } },
                    { 3, new List<int>() { 4, 1 } },
                    { 4, new List<int>() { 9, 0 } },
                    { 5, new List<int>() { 10 } },
                    { 6, new List<int>() { 9, 1 } },
                    { 7, new List<int>() { 4, 2 } },
                    { 8, new List<int>() { 0, 3 } },
                    { 9, new List<int>() { 5, 5 } },
                    { 10, new List<int>() { 3, 1, 8 } },
                }, "You've thrown an illegal extra ball during 10th frame" };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}

