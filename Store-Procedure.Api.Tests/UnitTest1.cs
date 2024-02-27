namespace unit_test;

public static class StringExtensions
{
    public static bool StartsWithUpper(this string str)
    {
        if (string.IsNullOrEmpty(str))
        {
            return false;
        }

        char firstChar = str[0];
        return char.IsUpper(firstChar);
    }
}

public class UnitTest1
{
    [Fact]
    public void TestStartsWithUpper()
    {
        // Tests that we expect to return true.
        string[] words = { "Alphabet", "Zebra", "ABC", "Αθήνα", "Москва" };
        foreach (var word in words)
        {
            bool result = word.StartsWithUpper();

            Assert.True(result,
                string.Format("Expected for '{0}': true; Actual: {1}",
                                word, result));
        }
    }
}