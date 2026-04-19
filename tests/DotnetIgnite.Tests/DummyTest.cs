using FluentAssertions;

namespace DotnetIgnite.Tests;

public class DummyTest
{
    [Fact]
    public void DummyTestShouldPass()
    {
        bool expected = true;
        bool actual = true;
        actual.Should().Be(expected);
    }
}
