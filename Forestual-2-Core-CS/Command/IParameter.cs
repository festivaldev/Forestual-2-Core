#pragma warning disable 1591

namespace F2Core.Command
{
    public interface IParameter
    {
        string Name { get; set; }
        int Index { get; set; }
        ParameterType Type { get; set; }
        dynamic GetDefault();
    }

    public enum ParameterType
    {
        Optional,
        Required
    }
}
