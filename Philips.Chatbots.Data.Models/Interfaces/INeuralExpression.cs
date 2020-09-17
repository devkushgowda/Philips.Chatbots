using Philips.Chatbots.Data.Models.Neural;

namespace Philips.Chatbots.Data.Models.Interfaces
{

    /// <summary>
    /// Relational operation type enum.
    /// </summary>
    public enum RelationalOpType
    {
        EQ = 0,
        NE = 1,
        LT = 2,
        GT = 3,
        LTE = 4,
        GTE = 5
    }

    /// <summary>
    /// Arithmetic operation type enum.
    /// </summary>
    public enum ArithmeticOpType
    {
        Add = 0,
        Sub = 1,
        Div = 2,
        Mul = 3,
        Mod = 4
    }

    /// <summary>
    /// Expression evaluation result type enum.
    /// </summary>
    public enum ExpEvalResultType
    {
        Exception = -2,
        Invalid = -1,
        False = 0,
        True = 1,
        Empty
    }

    /// <summary>
    /// Logical operation type enum.
    /// </summary>
    public enum LogicalOpType
    {
        And = 0,
        Or = 1
    }

    /// <summary>
    /// Type of neural expression.
    /// </summary>
    public enum NeuralExpType
    {
        Decision = 0,
        Link = 1
    }

    /// <summary>
    /// Action link type
    /// </summary>
    public enum LinkType
    {
        NeuralLink = 0,
        ActionLink = 1,
        NeuralResource = 2
    }

    /// <summary>
    /// Expression evaluation interface template.
    /// </summary>
    public interface IExpEval
    {
        LogicalOpType With { get; set; }
        object RVal { get; set; }
        ExpEvalResultType Evaluate(ref object LVal);
    }

    /// <summary>
    /// Neural expression evaluation interface template.
    /// </summary>
    public interface INeuralExpression
    {
        public bool SkipEvaluation { get; set; }
        NeuralExpType ExpType { get; }
        string Hint { get; set; }
        string QuestionTitle { get; set; }
        ActionLink GetDefaultLink();
        ExpEvalResultType Next(string input, out ActionLink actionLink);

    }
}
