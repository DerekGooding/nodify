namespace Nodify.Calculator.Operations;

public interface IOperation
{
    double Execute(params double[] operands);
}
