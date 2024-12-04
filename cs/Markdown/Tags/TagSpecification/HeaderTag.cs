namespace Markdown.Tags.TagSpecification;

public class HeaderTag() : BaseTag(new Tag("# ", "<h1>"), new Tag(Environment.NewLine, "</h1>"));