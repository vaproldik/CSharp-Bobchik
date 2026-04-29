public class BoldDecorator : TextDecorator
{
    public BoldDecorator(IText textComponent) : base(textComponent) { }

    public override string GetContent()
    {
        return "<b>" + base.GetContent() + "</b>";
    }
}
