namespace Markdown.Tags.TagSpecification;

public class BulletedListTag() : BasicSingleTag(new Tag("- ", "<li>"), new Tag(Environment.NewLine, "</li>"))
{
    public override string PerformTagFormatting(TagReplacementSpecification replacement,
        BaseTag? previousTag, BaseTag? nextTag)
    {
        if (replacement.Tag == Opening)
        {
            if (previousTag is BulletedListTag)
                return replacement.Tag.New;
            
            return "<ul>" + replacement.Tag.New;
        }

        if (nextTag is BulletedListTag)
            return replacement.Tag.New;
        
        return replacement.Tag.New + "</ul>";
    }
}