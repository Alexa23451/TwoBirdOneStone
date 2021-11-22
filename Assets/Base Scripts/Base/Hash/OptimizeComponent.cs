using System.Text;

public class OptimizeComponent
{
    static OptimizeComponent()
    {
        _stringBuilderOS = new StringBuilder();
    }

    private static StringBuilder _stringBuilderOS;

    public static string GetStringOptimize(params object[] pieces)
    {
        _stringBuilderOS.Clear();

        for (int i = 0; i < pieces.Length; i++)
        {
            _stringBuilderOS.Append(pieces[i]);
        }

        return _stringBuilderOS.ToString();
    }

    public static string GetStringOptimize(string[] stringArr)
    {
        _stringBuilderOS.Clear();

        for (int i = 0; i < stringArr.Length; i++)
        {
            _stringBuilderOS.Append(stringArr[i]);
        }

        return _stringBuilderOS.ToString();
    }
}