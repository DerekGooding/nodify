using System.Windows.Input;
using System.Linq;

namespace Nodify.Helpers;

/// <summary>Combines multiple input gestures.</summary>
public class MultiGesture : InputGesture
{
    /// <summary>The strategy used by <see cref="Matches(object, InputEventArgs)"/>.</summary>
    public enum Match
    {
        /// <summary>Any gesture can match.</summary>
        Any,
        /// <summary>All gestures must match.</summary>
        All
    }

    private readonly InputGesture[] _gestures;
    private readonly Match _match;

    /// <summary>Constructs an instance of a <see cref="MultiGesture"/>.</summary>
    /// <param name="match">The matching strategy.</param>
    /// <param name="gestures">The input gestures.</param>
    public MultiGesture(Match match, params InputGesture[] gestures)
    {
        _gestures = gestures;
        _match = match;
    }

    /// <inheritdoc />
    public override bool Matches(object targetElement, InputEventArgs inputEventArgs) 
        => _match == Match.Any ? 
        MatchesAny(targetElement, inputEventArgs) : 
        MatchesAll(targetElement, inputEventArgs);

    private bool MatchesAll(object targetElement, InputEventArgs inputEventArgs) 
        => !_gestures.Any(g => !g.Matches(targetElement, inputEventArgs));

    private bool MatchesAny(object targetElement, InputEventArgs inputEventArgs)
        => _gestures.Any(g => g.Matches(targetElement, inputEventArgs));

}
