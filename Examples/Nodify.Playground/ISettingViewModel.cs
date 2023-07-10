namespace Nodify.Playground;

public enum SettingsType
{
    Boolean,
    Number,
    Option,
    Point
}

public interface ISettingViewModel
{
    string Name { get; }
    string? Description { get; }
    object? Value { get; set; }

    SettingsType Type { get;}
}