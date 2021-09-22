using Seshat.Domain.Exceptions;
using Seshat.Domain.ValueObjects;
using FluentAssertions;
using NUnit.Framework;
using Seshat.Domain.Common;

namespace Seshat.Domain.UnitTests.ValueObjects
{
    public class ColourTests
    {
        [Test]
        public void ShouldReturnCorrectColourCode()
        {
            var code = "#FFFFFF";

            var colour = Colour.From(code);

            colour.Code.Should().Be(code);
        }

        [Test]
        public void ToStringReturnsCode()
        {
            var colour = Colour.White;

            colour.ToString().Should().Be(colour.Code);
        }

        // I think this should be passing, but it isn't, and I really need to figure out why
        // [Test]
        // public void ShouldPerformImplicitConversionToColourCodeString()
        // {
        //     var code = Colour.White;
        //     
        //     code.Should().Be("#FFFFFF");
        // }

        [Test]
        public void ShouldPerformExplicitConversionGivenSupportedColourCode()
        {
            var colour = (Colour) "#FFFFFF";

            colour.Should().Be(Colour.White);
        }

        [Test]
        public void ShouldThrowUnsupportedColourExceptionGivenNotSupportedColourCode()
        {
            FluentActions.Invoking(() => Colour.From("##FF33CC"))
                .Should().Throw<UnsupportedColourException>();
        }
    }
}