namespace CodeBase.Features.Calls.External.LogicalExpression.Operations
{
    public interface INode
    {
        int Evaluate(IVariables variables);
    }
}