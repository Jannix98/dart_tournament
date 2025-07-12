using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows;
using DartTournament.WPF.Controls.GameTreeControl.PositionCalculator;

namespace DartTournament.WPF.Test
{


    internal static class PositionConstants
    {
        public const double VERTICAL_SPACING = 20;
        public const double HORIZONTAL_SPACING = 40;
    }

    [TestClass]
    public class FirstRoundPositionCalculatorTests
    {
        [TestMethod]
        public void CalculatePositions_ReturnsCorrectRectangles_WhenEnoughSpaceAvailable()
        {
            // Arrange
            int maxScreenHeight = 1000;
            int singleItemHeight = 100;
            int singleItemWidth = 200;
            int itemCount = 3;

            var calculator = new FirstRoundPositionCalculator(
                maxScreenHeight, singleItemHeight, singleItemWidth, itemCount);

            // Act
            Rect[] positions = calculator.CalculatePositions();

            // Assert
            Assert.AreEqual(itemCount, positions.Length);

            // Calculate expected vertical spacing
            double totalHeight = singleItemHeight * itemCount;
            double spacing = (maxScreenHeight - totalHeight) / (itemCount + 1);
            if (spacing < PositionConstants.VERTICAL_SPACING)
                spacing = PositionConstants.VERTICAL_SPACING;

            double expectedX = 0; // roundIndex is 0, so x is always 0

            for (int i = 0; i < itemCount; i++)
            {
                double expectedY = spacing + i * (singleItemHeight + spacing);
                Rect expectedRect = new Rect(new Point(expectedX, expectedY), new Size(singleItemWidth, singleItemHeight));
                Assert.AreEqual(expectedRect, positions[i], $"Mismatch at index {i}");
            }
        }

        [TestMethod]
        public void CalculatePositions_UsesMinimumVerticalSpacing_WhenNotEnoughSpace()
        {
            // Arrange: force vertical spacing below minimum
            int maxScreenHeight = 300;
            int singleItemHeight = 100;
            int singleItemWidth = 200;
            int itemCount = 3;

            var calculator = new FirstRoundPositionCalculator(
                maxScreenHeight, singleItemHeight, singleItemWidth, itemCount);

            // Act
            Rect[] positions = calculator.CalculatePositions();

            // Assert
            double expectedSpacing = PositionConstants.VERTICAL_SPACING;

            for (int i = 0; i < itemCount; i++)
            {
                double expectedX = 0;
                double expectedY = expectedSpacing + i * (singleItemHeight + expectedSpacing);
                Rect expectedRect = new Rect(new Point(expectedX, expectedY), new Size(singleItemWidth, singleItemHeight));
                Assert.AreEqual(expectedRect, positions[i], $"Mismatch at index {i}");
            }
        }
    }
}
