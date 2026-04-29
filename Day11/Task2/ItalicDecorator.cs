public class ItalicDecorator : TextDecorator
{
    public ItalicDecorator(IText textComponent) : base(textComponent) { }

    public override string GetContent()
    {
        return "<i>" + base.GetContent() + "</i>";
    }
}
