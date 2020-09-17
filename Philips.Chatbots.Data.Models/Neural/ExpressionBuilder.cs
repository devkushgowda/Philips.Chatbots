using System;
using System.Collections.Generic;
using Philips.Chatbots.Data.Models.Interfaces;

namespace Philips.Chatbots.Data.Models.Neural
{
    /// <summary>
    /// Fluent interface extension methods for ExpressionTree
    /// </summary>
    public static class ExpressionBuilder
    {
        /// <summary>
        /// Builds and assigns the expression tree.
        /// </summary>
        /// <returns></returns>
        public static ExpressionTree Build()
        {
            return new ExpressionTree() { Nodes = new List<IExpEval>() };
        }

        /// <summary>
        /// Addition.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="val"></param>
        /// <param name="logicalOp"></param>
        /// <returns></returns>
        public static ExpressionTree Add(this ExpressionTree obj, object val, LogicalOpType logicalOp = LogicalOpType.And)
        {
            obj.Nodes.Add(new ArithmeticOp { RVal = val, With = logicalOp, AOp = ArithmeticOpType.Add });
            return obj;
        }

        /// <summary>
        /// Division.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="val"></param>
        /// <param name="logicalOp"></param>
        /// <returns></returns>
        public static ExpressionTree Div(this ExpressionTree obj, object val, LogicalOpType logicalOp = LogicalOpType.And)
        {
            obj.Nodes.Add(new ArithmeticOp { RVal = val, With = logicalOp, AOp = ArithmeticOpType.Div });
            return obj;
        }

        /// <summary>
        /// Multiplication.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="val"></param>
        /// <param name="logicalOp"></param>
        /// <returns></returns>
        public static ExpressionTree Mul(this ExpressionTree obj, object val, LogicalOpType logicalOp = LogicalOpType.And)
        {
            obj.Nodes.Add(new ArithmeticOp { RVal = val, With = logicalOp, AOp = ArithmeticOpType.Mul });
            return obj;
        }

        /// <summary>
        /// Subtraction.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="val"></param>
        /// <param name="logicalOp"></param>
        /// <returns></returns>
        public static ExpressionTree Sub(this ExpressionTree obj, object val, LogicalOpType logicalOp = LogicalOpType.And)
        {
            obj.Nodes.Add(new ArithmeticOp { RVal = val, With = logicalOp, AOp = ArithmeticOpType.Sub });
            return obj;
        }

        /// <summary>
        /// Modulus.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="val"></param>
        /// <param name="logicalOp"></param>
        /// <returns></returns>
        public static ExpressionTree Mod(this ExpressionTree obj, object val, LogicalOpType logicalOp = LogicalOpType.And)
        {
            obj.Nodes.Add(new ArithmeticOp { RVal = val, With = logicalOp, AOp = ArithmeticOpType.Mod });
            return obj;
        }

        /// <summary>
        /// Equals.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="val"></param>
        /// <param name="logicalOp"></param>
        /// <returns></returns>
        public static ExpressionTree EQ(this ExpressionTree obj, object val, LogicalOpType logicalOp = LogicalOpType.And)
        {
            obj.Nodes.Add(new RelationalOp { RVal = val, With = logicalOp, ROp = RelationalOpType.EQ });
            return obj;
        }

        /// <summary>
        /// Not equals.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="val"></param>
        /// <param name="logicalOp"></param>
        /// <returns></returns>
        public static ExpressionTree NE(this ExpressionTree obj, object val, LogicalOpType logicalOp = LogicalOpType.And)
        {
            obj.Nodes.Add(new RelationalOp { RVal = val, With = logicalOp, ROp = RelationalOpType.NE });
            return obj;
        }

        /// <summary>
        /// Greater than or equals.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="val"></param>
        /// <param name="logicalOp"></param>
        /// <returns></returns>
        public static ExpressionTree GTE(this ExpressionTree obj, object val, LogicalOpType logicalOp = LogicalOpType.And)
        {
            obj.Nodes.Add(new RelationalOp { RVal = val, With = logicalOp, ROp = RelationalOpType.GTE });
            return obj;
        }

        /// <summary>
        /// Lesser than or equals.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="val"></param>
        /// <param name="logicalOp"></param>
        /// <returns></returns>
        public static ExpressionTree LTE(this ExpressionTree obj, object val, LogicalOpType logicalOp = LogicalOpType.And)
        {
            obj.Nodes.Add(new RelationalOp { RVal = val, With = logicalOp, ROp = RelationalOpType.LTE });
            return obj;
        }

        /// <summary>
        /// Greater than or equals.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="val"></param>
        /// <param name="logicalOp"></param>
        /// <returns></returns>
        public static ExpressionTree GE(this ExpressionTree obj, object val, LogicalOpType logicalOp = LogicalOpType.And)
        {
            obj.Nodes.Add(new RelationalOp { RVal = val, With = logicalOp, ROp = RelationalOpType.GT });
            return obj;
        }

        /// <summary>
        /// Lesser than.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="val"></param>
        /// <param name="logicalOp"></param>
        /// <returns></returns>
        public static ExpressionTree LT(this ExpressionTree obj, object val, LogicalOpType logicalOp = LogicalOpType.And)
        {
            obj.Nodes.Add(new RelationalOp { RVal = val, With = logicalOp, ROp = RelationalOpType.LT });
            return obj;
        }

        /// <summary>
        /// Add innerexpression.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="innerExpression"></param>
        /// <param name="logicalOp"></param>
        /// <returns></returns>
        public static DecisionExpression AddInnerExpression(this DecisionExpression obj, ExpressionTree innerExpression, LogicalOpType logicalOp)
        {
            if (obj.InnerExpressions == null)
                obj.InnerExpressions = new List<InnerExpEval>();
            obj.InnerExpressions.Add(new InnerExpEval { With = logicalOp, Expression = innerExpression });
            return obj;
        }
    }
}
