using FluentAssertions;
using NUnit.Framework;

namespace Markdown;


[TestFixture]
internal class MdTests
{
    private Md md; 
    [SetUp]
    public void InitializeFild()
    {
        md = new Md();
    }

    [Test]
    public void Render_StringEmpty_NoExceptions()
    {
        var lambda = () => md.Render(string.Empty);

        lambda.Should().NotThrow();
    }

    [TestCase("_12_3", "_12_3")]
    [TestCase("_выделяется тегом_", "<em>выделяется тегом</em>")]
    [TestCase("эти_ подчерки_ не считаются выделением", "эти_ подчерки_ не считаются выделением",
        "За подчерками, начинающими выделение, должен следовать непробельный символ.")]
    [TestCase("_нач_але, и в сер_еди_не, и в кон_це._", "<em>нач</em>але, и в сер<em>еди</em>не, и в кон<em>це.</em>")]
    [TestCase("курсив в ра_зных сл_овах не работает", "курсив в ра_зных сл_овах не работает")]
    [TestCase("эти _подчерки _не считаются", "эти _подчерки _не считаются",
        "Подчерки, заканчивающие выделение, должны следовать за непробельным символом.")]
    public void Render_WrappedInSingleUnderscore_WrappedInTagEm(string markdown, string expected, string message = "")
    {
        var actual = md.Render(markdown);
        
        actual.Should().Be(expected);
    }

    [TestCase("__выделяется тегом__", "<strong>выделяется тегом</strong>")]
    public void Render_WrappedInDoubleUnderscore_WrappedInTagEm(string markdown, string expected)
    {
        var actual = md.Render(markdown);
        
        actual.Should().Be(expected);
    }

    [TestCase(@"\_текст\_", "_текст_")]
    [TestCase(@"\_\_не выделяется тегом\_\_", "__не выделяется тегом__")]
    [TestCase(@"\\_вот это будет выделено тегом_", @"\<em>вот это будет выделено тегом</em>")]
    [TestCase(@"Здесь сим\волы экранирования\ \должны остаться.\", @"Здесь сим\волы экранирования\ \должны остаться.\")]

    public void Render_EscapingCharacters_FormattingIsNotApplied(string markdown, string expected)
    {
        var actual = md.Render(markdown);
        
        actual.Should().Be(expected);
    }
    
    [TestCase("Внутри __двойного выделения _одинарное_ тоже__ работает",
              "Внутри <strong>двойного выделения <em>одинарное</em> тоже</strong> работает")]
    [TestCase("внутри _одинарного __двойное__ не_ работает", "внутри <em>одинарного __двойное__ не</em> работает")]
    public void Render_NestedKeywords(string markdown, string expected)
    {
        var actual = md.Render(markdown);
        
        actual.Should().Be(expected);
    }

    [TestCase("__Непарные_ символы", "__Непарные_ символы")]
    public void Render_UnpairedFormattingCharacters_FormattingIsNotApplied(string markdown, string expected)
    {
        var actual = md.Render(markdown);
        
        actual.Should().Be(expected);
    }

    [TestCase("__пересечения _двойных__ и одинарных_ подчерков", "__пересечения _двойных__ и одинарных_ подчерков")]
    public void Render_IntersectionDoubleAndSingleUnderscores_FormattingIsNotHappening(string markdown, string expected)
    {
        var actual = md.Render(markdown);
        
        actual.Should().Be(expected);
    }

    [TestCase("# Заголовок\n\r текст", "<h1>Заголовок</h1> текст")]
    public void Render_Heading_TurnsIntoTagH1(string markdown, string expected)
    {
        var actual = md.Render(markdown);
        
        actual.Should().Be(expected);
    }

    [TestCase("# Заголовок __с _разными_ символами__\n\r", "<h1>Заголовок <strong>с <em>разными</em> символами</strong></h1>")]
    public void Render_HeadingWithDifferentKeyCharacters(string markdown, string expected)
    {
        var actual = md.Render(markdown);
        
        actual.Should().Be(expected);
    }
}
