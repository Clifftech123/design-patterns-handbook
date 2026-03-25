// The CEO addresses the Japanese team through the translator
JapaneseTeamMember teamMember = new JapaneseTeamMember();
IEnglishSpeaker translator    = new Translator(teamMember);
CEO ceo = new CEO(translator);

ceo.Address("Good morning, team.");
ceo.Address("Please review the proposal.");


// What the CEO expects, someone who can receive a message in English
public interface IEnglishSpeaker
{
    void Speak(string message);
}

// The Japanese team member, speaks only Japanese (the adaptee)
public class JapaneseTeamMember
{
    public void SpeakJapanese(string message)
    {
        Console.WriteLine($"Team member (Japanese): {message}");
    }
}

// The Translator, adapts the Japanese speaker to the English interface
public class Translator : IEnglishSpeaker
{
    private readonly JapaneseTeamMember _teamMember;

    public Translator(JapaneseTeamMember teamMember)
    {
        _teamMember = teamMember;
    }

    public void Speak(string message)
    {
        string translated = TranslateToJapanese(message);
        _teamMember.SpeakJapanese(translated);
    }

    private string TranslateToJapanese(string english) => english switch
    {
        "Good morning, team."         => "おはようございます、チームの皆さん。",
        "Please review the proposal." => "提案書を確認してください。",
        _                             => $"[Japanese: {english}]"
    };
}

// The CEO, only knows how to talk to an IEnglishSpeaker
public class CEO
{
    private readonly IEnglishSpeaker _speaker;

    public CEO(IEnglishSpeaker speaker)
    {
        _speaker = speaker;
    }

    public void Address(string message)
    {
        Console.WriteLine($"CEO (English): {message}");
        _speaker.Speak(message);
    }
}
