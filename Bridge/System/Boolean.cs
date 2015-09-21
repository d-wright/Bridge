using Bridge;

namespace System
{
    [Ignore]
    [Name("Boolean")]
    //[Constructor("!!")]
    public struct Boolean
    {
        [Template("!!")]
        public Boolean(object value)
        {
        }

        public static string FalseString
        {
            [Template("'False'")]
            get { return string.Empty; }
        }

        public static string TrueString
        {
            [Template("'True'")]
            get { return string.Empty; }
        }

        [Template("Bridge.Boolean.parse({value})")]
        public static extern bool Parse(string value);

        [Template("Bridge.Boolean.tryParse({value}, {result})")]
        public static extern bool TryParse(string value, out bool result);
    }
}