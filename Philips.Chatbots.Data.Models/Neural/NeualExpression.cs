using MongoDB.Bson.Serialization.Attributes;
using Philips.Chatbots.Data.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Philips.Chatbots.Data.Models.Neural
{
    /// <summary>
    /// Expression tree used by Neural expression to link expressions.
    /// </summary>
    public class ExpressionTree
    {
        public List<IExpEval> Nodes { get; set; }
        public void Clear() => Nodes.Clear();

    }

    /// <summary>
    /// Arithmetic operations expression evaluator.
    /// </summary>
    public class ArithmeticOp : IExpEval
    {
        public LogicalOpType With { get; set; } = LogicalOpType.And;
        public object RVal { get; set; }
        public ArithmeticOpType AOp { get; set; } = ArithmeticOpType.Add;
        public ExpEvalResultType Evaluate(ref object LVal)
        {
            var res = ExpEvalResultType.True;
            try
            {
                switch (AOp)
                {
                    case ArithmeticOpType.Add:
                        LVal = ((dynamic)LVal + (dynamic)RVal);
                        break;
                    case ArithmeticOpType.Sub:
                        LVal = ((dynamic)LVal - (dynamic)RVal);
                        break;
                    case ArithmeticOpType.Div:
                        LVal = ((dynamic)LVal / (dynamic)RVal);
                        break;
                    case ArithmeticOpType.Mul:
                        LVal = ((dynamic)LVal * (dynamic)RVal);
                        break;
                    case ArithmeticOpType.Mod:
                        LVal = ((dynamic)LVal % (dynamic)RVal);
                        break;
                    default:
                        break;
                }
            }
            catch
            {
                res = ExpEvalResultType.Exception;
            }
            return res;
        }
    }

    /// <summary>
    /// Relational operations expression evaluator.
    /// </summary>
    public class RelationalOp : IExpEval
    {
        public LogicalOpType With { get; set; } = LogicalOpType.And;
        public object RVal { get; set; }
        public RelationalOpType ROp { get; set; } = RelationalOpType.EQ;
        public ExpEvalResultType Evaluate(ref object LVal)
        {
            var boolRes = false;
            switch (ROp)
            {
                case RelationalOpType.EQ:
                    boolRes = (dynamic)RVal == (dynamic)LVal;
                    break;
                case RelationalOpType.NE:
                    boolRes = (dynamic)RVal != (dynamic)LVal;
                    break;
                case RelationalOpType.LT:
                    boolRes = (dynamic)RVal > (dynamic)LVal;
                    break;
                case RelationalOpType.GT:
                    boolRes = (dynamic)RVal < (dynamic)LVal;
                    break;
                case RelationalOpType.LTE:
                    boolRes = (dynamic)RVal >= (dynamic)LVal;
                    break;
                case RelationalOpType.GTE:
                    boolRes = (dynamic)RVal <= (dynamic)LVal;
                    break;
            }
            return boolRes ? ExpEvalResultType.True : ExpEvalResultType.False;
        }

    }

    /// <summary>
    /// Inner expression for expression chaining.
    /// </summary>
    public class InnerExpEval
    {
        public LogicalOpType With { get; set; }
        public ExpressionTree Expression { get; set; }
    }

    /// <summary>
    /// Action link as a neural expression evaluation result.
    /// </summary>
    public class ActionLink
    {
        public LinkType Type { get; set; }
        public string Id { get; set; }
    }

    /// <summary>
    /// Action items metadata to be displayed to user.
    /// </summary>
    public class ActionItem
    {
        /// <summary>
        /// Display value.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Actual mapping Id.
        /// </summary>
        public string Value { get; set; }
    }

    /// <summary>
    /// Together as an action option.
    /// </summary>
    public class ActionOption
    {
        public ActionItem Item { get; set; }
        public ActionLink Link { get; set; }
    }

    /// <summary>
    /// Link expression, No evaluations! rather user inputs are directl mapped to the output.
    /// </summary>
    public class LinkExpression : INeuralExpression
    {
        public string Hint { get; set; }
        public string QuestionTitle { get; set; }
        public bool SkipEvaluation { get; set; }
        public List<ActionOption> Options { get; set; }
        public NeuralExpType ExpType => NeuralExpType.Link;

        public ExpEvalResultType Next(string input, out ActionLink actionLink)
        {
            actionLink = null;
            var res = ExpEvalResultType.Empty;
            if (Options == null || Options.Count == 0)
                return res;
            var searchResult = Options.FirstOrDefault(item => item.Item.Value.Equals(input, StringComparison.InvariantCultureIgnoreCase));
            if (searchResult?.Link != null)
            {
                res = ExpEvalResultType.True;
                actionLink = searchResult.Link;
            }
            else
            {
                res = ExpEvalResultType.Invalid;
            }
            return res;
        }

        public ActionLink GetDefaultLink()
        {
            return Options.FirstOrDefault().Link;
        }

        [BsonIgnore]
        public IEnumerable<ActionItem> ActionItems => Options?.Select(item => item.Item);
    }

    /// <summary>
    /// Decision expression could be used with int,float,bool,string,datetime etc.
    /// Be carefull while building the expessions, any mismatch in types could endup in runtime exception!
    /// First expression data type is considered to evaluate the entire expression
    /// E.g.
    /// var exp = new DecisionExpression();
    /// ExpressionTree can be built in two ways and has fluent building feature 
    /// 1. exp.Build().Add(10).EQ(20); //With internal method call, However this can't be done with constructor initilization. Hence use second one.
    /// 2. exp.ExpressionTree = ExpressionBuilder.Build().Add(10).Eq(20);
    /// Importent Notice: 
    /// Any strings add in lowercase
    /// Expressions are evaluated with the dynamic typecasting so
    /// the first expression RVal  data type in the ExpressionTree list is considered for the reference
    /// In above cases 'Add(10)' 10 (int) is the data type that is implied and all subsequent expression must be of type 10(int) else the evaluation yields Exception result.
    /// 
    /// //If no evaluation is required then set SkipEvaluation to true and invoke GetDefaultLink() to get result ActionLink that should be stored in ForwardAction.
    /// //Call Next(string,out ActionLink) to get the expression result which returns the evaluation resul state of type ExpEvalResultType
    /// 
    /// </summary>
    public class DecisionExpression : INeuralExpression
    {
        public NeuralExpType ExpType => NeuralExpType.Decision;

        /// <summary>
        /// When true ForwardAction will have direct solution link
        /// </summary>
        public bool SkipEvaluation { get; set; }
        /// <summary>
        /// Hint in-case expression evaluates invalid
        /// </summary>
        public string Hint { get; set; }
        public string QuestionTitle { get; set; }
        public List<ActionItem> Options { get; set; }

        /// <summary>
        /// Forward action incase expression evaluated to true.
        /// </summary>
        public ActionLink ForwardAction { get; set; }

        /// <summary>
        /// Falback action incase expression evaluated to true.
        /// </summary>
        public ActionLink FallbackAction { get; set; }
        public ExpressionTree ExpressionTree { get; set; }
        public List<InnerExpEval> InnerExpressions { get; set; }

        #region Methods

        public object Convert(Type type, string stringVal)
        {
            if (string.IsNullOrWhiteSpace(stringVal))
                return null;

            object boxedObject = null;
            if (type == typeof(bool))
            {
                bool val;
                if (bool.TryParse(stringVal, out val))
                {
                    boxedObject = val;
                }
            }
            else if (type == typeof(int))
            {
                int val;
                if (int.TryParse(stringVal, out val))
                {
                    boxedObject = val;
                }
            }
            else if (type == typeof(float))

            {
                float val;
                if (float.TryParse(stringVal, out val))
                {
                    boxedObject = val;
                }
            }
            else if (type == typeof(long))
            {
                long val;
                if (long.TryParse(stringVal, out val))
                {
                    boxedObject = val;
                }
            }
            else if (type == typeof(double))
            {
                double val;
                if (double.TryParse(stringVal, out val))
                {
                    boxedObject = val;
                }
            }
            else if (type == typeof(DateTime))
            {
                DateTime val;
                if (DateTime.TryParse(stringVal, out val))
                {
                    boxedObject = val;
                }
            }
            else if (type == typeof(string))
            {
                boxedObject = stringVal;
            }
            return boxedObject;
        }
        public ExpressionTree Build()
        {
            ExpressionTree = ExpressionBuilder.Build();
            return ExpressionTree;
        }
        public ExpEvalResultType Evaluate(string val) => _Evaluate(val.ToLower());
        protected ExpEvalResultType _Evaluate(string rValString)
        {
            if (ExpressionTree == null || ExpressionTree.Nodes == null || ExpressionTree.Nodes.Count == 0)
                return ExpEvalResultType.Empty;

            ExpEvalResultType res = ExpEvalResultType.Invalid;

            object rValObject = null;
            try
            {
                var type = ExpressionTree?.Nodes?.Select(item => item.RVal.GetType()).FirstOrDefault();
                rValObject = Convert(type, rValString);
            }
            catch
            {

            }

            if (rValObject != null)
            {
                var refExpTree = ExpressionTree;
                res = EvaluateExpressionTree(ref refExpTree, ref rValObject);
                if (InnerExpressions != null && res != ExpEvalResultType.Invalid)
                {
                    foreach (var ie in InnerExpressions)
                    {
                        switch (ie.With)
                        {
                            case LogicalOpType.And:
                                if (res == ExpEvalResultType.True)
                                {
                                    var et = ie.Expression;
                                    res = EvaluateExpressionTree(ref et, ref rValObject);
                                }
                                break;
                            case LogicalOpType.Or:
                                if (res == ExpEvalResultType.False)
                                {
                                    var et = ie.Expression;
                                    res = EvaluateExpressionTree(ref et, ref rValObject);
                                }
                                break;
                        }
                    }
                }
            }

            return res;
        }

        protected ExpEvalResultType EvaluateExpressionTree(ref ExpressionTree expressionTree, ref object rValObject)
        {
            ExpEvalResultType res = ExpEvalResultType.Empty;
            foreach (var op in ExpressionTree.Nodes)
            {
                res = op.Evaluate(ref rValObject);
                bool toBreak = false;
                switch (res)
                {
                    case ExpEvalResultType.False:
                        {
                            if (op.With == LogicalOpType.And)
                                toBreak = true;
                        }
                        break;
                    case ExpEvalResultType.Invalid:
                        toBreak = true;
                        break;
                    case ExpEvalResultType.True:
                        if (op.With == LogicalOpType.Or)
                            toBreak = true;
                        break;
                    default:
                        toBreak = true;
                        break;
                }
                if (toBreak)
                    break;
            }
            return res;
        }

        public ExpEvalResultType Next(string input, out ActionLink actionLink)
        {
            actionLink = null;
            var res = Evaluate(input);
            switch (res)
            {
                case ExpEvalResultType.False:
                    actionLink = FallbackAction;
                    break;
                case ExpEvalResultType.True:
                    actionLink = ForwardAction;
                    break;
                default:
                    break;
            }
            return res;
        }

        public ActionLink GetDefaultLink()
        {
            return ForwardAction;
        }
        #endregion
    }
}
