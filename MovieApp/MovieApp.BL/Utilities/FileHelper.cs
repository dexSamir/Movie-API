
namespace MovieApp.BL.Utilities;
public static class FileHelper
{
    public static int[] ParseIds(string ids) =>
        ids.Split(',').Select(int.Parse).ToArray();
}

