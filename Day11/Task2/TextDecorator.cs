public abstract class TextDecorator : IText
{
    protected IText _textComponent;

    public TextDecorator(IText textComponent)
    {
        _textComponent = textComponent;
    }

    public virtual string GetContent()
    {
        return _textComponent.GetContent();
    }
}
