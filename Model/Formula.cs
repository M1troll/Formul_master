using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Formula
    {
        public delegate bool Criterion(decimal x);
        public delegate decimal Counting(decimal x);

        public string Sigma { get; set; }
        public string Expression { get; set; }
        public string Condition { get; set; }
        public Criterion CodeCondition { get; set; }
        public Counting CodeSigma { get; set; }
        public Counting CodeExpression { get; set; }

        public Formula(){}
        public Formula(string sigma,string expression,string condition, 
                       Counting code_sigma,Counting code_exp, Criterion code_condition) 
        {
            Sigma = sigma;
            Expression = expression;
            Condition = condition;
            CodeCondition = code_condition;
            CodeSigma = code_sigma;
            CodeExpression = code_exp;
        }
        public bool Check(decimal n)
        {
            return CodeCondition(n);
        }
        public decimal F_Sigma(decimal x)
        {
            return CodeSigma(x);
        }
        public decimal F_Expression(decimal x)
        {
            return CodeExpression(x);
        }
        public override string ToString()
        {
            return Sigma + " = " + Expression + ";\r\nдля " + Condition;
        }
    }
}
