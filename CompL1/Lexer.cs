using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum TokenType
{
    IDENT,
    ENUM_KEYWORD,
    CLASS_KEYWORD,
    OPEN_BRACE,
    EQUALS,
    NUMBER,
    COMMA,
    CLOSE_BRACE,
    SEMICOLON,
    NEWLINE,
    INVALID
};

public class Token
{
    public string value;
    public int start,
               end;
    public TokenType type;

    public Token()
    {
        this.value = "";
        this.start = 0;
        this.end = 0;
        this.type = TokenType.INVALID;
    }

    public Token(string expr, int start, int end, TokenType type)
    {
        this.value = expr.Substring(start, end - start + 1);
        this.start = start;
        this.end = end;

        if (this.value == "enum") this.type = TokenType.ENUM_KEYWORD;
        else if (this.value == "class") this.type = TokenType.CLASS_KEYWORD;
        else this.type = type;
    }

    public string Stringize()
    {
        string ret = "name='" + (this.type == TokenType.NEWLINE ? "\\n" : this.value) + "' start=" + this.start + " end=" + this.end + " type=" + this.type;
        return ret;
    }
};

public class Lexer
{
    public List<Token> tokenize(string text)
    {
        List<Token> tokens = new List<Token>();
        TokenType type = TokenType.IDENT;
        int start = 0, end = 0;
        for (int i = 0; i < text.Length; i++)
        {
            start = end;
            switch (text[i])
            {
                case ' ':
                    end++;
                    continue;
                case >= 'a' and <= 'z':
                case >= 'A' and <= 'Z':
                    while ((i != text.Length) && 
                        ((text[i] >= 'a' && text[i] <= 'z') ||
                        (text[i] >= 'A' && text[i] <= 'Z') ||
                        (text[i] == '_') ||
                        (text[i] >= '0' && text[i] <= '9')))
                    {
                            i++;
                            end++;
                    }
                    i--;
                    end--;
                    type = TokenType.IDENT;
                    break;
                case >= '0' and <= '9':
                    while ((i != text.Length) && (text[i] >= '0' && text[i] <= '9'))
                    {
                            i++;
                            end++;
                    }
                    i--;
                    end--;
                    type = TokenType.NUMBER;
                    break;
                case '=':
                    type = TokenType.EQUALS;
                    break;
                case '{':
                    type = TokenType.OPEN_BRACE;

                    break;
                case '}':
                    type = TokenType.CLOSE_BRACE;
                    break;
                case ',':
                    type = TokenType.COMMA;
                    break;
                case ';':
                    type = TokenType.SEMICOLON;
                    break;
                case '\n':
                case '\r':
                    type = TokenType.NEWLINE;
                    break;
                default:
                    type = TokenType.INVALID;
                    break;
            }
            tokens.Add(new Token(text, start, end, type));
            end++;
        }
        return tokens;
    }
};
