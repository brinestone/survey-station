namespace SurvStation.Domain.Models.Forms;

public abstract class FormItemConfig
{

}

public enum JoiningOperator
{
    And, Or
}
public enum FormItemRelevanceConditionExpressionOperator
{
    In, Eq, Ne, Gt, Lt, Gte, Empty, NotEmpty, Between, Match, IsNull, IsNotNull, Checked, Unchecked, AnySelected, AllSelected, StartsWith, EndsWith, NoSelection, Before, After, AfterOrOn, BeforeOrOn
}

public record FormItemRelevance
{
    public bool Enabled { get; set; }
    public IList<FormItemRelevanceCondition> Conditions { get; set; } = [];
}

public record FormItemRelevanceCondition
{
    public JoiningOperator Operator { get; set; } = JoiningOperator.And;
    public IList<FormItemRelevanceConditionExpression> Expressions { get; set; } = [];
}

public class FormItemRelevanceConditionExpression
{
    public string? Field { get; set; }
    public bool Negated { get; set; }
    public FormItemRelevanceConditionExpressionOperator Operator { get; set; }
}

public class ValuedFormItemRelevanceConditionExpression<TValue> : FormItemRelevanceConditionExpression
{
    public TValue? Value { get; set; }
}

public class StringValueRelevanceConditionExpression : ValuedFormItemRelevanceConditionExpression<string>;
public class IntegerValueRelevanceConditionExpression : ValuedFormItemRelevanceConditionExpression<int>;
public class FloatValueRelevanceConditionExpression : ValuedFormItemRelevanceConditionExpression<double>;