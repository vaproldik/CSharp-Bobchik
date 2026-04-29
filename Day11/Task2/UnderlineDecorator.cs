public class UnderlineDecorator : TextDecorator
{
    public UnderlineDecorator(IText textComponent) : base(textComponent) { }

    public override string GetContent()
    {
        return "<u>" + base.GetContent() + "</u>";
    }
}
