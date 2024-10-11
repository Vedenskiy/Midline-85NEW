namespace CodeBase.Features.Calls.External.LogicalExpression.Operations.Common.Constant
{
    public class ConstantToken : IEmptyOperationToken
    {
        private readonly int _constant;

        public ConstantToken(int constant) => 
            _constant = constant;

        public INode Create() => 
            new ConstantNode(_constant);
    }
}