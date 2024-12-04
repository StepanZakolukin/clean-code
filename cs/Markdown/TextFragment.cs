using Markdown.Tags;

namespace Markdown;

public record class TextFragment(TagReplacementSpecification OpeningTag, TagReplacementSpecification ClosingTag);