
namespace TechEvent.Domain.UsefulClases
{
    public static class Extensions
    {
        public static string MaxChars(this string input, int nrOfChar)
        {
            if (nrOfChar > input.Length)
                return input;
            var index = input.LastIndexOf(" ", nrOfChar-3);
            return input.Substring(0, index)+"...";
        }
    }
}
