
using Gurukul.MVVM.Models;
using System.Collections;
using System.Text.RegularExpressions;

namespace Gurukul.Converters;

public class SortComparer : IComparer
{
    private static readonly Regex _regex = new(@"(\d+)|(\D+)");

    public int Compare(object x, object y)
    {
        if (x is not Class a || y is not Class b) 
            return 0;

        return NaturalCompare(a.ClassName, b.ClassName);
    }

    public int NaturalCompare(string x, string y)
    {
        var xMatches = _regex.Matches(x);
        var yMatches = _regex.Matches(y);

        int count = Math.Min(xMatches.Count, yMatches.Count);

        for (int i = 0; i < count; i++)
        {
            var xPart = xMatches[i].Value;
            var yPart = yMatches[i].Value;

            bool xIsNumber = int.TryParse(xPart, out int xNum);
            bool yIsNumber = int.TryParse(yPart, out int yNum);

            if (xIsNumber && yIsNumber)
            {
                int result = xNum.CompareTo(yNum);
                if (result != 0)
                    return result;
            }
            else
            {
                int result = string.Compare(xPart, yPart, StringComparison.OrdinalIgnoreCase);
                if (result != 0)
                    return result;
            }
        }

        return xMatches.Count.CompareTo(yMatches.Count);
    }
}
