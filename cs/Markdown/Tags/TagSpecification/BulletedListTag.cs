namespace Markdown.Tags.TagSpecification;

public class BulletedListTag() : BaseTag(new Tag("-", "<li>"), new Tag(Environment.NewLine, "</li>"));