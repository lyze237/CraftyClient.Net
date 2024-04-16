namespace CraftyClientTests;

public abstract class CraftyTest
{
    protected sealed class TestScope : IAsyncDisposable
    {
        public readonly CraftyContainer Crafty = new();

        public static async Task<TestScope> Create()
        {
            var scope = new TestScope();
            await scope.Setup();
            return scope;
        }

        public async Task Setup() =>
            await Crafty.Setup();

        public async ValueTask DisposeAsync() =>
            await Crafty.DisposeAsync();
    }

    protected static bool CompareObjects(object a, object b)
    {
        var bType = b.GetType();
        var correct = true;

        foreach (var aProperty in a.GetType().GetProperties())
        {
            var bProperty = bType.GetProperty(aProperty.Name);
            var aValue = aProperty.GetValue(a);
            var bValue = bProperty?.GetValue(b);

            if (!object.Equals(aValue, bValue))
            {
                Console.WriteLine($"[{aProperty.Name}] A {aValue} is different to B {bValue}");
                correct = false;
            }
        }

        return correct;
    }
}